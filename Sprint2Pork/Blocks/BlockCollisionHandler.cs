using Microsoft.Xna.Framework;
using Sprint2Pork;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

public class BlockCollisionHandler
{
    public static void HandleBlockCollision(Link link, int linkPreviousX, int linkPreviousY, List<Block> blocks, Rectangle roomBoundingBox, LinkHealth health)
    {
        foreach (Block block in blocks)
        {
            if (Collision.Collides(link.GetRect(), block.GetBoundingBox()))
            {
                if (block.IsMovable)
                {
                    Vector2 potentialPosition = block.Position;
                    bool canMove = true;

                    if (link.directionState is LeftFacingLinkState)
                        potentialPosition = new Vector2(block.Position.X - Block.TileSize, block.Position.Y);
                    else if (link.directionState is RightFacingLinkState)
                        potentialPosition = new Vector2(block.Position.X + Block.TileSize, block.Position.Y);
                    else if (link.directionState is UpFacingLinkState)
                        potentialPosition = new Vector2(block.Position.X, block.Position.Y - Block.TileSize);
                    else if (link.directionState is DownFacingLinkState)
                        potentialPosition = new Vector2(block.Position.X, block.Position.Y + Block.TileSize);

                    foreach (Block otherBlock in blocks)
                    {
                        if (otherBlock != block && Collision.Collides(new Rectangle((int)potentialPosition.X, (int)potentialPosition.Y, block.BoundingBox.Width, block.BoundingBox.Height), otherBlock.GetBoundingBox()))
                        {
                            canMove = false;
                            break;
                        }
                    }

                    if (canMove)
                    {
                        block.Move(link.directionState);
                    }
                    else
                    {
                        link.SetX(linkPreviousX);
                        link.SetY(linkPreviousY);
                    }
                }
                else
                {
                    link.SetX(linkPreviousX);
                    link.SetY(linkPreviousY);
                }
                break;
            }
            if (link.linkItem.Collides(block.GetBoundingBox()))
            {
                link.StopLinkItem();
            }
        }
        if (link.IsLinkUsingItem())
        {
            if (Collision.CollidesWithOutside(link.linkItem.SpriteGet().GetRect(), roomBoundingBox))
            {
                link.StopLinkItem();
            }
            if(link.linkItem is Bomb)
            {
                if (link.linkItem.Collides(link.GetRect()))
                {
                    link.TakeDamage();
                    health.TakeDamage();
                }
            }
        }
    }
}
