using System;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;
using ConsoleRpg_2.GameObjects.Character.Dialogues;

namespace ConsoleRpg_2.Ui
{
    public class TalkToScreen
    {
        public UiSelectList TalkToList { get; set; }
        public Dialogue CurrentDialogue { get; set; }

        public static TalkToScreen CreateOrDefault(Character currentCharacter)
        {
            var talkToScreen = new TalkToScreen();
            var labels = currentCharacter.CurrentScene.GetConversableCharacters()
                .Select((o, i) => 
                    new UiLabel
                    {
                        Text = o.Name,
                        Row = i,
                        OnPress = (_, __) =>
                        {
                            talkToScreen.CurrentDialogue = o.Dialogue;
                        }
                    }
                ).ToList();

            if (labels.Any())
            {
                talkToScreen.TalkToList = new UiSelectList(labels);
                return talkToScreen;
            }

            return null;
        }

        public void Render()
        {
            if (CurrentDialogue == null)
            {
                Console.WriteLine("Talk to... (Q) to abort");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"  {string.Join("\n  ", CurrentDialogue.Text.Split(Configuration.BufferLength - 2))}");
                Console.WriteLine();
            }
                    
            TalkToList.Render();
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key, Action endDialogueCallback)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case ConsoleKey.Q:
                    if (CurrentDialogue == null)
                    {
                        result.SwitchState = GameState.World;
                        result.RefreshFlag = true;
                    }
                
                    break;
                
                case ConsoleKey.UpArrow:
                    TalkToList.PrevItem();
                    result.RefreshFlag = true;
                    break;
                
                case ConsoleKey.DownArrow:
                    TalkToList.NextItem();
                    result.RefreshFlag = true;
                    break;
                
                case ConsoleKey.Enter:
                    TalkToList.PressCurrentItem();
                
                    if (CurrentDialogue != null)
                    {
                        TalkToList = new UiSelectList(
                            CurrentDialogue.Choices.Select((c, i) => new UiLabel
                            {
                                Row = i,
                                Text = $"  {c.Text}",
                                OnPress = (_, __) =>
                                {
                                    CurrentDialogue = c.NextDialogue;
                                    if (CurrentDialogue == null)
                                    {
                                        endDialogueCallback();
                                    }
                                }
                            }).ToList()
                        );
                    }
                    result.RefreshFlag = true;
                    break;
            }
            
            return result;
        }
    }
}