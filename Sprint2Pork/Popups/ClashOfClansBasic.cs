using Sprint2Pork.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class ClashOfClansBasic {

        private Popup gamePopup;

        public ClashOfClansBasic() {
            gamePopup = new Popup(300, 0, 1356, 1040);
            gamePopup.AddImage("../ClashOfClansImage.jpg", 0, 0, 1356, 1040);
        }

        public void Draw() {
            gamePopup.Draw();
        }

        public void Update() {
            gamePopup.Update();    
        }

        public void TogglePopup(Game1 game) { 
            gamePopup.ToggleRender();
        }

    }
}
