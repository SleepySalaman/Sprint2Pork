using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class AttackingActionState : ILinkActionState
    {
        private Link link;
        private LinkAttackingSpriteFactory spriteFactory = new LinkAttackingSpriteFactory();
        //private SoundEffect sfxSword = Content.Load<SoundEffect>("sfxSwordZap");

        public AttackingActionState(Link link)
        {
            this.link = link;

            //spriteFactory = new LinkAttackingSpriteFactory();
            spriteFactory.SetLinkAttackSprite(link);
            link.PlaySound("sfxSwordZap");
        }

        public void BeIdle()
        {
            link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            link.actionState = new MovingActionState(link);
        }

        public void BeAttacking()
        {
            // NO-OP
        }

        public void TakeDamage()
        {
            // NO-OP
        }

        public void Update()
        {
            link.Attack();
        }
    }
}
