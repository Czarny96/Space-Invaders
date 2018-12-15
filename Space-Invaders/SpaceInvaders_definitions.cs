using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System.Threading;
using System.Linq;

namespace Space_Invaders
{
    public partial class SpaceInvaders : Game
    {
        protected void startPlane()
        {

            //hasPlaneStarted = true;
           

        }
       
        protected void makeSplash()
        {
            
            if (!isSplash)
            {
                //Console.WriteLine("Make Splash");
                names_to_load = new List<string>() { "background", "splash" };
                screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
                isSplash = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentGameState = GameStatesEnum.GAME;
            }
            
        }


      
        protected void playGame()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").ObjectShape.X > 0)
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(plane_x_direction, plane_y_direction);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").ObjectShape.X < width)
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(-plane_x_direction, plane_y_direction);
            }
            else
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(0, 0);
            }

            List<string> enemies = new List<string>()
            {
                 "enemy_red",
                 "enemy_blue",
                 "enemy_green"
            };
            if (t_point<0.2f)
            {
                foreach (var enemy in enemies)
                {
                    screenManager.moveObjectOnTheScreen(currentGameState, enemy, Utils.GetPointOnBezierCurve(
                    new Vector2(screenManager.getGameObjectFromTheScreen(currentGameState, enemy).ObjectShape.X, 0), new Vector2(screenManager.getGameObjectFromTheScreen(currentGameState, enemy).ObjectShape.X+50, 0), new Vector2(screenManager.getGameObjectFromTheScreen(currentGameState, enemy).ObjectShape.X+50, 100), new Vector2(screenManager.getGameObjectFromTheScreen(currentGameState, enemy).ObjectShape.X+10, 100), t_point).ToPoint());
                }
                
                t_point +=  0.01F;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isSpacePress)
            {
                generateBullet();
                isSpacePress = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isSpacePress)
                isSpacePress = false;



            foreach (var bullet in nameBullet)
            {
                pottentialCollisionObjectName = screenManager.getScreen(GameStatesEnum.GAME).checkIfObjectIsInCollisionWithOtherObjects(bullet, enemies);

                if (pottentialCollisionObjectName != null)
                {
                        screenManager.getScreen(GameStatesEnum.GAME).removeObject(pottentialCollisionObjectName);
                        objectsToCheckForCollision.Remove(pottentialCollisionObjectName);
                        BulletsToRemove.Add(bullet);
                        
                        
                        points++;

                        screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "point_font", new Point(85, 460));
                        screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "point_font", "Points: " + points);

                }
            }

            foreach (var bullet in BulletsToRemove)
            {
                screenManager.getScreen(GameStatesEnum.GAME).removeObject(bullet);
                nameBullet.Remove(bullet);
            }
            BulletsToRemove.Clear();












            if (life == 0) currentGameState = GameStatesEnum.SUMMARY;
        }
        protected void makeSummary()
        {
            if (!isSummary)
            {
                names_to_load = new List<string>() { "background" };
                screenManager.getScreen(GameStatesEnum.SUMMARY).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
                isSummary = true;
            }
            

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_font", new Point(359, 235));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_font", "Points:" + points + "\nLife: " + life + " \nNew Game: Y     Exit: N");

            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                RestartGame();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                Exit();
            }
        }

        protected void generateBullet()
        {
            gameObjectsGenerator.GenerateContent("bullet" + counterBullet,
                    textureLoader.getContent(textures_locations[3]));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObject("bullet" + counterBullet, gameObjectsGenerator.getGameObject("bullet" + counterBullet));
            isShoot = true;
            planeObject = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane");
            int bullet_width = screenManager.getScreen(GameStatesEnum.GAME).getHoldoutObject("bullet" + counterBullet).ObjectShape.Width;

            Point location = new Point(planeObject.ObjectShape.X + (planeObject.ObjectShape.Width / 2 - 2), planeObject.ObjectShape.Y - 20);

            if (screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet) == null)
            {
                screenManager.orderScreenToDisplayHoldoutObject(GameStatesEnum.GAME, "bullet" + counterBullet, location);

                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet).MovmentVector = new MovmentVector(0, shoot_y_direction);
                screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheNewLocation("bullet" + counterBullet, location);

                nameBullet.Add("bullet" + counterBullet);
                counterBullet++;


            }
        }

        protected void RestartGame()
        {
            life = 3;
            points = 0;
            isSplash = false;
            isSummary = false;
            isSpacePress = false;
            isTimerOff = true;
            isPPress = false;

            nameEnemy.Clear();
            nameBullet.Clear();
            BulletsToRemove.Clear();

            objectsToCheckForCollision.Clear();

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(25, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Life: " + life);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "point_font", new Point(85, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "point_font", "Points: " + points);

            currentGameState = GameStatesEnum.GAME;
        }


        private void MakeEnemyMoveOnCurve(GameObject enemy)
        {
            
        }
    }

}




  
