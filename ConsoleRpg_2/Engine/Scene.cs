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
        public List<Item> Items { get; set; }
        public List<Decoration> Decorations { get; set; }
        public List<Prop> Props { get; set; }
        public List<Character> Characters { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<int, GameObject> GetObjectDict()
        {
            var dict = new Dictionary<int, GameObject>();
            
            for (int i = 0; i < Items.Count; ++i)
            {
                dict[i + 1] = Items[i];
            }
            for (int i = 0; i < Characters.Count; ++i)
            {
                dict[i + 1 + Items.Count] = Characters[i];
            }
            for (int i = 0; i < Props.Count; ++i)
            {
                dict[i + 1 + Items.Count + Characters.Count] = Props[i];
            }
            for (int i = 0; i < Decorations.Count; ++i)
            {
                dict[i + 1 + Items.Count + Characters.Count + Props.Count] = Decorations[i];
            }

            return dict;
        }

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