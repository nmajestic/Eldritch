using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eldritch.Content.Item.Tile.Ore.Entropy;

public abstract class EntropyOre : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 410;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 500;
        Main.tileMergeDirt[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(152, 171, 198), name);

        DustType = 84;
        HitSound = SoundID.Tink;
        MineResist = 1;
        MinPick = 25;
    }
}