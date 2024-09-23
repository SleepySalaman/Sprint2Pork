﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint2Pork
{
    // TODO: Do methods inside need to be public?
    public class Link : ILink
    {
        ILinkState state;
        private Viewport viewport;
        int x;
        int y;

        public Link()
        {

        }

        public void ChangeDirection()
        {
            state.changeDirection();
        }

        public void TakeDamage()
        {
            state.takeDamage();
        }

        public void UseItem()
        {
            state.useItem();
        }

        public void AttackSword()
        {
            state.attackSword();
        }

        public void Update()
        {
            state.Update();
        }

        public void MoveLeft()
        {

        }
    }
}
