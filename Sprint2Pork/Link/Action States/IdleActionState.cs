using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class IdleActionState : ILinkActionState
    {
        private Link link;

        public IdleActionState(Link link)
        {
            this.link = link;
            Rectangle rect = new Rectangle(69, 11, 16, 15);
            bool flipped = false;
            switch (link.directionState)
            {
                case UpFacingLinkState:
                    rect = (new Rectangle(100, 0, 16, 15));
                    flipped = false;
                    break;
                case DownFacingLinkState:
                    rect = new Rectangle(0, 0, 16, 15);
                    flipped = false;
                    break;
                case LeftFacingLinkState:
                    rect = new Rectangle(34, 0, 16, 15);
                    flipped = true;
                    break;
                case RightFacingLinkState:
                    rect = new Rectangle(34, 0, 16, 15);
                    flipped = false;
                    break;
            }
            link.LinkSpriteSet(new NonMovingNonAnimatedSprite(link.GetX(), link.GetY(), rect, flipped));

        }

        public void BeIdle()
        {
            // NO-OP
        }

        public void BeMoving()
        {
            link.actionState = new MovingActionState(link);
        }

        public void BeAttacking()
        {
            link.actionState = new AttackingActionState(link);
        }

        public void TakeDamage()
        {
            link.actionState = new DamagedActionState(link, false);
        }
        public void Update()
        {
            link.Idle();
        }
    }
}
