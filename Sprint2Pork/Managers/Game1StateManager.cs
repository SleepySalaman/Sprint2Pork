namespace Sprint2Pork.Managers
{
    public enum Game1State
    {
        StartScreen,
        Title,
        Playing,
        Paused,
        Transitioning,
        GameOver,
        Win,
        Inventory
    }
    public class Game1StateManager
    {
        private Game1State currentState;
        public Game1StateManager(Game1State state)
        {
            currentState = state;
        }
    }
}
