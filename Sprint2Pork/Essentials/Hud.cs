using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public class HUD
    {
        private Inventory inventory;
        private SpriteFont font;
        private int currentRoomNumber;


        public HUD(Inventory inventory, SpriteFont font)
        {
            this.inventory = inventory;
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Rupee")}", new Vector2(335, 10), Color.White);

            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Key")}", new Vector2(335, 35), Color.White);

            spriteBatch.DrawString(font, $"{inventory.GetItemCount("GroundBomb")}", new Vector2(335, 60), Color.White);

            // If getting the room number fails, don't show a room in the HUD
            if (currentRoomNumber != -1)
            {
                spriteBatch.DrawString(font, $"{currentRoomNumber}", new Vector2(250, 11), Color.White);
            }
        }

        public void UpdateRoomNumber(int roomNumber)
        {
            this.currentRoomNumber = roomNumber;
        }
    }
}
