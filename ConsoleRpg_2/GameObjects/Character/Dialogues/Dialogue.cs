using System.Collections.Generic;

namespace ConsoleRpg_2.GameObjects.Character.Dialogues
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
    }
}