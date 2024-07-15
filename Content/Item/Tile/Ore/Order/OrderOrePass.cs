using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eldritch.Content.Item.Tile.Ore.Order;
public class OrderOrePass(string name, double loadWeight) : GenPass(name, loadWeight)
{
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = OrderOreSystem.OrderOrePassMessage.Value;
        
        int totalIterations = (int)(Main.maxTilesX * Main.maxTilesY * 0.00005);
        for (var iteration = 0; iteration < totalIterations; iteration++) {

            var x = WorldGen.genRand.Next(0, Main.maxTilesX);
            
            var y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

            var nextRunnerWidth = WorldGen.genRand.Next(3, 6);
            var nextRunnerHeight = WorldGen.genRand.Next(2, 6);
            var tileType = ModContent.TileType<OrderOre>();
            
            WorldGen.TileRunner(x, y, nextRunnerWidth, nextRunnerHeight, tileType);
        }
    }
}