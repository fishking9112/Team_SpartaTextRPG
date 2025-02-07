using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum Skill_Key
    {
        WarriorSkill01, WarriorSkill02, WarriorSkill03,
        ThiefSkill01, ThiefSkill02, ThiefSkill03,
        ArcherSkill01, ArcherSkill02, ArcherSkill03,
        WizardSkill01, WizardSkill02, WizardSkill03
    }

    internal class SkillManager : Helper.Singleton<SkillManager>
    {
        // _description은 스킬의 설명에 대한 string 반환 해줌
        // _damage의 반환값이 -1f 라면 스킬 사용 불가능, 0f 라면 스킬 사용은 가능하나 대상 지정 없이 사용하시겠습니까? 하는게 좋을 듯
        // _cost시 스킬에 대한 비용 지불
        private Dictionary<Skill_Key, (string _description, Func<Player, float> _damage, Action<Player> _cost)> Skill_Map = new()
        {
            { Skill_Key.WarriorSkill01, ("몸통 박치기 - 체력을 10 사용하여 자신의 방어력의 5배의 피해를 준다" , player => { // 스킬 데미지
                if (player.HP <= 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.Defense * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.HP -= 10;
            })},
            { Skill_Key.WarriorSkill02, ("크게 휘두르기 - 마나를 10 사용하여 자신의 데미지의 2배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 10;
            })},
            { Skill_Key.WarriorSkill03, ("울끈 불끈 - 체력(+10), 마나(+50)를 회복한다." , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.HP += 10;
                player.MP += 50;
            })},

            { Skill_Key.ThiefSkill01, ("발목 긋기 - 마나를 10 사용하여 자신의 데미지의 1.5배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 10;
            })},
            { Skill_Key.ThiefSkill02, ("소매치기 - 데미지를 1 주고 골드를 1000G 얻는다" , player => { // 스킬 데미지
                return 1f;
            }, player => { // 스킬 코스트 비용 지불
                player.Gold += 1000;
            })},
            { Skill_Key.ThiefSkill03, ("돈 뿌리기 - 돈을 100G 소비 후 자신의 데미지의 3배의 피해를 준다" , player => { // 스킬 데미지
                if (player.Gold < 100){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 3f;
            }, player => { // 스킬 코스트 비용 지불
                player.Gold -= 100;
            })},


            { Skill_Key.ArcherSkill01, ("에로우 - 마나를 1 사용하여 자신의 데미지의 2배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 1){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 1;
            })},
            { Skill_Key.ArcherSkill02, ("파이어 에로우 - 마나를 10 사용하여 자신의 데미지의 3배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 3f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 10;
            })},
            { Skill_Key.ArcherSkill03, ("일렉트로닉 에로우 - 마나를 20 사용하여 자신의 데미지의 5배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 20){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 5f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 20;
            })},


            { Skill_Key.WizardSkill01, ("에너지 볼트 - 마나를 5 사용하여 자신의 데미지의 2배의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 5){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 5;
            })},
            { Skill_Key.WizardSkill03, ("매직 클로 - 마나를 20 사용하여 자신의 최대 마나의 피해를 준다" , player => { // 스킬 데미지
                if (player.MP < 20){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.MaxMP;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 20;
            })},
            { Skill_Key.WizardSkill03, ("마나 숨결 - 자신의 마나를 50 회복한다" , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP += 50;
            })},
        };

        // _description의 반환값이 "" 라면 스킬이 없는 것 (enum으로 체크하기 때문에 "" 반환되는 일은 거의 없을 것)
        public string GetSkillDescription(Player player, Skill_Key skillKey)
        {
            if (Skill_Map.TryGetValue(skillKey, out var skill))
            {
                return skill._description;
            }
            return "";
        }

        // _skill의 반환값이 -1f 라면 스킬 사용 불가능, 스킬이 없다면 -2f (enum으로 체크하기 때문에 -2f 반환되는 일은 거의 없을 것)
        public float GetSkillDamage(Player player, Skill_Key skillKey)
        {
            if (Skill_Map.TryGetValue(skillKey, out var skill))
            {
                return skill._damage(player);
            }
            return -2f;
        }

        // _cost 실행
        public void ExecuteSkillCost(Player player, Skill_Key skillKey)
        {
            if (Skill_Map.TryGetValue(skillKey, out var skill))
            {
                skill._cost(player);
            }
        }
    }
}
