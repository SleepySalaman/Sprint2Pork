using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class RoomManager
    {
        private List<string> roomList;
        private Dictionary<string, Texture2D> roomMap;
        private Dictionary<string, Vector2> transitionDirections;

        private int leftBorder;
        private int rightBorder;
        private int topBorder;
        private int bottomBorder;

        public RoomManager(GraphicsDevice GraphicsDevice)
        {
            this.roomList = new List<string>();
            roomList.Add("room1");
            roomList.Add("room2");
            roomList.Add("room3");
            roomList.Add("room4");
            roomList.Add("room5");
            roomList.Add("room6");
            roomList.Add("room7");
            roomList.Add("room8");
            roomList.Add("room9");
            leftBorder = GameConstants.ROOM_EDGE_BUFFER;
            rightBorder = GraphicsDevice.Viewport.Width - leftBorder;
            topBorder = GameConstants.ROOM_EDGE_BUFFER;
            bottomBorder = GraphicsDevice.Viewport.Height - GameConstants.ROOM_EDGE_THRESHOLD;
        }

        public void InitializeRooms(ContentManager Content)
        {
            roomMap = new Dictionary<string, Texture2D>();
            foreach (var room in roomList)
            {
                roomMap.Add(room, Content.Load<Texture2D>(room));
            }
            this.transitionDirections = new Dictionary<string, Vector2>();
            transitionDirections.Add("right", new Vector2(1, 0));
            transitionDirections.Add("left", new Vector2(-1, 0));
            transitionDirections.Add("up", new Vector2(0, -1));
            transitionDirections.Add("down", new Vector2(0, 1));
        }

        public string GetNextRoom(string currentRoom, Link link)
        {
            string nextRoom = "none";
            switch (currentRoom)
            {
                case "room1":
                    if (link.GetX() > rightBorder) { nextRoom = "room2"; }
                    if (link.GetX() < leftBorder) { nextRoom = "room5"; }
                    break;
                case "room2":
                    if (link.GetX() > rightBorder) { nextRoom = "room4"; }
                    if (link.GetX() < leftBorder) { nextRoom = "room1"; }
                    if (link.GetY() < topBorder) { nextRoom = "room3"; }
                    break;
                case "room3":
                    if (link.GetY() > bottomBorder) { nextRoom = "room2"; }
                    break;
                case "room4":
                    if (link.GetX() < leftBorder) { nextRoom = "room2"; }
                    break;
                case "room5":
                    if (link.GetX() > rightBorder) { nextRoom = "room1"; }
                    if (link.GetY() > bottomBorder) { nextRoom = "room6"; }
                    if (link.GetX() < leftBorder) { nextRoom = "room8"; }
                    break;
                case "room6":
                    if (link.GetY() > bottomBorder) { nextRoom = "room7"; }
                    if (link.GetY() < topBorder) { nextRoom = "room5"; }
                    break;
                case "room7":
                    if (link.GetY() < topBorder) { nextRoom = "room6"; }
                    break;
                case "room8":
                    if (link.GetX() > rightBorder) { nextRoom = "room5"; }
                    if (link.GetY() < topBorder) { nextRoom = "room9"; }
                    break;
                case "room9":
                    if (link.GetY() > bottomBorder) { nextRoom = "room8"; }
                    break;
            }
            return nextRoom;

            }
        public Texture2D GetNextRoomTexture(string nextRoom)
        {
            roomMap.TryGetValue(nextRoom, out Texture2D room);
            return room;
        }

        public Vector2 GetDirection(Link link)
        {
            Vector2 nextDirection = new Vector2(0,0);
            if (link.GetX() > rightBorder) { transitionDirections.TryGetValue("right", out nextDirection); }
            else if (link.GetX() < leftBorder) { transitionDirections.TryGetValue("left", out nextDirection); }
            else if (link.GetY() < topBorder) { transitionDirections.TryGetValue("up", out nextDirection); }
            else if (link.GetY() > bottomBorder) { transitionDirections.TryGetValue("down", out nextDirection); }
            return nextDirection;
        }

        public void PlaceLink(Link link, GraphicsDevice GraphicsDevice)
        {
            if (link.GetX() > rightBorder) { link.SetX(GameConstants.ROOM_EDGE_BUFFER); }
            else if (link.GetX() < leftBorder) { link.SetX(GraphicsDevice.Viewport.Width - 101); }
            else if (link.GetY() < topBorder) { link.SetY(GraphicsDevice.Viewport.Height - 50); }
            else if (link.GetY() > bottomBorder) { link.SetY(101); }
        }
        }
    }

