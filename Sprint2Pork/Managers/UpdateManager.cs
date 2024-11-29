﻿using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System.Collections.Generic;

namespace Sprint2Pork.Managers
{
    public class UpdateManager
    {
        private Game1 game;
        private Link link;
        private LinkHealth healthCount;
        private List<IController> controllerList;
        private SoundManager soundManager;

        public UpdateManager(Game1 game, Link link, LinkHealth healthCount, List<IController> controllerList, SoundManager soundManager)
        {
            this.game = game;
            this.link = link;
            this.healthCount = healthCount;
            this.controllerList = controllerList;
            this.soundManager = soundManager;
        }

        public void UpdateControllers()
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
        }

        public void UpdateEnemies(List<IEnemy> enemies, List<Block> blocks, List<EnemyManager> fireballManagers, GameTime gameTime, EnemyManager enemyManager)
        {
            EnemyUpdater.UpdateEnemies(link, enemies, blocks, fireballManagers, healthCount);
            EnemyUpdater.UpdateFireballs(enemyManager, ref link, ref fireballManagers, gameTime, ref healthCount);

            if (!healthCount.IsLinkAlive())
            {
                game.GameOver();
            }
        }

        public void UpdateLink(int linkPreviousX, int linkPreviousY, GameTime gameTime, List<Block> blocks, Rectangle roomBoundingBox)
        {
            ISprite itemSprite = link.linkItem.SpriteGet();
            if (itemSprite != null)
            {
                link.linkItem.SpriteSet(itemSprite);
            }
            link.UpdateInvincibilityTimer(gameTime);
            link.actionState.Update();
            link.LinkSpriteUpdate();
            BlockCollisionHandler.HandleBlockCollision(link, linkPreviousX, linkPreviousY, blocks, roomBoundingBox);
        }

        public void UpdateGroundItems(List<GroundItem> groundItems)
        {
            var itemsToRemove = new List<GroundItem>();

            foreach (var item in groundItems)
            {
                item.Update(item.destinationRect.X, item.destinationRect.Y);

                if (Collision.CollidesWithGroundItem(link.GetRect(), item.GetRect()))
                {
                    HandleItemCollision(item);
                    itemsToRemove.Add(item);
                }
            }

            RemoveGroundItems(groundItems, itemsToRemove);
        }

        private void HandleItemCollision(GroundItem item)
        {
            switch (item)
            {
                case Key:
                    soundManager.PlaySound("sfxItemReceived");
                    break;
                case Triangle:
                    game.GameOver();
                    break;
                case Potion:
                    healthCount.HealFullHeart();
                    break;
                case Heart:
                    healthCount.HealHalfHeart();
                    break;
                default:
                    soundManager.PlaySound("sfxItemObtained");
                    break;
            }

            item.PerformAction();
            link.CollectItem(item);
        }

        private void RemoveGroundItems(List<GroundItem> groundItems, List<GroundItem> itemsToRemove)
        {
            foreach (var item in itemsToRemove)
            {
                groundItems.Remove(item);
            }
        }
    }
}