using System.Collections.Generic;

namespace ConsoleRpg_2
{
    class Scene
    {
        public List<Item> Items { get; set; }
        public List<Decoration> Decorations { get; set; }
        public List<Prop> Props { get; set; }
        public List<Character> Characters { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<int, GameObject> GetObjectDict()
        {
            var dict = new Dictionary<int, GameObject>();
            
            for (int i = 0; i < Items.Count; ++i)
            {
                dict[i + 1] = Items[i];
            }
            for (int i = 0; i < Characters.Count; ++i)
            {
                dict[i + 1 + Items.Count] = Characters[i];
            }
            for (int i = 0; i < Props.Count; ++i)
            {
                dict[i + 1 + Items.Count + Characters.Count] = Props[i];
            }
            for (int i = 0; i < Decorations.Count; ++i)
            {
                dict[i + 1 + Items.Count + Characters.Count + Props.Count] = Decorations[i];
            }

            return dict;
        }
    }
}