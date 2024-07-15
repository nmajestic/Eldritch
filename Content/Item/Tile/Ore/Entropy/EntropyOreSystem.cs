using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eldritch.Content.Item.Tile.Ore.Entropy;
public abstract class EntropyOreSystem : ModSystem
{
		public static LocalizedText EntropyOrePassMessage { get; private set; }
		public static LocalizedText BlessedWithEntropyOreMessage { get; private set; }
		
		public override void SetStaticDefaults() {
			EntropyOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(EntropyOrePassMessage)}");
			BlessedWithEntropyOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithEntropyOreMessage)}");
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
				Main.NewText(BlessedWithEntropyOreMessage.Value, 50, 255, 130);
			}
			else if (Main.netMode == NetmodeID.Server) {
				ChatHelper.BroadcastChatMessage(BlessedWithEntropyOreMessage.ToNetworkText(), new Color(50, 255, 130));
			}
		}

		private void AddOreSplotches(){
			var splotches = (int)(100 * (Main.maxTilesX / 4200f));
			var highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
			for (var iteration = 0; iteration < splotches; iteration++) {
				var i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				var j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

				WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<EntropyOre>());
			}
		}
		
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			var shiniesIndex = tasks.FindIndex(genPass => genPass.Name.Equals("Shinies"));
			if (shiniesIndex != -1) {
				tasks.Insert(shiniesIndex + 1, new EntropyOrePass("Entropy Ores", 237.4298f));
			}
		}
}