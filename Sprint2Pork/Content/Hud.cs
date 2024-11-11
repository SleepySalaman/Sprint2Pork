using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public class HUD
    {
        private Inventory inventory;
        private SpriteFont font;

        public HUD(Inventory inventory, SpriteFont font)
        {
            this.inventory = inventory;
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Rupee count
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Rupee")}", new Vector2(335, 10), Color.White);

            // Draw Key count
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Key")}", new Vector2(335, 35), Color.White);

            // Draw Bomb count
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("GroundBomb")}", new Vector2(335, 60), Color.White);
           }
    }
}
