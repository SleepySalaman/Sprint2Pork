using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Enemies {
    public interface IEnemy {

        void Update();
        void takeDamage();
        void Draw(SpriteBatch sb, Texture2D txt);

    }
}
