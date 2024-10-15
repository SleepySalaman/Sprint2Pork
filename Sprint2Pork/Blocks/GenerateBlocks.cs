using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sprint2Pork.Blocks
{
    public class GenerateBlocks
    {

        public static void fillBlockList(List<Block> blocks, Texture2D txt, Vector2 pos)
        {
            blocks.Add(new Block1(txt, pos));
            blocks.Add(new Block2(txt, pos));
            blocks.Add(new Block3(txt, pos));
            blocks.Add(new Block4(txt, pos));
            blocks.Add(new Block5(txt, pos));
            blocks.Add(new Block6(txt, pos));
            blocks.Add(new Block7(txt, pos));
            blocks.Add(new Block8(txt, pos));
            blocks.Add(new Block9(txt, pos));
            blocks.Add(new Block10(txt, pos));
        }

    }
}
