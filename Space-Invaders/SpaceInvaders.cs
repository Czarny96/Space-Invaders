﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class SpaceInvaders : Game
    {
        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;

        public SpaceInvaders()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
            //TODO: Tymczasowo
            enemy_arrival_vectors_dict.Add("left_top",GenerateListOfBezierPoints(new Vector2(0, 0), new Vector2(200, 0), new Vector2(200, 200), new Vector2(150, 100),2.0f,0.01f));
            enemy_arrival_vectors_dict.Add("left_bottom", GenerateListOfBezierPoints(new Vector2(0, 100), new Vector2(200, 100), new Vector2(200, 300), new Vector2(300, 200), 2.0f, 0.01f));
            enemy_arrival_vectors_dict.Add("right_top", GenerateListOfBezierPoints(new Vector2(480, 0), new Vector2(240, 0), new Vector2(290, 100), new Vector2(600, 100), 2.0f, 0.01f));
            enemy_arrival_vectors_dict.Add("right_bottom", GenerateListOfBezierPoints(new Vector2(700, 130), new Vector2(240, 130), new Vector2(290, 200), new Vector2(670, 200), 2.0f, 0.01f));

            


            graphics.IsFullScreen = false;

            textureLoader = new ContentLoader<Texture2D>();
            gameObjectsGenerator = new GameObjectsGenerator();
            fontGenerator = new FontGenerator();
            fontLoader = new ContentLoader<SpriteFont>();
            screenManager = new ScreenManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            textureLoader.Load(Content, textures_locations);
            fontLoader.Load(Content, fonts_locations);
            splashScreen = new Screen(graphics.GraphicsDevice);
            gameScreen = new Screen(graphics.GraphicsDevice);
            pauseScreen = new Screen(graphics.GraphicsDevice);
            summaryScreen = new Screen(graphics.GraphicsDevice);

            screenManager.addScreen(GameStatesEnum.SPLASH, splashScreen);
            screenManager.addScreen(GameStatesEnum.GAME, gameScreen);
            screenManager.addScreen(GameStatesEnum.PAUSE, pauseScreen);
            screenManager.addScreen(GameStatesEnum.SUMMARY, summaryScreen);

            gameObjectsGenerator.GenerateContent(new List<string>() { "background", "splash", "plane", "bullet", 
                "enemy_blue", "enemy_green", "enemy_red" },
               textureLoader.getListedContent(textures_locations));

            names_to_load = new List<string>() { "background" };
            screenManager.getScreen(GameStatesEnum.GAME).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectToTheScreen("plane", gameObjectsGenerator.getGameObject("plane"));
            screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheMiddleOfTheWidth("plane",
                screenManager.getSelectedScreenHeight(GameStatesEnum.GAME) -
                    (screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME, "plane").ObjectShape.Height + 25));


            names_to_load = new List<string>() { "life_font", "point_font" };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]) });
            screenManager.getScreen(GameStatesEnum.GAME).addNewFontsToTheScreen(names_to_load, fontGenerator.getListOfFontObjects(names_to_load));

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(25, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Life: " + life);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "point_font", new Point(85, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "point_font", "Points: " + points);


            names_to_load = new List<string>() { "background" };
            screenManager.getScreen(GameStatesEnum.PAUSE).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));


            names_to_load = new List<string>() { "pause_font" };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]) });
            screenManager.getScreen(GameStatesEnum.PAUSE).addNewFontsToTheScreen(names_to_load, fontGenerator.getListOfFontObjects(names_to_load));

            screenManager.moveFontOnTheScreen(GameStatesEnum.PAUSE, "pause_font", new Point(360, 220));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_font", "PAUSE");



           
           

           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            if (Keyboard.GetState().IsKeyDown(Keys.P) && !isPPress)
            {
                if (currentGameState == GameStatesEnum.GAME)
                {
                    currentGameState = GameStatesEnum.PAUSE;

                    isPPress = true;
                }
                else if (currentGameState == GameStatesEnum.PAUSE)
                {
                    currentGameState = GameStatesEnum.GAME;
                    isPPress = true;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.P) && isPPress)
                isPPress = false;

            if (currentGameState != GameStatesEnum.PAUSE)
            {

                // TODO: Add your update logic here	
                if (currentGameState.Equals(GameStatesEnum.SPLASH))
                {
                    makeSplash();

                }
                else if (currentGameState.Equals(GameStatesEnum.GAME))
                {

                    playGame();

                }
                else if (currentGameState.Equals(GameStatesEnum.SUMMARY))
                {

                    makeSummary();
                }

                screenManager.getScreen(currentGameState).AnimateScreen();

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            screenManager.DrawSelectedScreen(currentGameState,spriteBatch);
            base.Draw(gameTime);
        }
    }
}
