using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ConsoleRpg_2.GameObjects;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Engine
{
    public class Scene
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Decoration> Decorations { get; set; } = new List<Decoration>();
        public List<Prop> Props { get; set; } = new List<Prop>();
        public List<Character> Characters { get; set; } = new List<Character>();

        public string Name { get; set; }
        public string Description { get; set; }

        public void InitializeScene()
        {           
            // Initialize attitudes if needed.
            foreach (var character in Characters)
            {
                foreach (var otherCharacter in Characters)
                {
                    if (character == otherCharacter)
                    {
                        continue;
                    }

                    if (!character.Attitudes.ContainsKey(otherCharacter))
                    {
                        character.Attitudes[otherCharacter] = character.DefaultAttitude;
                    }
                }   
            }
        }
        
        public void Update()
        {
            
        }

        public List<GameObject> GetObservableObjects()
        {
            var objects = new List<GameObject>();
            
            objects.AddRange(Items);
            objects.AddRange(Characters);
            objects.AddRange(Props);
            objects.AddRange(Decorations);

            return objects;
        }
        
        public List<GameObject> GetConversableObjects()
        {
            var objects = new List<GameObject>();
            
            objects.AddRange(Characters);

            return objects;
        }
        
        public SceneDescription GetSceneDescriptions()
        {
            var result = new SceneDescription
            {
                GeneralDescription = $"This is {Description}"
            };


            if (Characters.Count > 5)
            {
                result.CharacterDescription = "Pretty crowded in here.";
            }
            else if (Characters.Count == 0)
            {
                result.CharacterDescription = "Quite empty in here.";
            }

            if (Props.Count > 5)
            {
                result.PropDescription = "There are quite a few things around here.";
            }

            if (Props.Count == 0)
            {
                result.PropDescription = "There are next to no things to see in here.";
            }

            return result;
        }

        public Attitude GetAverageCharacterAttitude(Character character)
        {
            var attitudes = Characters
                .Where(c => c != character)
                .SelectMany(c =>
                    c.Attitudes.Where(a => a.Key == character)
                );

            var average = attitudes.Average(a => (int) a.Value);

            return (Attitude) Math.Floor(average);
        }
    }
}