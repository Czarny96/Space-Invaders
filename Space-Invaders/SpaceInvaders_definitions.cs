using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System.Threading;
using System.Linq;

namespace Space_Invaders
{
    public partial class SpaceInvaders : Game
    {
      
       
        protected void makeSplash()
        {
            
            if (!isSplash)
            {
                //Console.WriteLine("Make Splash");
                names_to_load = new List<string>() { "background", "splash" };
                screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
                isSplash = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentGameState = GameStatesEnum.GAME;
            }
            
        }


      
        protected void playGame()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").ObjectShape.X > 0)
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(plane_x_direction, plane_y_direction);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").ObjectShape.X < width)
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(-plane_x_direction, plane_y_direction);
            }
            else
            {
                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane").MovmentVector = new MovmentVector(0, 0);
            }

           
            

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isSpacePress)
            {
                generateBullet();
                isSpacePress = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isSpacePress)
                isSpacePress = false;



            foreach (var bullet in nameBullet)
            {
                List<string> collision_objects= new List<string>();
                current_enemies.ForEach(enemy=>collision_objects.Add(enemy));
                collision_objects.Add("plane");
                pottentialCollisionObjectName = screenManager.getScreen(GameStatesEnum.GAME).checkIfObjectIsInCollisionWithOtherObjects(bullet, collision_objects);

                if (pottentialCollisionObjectName != null && pottentialCollisionObjectName!="plane")
                {
                    if (pottentialCollisionObjectName==enemy_selected_to_charge_name)
                    {
                        was_enemy_shoot_down = true;
                    }
                        screenManager.getScreen(GameStatesEnum.GAME).removeObject(pottentialCollisionObjectName);
                      
                        current_enemies.Remove(pottentialCollisionObjectName);
                        RemoveEnemyFromHisDictionnary(pottentialCollisionObjectName);
                        BulletsToRemove.Add(bullet);
                        
                        
                        points++;

                        screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "point_font", new Point(85, 460));
                        screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "point_font", "Points: " + points);

                }
                else if (pottentialCollisionObjectName=="plane")
                {
                   
                    BulletsToRemove.Add(bullet);
                    life--;

                    
                    screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheMiddleOfTheWidth("plane",
                        screenManager.getSelectedScreenHeight(GameStatesEnum.GAME) -
                            (screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME, "plane").ObjectShape.Height + 25));

                    screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(25, 460));
                    screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Life: " + life);
                }
            }

            foreach (var bullet in BulletsToRemove)
            {
                screenManager.getScreen(GameStatesEnum.GAME).removeObject(bullet);
                nameBullet.Remove(bullet);
            }
            BulletsToRemove.Clear();


            if (enemy_arrival_dict["left_top"].Count==0)
            {
                generateEnemiesOnTheMap("left_top");
                left_top_index = 0;
                move_left_top = true;


            }
            if (enemy_arrival_dict["left_bottom"].Count == 0)
            {
               
                generateEnemiesOnTheMap("left_bottom");
                left_bottom_index = 0;
                move_left_bottom = true;



            }
            if (enemy_arrival_dict["right_top"].Count == 0)
            {
                
                generateEnemiesOnTheMap("right_top");
                right_top_index = 0;
                move_right_top = true;
            }
            if (enemy_arrival_dict["right_bottom"].Count == 0)
            {
              
                generateEnemiesOnTheMap("right_bottom");
                right_bottom_index = 0;
                move_right_bottom = true;
            }
            //TODO: Tymczasowo twardo zakodowane wartości
            if (move_left_top)
            {
                moveEnemiesFromSelectedGroupByCurve("left_top",ref left_top_index);
            }
            if (move_left_bottom)
            {
                moveEnemiesFromSelectedGroupByCurve("left_bottom",ref left_bottom_index);
            }
            if (move_right_top)
            {
                moveEnemiesFromSelectedGroupByCurve("right_top",ref right_top_index);
            }
            if (move_right_bottom)
            {
                moveEnemiesFromSelectedGroupByCurve("right_bottom",ref right_bottom_index);
            }
            if (enemy_movment_counter == 0)
            {
                enemy_x_movment = -enemy_x_movment;
                //ruch przeciwników w lewo i prawo
                foreach (var enemy in current_enemies)
                {
                   
                    screenManager.getGameObjectFromTheScreen(currentGameState, enemy).changeMovementVector(enemy_x_movment, 
                        screenManager.getGameObjectFromTheScreen(currentGameState, enemy).MovmentVector.Y_movment);
                    
                }

                enemy_movment_counter = 60;

            }
            else
            {
                enemy_movment_counter--;
            }
            if (enemy_charge_counter == 0 && is_enemy_charging == false)
            {
                ChargeEnemy();
                was_enemy_shoot_down = false;
                lasting_charge_counter = 60;
                enemy_charge_counter = 240;
            }else if(enemy_charge_counter >0 && is_enemy_charging==false)
            {
                enemy_charge_counter --;
            }
            if (lasting_charge_counter==0 && is_enemy_charging==true)
            {
                if (was_enemy_shoot_down==false)
                {
                    generate_enemy_bullet();
                }
              
                make_enemy_return();
                lasting_return_counter = 60;
                
            }
            else if(lasting_charge_counter > 0 && is_enemy_charging == true)
            {
                lasting_charge_counter--;
            }

            if (lasting_return_counter==0&&is_enemy_returning==true && is_enemy_charging == false)
            {
                stopEnemy();

            }
            else if(lasting_return_counter > 0 && is_enemy_returning == true && is_enemy_charging ==false)
            {
                lasting_return_counter--;
            }


            if (life == 0) currentGameState = GameStatesEnum.SUMMARY;
        }

        protected void make_enemy_return()
        {
            enemy_selected_to_charge.changeMovementVector(0, -2);
            is_enemy_charging = false;
            is_enemy_returning = true;

        }
        protected void stopEnemy()
        {
            enemy_selected_to_charge.changeMovementVector(enemy_x_movment, 0);
            is_enemy_returning = false;
        }

        protected void makeSummary()
        {
            if (!isSummary)
            {
                names_to_load = new List<string>() { "background" };
                screenManager.getScreen(GameStatesEnum.SUMMARY).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
                isSummary = true;

                names_to_load = new List<string>() { "summary_font" };
                fontGenerator.GenerateContent(names_to_load,
                    new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]) });
                screenManager.getScreen(GameStatesEnum.SUMMARY).addNewFontsToTheScreen(names_to_load, fontGenerator.getListOfFontObjects(names_to_load));
            }
            

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_font", new Point(359, 220));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_font", "Points:" + points + "\nLife: " + life + " \nNew Game: Y\nExit: N");
            
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                RestartGame();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                Exit();
            }
        }

        
        protected void generateBullet()
        {
            gameObjectsGenerator.GenerateContent("bullet" + counterBullet,
                    textureLoader.getContent(textures_locations[3]));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObject("bullet" + counterBullet, gameObjectsGenerator.getGameObject("bullet" + counterBullet));
            isShoot = true;
            planeObject = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("plane");
            int bullet_width = screenManager.getScreen(GameStatesEnum.GAME).getHoldoutObject("bullet" + counterBullet).ObjectShape.Width;

            Point location = new Point(planeObject.ObjectShape.X + (planeObject.ObjectShape.Width / 2 - 2), planeObject.ObjectShape.Y - 20);

            if (screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet) == null)
            {
                screenManager.orderScreenToDisplayHoldoutObject(GameStatesEnum.GAME, "bullet" + counterBullet, location);

                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet).MovmentVector = new MovmentVector(0, shoot_y_direction);
                screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheNewLocation("bullet" + counterBullet, location);

                nameBullet.Add("bullet" + counterBullet);
                counterBullet++;


            }
        }
        protected void generate_enemy_bullet()
        {
            gameObjectsGenerator.GenerateContent("bullet" + counterBullet,
                   textureLoader.getContent(textures_locations[3]));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObject("bullet" + counterBullet, gameObjectsGenerator.getGameObject("bullet" + counterBullet));
            isShoot = true;
            planeObject = enemy_selected_to_charge;
            int bullet_width = screenManager.getScreen(GameStatesEnum.GAME).getHoldoutObject("bullet" + counterBullet).ObjectShape.Width;

            Point location = new Point(planeObject.ObjectShape.X + (planeObject.ObjectShape.Width / 2 - 2), planeObject.ObjectShape.Y + planeObject.ObjectShape.Height+2);

            if (screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet) == null)
            {
                screenManager.orderScreenToDisplayHoldoutObject(GameStatesEnum.GAME, "bullet" + counterBullet, location);

                screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("bullet" + counterBullet).MovmentVector = new MovmentVector(0, -shoot_y_direction);
                screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheNewLocation("bullet" + counterBullet, location);

                nameBullet.Add("bullet" + counterBullet);
                counterBullet++;
            }
        }

        protected void RestartGame()
        {
            life = 3;
            points = 0;
            isSplash = false;
            isSummary = false;
            isSpacePress = false;
           
            isPPress = false;

            nameEnemy.Clear();
            nameBullet.Clear();
            BulletsToRemove.Clear();

            objectsToCheckForCollision.Clear();

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(25, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Life: " + life);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "point_font", new Point(85, 460));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "point_font", "Points: " + points);

            currentGameState = GameStatesEnum.GAME;
        }


       private List<Point> GenerateListOfBezierPoints(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3,float goal,float rate)
        {
            List<Point> bezier_points = new List<Point>();
            float t = 0f;
            while (t<goal)
            {
                bezier_points.Add(Utils.GetPointOnBezierCurve(p0,p1,p2,p3,t).ToPoint());
                t += rate;
            }

            return bezier_points;
        }

        private void generateEnemiesOnTheMap(string which_enemies)
        {
            //enemy_arrival_dict[key] = true;
            names_to_load = new List<string>() { "enemy_blue", "enemy_green", "enemy_red" };
            Dictionary<string, GameObject> enemy_test = EnemyGenerator.generateEnemies(gameObjectsGenerator.getListOfGameObjects(names_to_load), 4);
                    foreach (var item in enemy_test.Keys)
                    {
                        enemy_arrival_dict[which_enemies].Add(item);
                        current_enemies.Add(item);
                    }

                    screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(enemy_test);
                             
        }

        private void moveEnemiesByBezierCurve(List<string>enemies_to_move,string selected_bezier_curve, int point_index)
        {
            int object_width = screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME, enemies_to_move.First()).ObjectShape.Width;
            int i = 0;
            foreach (var enemy in enemies_to_move)
            {
                Point point = enemy_arrival_vectors_dict[selected_bezier_curve][point_index];
                point.X -= object_width*i;
                i++;
               // point.Y -= screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME, enemy).ObjectShape.Y;
                screenManager.moveObjectOnTheScreen(GameStatesEnum.GAME,enemy,point);
            }
        }
        private void moveEnemiesFromSelectedGroupByCurve(string group,ref int passed_index)
        {
            if (passed_index < 100)
            {

                List<string> item = enemy_arrival_dict[group];

                if (item.Count > 0)
                {
                    moveEnemiesByBezierCurve(item, group, passed_index);
                }


                passed_index++;
            }
            else
            {
              
                if (move_left_top)
                {
                    move_left_top = false;
                }
                else if (move_left_bottom)
                {
                    move_left_bottom = false;
                }
                else if (move_right_top)
                {
                    move_right_top = false;
                }
                else if(move_right_bottom)
                {
                    move_right_bottom = false;
                }
            }

        }
        private GameObject pick_enemy()
        {
            Random random_generator = new Random(DateTimeOffset.Now.Millisecond);
            enemy_selected_to_charge_name = current_enemies[random_generator.Next(0, current_enemies.Count)];
            return screenManager.getGameObjectFromTheScreen(GameStatesEnum.GAME,
               enemy_selected_to_charge_name );
        }

        private void ChargeEnemy()
        {
            enemy_selected_to_charge = pick_enemy();
            enemy_selected_to_charge.changeMovementVector(0, 2);
            is_enemy_charging = true;
        }

        private void RemoveEnemyFromHisDictionnary(string enemy_name)
        {
            if (enemy_arrival_dict["left_top"].Contains(enemy_name))
            {
                enemy_arrival_dict["left_top"].Remove(enemy_name);
            }
            else if (enemy_arrival_dict["left_bottom"].Contains(enemy_name))
            {
                enemy_arrival_dict["left_bottom"].Remove(enemy_name);
            }
            else if (enemy_arrival_dict["right_top"].Contains(enemy_name))
            {
                enemy_arrival_dict["right_top"].Remove(enemy_name);
            }
            else if (enemy_arrival_dict["right_bottom"].Contains(enemy_name))
            {
                enemy_arrival_dict["right_bottom"].Remove(enemy_name);
            }
        }
    }

}



  
