using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.UI {
    public class Button {

        private Rectangle buttonRect;
        private string buttonText;

        public Button(string text, Vector2 pos) {
            buttonText = text;
            buttonRect = new Rectangle((int)pos.X, (int)pos.Y, buttonText.Length * 4, 12);
        }

        public void Draw(SpriteBatch sb) {
            
        }

    }
}
