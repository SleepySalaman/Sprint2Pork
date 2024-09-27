﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Aquamentus : Enemy {

        private int moveCount = 0;
        private int moveMaxCount = 2;

        private int relativeX = 0;

        private bool movingRight = true;

        public Aquamentus() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(36, 0, 36, 36),
                new Rectangle(84, 0, 36, 36)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move() {
            moveCount++;
            if(moveCount > moveMaxCount) {
                moveCount = 0;
                if (movingRight && relativeX < 50) {
                    relativeX++;
                } else if (!movingRight && relativeX > -50) {
                    relativeX--;
                } else {
                    movingRight = !movingRight;
                }
            }
            destinationRect.X = initX + relativeX;
        }

        public override void Attack() {

        }

    }
}
