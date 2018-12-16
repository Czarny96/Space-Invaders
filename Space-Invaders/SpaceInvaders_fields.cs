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
        bool move_left_top=false, move_left_bottom=false, move_right_top=false, move_right_bottom=false;
        int left_top_index = 0, left_bottom_index = 0, right_top_index = 0, right_bottom_index = 0;
        int enemy_movment_counter = 60;
        int enemy_x_movment = 1;
        ContentLoader<SpriteFont> fontLoader;
        FontGenerator fontGenerator;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentLoader<Texture2D> textureLoader;
        GameObjectsGenerator gameObjectsGenerator;
        Screen splashScreen, gameScreen, pauseScreen, summaryScreen;
        ScreenManager screenManager;
        GameObject planeObject;
        int width = 750;
        bool where_enemies_generated = false;
        Dictionary<string, List<string>> enemy_arrival_dict = new Dictionary<string,List<string>>()
        {
            { "left_top",new List<string>() },
            { "left_bottom",new List<string>()},
            { "right_top",new List<string>() },
            {"right_bottom",new List<string>() }

        };
        Dictionary<string, List<Point>> enemy_arrival_vectors_dict = new Dictionary<string, List<Point>>();
        
        GameStatesEnum currentGameState = GameStatesEnum.SPLASH;
        int plane_y_direction = 0, plane_x_direction = -3;
        int shoot_y_direction = -3;


        int index_of_points = 0;
        bool isSplash = false, isGame = false, isPause = false, isSummary = false;
        bool isShoot = false;
        int points = 0, life = 3;
        static int counterBullet = 0;

        bool isSpacePress = false, isTimerOff = true, isPPress = false;

        List<string> BulletsToRemove = new List<string>();
        List<string> objectsToCheckForCollision = new List<string>();
        string pottentialCollisionObjectName;
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

        private List<string> current_enemies = new List<string>();


        private List<string> fonts_locations = new List<string>() {
            "fonts\\font",
        };
    }
}
