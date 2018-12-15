using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;


namespace Space_Invaders
{
    public partial class SpaceInvaders:Game
    {
        List<string> names_to_load = new List<string>();
        List<string> names_of_remove = new List<string>();
        List<string> nameEnemy = new List<string>();
        List<string> nameBullet = new List<string>();
        ContentLoader<SpriteFont> fontLoader;
        FontGenerator fontGenerator;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentLoader<Texture2D> textureLoader;
        GameObjectsGenerator gameObjectsGenerator;
        Screen splashScreen, gameScreen, pauseScreen, summaryScreen;
        ScreenManager screenManager;
        GameObject planeObject;
        GameStatesEnum currentGameState = GameStatesEnum.SPLASH;
       
        int shoot_y_direction = -3;


        float t_point = 0;
        bool isSplash = false, isGame = false, isPause = false, isSummary = false;
        bool isShoot = false;
        int points = 0, life = 3;
        static int counterBullet = 0;

        bool isSpacePress = false, isTimerOff = true, isPPress = false;

        List<string> BulletsToRemove = new List<string>();
        List<string> objectsToCheckForCollision = new List<string>();

        //KOLEJNOŚĆ JEST WAŻNA
        private List<string> textures_locations = new List<string>() {
            "images\\background",
            "images\\splash",
            "images\\plane",
            "images\\bullet",
            "images\\enemy_blue",
            "images\\enemy_green",
            "images\\enemy_red"
        };


        private List<string> fonts_locations = new List<string>() {
            "fonts\\font",
        };
    }
}
