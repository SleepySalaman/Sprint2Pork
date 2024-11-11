﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    internal class NoItem : ILinkItems
    {
        public ISprite sprite;

        public void Update(Link link)
        {
            link.loseItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            return;
        }
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
    }
}
