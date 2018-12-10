using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/// <summary>
/// Klasa odpowiedzialna za łączenie tekstur z wymiarami/pozycją i kolorem  pomiędzy ładowaniem contentu a umieszczaniem go w screenach
/// </summary>
namespace Space_Invaders
{
    class GameObjectsGenerator
    {
        private Dictionary<string, GameObject> gameObjects;
        internal Dictionary<string, GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }

        public GameObject getGameObject(string name) {
            return  GameObjects[name];
        }
        public GameObject generateCloneOfAObject(string name)
        {
            return new GameObject(this.getGameObject(name));
        }

        public List<GameObject> getListOfGameObjects(List<string> namesOfGameobjects)
        {
            List<GameObject> selectedGameObjects = new List<GameObject>();
            namesOfGameobjects.ForEach(name => selectedGameObjects.Add(this.getGameObject(name)));
            return selectedGameObjects;
        }

        public GameObjectsGenerator()
        {
            gameObjects = new Dictionary<string, GameObject>();
        }

        public void GenerateContent(string objectName, Texture2D texture, Rectangle rectangle, Color color)
        {
            GameObjects.Add(objectName, new GameObject(texture, rectangle, color));
        }

        public void GenerateContent(string objectName, Texture2D texture, Point point, Color color)
        {
            this.GenerateContent(objectName, texture, new Rectangle(point.X,point.Y,texture.Width,texture.Height), color);
        }

        public void GenerateContent(string objectName, Texture2D texture, Rectangle rectangle)
        {
            GameObjects.Add(objectName, new GameObject(texture, rectangle, Color.White));
        }

        public void GenerateContent(string objectName, Texture2D texture, Point point)
        {
            this.GenerateContent(objectName, texture, new Rectangle(point.X, point.Y, texture.Width, texture.Height));
        }
        public void GenerateContent(string objectName, Texture2D texture)
        {
            GameObjects.Add(objectName, new GameObject(texture, new Rectangle(0,0,texture.Width,texture.Height), Color.White));
        }

        public void GenerateContent(List<string> objectsNames, List<Texture2D> textures, List<Rectangle> rectangles, List<Color> colors)
        {
            for (int i = 0; i < objectsNames.Count; i++)
            {
                this.GenerateContent(objectsNames[i], textures[i], rectangles[i], colors[i]);
            }
        }
        public void GenerateContent(List<string> objectsNames, List<Texture2D> textures, List<Point> points, List<Color> colors)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < points.Count; i++)
            {
                Point point = points[i];
                Texture2D texture = textures[i];
                rectangles.Add(new Rectangle(point.X, point.Y, texture.Width, texture.Height));
            }
            this.GenerateContent(objectsNames, textures, rectangles, colors);
        }
        public void GenerateContent(List<string> objectsNames, List<Texture2D> textures, List<Rectangle> rectangles)
        {
            for (int i = 0; i < objectsNames.Count; i++)
            {
                this.GenerateContent(objectsNames[i], textures[i], rectangles[i]);
            }
        }
        public void GenerateContent(List<string> objectsNames, List<Texture2D> textures)
        {
            for (int i = 0; i < objectsNames.Count; i++)
            {
                this.GenerateContent(objectsNames[i], textures[i]);
            }
        }
        public void GenerateContent(List<string> objectsNames, List<Texture2D> textures, List<Point> points)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < points.Count; i++)
            {
                Point point = points[i];
                Texture2D texture = textures[i];
                rectangles.Add(new Rectangle(point.X, point.Y, texture.Width, texture.Height));

            }
            this.GenerateContent(objectsNames, textures, rectangles);
        }

        public GameObjectsGenerator(List<string> objectsNames, List<Texture2D> textures, List<Rectangle> rectangles, List<Color> colors)
        {
            this.GenerateContent(objectsNames, textures, rectangles, colors);
        }

        public GameObjectsGenerator(List<string> objectsNames, List<Texture2D> textures, List<Point> points, List<Color> colors)
        {

            this.GenerateContent(objectsNames, textures, points, colors);
        }
    }
}