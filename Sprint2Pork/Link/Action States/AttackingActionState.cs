using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class AttackingActionState : ILinkActionState
    {
        private Link link;
        private LinkAttackingSpriteFactory spriteFactory;

        public AttackingActionState(Link link)
        {
            this.link = link;

            spriteFactory = new LinkAttackingSpriteFactory();
            spriteFactory.SetLinkAttackSprite(link);
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
