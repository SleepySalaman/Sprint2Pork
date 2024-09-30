using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class LoadTextures {

        public static void loadAllTextures(List<Texture2D> allTextures, ContentManager Content) {
            allTextures.Add(Content.Load<Texture2D>("LinkMovingWithDamage")); //character
            allTextures.Add(Content.Load<Texture2D>("zeldabosses")); //fireball
            allTextures.Add(Content.Load<Texture2D>("zeldaenemies")); //enemies
            allTextures.Add(Content.Load<Texture2D>("gel")); //gel
            allTextures.Add(Content.Load<Texture2D>("bat")); //bat
            allTextures.Add(Content.Load<Texture2D>("red_goriya")); //goriya
            allTextures.Add(Content.Load<Texture2D>("wizard")); //wizard
            allTextures.Add(Content.Load<Texture2D>("stalfos")); //stalfos
            allTextures.Add(Content.Load<Texture2D>("blocks")); //blocks
            allTextures.Add(Content.Load<Texture2D>("items_and_weapons")); //items and weapons
            allTextures.Add(Content.Load<Texture2D>("ItemsAndWeapons")); //items and weapons expanded
        }

    }
}
