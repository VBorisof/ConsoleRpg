using System;
using System.Linq;
using System.Xml.Linq;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects;
using ConsoleRpg_2.GameObjects.Characters;
using ConsoleRpg_2.GameObjects.Characters.Dialogues;
using ConsoleRpg_2.GameObjects.Characters.FightComponent;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2
{
    public class XmlLoader
    {
        private readonly GameLog _gameLog;
        private const string ScenesPath = "res/scenes";
        private const string CharactersPath = "res/characters";

        private XElement ItemsRoot = XElement.Load($"res/items.res.xml");

        public XmlLoader(GameLog gameLog)
        {
            _gameLog = gameLog;
        }
        
        
        public Item LoadItem(int id)
        {
            var itemElement = ItemsRoot.Elements("item")
                .Single(e => e.Attribute("id").Value == $"{id}");

            var effectsElement = itemElement.Element("effects");
            
            var maxHealthParsed = int.TryParse(effectsElement.Element("MaxHealth")?.Value, out var maxHealth);
            var maxManaParsed = int.TryParse(effectsElement.Element("MaxMana")?.Value, out var maxMana);
            var maxActionPointsParsed = int.TryParse(effectsElement.Element("MaxActionPoints")?.Value, out var maxActionPoints);
            var strengthParsed = int.TryParse(effectsElement.Element("Strength")?.Value, out var strength);
            var perceptionParsed = int.TryParse(effectsElement.Element("Perception")?.Value, out var perception);
            var staminaParsed = int.TryParse(effectsElement.Element("Stamina")?.Value, out var stamina);
            var charismaParsed = int.TryParse(effectsElement.Element("Charisma")?.Value, out var charisma);
            var intelligenceParsed = int.TryParse(effectsElement.Element("Intelligence")?.Value, out var intelligence);
            var agilityParsed = int.TryParse(effectsElement.Element("Agility")?.Value, out var agility);
            
            var item = new Item
            {
                Name = itemElement.Attribute("name").Value,
                Description = itemElement.Attribute("description").Value,
                Effects = new StatSet
                {
                    MaxHealth = maxHealthParsed ? maxHealth : 0, 
                    MaxMana = maxManaParsed ? maxMana : 0, 
                    MaxActionPoints = maxActionPointsParsed ? maxActionPoints : 0, 
                    Strength = strengthParsed ? strength : 0, 
                    Perception = perceptionParsed ? perception : 0, 
                    Stamina = staminaParsed ? stamina : 0, 
                    Charisma = charismaParsed ? charisma : 0, 
                    Intelligence = intelligenceParsed ? intelligence : 0, 
                    Agility = agilityParsed ? agility : 0, 
                }
            };

            return item;
        }

        public Character LoadCharacter(int id)
        {
            var root = XElement.Load($"{CharactersPath}/{id}.res.xml");

            Character character;

            // General info
            character = new Character(_gameLog)
            {
                Name = root.Element("name").Value,
                CurrentAction = root.Element("startAction").Value,
                DefaultAttitude = Enum.Parse<Attitude>(root.Element("defaultAttitude").Value)
            };

            
            // Stats
            var stats = root.Element("stats")
                .Elements();

            var statSet = new StatSet()
            {
                Level = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Level")).Value),
                MaxHealth = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("MaxHealth")).Value),
                MaxMana = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("MaxMana")).Value),
                MaxActionPoints =
                    int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("MaxActionPoints")).Value),
                Strength = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Strength")).Value),
                Perception = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Perception")).Value),
                Stamina = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Stamina")).Value),
                Charisma = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Charisma")).Value),
                Intelligence = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Intelligence")).Value),
                Agility = int.Parse(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Agility")).Value),
                Race = Enum.Parse<Race>(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Race")).Value),
                Gender = Enum.Parse<Gender>(stats.Single(e => e.Name.ToString().EqualsIgnoreCase("Gender")).Value),
            };

            character.Stats = statSet;
            character.Stats.Health = character.Stats.MaxHealth;
            character.Stats.Mana = character.Stats.MaxMana;
            character.Stats.ActionPoints = character.Stats.MaxActionPoints;
            
            
            // Inventory 
            var inventory = root.Element("inventory");

            var items = inventory.Elements("item")
                .Select(e => LoadItem(int.Parse(e.Attribute("id").Value)))
                .ToList();

            character.Inventory = new Inventory()
            {
                Items = items
            };
            
            
            // Dialogues
            var dialoguesElement = root.Element("dialogues");

            if (dialoguesElement != null)
            {
                var dialogues = dialoguesElement.Elements("dialogue")
                    .Select(d => new Dialogue
                    {
                        Id = int.Parse(d.Attribute("id").Value), Text = d.Element("text").Value,
                        Choices = d.Element("choices")
                            .Elements("choice")
                            .Select(e => new Choice
                            {
                                Text = e.Attribute("text").Value,
                                ConditionCode = e.Attribute("condition")?.Value,
                                NextDialogueId = e.Attribute("nextDialogue") != null 
                                    ? (int?) int.Parse(e.Attribute("nextDialogue")?.Value)
                                    : null,
                                IsLeave = e.Attribute("leave") != null
                                    ? (bool?) bool.Parse(e.Attribute("leave")?.Value)
                                    : null,
                                IsFight = e.Attribute("fight") != null
                                    ? (bool?) bool.Parse(e.Attribute("fight")?.Value)
                                    : null,
                                Commands = e.Elements("command").Select(c => new Command
                                {
                                    Code = c.Value
                                })
                            })
                            .ToList()
                    })
                    .ToList();

                var branches = dialogues.SelectMany(d => d.Choices)
                    .Where(c => c.NextDialogueId != null)
                    .ToList();

                foreach (var branch in branches)
                {
                    branch.NextDialogue = dialogues.Single(d => d.Id == branch.NextDialogueId);
                }

                character.Dialogue = dialogues.OrderBy(d => d.Id).First();
            }
            
            character.FightComponent = new NpcFightComponent(_gameLog, character);
            
            return character;
        }
        
        public Scene LoadScene(int id)
        {
            var root = XElement.Load($"{ScenesPath}/{id}.res.xml");

            var scene = new Scene
            {
                Name = root.Element("name").Value,
                Description = root.Element("description").Value,
            };
            
            var charactersElement = root.Element("characters");
            if (charactersElement != null)
            {
                var characters = charactersElement.Elements("character")
                    .Select(e => LoadCharacter(int.Parse(e.Attribute("id").Value)))
                    .ToList();
                
                scene.Characters = characters;
                foreach (var character in scene.Characters)
                {
                    character.CurrentScene = scene;
                }
            }

            return scene;
        }
    }
}