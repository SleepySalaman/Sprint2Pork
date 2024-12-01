using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class ClashOfClans {

        private Popup gamePopup;

        public ClashOfClans(int x, int y, int width, int height) {
            gamePopup = new Popup(x, y, width, height);
            gamePopup.AddImage("../th1.png", 0, 0, 200, 200);
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
