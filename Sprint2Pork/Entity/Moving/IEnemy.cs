using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public interface IEnemy : IEntity
    {

        void Move(List<Block> blocks);
        int getX();

        Rectangle GetRect();

        void UpdateFromCollision(bool collides, Color c);

        int getTextureIndex();

        int getHealth();

        int GetFireballID();

    }
}
