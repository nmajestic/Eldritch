using Terraria.ID;
using Terraria.ModLoader;

namespace Eldritch.Content.Item.Placeable.Order;

public class OrderOre : ModItem
{
    public override void SetStaticDefaults() {
        Item.ResearchUnlockCount = 100;
        ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        ItemID.Sets.OreDropsFromSlime[Type] = (3, 13);
    }

    public override void SetDefaults() {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tile.Ore.Order.OrderOre>());
        Item.width = 12;
        Item.height = 12;
        Item.value = 3000;
    }
}