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
                Console.WriteLine("Make Splash");
                //names_to_load = new List<string>() { "splash" };
                //screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentGameState = GameStatesEnum.GAME;
            }
            isSplash = true;
        }


      
        protected void playGame()
        {

            Console.WriteLine("playGame");
        }



        protected void generateBullet()
        {
            gameObjectsGenerator.GenerateContent("bullet" + counterBullet,
                    textureLoader.getContent(textures_locations[10]));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObject("bullet" + counterBullet, gameObjectsGenerator.getGameObject("bullet" + counterBullet));
            isShoot = true;
            planeObject = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane");
            int bullet_width = screenManager.getScreen(GameStatesEnum.GAME).getHoldoutObject("bullet" + counterBullet).ObjectShape.Width;

            Point location = new Point(planeObject.ObjectShape.X + (planeObject.ObjectShape.Width / 2), planeObject.ObjectShape.Y - 20);

            if (screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet) == null)
            {
                screenManager.orderScreenToDisplayHoldoutObject(GameStatesEnum.GAME, "bullet" + counterBullet, location);

                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet).MovmentVector = new MovmentVector(0, shoot_y_direction);
                screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheNewLocation("bullet" + counterBullet, location);

                nameBullet.Add("bullet" + counterBullet);
                counterBullet++;


            }
        }


        protected void makeSummary()
        {
            /*
            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_font", new Point(359, 235));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_font", "Points:" + points + "\nLife: " + life + " \nNew Game: Y     Exit: N");

            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                RestartGame();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                Exit();
            }*/
        }

        protected void RestartGame()
        {
            life = 3;
            points = 0;

            currentGameState = GameStatesEnum.GAME;
        }
    }

}




  
