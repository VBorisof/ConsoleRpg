using System.Collections.Generic;

namespace ConsoleRpg_2.GameObjects.Characters.Dialogues
{
    public class Choice
    {
        public string Text { get; set; } 
        public string ConditionCode { get; set; } 
        public int? NextDialogueId { get; set; } 
        public Dialogue NextDialogue { get; set; } 
        public bool? IsLeave { get; set; } 
        public bool? IsFight { get; set; } 
        public IEnumerable<Command> Commands { get; set; } 
    }
}