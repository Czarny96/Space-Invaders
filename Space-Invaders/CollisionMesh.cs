using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Space_Invaders
{
    class CollisionMesh
    {
        Point top_left_point;
        Point top_middle_point;
        Point top_right_point;
        Point middle_left_point;
        Point middle_right_point;
        Point bottom_left_point;
        Point bottom_middle_point;
        Point bottom_right_point;

        public CollisionMesh(Rectangle objectRectangle)
        {
            this.Top_left_point = new Point(objectRectangle.X, objectRectangle.Y);
            this.Top_middle_point = new Point(objectRectangle.X + (objectRectangle.Width / 2), objectRectangle.Y);
            this.Top_right_point = new Point(objectRectangle.X + objectRectangle.Width, objectRectangle.Y);
            this.Middle_left_point = new Point(objectRectangle.X,objectRectangle.Y + (objectRectangle.Height/2));
            this.Middle_right_point = new Point(objectRectangle.X + objectRectangle.Width, objectRectangle.Y + (objectRectangle.Height / 2));
            this.Bottom_left_point = new Point(objectRectangle.X, objectRectangle.Y + objectRectangle.Height);
            this.Bottom_middle_point = new Point(objectRectangle.X + (objectRectangle.Width / 2), objectRectangle.Y + objectRectangle.Height);
            this.Bottom_right_point = new Point(objectRectangle.X + objectRectangle.Width, objectRectangle.Y + objectRectangle.Height);
        }

        public bool isCollisionTopLeft_with_bottom(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_top_left_point = other_object_collision_mesh.Top_left_point;
            if (other_top_left_point.X >= this.Bottom_left_point.X && 
                other_top_left_point.X <= this.Bottom_right_point.X &&
                other_top_left_point.Y <= this.Bottom_middle_point.Y&&
                other_top_left_point.Y >= this.Top_middle_point.Y)
            {
                answer = true;
            }
            return answer;
        }
        public bool isCollisionTopMiddle_with_bottom(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_top_middle_point = other_object_collision_mesh.Top_middle_point;

            if (Math.Abs(other_top_middle_point.X) >= Math.Abs(this.Bottom_left_point.X) && 
                Math.Abs(other_top_middle_point.X) <= Math.Abs(this.Bottom_right_point.X) &&
                Math.Abs(other_top_middle_point.Y) <= Math.Abs(this.bottom_middle_point.Y)&&
                Math.Abs(other_top_middle_point.Y) >= Math.Abs(this.Top_middle_point.Y))
            {
                answer = true;
            }
            return answer;
        }
        public bool isCollisionTopRight_with_bottom(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_top_right_point = other_object_collision_mesh.Top_right_point;

            if (other_top_right_point.X >= this.Bottom_left_point.X &&
                other_top_right_point.X <= this.Bottom_right_point.X &&
                other_top_right_point.Y <= this.Bottom_middle_point.Y&&
                other_top_right_point.Y >= this.Top_middle_point.Y)
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionTop_Bottom(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            if (
                isCollisionTopMiddle_with_bottom(other_object_collision_mesh)
               )
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionBottomLeft_with_Top(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_bottom_left_point = other_object_collision_mesh.Bottom_left_point;

            if (other_bottom_left_point.X >= this.Top_left_point.X &&
                other_bottom_left_point.X <= this.Top_right_point.X &&
                other_bottom_left_point.Y >= this.Top_middle_point.Y &&
                other_bottom_left_point.Y <= this.Bottom_middle_point.Y)
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionBottomMiddle_with_Top(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_bottom_middle_point = other_object_collision_mesh.Bottom_middle_point;

            if (Math.Abs(other_bottom_middle_point.X) >= Math.Abs(this.Top_left_point.X) &&
                Math.Abs(other_bottom_middle_point.X) <= Math.Abs(this.Top_right_point.X) &&
                Math.Abs(other_bottom_middle_point.Y) >= Math.Abs(this.Top_middle_point.Y) &&
                Math.Abs(other_bottom_middle_point.Y) <= Math.Abs(this.Bottom_middle_point.Y))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionBottomRight_with_Top(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_bottom_right_point = other_object_collision_mesh.Bottom_right_point;

            if (other_bottom_right_point.X >= this.Top_left_point.X &&
                other_bottom_right_point.X <= this.Top_right_point.X && 
                other_bottom_right_point.Y >= this.Top_middle_point.Y&&
                other_bottom_right_point.Y <= this.Bottom_middle_point.Y)
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionBottom_Top(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            if (
                isCollisionBottomMiddle_with_Top(other_object_collision_mesh)
               )
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionTopRight_with_Left(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_top_right_point = other_object_collision_mesh.Top_right_point;

            if (Math.Abs(other_top_right_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                 Math.Abs(other_top_right_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                  Math.Abs(other_top_right_point.X) >= Math.Abs(this.Middle_left_point.X) &&
                  Math.Abs(other_top_right_point.X) <= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionRightMiddle_with_Left(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_right_middle_point = other_object_collision_mesh.Middle_right_point;

            if (Math.Abs(other_right_middle_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                Math.Abs(other_right_middle_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                 Math.Abs(other_right_middle_point.X) >= Math.Abs(this.Middle_left_point.X) &&
                 Math.Abs(other_right_middle_point.X) <= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionRightBottom_with_Left(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_right_bottom_point = other_object_collision_mesh.Bottom_right_point;

            if (Math.Abs(other_right_bottom_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                Math.Abs(other_right_bottom_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                Math.Abs(other_right_bottom_point.X) >= Math.Abs(this.Middle_left_point.X) &&
                Math.Abs(other_right_bottom_point.X) <= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionRight_Left(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            if (
                 isCollisionRightMiddle_with_Left(other_object_collision_mesh))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionTopLeft_with_Right(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_top_left_point = other_object_collision_mesh.Top_left_point;

            if (Math.Abs(other_top_left_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                 Math.Abs(other_top_left_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                  Math.Abs(other_top_left_point.X) <= Math.Abs(this.Middle_left_point.X) &&
                   Math.Abs(other_top_left_point.X) >= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionLeftMiddle_with_Right(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_left_middle_point = other_object_collision_mesh.Middle_left_point;

            if (Math.Abs(other_left_middle_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                Math.Abs(other_left_middle_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                 Math.Abs(other_left_middle_point.X) <= Math.Abs(this.Middle_left_point.X) &&
                  Math.Abs(other_left_middle_point.X) >= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }

        public bool isCollisionLeftBottom_with_Right(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            Point other_left_bottom_point = other_object_collision_mesh.Bottom_left_point;

            if (Math.Abs(other_left_bottom_point.Y) >= Math.Abs(this.Top_left_point.Y) &&
                Math.Abs(other_left_bottom_point.Y) <= Math.Abs(this.Bottom_left_point.Y) &&
                Math.Abs(other_left_bottom_point.X) <= Math.Abs(this.Middle_left_point.X) &&
                 Math.Abs(other_left_bottom_point.X) >= Math.Abs(this.Middle_right_point.X))
            {
                answer = true;
            }
            return answer;
        }
        public bool isCollisionLeft_Right(CollisionMesh other_object_collision_mesh)
        {
            bool answer = false;
            if (
                isCollisionLeftMiddle_with_Right(other_object_collision_mesh))
               
            {
                answer = true;
            }

            return answer;
        }

        public Point isAnyCollision(CollisionMesh other_object_collision_mesh)
        {
            Point collisionPoint = new  Point(-1,-1);

            if (isCollisionTop_Bottom(other_object_collision_mesh))
            {
                collisionPoint = this.Top_middle_point;
            }
            else if (isCollisionBottom_Top(other_object_collision_mesh))
            {
                collisionPoint = this.Bottom_middle_point;
            }
            else if (isCollisionLeft_Right(other_object_collision_mesh))
            {
                    collisionPoint = this.Middle_left_point;
            }
            else if (isCollisionRight_Left(other_object_collision_mesh))
            {
                collisionPoint = this.Middle_right_point;
            }

            return collisionPoint;
        }

        public Dictionary<string,bool> generateCollisionDictionary(CollisionMesh other_object_collision_mesh)
        {
            Dictionary<string, bool> collisionDictionary = new Dictionary<string, bool>();

            collisionDictionary.Add("TOP", false);
            collisionDictionary.Add("BOTTOM", false);
            collisionDictionary.Add("LEFT", false);
            collisionDictionary.Add("RIGHT", false);

            if (isCollisionTop_Bottom(other_object_collision_mesh))
            {
                collisionDictionary["TOP"] = true;
            }
            else if(isCollisionBottom_Top(other_object_collision_mesh))
            {
                collisionDictionary["BOTTOM"] = true;
            }
            else if (isCollisionLeft_Right(other_object_collision_mesh))
            {
                collisionDictionary["LEFT"] = true;
            }
            else if (isCollisionRight_Left(other_object_collision_mesh))
            {
                collisionDictionary["RIGHT"] = true;
            }
            return collisionDictionary;
        }

        public Point Top_left_point { get => top_left_point; set => top_left_point = value; }
        public Point Top_middle_point { get => top_middle_point; set => top_middle_point = value; }
        public Point Top_right_point { get => top_right_point; set => top_right_point = value; }
        public Point Middle_left_point { get => middle_left_point; set => middle_left_point = value; }
        public Point Middle_right_point { get => middle_right_point; set => middle_right_point = value; }
        public Point Bottom_left_point { get => bottom_left_point; set => bottom_left_point = value; }
        public Point Bottom_middle_point { get => bottom_middle_point; set => bottom_middle_point = value; }
        public Point Bottom_right_point { get => bottom_right_point; set => bottom_right_point = value; }
    }
}
