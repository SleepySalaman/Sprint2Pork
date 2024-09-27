using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity {
    public interface IEntity {

        void Update();
        void Draw(SpriteBatch sb, Texture2D txt);

    }
}
