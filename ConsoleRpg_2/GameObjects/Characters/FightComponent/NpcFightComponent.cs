using System;
using System.Linq;
using System.Threading;
using ConsoleRpg_2.GameObjects.Characters.Skills;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.GameObjects.Characters.FightComponent
{
    public class NpcFightComponent : IFightComponent
    {
        private readonly GameLog _gameLog;
        private readonly Character _npc;

        public NpcFightComponent(GameLog gameLog, Character npc)
        {
            _gameLog = gameLog;
            _npc = npc;
        }


        public void Render() { }

        public FightComponentResult Process(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
            Thread.Sleep(1000);
            var target = _npc.CurrentScene.Characters.First(c => c != _npc);

            var attackSkill = _npc.Skills.First(s => s.SkillType == SkillType.Melee);

            if (_npc.Stats.ActionPoints < attackSkill.ActionPoints)
            {
                result.TurnEnd = true;
                return result;
            }
            
            var skillResult = _npc.ApplySkill(_npc.Skills.First(s => s.SkillType == SkillType.Melee), target);
            if (! skillResult.IsSuccess)
            {
                _gameLog.WriteLine(skillResult.Comment);    
            }
            else
            {
                if (skillResult.Damage >= 0)
                {
                    _gameLog.WriteLine(
                        $"{_npc.Name} deals {skillResult.Damage} damage to {target.Name}."
                    );
                }
                else
                {
                    _gameLog.WriteLine(
                        $"{_npc.Name} heals {target.Name} for {-skillResult.Damage} health points."
                    );
                }    
            }

            return result;
        }
    }
}