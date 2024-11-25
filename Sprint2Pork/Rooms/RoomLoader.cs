using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using Sprint2Pork.rooms;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class RoomLoader
    {

        public static void LoadRooms(Texture2D blockTexture, Texture2D groundItemTexture, Texture2D enemyTexture,
            ref List<Block> blocks, ref List<GroundItem> groundItems, ref List<IEnemy> enemies,
            ref List<EnemyManager> fireballManagers, ref Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms,
            ref SoundManager soundManager, ref string currentRoom)
        {
            CSVLevelLoader.LoadObjectsFromCSV("room1.csv", blockTexture, groundItemTexture, enemyTexture, out var room1Blocks, out var room1Items, out var room1Enemies, out var fireballManagerRoom1, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room2.csv", blockTexture, groundItemTexture, enemyTexture, out var room2Blocks, out var room2Items, out var room2Enemies, out var fireballManagersRoom2, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room3.csv", blockTexture, groundItemTexture, enemyTexture, out var room3Blocks, out var room3Items, out var room3Enemies, out var fireballManagersRoom3, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room4.csv", blockTexture, groundItemTexture, enemyTexture, out var room4Blocks, out var room4Items, out var room4Enemies, out var fireballManagersRoom4, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room5.csv", blockTexture, groundItemTexture, enemyTexture, out var room5Blocks, out var room5Items, out var room5Enemies, out var fireballManagersRoom5, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room6.csv", blockTexture, groundItemTexture, enemyTexture, out var room6Blocks, out var room6Items, out var room6Enemies, out var fireballManagersRoom6, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room7.csv", blockTexture, groundItemTexture, enemyTexture, out var room7Blocks, out var room7Items, out var room7Enemies, out var fireballManagersRoom7, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room7locked.csv", blockTexture, groundItemTexture, enemyTexture, out var room7lockedBlocks, out var room7lockedItems, out var room7lockedEnemies, out var fireballManagersRoom7locked, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room8.csv", blockTexture, groundItemTexture, enemyTexture, out var room8Blocks, out var room8Items, out var room8Enemies, out var fireballManagersRoom8, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room9.csv", blockTexture, groundItemTexture, enemyTexture, out var room9Blocks, out var room9Items, out var room9Enemies, out var fireballManagersRoom9, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room10.csv", blockTexture, groundItemTexture, enemyTexture, out var room10Blocks, out var room10Items, out var room10Enemies, out var fireballManagersRoom10, soundManager);


            rooms["room1"] = (new List<Block>(room1Blocks), new List<GroundItem>(room1Items), new List<IEnemy>(room1Enemies), new List<EnemyManager>(fireballManagerRoom1));
            rooms["room2"] = (new List<Block>(room2Blocks), new List<GroundItem>(room2Items), new List<IEnemy>(room2Enemies), new List<EnemyManager>(fireballManagersRoom2));
            rooms["room3"] = (new List<Block>(room3Blocks), new List<GroundItem>(room3Items), new List<IEnemy>(room3Enemies), new List<EnemyManager>(fireballManagersRoom3));
            rooms["room4"] = (new List<Block>(room4Blocks), new List<GroundItem>(room4Items), new List<IEnemy>(room4Enemies), new List<EnemyManager>(fireballManagersRoom4));
            rooms["room5"] = (new List<Block>(room5Blocks), new List<GroundItem>(room5Items), new List<IEnemy>(room5Enemies), new List<EnemyManager>(fireballManagersRoom5));
            rooms["room6"] = (new List<Block>(room6Blocks), new List<GroundItem>(room6Items), new List<IEnemy>(room6Enemies), new List<EnemyManager>(fireballManagersRoom6));
            rooms["room7locked"] = (new List<Block>(room7lockedBlocks), new List<GroundItem>(room7lockedItems), new List<IEnemy>(room7lockedEnemies), new List<EnemyManager>(fireballManagersRoom7locked));
            rooms["room7"] = (new List<Block>(room7Blocks), new List<GroundItem>(room7Items), new List<IEnemy>(room7Enemies), new List<EnemyManager>(fireballManagersRoom7));
            rooms["room8"] = (new List<Block>(room8Blocks), new List<GroundItem>(room8Items), new List<IEnemy>(room8Enemies), new List<EnemyManager>(fireballManagersRoom8));
            rooms["room9"] = (new List<Block>(room9Blocks), new List<GroundItem>(room9Items), new List<IEnemy>(room9Enemies), new List<EnemyManager>(fireballManagersRoom9));
            rooms["room10"] = (new List<Block>(room10Blocks), new List<GroundItem>(room10Items), new List<IEnemy>(room10Enemies), new List<EnemyManager>(fireballManagersRoom10));

            currentRoom = "room1";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
        }

    }
}
