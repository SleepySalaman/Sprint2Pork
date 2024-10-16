using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sprint2Pork.Blocks
{
    public class GenerateBlocks
    {

        public static void fillBlockList(List<Block> blocks, Texture2D txt, Vector2 pos)
        {
            blocks.Add(new FloorBlock(txt, pos));
            blocks.Add(new Block2(txt, pos));
            blocks.Add(new EnemyBlock1(txt, pos));
            blocks.Add(new Block4(txt, pos));
            blocks.Add(new BlackBlock(txt, pos));
            blocks.Add(new SpeckledBlock(txt, pos));
            blocks.Add(new DarkBlueBlock(txt, pos));
            blocks.Add(new StairBlock(txt, pos));
            blocks.Add(new Block9(txt, pos));
            blocks.Add(new StripedBlock(txt, pos));
        }

    }
}
