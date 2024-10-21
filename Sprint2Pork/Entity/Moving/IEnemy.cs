using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public interface IEnemy : IEntity
    {

        void Move();
        int getX();

        Rectangle getRect();

        void updateFromCollision(bool collides, Color c);

    }
}
