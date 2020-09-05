using System.Collections.Generic;

namespace ConsoleRpg_2.GameObjects.Characters.Dialogues
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
    }
}