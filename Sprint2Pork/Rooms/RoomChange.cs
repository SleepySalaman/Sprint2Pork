using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Essentials;
using Sprint2Pork.Items;
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
    }
}
