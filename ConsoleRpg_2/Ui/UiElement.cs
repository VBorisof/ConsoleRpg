using System;

namespace ConsoleRpg_2.Ui
{
    public abstract class UiElement
    {
        public bool IsFocused { get; set; }
        public string Text { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public abstract void Render();

        public EventHandler OnPress = (_, __) => { };

        public void Press()
        {
            OnPress(this, EventArgs.Empty);
        }
    }
}