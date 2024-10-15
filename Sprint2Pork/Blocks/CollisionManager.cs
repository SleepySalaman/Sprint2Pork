using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public static class CollisionManager
    {
        public static bool CheckCollision(Rectangle entityRect, List<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if (entityRect.Intersects(block.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
