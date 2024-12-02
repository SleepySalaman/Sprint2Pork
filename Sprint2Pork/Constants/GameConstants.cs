using System.CodeDom;

namespace Sprint2Pork.Constants
{
    public static class GameConstants
    {
        // Screen/Viewport
        public const int VIEWPORT_WIDTH = 800;
        public const int VIEWPORT_HEIGHT = 600;
        public const int HUD_HEIGHT = 89;
        public const int ROOM_Y_OFFSET = 85;
        public const float TILE_HEIGHT_MULTIPLIER = 1.025f;
        public const int LINK_DEFAULT_X = 115;
        public const int LINK_DEFAULT_Y = 180;
        public const float BLOCK_SCALE = 1.875f;

        // Movement/Positions
        public const int ROOM_EDGE_BUFFER = 100;
        public const int ROOM_EDGE_THRESHOLD = 30;
        public const int DEFAULT_SPRITE_POSITION = 50;
        public const int ENEMY_INIT_X = 450;
        public const int ENEMY_INIT_Y = 320;

        // Item Related
        public const int ITEM_SPRITE_SIZE = 32;
        public const int ITEM_DISPLAY_X = 400;
        public const int ITEM_DISPLAY_Y = 200;
        public const int INVENTORY_START_X = 400;
        public const int INVENTORY_START_Y = 220;
        public const int INVENTORY_ITEM_SIZE = 64;
        public const int INVENTORY_PADDING = 10;
        public const int INVENTORY_ITEMS_PER_ROW = 4;
        public const int INVENTORY_RECTANGLE_ADJUSTMENT_SIZE = 5;
        public const int INVENTORY_LINE_THICKNESS = 3;
        public const int INVENTORY_SELECTION_BOX_LENGTH_ADJUSTMENT = 9;
        public const int INVENTORY_PAUSED_TEXT_HEIGHT = 150;
        public const int BLOCK_TILE_SIZE = 40;

        // Transition Related
        public const float TRANSITION_DURATION = 1.0f;
        public const float TRANSITION_SPEED = 880.0f;
        public const float VERTICAL_TRANSITION_SPEED = 25.0f;

        // Text Positions
        public const int TEXT_DISPLAY = 100;

        // Enemy Related
        public const float ENEMY_FREEZE_TIMER = 3.0f;
    }
}