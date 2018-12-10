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
    class FontGenerator
    {
        private Dictionary<string, FontObject> fontObjects;
        internal Dictionary<string, FontObject> FontObjects { get => fontObjects; set => fontObjects = value; }
        public FontGenerator()
        {
            this.FontObjects = new Dictionary<string, FontObject>();
        }

        public FontObject getFontObject(string name)
        {
            return FontObjects[name];
        }

        public List<FontObject> getListOfFontObjects(List<string> namesOfGameobjects)
        {
            List<FontObject> selectedFontObjects = new List<FontObject>();
            namesOfGameobjects.ForEach(name => selectedFontObjects.Add(this.getFontObject(name)));
            return selectedFontObjects;
        }

        public void GenerateContent(string objectName, SpriteFont font, Vector2 vector, Color color,string text)
        {

            FontObjects.Add(objectName, new FontObject(font, vector, color,text));
        }

        public void GenerateContent(List<string> objectsNames, List<SpriteFont> fonts, List<Point> points, List<Color> colors,List<string> textes)
        {
           
            for (int i = 0; i < objectsNames.Count; i++)
            {
                Point point = points[i];
                SpriteFont font = fonts[i];
                this.GenerateContent(objectsNames[i],font, new Vector2(point.X, point.Y),colors[i],textes[i]);
            }
        }
        public void GenerateContent(List<string> objectsNames, List<SpriteFont> fonts)
        {
            for (int i = 0; i < objectsNames.Count; i++)
            {
                SpriteFont font = fonts[i];
                this.GenerateContent(objectsNames[i], font, new Vector2(0, 0), Color.White, "");
            }
        }

        public FontGenerator(List<string> objectsNames, List<SpriteFont> fonts, List<Point> points, List<Color> colors, List<string> textes)
        {
            this.GenerateContent(objectsNames, fonts, points, colors,textes);
        }
    }
}