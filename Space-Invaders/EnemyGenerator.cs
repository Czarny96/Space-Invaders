using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    static class EnemyGenerator
    {
        static private int number_of_generated_enemies = 0;
        public static Dictionary<string,GameObject> generateEnemies(List<GameObject> enemyTypes, int enemies_to_generate) {

            
            List<string> names = EnemyGenerator.generateEnemyNames(enemies_to_generate);
            Dictionary<string, GameObject> enemy_dict = new Dictionary<string, GameObject>();
            Random random_generator = new Random(DateTimeOffset.Now.Millisecond);
            int enemy_width = enemyTypes[0].ObjectShape.Width;
            int current_x_position_of_generated_enemy = 0;

            for (int i = 0; i < enemies_to_generate; i++)
            {
                GameObject new_enemy = 
                new GameObject(enemyTypes[random_generator.Next(0, enemyTypes.Count)]);
                new_enemy.moveObject(new Point(current_x_position_of_generated_enemy, 0));
                current_x_position_of_generated_enemy += enemy_width;
                enemy_dict.Add(names[i],new_enemy);
            }


            return enemy_dict;
        }
        public static List<string> generateEnemyNames(int number_of_names_to_generate)
        {
            List<string> names_list = new List<string>();

            for (int i = 1; i <= number_of_names_to_generate; i++)
            {
                number_of_generated_enemies += i;
                names_list.Add("Enemy" + number_of_generated_enemies);
            }

            return names_list;

        }
    }
}
