using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface ISprite
{
    void Update(int x, int y);
    void Draw(SpriteBatch sb, Texture2D txt);

    Rectangle getRect();
}
