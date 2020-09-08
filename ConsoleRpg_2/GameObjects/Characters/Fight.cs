using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.Extensions;

namespace ConsoleRpg_2.GameObjects.Characters
{
    public enum FightCharacterFaction
    {
        Player,
        Ally,
        Enemy
    }
    
    public class FightCharacter
    {
        public Character Character { get; set; }
        public FightCharacterFaction Faction { get; set; }
        public double QueueProbability { get; }

        public FightCharacter(Character character, FightCharacterFaction faction)
        {
            Character = character;
            Faction = faction;

            QueueProbability = 1f;
            if (Character.Stats.Agility < 8)
            {
                QueueProbability = 0.9f;
            }
            if (Character.Stats.Agility < 6)
            {
                QueueProbability = 0.8f;
            }
            if (Character.Stats.Agility < 5)
            {
                QueueProbability = 0.5f;
            }
            if (Character.Stats.Agility < 2)
            {
                QueueProbability = 0.3f;
            }
        }
    }
    
    public class Fight
    {
        private readonly Random _random = new Random();
        
        public List<FightCharacter> FightCharacters { get; set; }

        private const int QueueSize = 10;
        public List<FightCharacter> Queue { get; set; } = new List<FightCharacter>();

        
        public Fight(List<FightCharacter> fightCharacters)
        {
            FightCharacters = fightCharacters;
            UpdateQueue();
        }

        private void UpdateQueue()
        {
            for (int i = 0; i < QueueSize - Queue.Count; ++i)
            {
                FightCharacter toPut = null;
                while (toPut == null)
                {
                    var eligibleChars = FightCharacters
                        .Where(qp => _random.NextDouble() < qp.QueueProbability)
                        .ToList();

                    toPut = eligibleChars.ElementAtOrDefault(_random.Next(0, eligibleChars.Count));
                }
                
                Queue.Add(toPut);
            }
        }

        public void EndCurrentTurn()
        {
            Queue.Remove(Queue.First());
            UpdateQueue();
        }
        
        public FightUpdateResult UpdateFight()
        {
            var deadChars = FightCharacters.Where(c => c.Character.Stats.IsDead).ToList();

            foreach (var deadChar in deadChars)
            {
                Queue.Remove(deadChar);
            }

            FightOutcome? fightOutcome = null;
            
            var aliveChars = FightCharacters.Where(c => !c.Character.Stats.IsDead).ToList();

            if (aliveChars.All(c => c.Faction == FightCharacterFaction.Enemy))
            {
                fightOutcome = FightOutcome.PlayerLose;
            }
            if (aliveChars.All(c => c.Faction == FightCharacterFaction.Ally || c.Faction == FightCharacterFaction.Player))
            {
                fightOutcome = FightOutcome.PlayerWin;
            }
            
            return new FightUpdateResult
            {
                DeadChars = deadChars,
                FightOutcome = fightOutcome
            };
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

        public void PostFight()
        {
            FightCharacters
                .Where(c => !c.Character.Stats.IsDead)
                .ForEach(c =>
                {
                    c.Character.Stats.Health = c.Character.Stats.MaxHealth;
                    c.Character.Stats.Mana = c.Character.Stats.MaxMana;
                    c.Character.Stats.ActionPoints = c.Character.Stats.MaxActionPoints;
                });
        }
    }

    public enum FightOutcome
    {
        PlayerWin,
        PlayerLose
    }
    
    public class FightUpdateResult
    {
        public List<FightCharacter> DeadChars { get; set; }
        public FightOutcome? FightOutcome { get; set; }
    }
}