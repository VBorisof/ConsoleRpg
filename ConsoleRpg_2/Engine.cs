﻿using System;
using System.Collections.Generic;
using ConsoleRpg_2.Extensions;

namespace ConsoleRpg_2
{
    public enum GameState
    {
        Playing,
        Inventory,
        Stats
    }
    
    public class Engine
    {
        private Character _currentCharacter;
        private Scene _currentScene;
        private GameState _currentState;
        
        private void ProcessLookAt()
        {
            Console.WriteLine("Look at...");
            var dict = _currentScene.GetObjectDict();
            foreach (var obj in dict)
            {
                Console.WriteLine($"{obj.Key} : {obj.Value.Name}");
            }

            var decisionKey = Console.ReadKey(intercept: true);
            int.TryParse(decisionKey.KeyChar.ToString(), out var decisionIndex);
                                            
            while (decisionKey.Key != ConsoleKey.Q && !dict.ContainsKey(decisionIndex))
            {
                Console.WriteLine("Wrong input. Try again or use <q> to abort.");
                        
                decisionKey = Console.ReadKey(intercept: true);
                int.TryParse(decisionKey.KeyChar.ToString(), out decisionIndex);
            }

            if (decisionKey.Key == ConsoleKey.Q)
            {
                return;
            }
                    
            Console.WriteLine(_currentCharacter.Inspect(dict[decisionIndex]).Response);
        }

        private void PrintGameScreen()
        {
            Console.Clear();
                    
            ConsoleEx.WriteLine($"Press `?` for help.", ConsoleColor.DarkGray);
            ConsoleEx.WriteLine($"== Game ======================================", ConsoleColor.Green);
            Console.WriteLine();
            ConsoleEx.WriteLine($"You are in {_currentCharacter.Name}", ConsoleColor.White);
            ConsoleEx.WriteLine($"This is {_currentScene.Description}", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine($"______________________________________________", ConsoleColor.Green);
        }
        
        private void PrintStatScreen()
        {
            Console.Clear();
                    
            ConsoleEx.WriteLine($"Press `?` for help.", ConsoleColor.DarkGray);
            ConsoleEx.WriteLine($"== Stats =====================================", ConsoleColor.Green);                    

            Console.WriteLine();
            
            Console.WriteLine($"{_currentCharacter.Name} -- Level {_currentCharacter.Stats.Level}");
            Console.WriteLine($"{_currentCharacter.Stats.Race} {_currentCharacter.Stats.Gender}");
            
            Console.WriteLine();
            
            ConsoleEx.Write($"HP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Health}/{_currentCharacter.Stats.MaxHealth}", ConsoleColor.White);
            ConsoleEx.Write($"MP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Mana}/{_currentCharacter.Stats.MaxMana}", ConsoleColor.White);
            ConsoleEx.Write($"AP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.ActionPoints}/{_currentCharacter.Stats.MaxActionPoints}", ConsoleColor.White);

            Console.WriteLine();
            
            ConsoleEx.Write($"Strength     : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Strength }", ConsoleColor.White);
            ConsoleEx.Write($"Perception   : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Perception }", ConsoleColor.White);
            ConsoleEx.Write($"Stamina      : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Stamina }", ConsoleColor.White);
            ConsoleEx.Write($"Charisma     : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Charisma }", ConsoleColor.White);
            ConsoleEx.Write($"Intelligence : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Intelligence }", ConsoleColor.White);
            ConsoleEx.Write($"Agility      : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_currentCharacter.Stats.Agility }", ConsoleColor.White);
            
            Console.WriteLine();
            
            Console.WriteLine($"______________________________________________", ConsoleColor.Green);
        }

        private void HelpScreen()
        {
            
        }
        
        public void Run()
        {
            var orc = new Character
            {
                Name = "Ogrem",
                CurrentAction = "Nothing",
                Inventory = new Inventory
                {
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Name = "Ring of Eternity",
                            Description = "Gives lotsa health",
                            Effects = new StatSet
                            {
                                MaxHealth = 200
                            }
                        }
                    }
                },
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Orc,
                    Gender = Gender.Male,
                    Health = 400,
                    Mana = 50,
                    ActionPoints = 6,
                    MaxHealth = 400,
                    MaxMana = 50,
                    MaxActionPoints = 6,
                    Strength = 8,
                    Perception = 2,
                    Stamina = 6,
                    Charisma = 3,
                    Intelligence = 1,
                    Agility = 5,
                }
            };
            
            var player = new Character
            {
                Name = "Player",
                CurrentAction = "Nothing",
                Inventory = new Inventory
                {
                    Items = new List<Item>
                    {
                    }
                },
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Human,
                    Gender = Gender.Male,
                    Health = 150,
                    Mana = 50,
                    ActionPoints = 6,
                    MaxHealth = 400,
                    MaxMana = 50,
                    MaxActionPoints = 6,
                    Strength = 5,
                    Perception = 5,
                    Stamina = 5,
                    Charisma = 5,
                    Intelligence = 5,
                    Agility = 5,
                }
            };

            _currentCharacter = player;
            
            var scene = new Scene
            {
                Name = "The Room",
                Description = "Just a silly room. Four walls and a surprising absence of doors or windows.",
                Characters = new List<Character>()
                {
                    orc, player
                },
                Items = new List<Item>(),
                Props = new List<Prop>(),
                Decorations = new List<Decoration>()
            };

            _currentScene = scene;

            _currentState = GameState.Playing;
            
            ConsoleKeyInfo key;
            bool refreshFlag = true;
            do
            {
                key = new ConsoleKeyInfo();

                if (refreshFlag)
                {
                    switch (_currentState)
                    {
                        case GameState.Playing:
                            PrintGameScreen();
                            break;
                        case GameState.Inventory:
                            break;
                        case GameState.Stats:
                            PrintStatScreen();
                            break;
                    }

                    refreshFlag = false;
                }
                
                key = Console.ReadKey(intercept: true);

                
                switch (_currentState)
                {
                    case GameState.Playing:
                        if (key.Key == ConsoleKey.L)
                        {
                            ProcessLookAt();
                        }
                        if (key.Key == ConsoleKey.P)
                        {
                            _currentState = GameState.Stats;
                            refreshFlag = true;
                        }
                        break;
                    
                    case GameState.Inventory:
                        break;
                    
                    case GameState.Stats:
                        if (key.Key == ConsoleKey.Q)
                        {
                            _currentState = GameState.Playing;
                            refreshFlag = true;
                        }                        
                        break;
                }
                
            } 
            while (key.Key != ConsoleKey.X);
            
            Console.Clear();
            Console.WriteLine("You have left the game.");
        }        
    }
}