using System;
using ConsoleRpg_2.GameObjects.Characters;

namespace ConsoleRpg_2.Helpers
{
    public static class RandomsHelper
    {
        private static readonly Random Random = new Random();

        public static StatSet GuessStats(StatSet actual, int perception)
        {
            int baseStatErrorUp = 0;
            int baseStatErrorDown = 0;

            if (perception >= 10)
            {
                baseStatErrorDown = 0;
                baseStatErrorUp = 0;
            }
            else
            {
                switch (perception)
                {
                    case 9: 
                        baseStatErrorDown = 0;
                        baseStatErrorUp = 1;
                        break;
                    
                    case 8: 
                        baseStatErrorDown = 1;
                        baseStatErrorUp = 1;
                        break;
                    
                    case 7: 
                        baseStatErrorDown = 1;
                        baseStatErrorUp = 2;
                        break;
                    
                    case 6: 
                        baseStatErrorDown = 1;
                        baseStatErrorUp = 2;
                        break;
                    
                    case 5: 
                        baseStatErrorDown = 2;
                        baseStatErrorUp = 2;
                        break;
                    
                    case 4: 
                        baseStatErrorDown = 2;
                        baseStatErrorUp = 2;
                        break;
                    
                    case 3: 
                        baseStatErrorDown = 3;
                        baseStatErrorUp = 4;
                        break;
                    
                    case 2: 
                        baseStatErrorDown = 4;
                        baseStatErrorUp = 4;
                        break;
                    
                    case 1: 
                        baseStatErrorDown = 4;
                        baseStatErrorUp = 4;
                        break;
                    
                    case 0: 
                        baseStatErrorDown = 5;
                        baseStatErrorUp = 5;
                        break;
                }
            }

            var race = actual.Race;
            var gender = actual.Gender;

            if (perception <= 2)
            {
                gender = Gender.Other;
            }
            if (perception <= 1)
            {
                race = Race.Undefined;
            }

            return new StatSet
            {
                Race = race,
                Gender = gender,
                Health = actual.Health + Random.Next(-baseStatErrorDown * 10, baseStatErrorUp * 10),
                Mana = actual.Mana + Random.Next(-baseStatErrorDown * 10, baseStatErrorUp * 10),
                MaxHealth = actual.MaxHealth + Random.Next(-baseStatErrorDown * 10, baseStatErrorUp * 10),
                MaxMana = actual.MaxMana + Random.Next(-baseStatErrorDown * 10, baseStatErrorUp * 10),

                ActionPoints = actual.ActionPoints + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                MaxActionPoints = actual.MaxActionPoints + Random.Next(-baseStatErrorDown, baseStatErrorUp),

                Strength = actual.Strength + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                Perception = actual.Perception + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                Stamina = actual.Stamina + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                Charisma = actual.Charisma + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                Intelligence = actual.Intelligence + Random.Next(-baseStatErrorDown, baseStatErrorUp),
                Agility = actual.Agility + Random.Next(-baseStatErrorDown, baseStatErrorUp),
            };
        }
    }
}