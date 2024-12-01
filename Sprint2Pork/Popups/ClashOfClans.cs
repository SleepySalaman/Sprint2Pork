using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class ClashOfClans {

        private Popup gamePopup;

        public ClashOfClans() {
            gamePopup = new Popup(100, 100, 1200, 580);
            gamePopup.AddImage("../th1.png", 0, 0, 200, 200);
            for(int i = 6; i >= 0; i--) {
                gamePopup.AddImage("../Wall1.png", 220, 30 * i - 4, 20, 30);
            }
            for(int i = 0; i < 12; i++) {
                gamePopup.AddImage("../Wall1.png", 20 * i, 200, 20, 40);
            }
            gamePopup.AddImage("../Cannon1.png", 240, 0, 80, 80);

            gamePopup.AddImage("../Grass.jpg", 0, -340, 1200, 1200);
        }

        public void Update() {
            gamePopup.Update();
        }

        public void Draw() {
            gamePopup.Draw();
        }

        public void TogglePopup() {
            gamePopup.ToggleRender();
        }

    }
}
