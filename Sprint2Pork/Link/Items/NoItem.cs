﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    internal class NoItem : ILinkItems
    {

        public void Update(Link link)
        {
            link.loseItem();
        }
    }
}
