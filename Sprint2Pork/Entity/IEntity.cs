using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Entity
{
    public interface IEntity
    {

        void Update();
        void Draw(SpriteBatch sb, Texture2D txt, Texture2D livesTxt, Texture2D hitboxTxt, bool showHitbox);

    }
}
