using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class TownScene : Helper.Singleton<TownScene>
    {
        public void Game_Main()
        {
            TitleManager.instance.WriteTitle("강렬한 인상 is 게임");
            // ScreenManager.instance.AsyncVideo("resources/village.gif", _isContinue: true, _isReversal: false);
            ScreenManager.instance.AsyncImage("resources/village.png");

            InputKeyManager.instance.ArtMenu(
                ($"상태보기", "플레이어의 상태를 확인합니다.", () => Select_Menu(1)), 
                ($"인벤토리", "아이템을 장착하거나 뺄 수 있습니다.", () => Select_Menu(2)),
                ($"상점", "무기와 방어구를 사거나 팔 수 있습니다.", () => Select_Menu(3)),
                ($"여관", "쉴 수 있습니다.", () => Select_Menu(4)), 
                ($"던전입장", "던전에 입장합니다.", () => Select_Menu(5)),
                ($"퀘스트", "퀘스트 수락, 취소, 완료를 할 수 있습니다.", () => Select_Menu(6)),
                ($"저장", "현재 데이터를 저장합니다.", () => Select_Menu(7)),
                ($"종료", "게임을 종료합니다.", () => Select_Menu(8)));
        }
        public void Select_Menu(int _index)
        {
            switch(_index)
            {

                case 1: // 상태보기
                    InputKeyManager.instance.GoMenu(StatusScene.instance.Player_Stats);
                    break;
                case 2: // 인벤토리
                    InputKeyManager.instance.GoMenu(InventoryScene.instance.ShowInventory);
                    break;
                case 3: // 상점
                    InputKeyManager.instance.GoMenu(ShopScene.instance.ShowMenu);
                    break;
                case 4: // 여관
                    InputKeyManager.instance.GoMenu(RestScene.instance.Show_Rest);
                    break;
                case 5: // 던전 입장
                    InputKeyManager.instance.GoMenu(DungeonScene.instance.Dungeon_Title);
                    break;
                case 6: // 퀘스트
                    InputKeyManager.instance.GoMenu(QuestBoardScene.instance.Show_Quest_Board);
                    break;
                case 7: // 저장
                    InputKeyManager.instance.GoMenu(GameSave);
                    break;
                case 8: // 종료
                    GameManager.instance.isPlaying = false;
                    break;

            }
        }

        public void GameSave(){
            TitleManager.instance.WriteTitle("게임 저장");
            ScreenManager.instance.AsyncImage("./resources/save.png",_startX:40, _startY:2, imageSizeX:20, imageSizeY:20);

            SaveLoadManager.instance.SaveToJson(GameManager.instance.player);

            InputKeyManager.instance.ArtMenu(($"저장완료!", "저장되었습니다.", () => Game_Main()));
        }
    }
}
