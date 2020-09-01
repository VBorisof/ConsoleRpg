using System.Collections.Generic;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.GameObjects.Character
{
    public class HotBar
    {
        private readonly GameLog _gameLog;
        
        public HotBarSlot Slot1 { get; set; }
        public HotBarSlot Slot2 { get; set; }
        public HotBarSlot Slot3 { get; set; }
        public HotBarSlot Slot4 { get; set; }
        public HotBarSlot Slot5 { get; set; }
        public HotBarSlot Slot6 { get; set; }
        public HotBarSlot Slot7 { get; set; }
        public HotBarSlot Slot8 { get; set; }
        public HotBarSlot Slot9 { get; set; }
        public HotBarSlot Slot10 { get; set; }

        public HotBarSlot SelectedSlot { get; set; }

        public HotBar(GameLog gameLog)
        {
            _gameLog = gameLog;
            
            Slot1 = new HotBarSlot(_gameLog);
            Slot2 = new HotBarSlot(_gameLog);
            Slot3 = new HotBarSlot(_gameLog);
            Slot4 = new HotBarSlot(_gameLog);
            Slot5 = new HotBarSlot(_gameLog);
            Slot6 = new HotBarSlot(_gameLog);
            Slot7 = new HotBarSlot(_gameLog);
            Slot8 = new HotBarSlot(_gameLog);
            Slot9 = new HotBarSlot(_gameLog);
            Slot10 = new HotBarSlot(_gameLog);
        }

        public List<HotBarSlot> GetSlots()
        {
            return new List<HotBarSlot>
            {
                Slot1,
                Slot2,
                Slot3,
                Slot4,
                Slot5,
                Slot6,
                Slot7,
                Slot8,
                Slot9,
                Slot10,
            };
        }
    }
}