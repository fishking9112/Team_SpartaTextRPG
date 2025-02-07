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
        ArcherSkill01, ArcherSkill02, ArcherSkill03,
        WizardSkill01, WizardSkill02, WizardSkill03
    }

    internal class SkillManager : Helper.Singleton<SkillManager>
    {
        // _skill의 반환값이 -1 이라면 스킬 사용 불가능
        private Dictionary<Skill_Key, (string _name, Func<Player, float> _skill)> functionMap = new()
        {
            { Skill_Key.WarriorSkill01, ("몸통 박치기 - 체력을 10 사용하여 기본 데미지의 2배의 피해를 준다" , player => {
                if (player.HP <= 10){ return -1f; }
                player.HP -= 10;
                return player.AttDamage * 2f;
            })},
            { Skill_Key.WarriorSkill02, ("크게 휘두르기 - 마나를 10 사용하여 기본 데미지의 3배의 피해를 준다" , player => {
                if (player.MP <= 10){ return -1f; }
                player.MP -= 10;
                return player.AttDamage * 3f;
            })},
            { Skill_Key.WarriorSkill03, ("울끈 불끈 - 마나를 50 회복한다. 상대에게 1피해를 준다" , player => {
                player.MP += 50;
                return 1f;
            })},


            { Skill_Key.ArcherSkill01, ("에로우 - 마나를 1 사용하여 기본 데미지의 2배의 피해를 준다" , player => {
                if (player.MP < 1){ return -1f; }
                player.MP -= 1;
                return player.AttDamage * 2f;
            })},
            { Skill_Key.ArcherSkill02, ("아이스 에로우 - 마나를 20 사용하여 기본 데미지의 2배의 피해를 준다" , player => {
                if (player.MP < 10){ return -1f; }
                player.MP -= 10;
                return 1f;
            })},
            { Skill_Key.ArcherSkill03, ("일렉트로닉 에로우 - 자신의 마나를 50 회복한다" , player => {

                return 0f;
            })},


            { Skill_Key.WizardSkill01, ("에너지 볼트 - 자신의 마나를 10 사용하여 기본 데미지의 2배의 피해를 준다" , player => {

                return 1f;
            })},
            { Skill_Key.WizardSkill03, ("크게 휘두르기 - 자신의 마나를 10 사용하여 기본 데미지의 2배의 피해를 준다" , player => {
                player.MP -= 10;
                return 1f;
            })},
            { Skill_Key.WizardSkill03, ("울끈 불끈 - 자신의 마나를 50 회복한다" , player => {

                return 0f;
            })},
        };

        public float DescriptionSkill(Player player, Skill_Key functionKey)
        {
            if (functionMap.TryGetValue(functionKey, out var func))
            {
                return func._skill(player);
            }
            return 0f;
        }

        public float ExecuteSkill(Player player, Skill_Key functionKey)
        {
            if (functionMap.TryGetValue(functionKey, out var func))
            {
                return func._skill(player);
            }
            return 0f;
        }
    }
}
