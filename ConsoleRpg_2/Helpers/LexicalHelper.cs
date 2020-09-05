using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.GameObjects.Characters;

namespace ConsoleRpg_2.Helpers
{
    public static class LexicalHelper
    {
        private static readonly Random Random = new Random();
        
        public static string GetStregthEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Very Strong", "Massive", "Great"
                    }
                },
                {
                    7,
                    new List<string>
                    {
                        "Strong", "Tough", "Big"
                    }
                },
                {
                    5,
                    new List<string>
                    {
                        "Average", "Simple", "Decent"
                    }
                },
                {
                    3,
                    new List<string>
                    {
                        "Weak", "Tiny", "Small"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Miserable", "Ridiculous", "Petty"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }
            
            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));
        }
        public static string GetPerceptionEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Amazingly Perceptive", "Mindreading", "Great"
                    }
                },
                {
                    7,
                    new List<string>
                    {
                        "Perceptive", "Clever", "Reactive"
                    }
                },
                {
                    5,
                    new List<string>
                    {
                        "Decent"
                    }
                },
                {
                    3,
                    new List<string>
                    {
                        "Slow", "Narrow-minded", "Stubborn"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Blind", "Blind", "Blind"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }
            
            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));
        }
        public static string GetStaminaEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Unyielding", "Tireless", "Relentless"
                    }
                },
                {
                    4,
                    new List<string>
                    {
                        "Young", "Fresh", "Energetic"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Ill", "Heavybreathing", "Old"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }
            
            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));        
        }
        public static string GetCharismaEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Gorgeous", "Godlike", "Beautiful"
                    }
                },
                {
                    7,
                    new List<string>
                    {
                        "Handsome", "Charming", "Magnetic"
                    }
                },
                {
                    5,
                    new List<string>
                    {
                        "Good-Looking", "Pretty", "Nice"
                    }
                },
                {
                    3,
                    new List<string>
                    {
                        "Below-Avegare", "Ugly", "Unattractive"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Beast-Like", "Monstrous", "Horrible"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }

            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));
        }
        public static string GetIntelligenceEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Genius", "Incredibly Smart"
                    }
                },
                {
                    7,
                    new List<string>
                    {
                        "Smart", "Intelligent", "Brainy"
                    }
                },
                {
                    5,
                    new List<string>
                    {
                        "Average" // Boring?
                    }
                },
                {
                    3,
                    new List<string>
                    {
                        "Dumbass", "Idiot", "Dimwit"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Ill", "Retarded", "Cookoo"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }
            
            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));
        }
        
        public static string GetAgilityEpithet(int value)
        {
            var dict = new Dictionary<int, List<string>>()
            {
                {
                    10,
                    new List<string>
                    {
                        "Feather-light", "Lightning-fast"
                    }
                },
                {
                    7,
                    new List<string>
                    {
                        "Dexterous", "Agile", "Fast"
                    }
                },
                {
                    5,
                    new List<string>
                    {
                        "Average"
                    }
                },
                {
                    3,
                    new List<string>
                    {
                        "Slow", "Weary", "Heavy"
                    }
                },
                {
                    0,
                    new List<string>
                    {
                        "Rigid", "Disabled"
                    }
                },
            };

            KeyValuePair<int, List<string>> pair;

            try
            {
                pair = dict.First(p => p.Key <= value);
            }
            catch
            {
                pair = dict.Last();
            }
            
            return pair.Value.ElementAt(Random.Next(0, pair.Value.Count));
        }


        public static string GetDescriptionString(Race race, Gender gender)
        {
            string raceString = "";
            
            switch (race)
            {
                case Race.Human:
                    switch (gender)
                    {
                        case Gender.Male:
                            return "Man";
                        case Gender.Female:
                            return "Woman";
                        case Gender.Other:
                            return "Transgender";
                    }
                    break;
                case Race.Elf:
                    raceString = "Elven";
                    break;
                case Race.Orc:
                    raceString = "Orcish";
                    break;
                case Race.Undead:
                    raceString = "Undead";
                    break;
                case Race.Bug:
                    return "Bug";
                case Race.Critter:
                    return "Critter";
                case Race.Undefined:
                    return "Creature";
            }

            switch (gender)
            {
                case Gender.Male:
                    return $"{raceString} Man";
                case Gender.Female:
                    return $"{raceString} Woman";
                case Gender.Other:
                    return $"{raceString} Transgender";
            }

            return "Creature";
        }


        public static string GenderPronoun(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "He";
                case Gender.Female:
                    return "She";
            }
            return "It";
        }

        public static string GetHealthString(int health, int maxHealth, LexicalVerbosity verbosity)
        {
            var percent = health / (float) maxHealth;
            
            switch (verbosity)
            {
                case LexicalVerbosity.Poor:
                {
                    if (percent >= 1)
                    {
                        return "Healthy";
                    }
                    else if (percent >= 0.75)
                    {
                        return "Unwell";
                    }
                    else if (percent >= 0.50)
                    {
                        return "Beaten";
                    }
                    else if (percent >= 0.25)
                    {
                        return "Heavily beaten";
                    }
                    else if (percent >= 0.10)
                    {
                        return "Barely alive";
                    }
                    else if (percent > 0)
                    {
                        return "Almost dead";
                    }
                    break;
                }
                
                case LexicalVerbosity.Normal:
                {
                    if (percent >= 1)
                    {
                        return "Very healthy";
                    }
                    else if (percent >= 0.80)
                    {
                        return "Healthy";
                    }
                    else if (percent >= 0.60)
                    {
                        return "Slightly beaten";
                    }
                    else if (percent >= 0.40)
                    {
                        return "Fairly beaten";
                    }
                    else if (percent >= 0.20)
                    {
                        return "Heavily beaten";
                    }
                    else if (percent >= 0.10)
                    {
                        return "Barely alive";
                    }
                    else if (percent > 0)
                    {
                        return "Almost dead";
                    }
                    break;
                }
                
                case LexicalVerbosity.High:
                {
                    if (percent >= 1)
                    {
                        return "Perfectly Healthy";
                    }
                    else if (percent >= 0.90)
                    {
                        return "Very Healthy";
                    }
                    else if (percent >= 0.80)
                    {
                        return "Slightly unwell";
                    }
                    else if (percent >= 0.70)
                    {
                        return "Slightly damaged";
                    }
                    else if (percent >= 0.60)
                    {
                        return "Fairly damaged";
                    }
                    else if (percent >= 0.50)
                    {
                        return "Half-alive";
                    }
                    else if (percent >= 0.40)
                    {
                        return "Very damaged";
                    }
                    else if (percent >= 0.30)
                    {
                        return "Heavily damaged";
                    }
                    else if (percent >= 0.20)
                    {
                        return "One-fifth alive";
                    }
                    else if (percent >= 0.10)
                    {
                        return "Slightly Alive";
                    }
                    else if (percent >= 0.05)
                    {
                        return "Barely Alive";
                    }
                    else if (percent > 0)
                    {
                        return "Almost dead";
                    }
                    break;
                }
                case LexicalVerbosity.VeryHigh:
                {
                    return $"{health}/{maxHealth}";
                    break;
                }
            }

            return "Dead";
        }
    }

    public enum LexicalVerbosity
    {
        Poor,
        Normal,
        High,
        VeryHigh
    }
}