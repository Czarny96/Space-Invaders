using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;


namespace Space_Invaders
{
    class Screen
    {
        private Dictionary<string,GameObject> gameObjectsOnScreen;

        //Tu przechowywać elementy, które nie mają być od razu rysowane na ekranie, a pojawiać się jedynie w razie potrzeby
        private Dictionary<string, GameObject> holdOutObjects;

        private Dictionary<string, FontObject> fontsOnScreen;

        //TODO: TU dać dodawanie mapy
        //private MapObject map;

        private ScreenBoundaries screenBoundaries;
        private Timer screenEventTimer;
        
        internal Dictionary<string, GameObject> GameObjectsOnScreen { get => gameObjectsOnScreen; set => gameObjectsOnScreen = value; }
        internal ScreenBoundaries ScreenBoundary { get => screenBoundaries; set => screenBoundaries = value; }
        internal Dictionary<string, FontObject> FontsOnScreen { get => fontsOnScreen; set => fontsOnScreen = value; }
        public Timer ScreenEventTimer { get => screenEventTimer; set => screenEventTimer = value; }
        internal Dictionary<string, GameObject> HoldOutObjects { get => holdOutObjects; set => holdOutObjects = value; }
        //public MapObject Map { get => map; set => map = value; }

        internal struct ScreenBoundaries
        {
            private int windowWidth;
            private int windowHeight;

            public ScreenBoundaries(GraphicsDevice graphicsDevice)
            {
                this.windowWidth = graphicsDevice.Viewport.Width;
                this.windowHeight = graphicsDevice.Viewport.Height;
            }

            public int WindowWidth { get => windowWidth; set => windowWidth = value; }
            public int WindowHeight { get => windowHeight; set => windowHeight = value; }

            public void obtainGameScreenSize(GraphicsDevice graphicsDevice)
            {
                this.windowWidth = graphicsDevice.Viewport.Width;
                this.windowHeight = graphicsDevice.Viewport.Height;
            }
        }

        public Screen()
        {
            this.gameObjectsOnScreen = new Dictionary<string, GameObject>();
            this.FontsOnScreen = new Dictionary<string, FontObject>();
            this.HoldOutObjects = new Dictionary<string, GameObject>();
            //this.Map = new MapObject();
        }

        public Screen(GraphicsDevice graphicsDevice):this()
        {
            this.screenBoundaries = new ScreenBoundaries(graphicsDevice);
        }

        public GameObject GetGameObject(string name) {

            GameObject foundObject;
            try
            {
               foundObject = GameObjectsOnScreen[name];

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                foundObject = null;
                
            }
            return foundObject;
        }

        public FontObject GetFontObject(string name)
        {
            FontObject foundObject;
            try
            {
                foundObject = FontsOnScreen[name];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                foundObject = null;
            }
            return foundObject;
        }
        public void setTextInChosenFontObject(string fontName, string new_text)
        {
            this.GetFontObject(fontName).Text = new_text;
        }

        /*public void setMapOnTheScreen(MapObject map)
        {
            this.Map = map;
        }*/

        public void addNewObjectToTheScreen(string name,GameObject gameObject)
        {
            GameObjectsOnScreen.Add(name,gameObject);
        }
        public void addNewFontToTheScreen(string name, FontObject font)
        {
            fontsOnScreen.Add(name, font);
        }
        public void addNewObjectsToTheScreen(List<string> names,List<GameObject> gameObjects)
        {
            for (int i = 0; i < names.Count; i++)
            {
                this.addNewObjectToTheScreen(names[i], gameObjects[i]);
            }
        }

        public void addNewFontsToTheScreen(List<string> names, List<FontObject> fonts)
        {
            for (int i = 0; i < names.Count; i++)
            {
                this.addNewFontToTheScreen(names[i], fonts[i]);
            }
        }
        public void addNewObjectsToTheScreen(Dictionary<string,GameObject> dictOfGameObjects)
        {
            foreach (var key in dictOfGameObjects)
            {
                GameObjectsOnScreen.Add(key.Key,dictOfGameObjects[key.Key]);
            }
         
        }

        public void addObjectAsABackGround(string name,GameObject gameObject)
        {
            this.addNewObjectToTheScreen(name,new GameObject(gameObject.ObjectType,
                new Rectangle(0,0,ScreenBoundary.WindowWidth,ScreenBoundary.WindowHeight),
                gameObject.ObjectColor));
        }

        public void addObjectsAsABackGround(List<string> names,List<GameObject> backgroundObjects) {

            for (int i = 0; i < names.Count; i++)
            {
                this.addObjectAsABackGround(names[i], backgroundObjects[i]);
            }
        }
        public void addObjectsAsABackGround(Dictionary<string, GameObject> dictOfGameObjects)
        {
            for (int i = 0; i < dictOfGameObjects.Count; i++)
            {
                this.addObjectAsABackGround(dictOfGameObjects.Keys.ToList()[i], dictOfGameObjects[dictOfGameObjects.Keys.ToList()[i]]);
            }
        }

        public void moveObjectToTheNewLocation(string objectName,Point location)
        {
            this.GetGameObject(objectName).moveObject(location);
        }
        public void moveFontToTheNewLocation(string objectName, Point location)
        {
            this.GetFontObject(objectName).moveObject(location);
        }

        public void removeObject(string objectName)
        {
            this.gameObjectsOnScreen.Remove(objectName);
        }

