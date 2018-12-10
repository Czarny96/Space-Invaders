using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Space_Invaders
{
    class FontObject
    {
        private SpriteFont objectType;
        private Vector2 objectShape;
        private Color objectColor;
        private string text;

        public FontObject(SpriteFont objectType, Vector2 objectShape, Color objectColor)
        {
            this.objectType = objectType;
            this.objectShape = objectShape;
            this.objectColor = objectColor;
            this.Text = "";
        }

        public FontObject(SpriteFont objectType, Vector2 objectShape, Color objectColor, string text):this(objectType,objectShape,objectColor)
        {
            this.text = text;
        }

        public void DrawFontObject(SpriteBatch spriteBatch)
        {
            if (ObjectType != null && ObjectShape != null && ObjectColor != null)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(ObjectType,Text,ObjectShape, ObjectColor);
                spriteBatch.End();
            }
        }

        public void moveObject(Point newPosition)
        {
            this.objectShape.X = newPosition.X;
            this.objectShape.Y = newPosition.Y;
        }

        public SpriteFont ObjectType { get => objectType; set => objectType = value; }
        public Vector2 ObjectShape { get => objectShape; set => objectShape = value; }
        public Color ObjectColor { get => objectColor; set => objectColor = value; }
        public string Text { get => text; set => text = value; }
    }
}
