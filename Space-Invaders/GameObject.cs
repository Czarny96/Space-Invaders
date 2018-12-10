using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


/// <summary>
/// Klasa przechowuje lokacje obiektu, jego teksturę i Kolor. Dodatkowo ma funkcję rysującą.
/// </summary>
namespace Space_Invaders
{
    class GameObject
    {
        private Texture2D objectType;
        private Rectangle objectShape;
        private Color objectColor;
        private MovmentVector movmentVector = new MovmentVector(0,0);
        private CollisionMesh collisionMesh;
       

        public GameObject(Texture2D objectType, Rectangle objectShape, Color objectColor)
        {
            this.ObjectType = objectType;
            this.ObjectShape = objectShape;
            this.ObjectColor = objectColor;
            this.CollisionMesh = new CollisionMesh(this.ObjectShape);
        }

        public GameObject(Texture2D objectType, Rectangle objectShape, Color objectColor, MovmentVector movmentVector) : this(objectType, objectShape, objectColor)
        {
            this.MovmentVector = movmentVector;
        }

        public GameObject(GameObject gameObject)
        {
            this.ObjectType = gameObject.ObjectType;
            this.ObjectShape = gameObject.ObjectShape;
            this.ObjectColor = gameObject.ObjectColor;
            this.CollisionMesh = gameObject.CollisionMesh;
        }

        public Texture2D ObjectType { get => objectType; set => objectType = value; }
        public Rectangle ObjectShape { get => objectShape; set => objectShape = value; }
        public Color ObjectColor { get => objectColor; set => objectColor = value; }
        internal MovmentVector MovmentVector { get => movmentVector; set => movmentVector = value; }
        internal CollisionMesh CollisionMesh { get => collisionMesh; set => collisionMesh = value; }

        public void DrawGameObject(SpriteBatch spriteBatch)
        {
            if (ObjectType!=null && ObjectShape!=null && ObjectColor!=null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ObjectType, ObjectShape, ObjectColor);
                spriteBatch.End();
            }
        }

        public void moveObject(Point newPosition)
        {
            this.objectShape.X = newPosition.X;
            this.objectShape.Y = newPosition.Y;
            this.updateCollisionMesh();
        }

        public void changeMovementVector(int x_movment, int y_movment)
        {
            this.movmentVector.X_movment = x_movment;
            this.movmentVector.Y_movment = y_movment;
        }

        public void updateCollisionMesh()
        {
            this.CollisionMesh = new CollisionMesh(this.ObjectShape);
        }

        public void moveObjectBasedOnItsMovmentVector()
        {
            this.objectShape.X += this.movmentVector.X_movment;
            this.objectShape.Y += this.movmentVector.Y_movment;
            updateCollisionMesh();

        }

        public Point isInCollisionWithOtherObject(GameObject gameObject)
        {

            try
            {
                return this.CollisionMesh.isAnyCollision(gameObject.CollisionMesh);
            }
            catch (Exception)
            {

                return new Point(-1,-1);
            }
            
                
        }

        public Dictionary<string,bool> getCollisionDictionary(GameObject gameObject)
        {
            return this.CollisionMesh.generateCollisionDictionary(gameObject.CollisionMesh);
        }

        public void changeGameObjectShape(Rectangle newShape)
        {
            this.ObjectShape = newShape;
            this.updateCollisionMesh();
        }

        public void stretchObjectNTimes(int n)
        {
            int current_width = this.ObjectShape.Width;
            this.changeGameObjectShape(new Rectangle(this.ObjectShape.X,this.ObjectShape.Y,current_width*n,this.ObjectShape.Height));
        }

        public void squeezeObjectNTimes(int n)
        {
            int current_width = this.ObjectShape.Width;
            this.changeGameObjectShape(new Rectangle(this.ObjectShape.X, this.ObjectShape.Y, current_width/n, this.ObjectShape.Height));
        }

        public Color obtainColorOfThePixelInGivenPoint(Point point)
        {
            Color [,] colors2D = new Color[this.ObjectType.Width, this.ObjectType.Height];
            Color[] colors1D = new Color[this.ObjectType.Width * ObjectType.Height];
            
           
            
            this.ObjectType.GetData<Color>(colors1D);

            for (int x = 0; x < this.ObjectType.Width; x++)
            {
                for (int y = 0; y < this.ObjectType.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * this.ObjectType.Width];
                }
            }

            return colors2D[point.X,point.Y];
        }
    }
}