        public void moveObjectToTheMiddleOfTheWidth(string objectName, int height) {
            Rectangle Object = this.GetGameObject(objectName).ObjectShape;
            int middleOfTheWidth = ScreenBoundary.WindowWidth / 2;
            int middleLocationOfAnObject = middleOfTheWidth - (Object.Width / 2);
            this.GetGameObject(objectName).ObjectShape = new Rectangle(middleLocationOfAnObject, height, Object.Width, Object.Height);
            this.GetGameObject(objectName).updateCollisionMesh();
        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            //Map.drawMapObject(spriteBatch);
            GameObjectsOnScreen.Values.ToArray().ToList().ForEach(gameObject => gameObject.DrawGameObject(spriteBatch));
            FontsOnScreen.Values.ToArray().ToList().ForEach(fontObject => fontObject.DrawFontObject(spriteBatch));

        }
        public void AnimateScreen()
        {
            foreach (GameObject gameObject in this.GameObjectsOnScreen.Values)
            {
                gameObject.moveObjectBasedOnItsMovmentVector();
            }
        }

        public string checkIfObjectIsInCollisionWithOtherObjects(string nameOfObjectToHaveCollision, List<string> namesOfObjectToCheck)
        {
            string answer = null;

            
            foreach (string objectName in namesOfObjectToCheck)
            {
                if (nameOfObjectToHaveCollision!=objectName &&
                    this.GetGameObject(objectName)!=null && 
                    !this.GetGameObject(objectName).isInCollisionWithOtherObject(this.GetGameObject(nameOfObjectToHaveCollision)).Equals(new Point(-1,-1)))
                {
                    answer = objectName;
                    break;
                }
            }
            return answer;
        }

        public string checkIfObjectIsInCollisionWithOtherObjects(GameObject ObjectToHaveCollision, List<string> namesOfObjectToCheck)
        {
            string answer = null;

           
            foreach (string objectName in namesOfObjectToCheck)
            {

                if (
                    this.GetGameObject(objectName) != null &&
                    !this.GetGameObject(objectName).isInCollisionWithOtherObject(ObjectToHaveCollision).Equals(new Point(-1, -1)))
                {
                    answer = objectName;
                    break;
                }
            }
            return answer;
        }

       

        public bool checkIfObjectIsBeyondBottomOfTheScreen(string objectName)
        {
            CollisionMesh gameObjectCollisionMesh = this.GetGameObject(objectName).CollisionMesh;
            bool answer = false;

            if (gameObjectCollisionMesh.Top_middle_point.Y >= this.ScreenBoundary.WindowHeight)
            {
                answer = true;
            }
            return answer;
        }

        public void setUpEventTimer(int interval, bool cyclic = true,ElapsedEventHandler calledMethod = null)
        {
            this.ScreenEventTimer = new Timer(interval);
            this.ScreenEventTimer.Elapsed += calledMethod;
            this.ScreenEventTimer.AutoReset = cyclic;
            this.ScreenEventTimer.Enabled = true;
        }

        public void stopEventTimer()
        {
            this.ScreenEventTimer.Stop();
            this.ScreenEventTimer.Dispose();
        }

        public void addHoldOutObject(string objectName, GameObject gameObject)
        {
            this.HoldOutObjects.Add(objectName, gameObject);
        }

        public void addHoldOutObjects(List<string> objectNames, List<GameObject> gameObjects )
        {
            for (int i = 0; i < objectNames.Count; i++)
            {
                this.addHoldOutObject(objectNames[i],gameObjects[i]);
            }
        }
        public GameObject getHoldoutObject(string name)
        {
            return this.HoldOutObjects[name];
        }

        public void moveHoldOutObjectToDisplay(string name)
        {
            this.addNewObjectToTheScreen(name, this.getHoldoutObject(name));
        }

        public void moveHoldOutObjectToDisplay(string name, Point display_location)
        {
            this.getHoldoutObject(name).moveObject(display_location);
            this.addNewObjectToTheScreen(name, this.getHoldoutObject(name));
        }

        public void moveHoldOutObjectToDisplayInRandomPlace(string name, Rectangle zoneToPickPoint)
        {
            Point randomPoint;
            GameObject objectToPlace = this.getHoldoutObject(name);
            
            Random r = new Random();
            bool isLocationCorrect = false;
            List<string> objectsToCheck = GameObjectsOnScreen.Keys.ToList();
            if (objectsToCheck.Contains("tlo"))
            {
                objectsToCheck.Remove("tlo");
            }

            while (isLocationCorrect != true)
            {
                randomPoint = new Point(r.Next(zoneToPickPoint.X, zoneToPickPoint.Width - objectToPlace.ObjectShape.Width),
                                        r.Next(zoneToPickPoint.Y, zoneToPickPoint.Height - objectToPlace.ObjectShape.Height));

                objectToPlace.moveObject(randomPoint);

                if (this.checkIfObjectIsInCollisionWithOtherObjects(objectToPlace, objectsToCheck) == null)
                {
                    isLocationCorrect = true;
                }
            }

            this.moveHoldOutObjectToDisplay(name);
        }

        public void stretchGameObjectOnScreenNTimes(string name, int n)
        {
            this.GetGameObject(name).stretchObjectNTimes(n);
        }

        public void squeezeGameObjectOnScreenNTimes(string name, int n)
        {
            this.GetGameObject(name).squeezeObjectNTimes(n);
        }

        
    }
}
