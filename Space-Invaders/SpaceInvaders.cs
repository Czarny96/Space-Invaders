using Microsoft.Xna.Framework;
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

            names_to_load = new List<string>() { "background", "splash" };
            screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "enemy_red" };
            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            

            /*screenManager.getScreen(GameStatesEnum.GAME).addNewObjectToTheScreen("plane", gameObjectsGenerator.getGameObject("plane"));
            screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheMiddleOfTheWidth("plane",
                screenManager.getSelectedScreenHeight(GameStatesEnum.GAME) -
                    (screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME, "plane").ObjectShape.Height + 1));
                    */
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
