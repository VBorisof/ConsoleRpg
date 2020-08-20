using System;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleRpg_2
{    
    public class XmlLoader
    {
        private const string ScenesPath = "res/scenes";

        public void LoadCharacter(int id)
        {
            Console.WriteLine($"Load character {id}");
        }
        
        public void LoadScene(int id)
        {
            var root = XElement.Load($"{ScenesPath}/{id}.res.xml");

            var characters = root.Elements().SingleOrDefault(e => e.Name == "characters");

            if (characters != null)
            {
                var ids = characters.Elements()
                    .Where(e => e.Name == "character")
                    .Select(e => int.Parse(e.Attribute("id").Value));

                foreach (var i in ids)
                {
                    LoadCharacter(i);
                }
            }
        }
    }

    
    
    class Program
    {
        static void Main(string[] args)
        {
            new XmlLoader().LoadScene(1);
        }
    }
}