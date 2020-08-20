using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleRpg_2.Ui
{
    public class UiSelectList
    {
        private int _index;
        private readonly IOrderedEnumerable<IGrouping<int, UiElement>> _rows;

        public UiSelectList(IEnumerable<UiElement> elements)
        {
            _rows = elements.GroupBy(e => e.Row)
                .OrderBy(e => e.Key);
            
            SetCurrentItemFocus(true);
        }


        public void NextItem()
        {
            SetCurrentItemFocus(false);
            
            ++_index;
            if (_index >= _rows.Count())
            {
                _index = 0;
            }
            
            SetCurrentItemFocus(true);
        }
        
        public void PrevItem()
        {
            SetCurrentItemFocus(false);
            
            --_index;
            if (_index < 0)
            {
                _index = _rows.Count() - 1;
            }
            
            SetCurrentItemFocus(true);
        }

        public void PressCurrentItem()
        {
            foreach (var uiElement in _rows.ElementAt(_index).Select(x => x))
            {
                uiElement.Press();
            }
        }

        public void Render()
        {
            foreach (var row in _rows)
            {
                foreach (var column in row.OrderBy(e => e.Column))
                {
                    column.Render();
                }
                Console.WriteLine();
            }
        }


        private void SetCurrentItemFocus(bool value)
        {
            var columns = _rows.ElementAt(_index).Select(x => x);
            foreach (var column in columns)
            {
                column.IsFocused = value;
            }
        }

        public UiValue GetCurrentUiValueOrDefault()
        {
            return _rows.ElementAt(_index)
                .Select(x => x)
                .FirstOrDefault(e => e is UiValue) as UiValue;
        }
    }
}