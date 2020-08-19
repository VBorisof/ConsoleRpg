namespace ConsoleRpg_2
{
    public abstract class UiElement
    {
        public bool IsFocused { get; set; }
        public string Text { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public abstract void Render();
    }
}