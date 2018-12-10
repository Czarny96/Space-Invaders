using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/// <summary>
/// Klasa odpowiedzialna za wyswietlanie odpowienich screenów
/// </summary>
namespace Space_Invaders
{
    class ScreenManager
    {
        private Dictionary<GameStatesEnum, Screen> screens;
        internal Dictionary<GameStatesEnum, Screen> Screens { get => screens; set => screens = value; }

        public ScreenManager()
        {
            screens = new Dictionary<GameStatesEnum, Screen>();
        }

        public ScreenManager(Dictionary<GameStatesEnum, Screen> screens)
        {
            Screens = screens;
        }

        public Screen getScreen(GameStatesEnum screen) {
            return Screens[screen];
        }

        public void addScreen(GameStatesEnum screenType, Screen screen) {
            Screens.Add(screenType, screen);
        }

        public void DrawSelectedScreen(GameStatesEnum selectedScreen, SpriteBatch spriteBatch)
        {
            this.getScreen(selectedScreen).DrawScreen(spriteBatch);

        }
        public int getSelectedScreenWidth(GameStatesEnum selectedScreen)
        {
            return this.screens[selectedScreen].ScreenBoundary.WindowWidth;
        }
        public int getSelectedScreenHeight(GameStatesEnum selectedScreen)
        {
            return this.screens[selectedScreen].ScreenBoundary.WindowHeight;
        }
        public GameObject getGameObjectFromTheScreen(GameStatesEnum selectedScreen, string name)
        {
            return this.screens[selectedScreen].GetGameObject(name);

        }
        public void moveObjectOnTheScreen(GameStatesEnum selectedScreen, string name, Point location)
        {
            this.screens[selectedScreen].moveObjectToTheNewLocation(name, location);
        }
        public void moveFontOnTheScreen(GameStatesEnum selectedScreen, string name, Point location)
        {
            this.screens[selectedScreen].moveFontToTheNewLocation(name, location);
        }
        public void changeTextOfTheFontOnScreen(GameStatesEnum selectedScreen, string name, string new_text)
        {
            this.screens[selectedScreen].setTextInChosenFontObject(name, new_text);
        }
        public void orderScreenToDisplayHoldoutObject(GameStatesEnum selectedScreen, string name, Point objectLocation)
        {
            this.getScreen(selectedScreen).moveHoldOutObjectToDisplay(name, objectLocation);
        }
        public void orderScreenToDisplayHoldoutObjectInRandomPlace(GameStatesEnum selectedScreen, string name, Rectangle zone)
        {
            this.getScreen(selectedScreen).moveHoldOutObjectToDisplayInRandomPlace(name, zone);
        }
    }
}
