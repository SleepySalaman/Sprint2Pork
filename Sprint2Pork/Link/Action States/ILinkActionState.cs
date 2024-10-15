namespace Sprint2Pork
{
    public interface ILinkActionState
    {
        void BeIdle();
        void BeMoving();
        void BeAttacking();
        void TakeDamage();
        void Update();
    }
}
