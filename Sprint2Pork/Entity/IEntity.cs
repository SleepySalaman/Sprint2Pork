using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Entity
{
    public interface IEntity
    {

        void Update();
        void Draw(SpriteBatch sb, Texture2D txt);

    }
}
