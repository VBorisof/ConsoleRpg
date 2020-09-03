using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Engine
{
    public class Fight
    {
        private readonly Random _random = new Random();
        private List<Character> Characters { get; set; }

        private const int queueSize = 10;
        public List<Character> Queue { get; set; } = new List<Character>();

        public Fight(List<Character> characters)
        {
            Characters = characters;
            UpdateQueue();
        }

        private void UpdateQueue()
        {
            var queueProbabilities = Characters.Select(c =>
                {
                    double probability = 1.0;
                    if (c.Stats.Agility < 8)
                    {
                        probability = 0.9f;
                    }
                    if (c.Stats.Agility < 6)
                    {
                        probability = 0.8f;
                    }
                    if (c.Stats.Agility < 5)
                    {
                        probability = 0.5f;
                    }
                    if (c.Stats.Agility < 2)
                    {
                        probability = 0.3f;
                    }

                    return new
                    {
                        Character = c,
                        Probability = probability
                    };
                })
                .ToList();

            for (int i = 0; i < queueSize - Queue.Count; ++i)
            {
                Character toPut = null;
                while (toPut == null)
                {
                    var eligibleChars = queueProbabilities
                        .Where(qp => _random.NextDouble() < qp.Probability)
                        .ToList();

                    toPut = eligibleChars.ElementAtOrDefault(_random.Next(0, eligibleChars.Count))
                        ?.Character;
                }
                
                Queue.Add(toPut);
            }
        }

        public void EndCurrentTurn()
        {
            Queue.Remove(Queue.First());
            UpdateQueue();
        }
        
        public void Tick()
        {
            
        }

        public void BeginTurn(Character activeCharacter)
        {
            if (activeCharacter.Stats.ActionPoints < activeCharacter.Stats.MaxActionPoints)
            {
                if (activeCharacter.Stats.Stamina > 0)
                {
                    activeCharacter.Stats.ActionPoints += 1;
                }
                
                else if (activeCharacter.Stats.Stamina >= 4)
                {
                    activeCharacter.Stats.ActionPoints += 2;
                }
                
                else if (activeCharacter.Stats.Stamina >= 8)
                {
                    activeCharacter.Stats.ActionPoints += 3;
                }
                
                else if (activeCharacter.Stats.Stamina >= 10)
                {
                    activeCharacter.Stats.ActionPoints += 4;
                }
            }

            if (activeCharacter.Stats.Health < activeCharacter.Stats.MaxHealth)
            {
                float percent = activeCharacter.Stats.Health / 100.0f;
                
                if (activeCharacter.Stats.Strength > 0)
                {
                    activeCharacter.Stats.Health += (int) (percent * 1);
                }
                else if (activeCharacter.Stats.Strength >= 5)
                {
                    activeCharacter.Stats.Health += (int) (percent * 2);
                }
                else if (activeCharacter.Stats.Strength >= 10)
                {
                    activeCharacter.Stats.Health += (int) (percent * 3);
                }
            }
            
            if (activeCharacter.Stats.Mana < activeCharacter.Stats.MaxMana)
            {
                float percent = activeCharacter.Stats.Mana / 100.0f;
                
                if (activeCharacter.Stats.Intelligence > 0)
                {
                    activeCharacter.Stats.Mana += (int) (percent * 1);
                }
                else if (activeCharacter.Stats.Intelligence >= 3)
                {
                    activeCharacter.Stats.Mana += (int) (percent * 3);
                }
                else if (activeCharacter.Stats.Intelligence >= 5)
                {
                    activeCharacter.Stats.Mana += (int) (percent * 5);
                }
                else if (activeCharacter.Stats.Intelligence >= 8)
                {
                    activeCharacter.Stats.Mana += (int) (percent * 6);
                }
                else if (activeCharacter.Stats.Intelligence >= 10)
                {
                    activeCharacter.Stats.Mana += (int) (percent * 8);
                }
            }
        }
    }
}