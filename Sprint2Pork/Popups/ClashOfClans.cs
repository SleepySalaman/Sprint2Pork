using Sprint2Pork.Managers;
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

        private int cooldown = 5;
        private int currentCooldownCount = 0;

        private int shotCooldown = 2;
        private int currentShotCount = 0;

        private int cannonStartX = 280;
        private int cannonStartY = 40;

        private int cannonballPosX = 280;
        private int cannonballPosY = 40;

        private int cannonballIndex;
        private int cannonTargetIndex;
        private bool cannonShooting = false;

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
            UpdateBarbs();
            UpdateCannonBall();
        }

        public void Draw() {
            gamePopup.Draw();
        }

        public void UpdateBarbs() {
            if (barbIndexes.Count > 0) {
                if (currentCooldownCount > cooldown) {
                    for (int i = 0; i < barbIndexes.Count; i++) {
                        int value = barbIndexes[i].getValue();
                        int dx = gamePopup.getImageX(value) - 100;
                        int dy = gamePopup.getImageY(value) - 100;
                        int moveX;
                        int moveY;
                        if (dx > 2) {
                            moveX = -2;
                        } else if (dx < -2) {
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

        public void UpdateCannonBall() {
            if (currentShotCount > shotCooldown && cannonShooting) {
                int value = barbIndexes[cannonTargetIndex].getValue();
                int dx = cannonballPosX - (gamePopup.getImageX(value) + 40);
                int dy = cannonballPosY - (gamePopup.getImageY(value) + 40);
                if((dx < 5 && dx > -5) && (dy < 5 && dy > -5)) {
                    bool barbDead = barbIndexes[cannonTargetIndex].takeDamage();
                    Debug.WriteLine("Barb dead: " + barbDead);
                    if (barbDead) {
                        gamePopup.removeImage(value);
                        barbIndexes.Remove(barbIndexes[cannonTargetIndex]);
                        cannonShooting = false;
                    }
                    gamePopup.setImagePos(cannonballIndex, 0, 0);
                    cannonballPosX = cannonStartX;
                    cannonballPosY = cannonStartY;
                } else {
                    int moveX, moveY;
                    if (dx > 4) {
                        moveX = -4;
                    } else if (dx < -4) {
                        moveX = 4;
                    } else {
                        moveX = 0;
                    }
                    if (dy > 4) {
                        moveY = -4;
                    } else if (dy < -4) {
                        moveY = 4;
                    } else {
                        moveY = 0;
                    }
                    if (moveX != 0 || moveY != 0) {
                        cannonballPosX += moveX;
                        cannonballPosY += moveY;
                        gamePopup.moveImage(cannonballIndex, moveX, moveY);
                    }
                }
                currentShotCount = 0;
            } else if (barbIndexes.Count > 0 && !cannonShooting) {
                fireCannon();
            }
            currentShotCount++;
        }

        public void TogglePopup(Game1 game) {
            gamePopup.ToggleRender();
            game.SetGameState(Game1State.Inventory);
        }

        private void MouseEvent(int x, int y) {
            pm.insert("barb", gamePopup.AddImage("../barbarian.jpg", x - 40, y - 40, 80, 80));
            barbIndexes = pm.listForType("barb");
            if (!cannonShooting) {
                fireCannon();
            }
        }

        public void fireCannon() {
            cannonShooting = true;
            cannonTargetIndex = getNearestBarb(cannonStartX, cannonStartY);
            cannonballIndex = gamePopup.AddImage("../cannonball.png", cannonStartX, cannonStartY, 20, 20);
        }

        private int getNearestBarb(int x, int y) {
            int nearest = -1;
            int minDistance = -1;
            for(int i = 0; i < barbIndexes.Count; i++) {
                int value = barbIndexes[0].getValue();
                int posX = gamePopup.getImageX(value);
                int posY = gamePopup.getImageY(value);
                int distance = (int)Math.Sqrt(((posX - x)*(posX - x)) + ((posY - y)*(posY - y)));
                if(i == 0 || distance < minDistance) {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }

    }
}
