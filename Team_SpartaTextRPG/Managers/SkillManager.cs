using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum Skill_Key
    {
        ProgrammerSkill01, ProgrammerSkill02, ProgrammerSkill03, ProgrammerSkill04, ProgrammerSkill05,
        PlannerSkill01, PlannerSkill02, PlannerSkill03, PlannerSkill04, PlannerSkill05
    }

    internal class SkillManager : Helper.Singleton<SkillManager>
    {
        // _description은 스킬의 설명에 대한 string 반환 해줌
        // _damage의 반환값이 -1f 라면 스킬 사용 불가능, 0f 라면 스킬 사용은 가능하나 대상 지정 없이 사용하시겠습니까? 하는게 좋을 듯
        // _cost시 스킬에 대한 비용 지불
        private Dictionary<Skill_Key, (string _description, Func<Player, float> _damage, Action<Player> _cost)> Skill_Map = new()
        {
            { Skill_Key.ProgrammerSkill01, ("튜터님의 도움(공용) : 체력 30을 사용한다. 모르는 문제 발생! 튜터님에게 빠르게 질문하러 가자!" , player => { // 스킬 데미지
                if (player.HP <= 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.Defense * 5f;
            }, player => { // 스킬 코스트 비용 지불
                player.HP -= 30;
            })},
            { Skill_Key.ProgrammerSkill02, ("C# 체크리스트(공용) : 정신력 30을 사용해서 데미지를 2배로 준다. 오늘 진행하는 체크리스트는..." , player => { // 스킬 데미지
                if (player.MP < 10){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 30;
            })},
            { Skill_Key.ProgrammerSkill03, ("구글링(공용) - 정신력 20을 사용한다. 자료를 찾아볼까~" , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 20;
            })},
            { Skill_Key.ProgrammerSkill04, ("Chat GPT(고유)​ - 정신력 50을 회복한다. 역시 AI야 내가 놓친 부분을 찾아주네." , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP += 50;
            })},
            { Skill_Key.ProgrammerSkill05, ("인도 Youtube의 도움(고유)​ - 체력(+10), 마나(+50)를 회복한다." , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.HP += 10;
                player.MP += 50;
            })},

            { Skill_Key.PlannerSkill01, ("튜터님의 도움(공용) : 체력 30을 사용한다. 모르는 문제 발생! 튜터님에게 빠르게 질문하러 가자!" , player => { // 스킬 데미지
                if (player.MP < 5){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.AttDamage * 2f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 30;
            })},
            { Skill_Key.PlannerSkill02, ("C# 체크리스트(공용) : 정신력 30를 사용해서 데미지를 2배로 준다. 오늘 진행하는 체크리스트는..." , player => { // 스킬 데미지
                if (player.MP < 20){ return -1f; } // 조건에 따라 사용 불가능하면 -1f 반환
                return player.MaxMP;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 30;
            })},
            { Skill_Key.PlannerSkill03, ("구글링(공용) - 정신력 20을 사용한다. 자료를 찾아볼까~" , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 20;
            })},
            { Skill_Key.PlannerSkill04, ("상급 프로그래머의 도움(고유) - 체력 60을 소모하고 높은 데미지를 준다. 그러니깐 이게 왜 안되는 건데요." , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.MP -= 60;
            })},
            { Skill_Key.PlannerSkill05, ("엑셀 팡션(고유) - 체력과 정신력을 30 회복한다. 엑셀 함수 is Good~!" , player => { // 스킬 데미지
                return 0f;
            }, player => { // 스킬 코스트 비용 지불
                player.HP += 30;
                player.MP += 30;
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
