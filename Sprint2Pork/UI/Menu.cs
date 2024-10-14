﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.UI {
    public class Menu {

        private List<Button> buttons;

        public Menu() {
            buttons = new List<Button> {
                new Button("Hello World!", new Vector2(100, 100))
            };
        }

        public void Draw(SpriteBatch sb) {
            for(int i = 0; i < buttons.Count; i++) {
                buttons[i].Draw(sb);
            }
        }

    }
}