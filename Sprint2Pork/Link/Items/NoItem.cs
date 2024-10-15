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
