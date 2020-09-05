using System;

namespace ConsoleRpg_2.GameObjects.Characters.FightComponent
{
    public interface IFightComponent
    {
        void Render();
        FightComponentResult Process(ConsoleKey key);
    }
}