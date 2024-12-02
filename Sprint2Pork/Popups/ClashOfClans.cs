using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class ClashOfClans {

        private Popup gamePopup;
        private PseudoMap pm;
        private List<PseudoMapPair> barbIndexes;

        public int cooldown = 5;
        public int currentCooldownCount = 0;

        public ClashOfClans() {
            gamePopup = new Popup(100, 100, 1200, 580);
            pm = new();

            pm.insert("th", gamePopup.AddImage("../th1.png", 0, 0, 200, 200));

            for(int i = 6; i >= 0; i--) {
                pm.insert("wall", gamePopup.AddImage("../Wall1.png", 220, 30 * i - 4, 20, 30));
            }
            for(int i = 0; i < 12; i++) {
                pm.insert("wall", gamePopup.AddImage("../Wall1.png", 20 * i, 200, 20, 40));
            }
            pm.insert("cannon", gamePopup.AddImage("../Cannon1.png", 240, 0, 80, 80));
            barbIndexes = new();
        }

        public void Update() {
            gamePopup.Update();
            int mX = gamePopup.getMouseX();
            int mY = gamePopup.getMouseY();
            if(mX != -100 || mY != -100) {
                MouseEvent(mX, mY);
            }
            if(barbIndexes.Count > 0) {
                if(currentCooldownCount > cooldown) {
                    for(int i = 0; i < barbIndexes.Count; i++) {
                        int value = barbIndexes[i].getValue();
                        int dx = gamePopup.getImageX(value) - 100;
                        int dy = gamePopup.getImageY(value) - 100;
                        int moveX;
                        int moveY;
                        if(dx > 2) {
                            moveX = -2;
                        } else if(dx < -2) {
                            moveX = 2;
                        } else {
                            moveX = 0;
                        }
                        if (dy > 2) {
                            moveY = -2;
                        } else if (dy < -2) {
                            moveY = 2;
                        } else {
                            moveY = 0;
                        }
                        if (moveX != 0 || moveY != 0) {
                            gamePopup.moveImage(value, moveX, moveY);
                        }
                    }
                    currentCooldownCount = 0;
                }
            }
            currentCooldownCount++;
        }

        public void Draw() {
            gamePopup.Draw();
        }

        public void TogglePopup() {
            gamePopup.ToggleRender();
        }

        private void MouseEvent(int x, int y) {
            pm.insert("barb", gamePopup.AddImage("../barbarian.jpg", x - 40, y - 40, 80, 80));
            barbIndexes = pm.listForType("barb");
        }

    }
}
