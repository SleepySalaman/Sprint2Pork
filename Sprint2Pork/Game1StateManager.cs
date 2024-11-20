﻿using Microsoft.Xna.Framework;

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
    }
}
