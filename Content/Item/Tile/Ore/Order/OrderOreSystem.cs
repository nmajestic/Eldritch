using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eldritch.Content.Item.Tile.Ore.Order;
public abstract class OrderOreSystem : ModSystem
{
		public static LocalizedText OrderOrePassMessage { get; private set; }
		public static LocalizedText BlessedWithOrderOreMessage { get; private set; }
		
		public override void SetStaticDefaults() {
			OrderOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(OrderOrePassMessage)}");
			BlessedWithOrderOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithOrderOreMessage)}");
		}

		public void DisplayOreBlessingMessageAndAddOreSplotches() {
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				return;
			}

			ThreadPool.QueueUserWorkItem(_ => {
				DisplayOreBlessingMessage();
				AddOreSplotches();
			});
		}
		
		private void DisplayOreBlessingMessage(){
			if (Main.netMode == NetmodeID.SinglePlayer) {
				Main.NewText(BlessedWithOrderOreMessage.Value, 50, 255, 130);
			}
			else if (Main.netMode == NetmodeID.Server) {
				ChatHelper.BroadcastChatMessage(BlessedWithOrderOreMessage.ToNetworkText(), new Color(50, 255, 130));
			}
		}

		private void AddOreSplotches(){
			var splotches = (int)(100 * (Main.maxTilesX / 4200f));
			var highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
			for (var iteration = 0; iteration < splotches; iteration++) {
				var i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				var j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

				WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<OrderOre>());
			}
		}
		
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			var shiniesIndex = tasks.FindIndex(genPass => genPass.Name.Equals("Shinies"));
			if (shiniesIndex != -1) {
				tasks.Insert(shiniesIndex + 1, new OrderOrePass("Order Ores", 237.4298f));
			}
		}
}