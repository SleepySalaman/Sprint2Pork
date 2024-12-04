using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Essentials;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.rooms
{
    public class RoomChange
    {

        public static void SwitchToNextRoom(ref string currentRoom, ref List<Block> blocks, ref List<GroundItem> groundItems,
            ref List<IEnemy> enemies, ref List<EnemyManager> fireballManagers,
            Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms)
        {
            var roomNames = new List<string>(rooms.Keys);
            int currentIndex = roomNames.IndexOf(currentRoom);
            int nextIndex = (currentIndex + 1) % roomNames.Count;
            SwitchRoom(roomNames[nextIndex], ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public static void SwitchToPreviousRoom(ref string currentRoom, ref List<Block> blocks, ref List<GroundItem> groundItems,
            ref List<IEnemy> enemies, ref List<EnemyManager> fireballManagers,
            Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms)
        {
            var roomNames = new List<string>(rooms.Keys);
            int currentIndex = roomNames.IndexOf(currentRoom);
            int previousIndex = (currentIndex - 1 + roomNames.Count) % roomNames.Count;
            SwitchRoom(roomNames[previousIndex], ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public static void SwitchRoom(string newRoom, ref string currentRoom, ref List<Block> blocks, ref List<GroundItem> groundItems,
            ref List<IEnemy> enemies, ref List<EnemyManager> fireballManagers,
            Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms)
        {
            currentRoom = newRoom;
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
        }

        public static void CheckRoomChange(Game1State gameState, ref string currentRoom, ref string nextRoom, ref Texture2D nextRoomTexture, ref Vector2 transitionDirection, RoomManager roomManager, Link link, GraphicsDevice graphicsDevice, HUD hud, Action setRectangles, Action<Game1State> setGameState, Func<int> getCurrentRoomNumber, Inventory inventory)
        {
            nextRoom = roomManager.GetNextRoom(currentRoom, link, inventory);
            if (nextRoom != "none")
            {
                nextRoomTexture = roomManager.GetNextRoomTexture(nextRoom);
                transitionDirection = roomManager.GetDirection(link);
                roomManager.PlaceLink(link, graphicsDevice);
                setRectangles();
                setGameState(Game1State.Transitioning);
            }
            hud.UpdateRoomNumber(getCurrentRoomNumber());
        }

        public static void HandleRoomTransition(GameTime gameTime, ref float transitionTimer, float transitionDuration, ref Rectangle oldRoomRectangle, Rectangle oldRoomRectangleSaved, ref Rectangle nextRoomRectangle, Rectangle nextRoomRectangleSaved, Vector2 transitionDirection, ref Game1State gameState, ref Texture2D roomTexture, Texture2D nextRoomTexture, ref string currentRoom, ref string nextRoom, ref List<Block> blocks, ref List<GroundItem> groundItems, ref List<IEnemy> enemies, ref List<EnemyManager> fireballManagers, Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms, Action checkForKey)
        {
            transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            int transitionConstant = (int)(Math.Abs(transitionDirection.X) * (transitionTimer * GameConstants.TRANSITION_SPEED)) + (int)(Math.Abs(transitionDirection.Y) * (transitionTimer * GameConstants.VERTICAL_TRANSITION_SPEED));

            oldRoomRectangle = new Rectangle(oldRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), oldRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), oldRoomRectangle.Width, oldRoomRectangle.Height);
            nextRoomRectangle = new Rectangle(nextRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), nextRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), nextRoomRectangle.Width, nextRoomRectangle.Height);

            if (transitionTimer >= transitionDuration || ((0 >= ((int)transitionDirection.X * nextRoomRectangle.X)) && (0 >= ((int)transitionDirection.Y * nextRoomRectangle.Y) - (int)transitionDirection.Y * GameConstants.ROOM_Y_OFFSET)))
            {
                transitionTimer = 0f;
                gameState = Game1State.Playing;
                roomTexture = nextRoomTexture;
                SwitchRoom(nextRoom, ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                currentRoom = nextRoom;
                checkForKey();
            }
        }
    }
}
