using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public enum Game1State
    {
        StartScreen,
        Title,
        Playing,
        Paused,
        Transitioning,
        GameOver,
        Inventory
    }
    public class Game1StateManager
    {
        private Game1State currentState;

        public Game1StateManager(Game1State state)
        {
            currentState = state;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void ChangeState(Game1State newState)
        {
            currentState = newState;
            //switch(newState)
            //{


            //}
        }

        private void InitializeTitleScreen()
        {

        }
        private void InitializePlaying()
        {

        }
        private void InitializePaused()
        {

        }
        private void InitializeTransitioning()
        {

        }
        private void InitializeGameOver()
        {

        }
    }
}
