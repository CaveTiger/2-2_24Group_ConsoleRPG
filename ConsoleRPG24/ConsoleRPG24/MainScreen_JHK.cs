namespace ConsoleRPG24
{
    //게임 내 아이템들 Init
    internal partial class MainScreen
    {
        //게임 내 아이템 전부 호출(아이템 이름, 효과 설명 보기)
        public void DisplayItems()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                string itemNameStr = itemList[i].ItemName;
                string effectDescriptionStr = itemList[i].EffectDescription;
                Console.WriteLine(String.Format("{0,-30}", itemNameStr) + " | " +
                    String.Format("{0,-70}", effectDescriptionStr));
            }
        }

        public void InitItem()
        {
            /*=====공격력 관련, 비조건부=====*/

            itemList.Add(new Item("낡은 힘의 부적", "길가다가 우연히 주운 부적이다. 많이 낡아보인다.", "+ 공격력 3%", Rank.common, Division.atk, 3));
            itemList.Add(new Item("힘의 부적", "평상시에 가지고 다니는 평범한 부적이다.", "+ 공격력 5%", Rank.rare, Division.atk, 5));
            itemList.Add(new Item("고급 힘의 부적", "가지고만 있어도 힘이 나는것 같다.", "+ 공격력 7%", Rank.epic, Division.atk, 7));

            /*=====공격력 관련, 조건부=====*/

            //파티 용병 수만큼 증가
            itemList.Add(new Item("우정의 고리", "우리들의 우정은 언제나 끊어지지 않는 고리로 이어져 있다.",
                " 전투 진입시 + 공격력 (현재 파티에 있는 용병의 수) * 2% / 전투 종료시 소멸 ", Rank.epic, Division.atk, 0));

            //적의 수만큼 증가
            itemList.Add(new Item("신비한 깃털", "일반 새의 깃털보다 조금 더 좋아 보이는 신비한 깃털이다.",
                " + 공격력 (적의 현재 수) * 2% / 전투 종료시 소멸 ", Rank.rare, Division.atk, 0));

            //플레이어가 피격시 증가
            itemList.Add(new Item("한 줌의 분노", "한줌의 분노가 쌓이고 쌓여서 폭발할 때가 되었다.",
                " 플레이어가 피격시 + 공격력 2% (최대 + 8%) / 전투 종료시 소멸 ", Rank.epic, Division.atk, 0));

            //플레이어가 치명타를 터트릴시 증가
            itemList.Add(new Item("승승장구의 피리뿔", "돌격 앞으로! 우리 앞에는 승리만이 남아있을 것이다!",
                " 플레이어가 치명타를 터트릴시 공격력 4% (최대 +12%) / 전투 종료시 소멸 ", Rank.legend, Division.atk, 0));

            //전투 진입시 용병이 없을 경우 증가
            itemList.Add(new Item("어느 영웅의 대서사시", "어느 영웅도 처음에는 혼자였던 시절이 있었다.",
                " 전투 진입시 용병이 없을 경우 + 공격력 12% ", Rank.epic, Division.atk, 0));

            //용병이 전투중 죽었을 경우 증가
            itemList.Add(new Item("전투의 흔적", "내 동료가 죽어가는 슬픔은 이루 말로 표현할 수 없다.",
                " 용병이 전투중 죽었을 경우 + 공격력 8% / 전투 종료시 소멸 ", Rank.rare, Division.atk, 0));

            //전투 진입시 랜덤 증가
            itemList.Add(new Item("나무 주사위", "나무로 만들어져 클래식 해보인다. 행운이 나와 함께하길.",
                " 전투 진입시 + 공격력 1~6% / 전투 종료시 소멸", Rank.rare, Division.atk, 0));

            //전투 진입시 증가, 턴이 지날수록 감소
            itemList.Add(new Item("기선 제압용 글러브", "초반 적의 기선은 확실하게 제압했으나 계속 착용하면 오히려 불편해진다.",
                " 전투 진입시 + 공격력 6% / 턴이 지날수록 - 공격력 1% / 전투 종료시 소멸 ", Rank.rare, Division.atk, 0));

            //플레이어의 체력이 30% 이하가 되었을시 증가
            itemList.Add(new Item("반격의 서막(수정본)", "위기상황에서 힘을 더 끌어올릴 방법들이 적혀있다.",
                " 플레이어의 체력이 30% 이하가 되었을시 + 공격력 12% / 전투 끝나면 소멸", Rank.epic, Division.atk, 0));




            /*=====방어력 관련, 비조건부=====*/

            itemList.Add(new Item("빛 바랜 옥조", "이미 빛바랜 옥조. 검게 물들어 있다.", "+ 방어력 3%", Rank.common, Division.def, 3));
            itemList.Add(new Item("녹색 옥조", "아직까지 빛을 유지중인 옥조. 맨들맨들한 느낌이 살아있다.", "+ 방어력 5%", Rank.rare, Division.def, 5));
            itemList.Add(new Item("투명한 옥조", "투명한 유리구슬처럼 보이지만 그렇게 쉽게 깨지진 않을 것이다.", "+ 방어력 7%", Rank.epic, Division.def, 7));

            /*=====방어력 관련, 조건부=====*/

            //파티 용병 수만큼 증가
            itemList.Add(new Item("신뢰의 고리", "우리들의 신뢰는 언제나 끊어지지 않는 고리로 이어져 있다..",
                " 전투 진입시 + 방어력 (현재 파티에 있는 용병의 수) * 2% / 전투 종료시 소멸 ", Rank.epic, Division.def, 0));

            //적의 수만큼 증가
            itemList.Add(new Item("영롱한 깃털", "일반 새의 깃털보다 조금 더 반짝이는 영롱한 깃털이다.",
                " + 방어력 (적의 현재 수) * 2% / 전투 종료시 소멸 ", Rank.rare, Division.def, 0));

            //플레이어가 피격시 증가
            itemList.Add(new Item("한 줌의 인내", "한줌의 인내가 쌓여 본인을 더 단단하게 만들어준다.",
                " 플레이어가 피격시 + 방어력 2% (최대 + 8%) / 전투 종료시 소멸 ", Rank.epic, Division.def, 0));

            //플레이어가 치명타를 터트릴시 증가
            itemList.Add(new Item("승승장구의 하프", "쓰러지지 마라! 우리 뒤에는 지켜야할 존재들이 있다!",
                " 플레이어가 치명타를 터트릴시 방어력 4% (최대 +12%) / 전투 종료시 소멸 ", Rank.legend, Division.def, 0));

            //전투 진입시 용병이 없을 경우 증가
            itemList.Add(new Item("어느 거인의 첫 발걸음", "어느 거인의 여행기가 세상을 놀라게 하였다.",
                " 전투 진입시 용병이 없을 경우 + 방어력 12% ", Rank.epic, Division.def, 0));

            //용병이 전투중 죽었을 경우 증가
            itemList.Add(new Item("전투의 슬픔", "전투의 부산물로는 말로 형용할 수 없는 슬픔만이 남아있다.",
                " 용병이 전투중 죽었을 경우 + 방어력 8% / 전투 종료시 소멸 ", Rank.rare, Division.def, 0));

            //전투 진입시 랜덤 증가
            itemList.Add(new Item("강철 주사위", "강철로 만들어져 단단해보인다. 행운이 나와 함께하길.",
                " 전투 진입시 + 방어력 1~6% / 전투 종료시 소멸", Rank.rare, Division.def, 0));

            //전투 진입시 증가, 턴이 지날수록 감소
            itemList.Add(new Item("기선 제압용 해머", "초반 적의 기선은 확실하게 제압했으나 계속 착용하면 오히려 무거워진다.",
                " 전투 진입시 + 방어력 6% / 턴이 지날수록 - 공격력 1% / 전투 종료시 소멸 ", Rank.rare, Division.def, 0));

            //플레이어의 체력이 30% 이하가 되었을시 증가
            itemList.Add(new Item("반격의 서막(단행본)", "위기상황에서 전의을 더 끌어올릴 방법들이 적혀있다.",
                " 플레이어의 체력이 30% 이하가 되었을시 + 방어력 12% / 전투 끝나면 소멸", Rank.epic, Division.def, 0));




            /*=====최대체력 관련, 비조건부=====*/

            itemList.Add(new Item("부서진 토템 파편", "토템이 부서지면서 생긴 파편이다.", "+ 최대 체력 2%", Rank.common, Division.hp, 2));
            itemList.Add(new Item("토템 조각", "부서진 토템 파편들을 일부 모아서 완성해두었다.", "+ 최대 체력 4%", Rank.rare, Division.hp, 4));
            itemList.Add(new Item("온전한 토템", "온전하게 그 위상을 자랑하는 신비한 토템이다.", "+ 최대 체력 8%", Rank.epic, Division.hp, 8));

            /*=====최대체력 관련, 조건부=====*/

            //다음 구역 진입시마다 중가
            itemList.Add(new Item("포도주가 담긴 성배", "전투를 끝내고 마시는 포도주의 맛은 말로 형용할 수 없다." +
                " 빠르게 다음 전투를 마치고 한잔 더 마시고 싶다.", " 다음 구역 진입시마다 + 최대 체력 2%", Rank.legend, Division.hp, 0));

            //플레이어가 피격시 증가
            itemList.Add(new Item("한 줌의 복수심", "한줌의 복수심이 쌓여 본인을 더 튼튼하게 만들어준다.",
                " 플레이어가 피격시 + 최대체력 2% (최대 + 8%) ", Rank.epic, Division.hp, 0));

            //전투 진입시 용병이 없을 경우 증가
            itemList.Add(new Item("어느 여행자의 노하우", "여행을 하면서 도움이 되만한 지식들이 적혀있다.",
                "전투 진입시 용병이 없을 경우 + 최대체력 6%", Rank.rare, Division.hp, 0));




            /*=====치명타 관련, 비조건부=====*/

            //치확 증가
            itemList.Add(new Item("초침이 멈춘 회중시계", "토템이 부서지면서 생긴 파편이다.", "+ 치확 3%", Rank.common, Division.cHit, 3));
            itemList.Add(new Item("평범한 회중시계", "시간을 잴 때 용이한 회중시계이다. 정확한 타이밍을 제공해준다.", "+ 치확 3%", Rank.rare, Division.cHit, 5));
            itemList.Add(new Item("금빛 회중시계", "한정판으로 나온 금빛 회중시계이다. 혹여나 금칠이 벗겨질까 사용하기 두렵다.", "+ 치확 3%", Rank.epic, Division.cHit, 7));

            //치피 증가
            itemList.Add(new Item("망가진 팬던트", "망가져 버려 제 기능을 못하는 팬던트다.", "+ 치피 6%", Rank.common, Division.cDmg, 6));
            itemList.Add(new Item("복원된 팬던트", "일부만 복원된 팬던트다. 하지만 아직 고칠 데가 많다.", "+ 치피 10%", Rank.rare, Division.cDmg, 10));
            itemList.Add(new Item("온전한 팬던트", "온전히 제 기능을 하는 팬던트다.", "+ 치피 14%", Rank.epic, Division.cDmg, 14));


            /*=====치명타 관련, 조건부=====*/

            //치명타를 터트리지 못했을 경우 치확 증가
            itemList.Add(new Item("멈추지 않는 회중시계", "무한한 시간을 순환하는 듯한 회중시계이다. 초침이 도는 것을 보면 나도 모르게 빨려들어갈 것만 같다.",
                " 플레이어가 치명타를 터트리지 못할 시 + 치확 4% (최대 +16%) / 치명타다 터질 시 소멸", Rank.legend, Division.cHit, 0));




            /*=====골드 관련, 비조건부=====*/


            /*=====골드 관련, 조건부=====*/

            //현재 보유 골드 X당 공격력 Y% 증가
            itemList.Add(new Item("핏빛 금화주머니", "이상하게 피로 물들어있는 금화주머니다. 실제로 사용도 가능하다.",
                " 현재 보유 골드 X당 공격력 Y% 증가 / 전투마다 갱신", Rank.epic, Division.gold, 0));

            //현재 보유 골드 X당 방어력 Y% 증가
            itemList.Add(new Item("은빛 금화주머니", "금화주머니지만 색깔은 은빛이다. 안의 금화보다 은빛 주머니가 더 눈길을 사로잡는다.",
                " 현재 보유 골드 X당 방어력 Y% 증가 / 전투마다 갱신", Rank.epic, Division.gold, 0));

            //현재 보유 골드 X당 치피 Y% 증가
            itemList.Add(new Item("산호빛 금화주머니", "바다에서만 살아간다는 산호의 빛깔을 보게 해주는 금화주머니다.",
                " 현재 보유 골드 X당 치피 Y% 증가 / 전투마다 갱신", Rank.epic, Division.gold, 0));

            //현재 보유 골드 X당 속도 Y 증가
            itemList.Add(new Item("영롱한 금화주머니", "보기만 해도 영롱함을 느끼게 해준다는 금화주머니다.",
                " 현재 보유 골드 X당 속도 Y 증가 / 전투마다 갱신", Rank.epic, Division.gold, 0));

            //다음 구역 진입시 보유 골드 증가
            itemList.Add(new Item("부자의 증표", "“내가 달라면 줄것이지 뭐가 그렇게 말이 많아?!”",
                " 다음 구역 진입시 보유 골드 5% 증가 / 전투마다 갱신", Rank.epic, Division.gold, 0));




            /*=====속도 관련, 비조건부=====*/

            itemList.Add(new Item("어둠의 잔상", "어둠을 쫓아가는것이 때로는 도움이 될 때도 있다.", "+ 속도 5", Rank.common, Division.spd, 5));
            itemList.Add(new Item("그림자의 잔상", "상대의 그림자가 보였다는 것은 상대도 내 근처에 있다는 것이다.", "+ 속도 10", Rank.rare, Division.spd, 10));
            itemList.Add(new Item("빛의 잔상", "적이 쫓아오지 못할 정도로 뛰어갔더니 그 곳에는 빛만이 남아 있었다.", "+ 속도 15", Rank.epic, Division.spd, 15));

            /*=====속도 관련, 조건부=====*/




            /*=====상점 관련, 비조건부=====*/


            /*=====상점 관련, 조건부=====*/

            //상점 내 아이템 가격 감소
            itemList.Add(new Item("할인 쿠폰", "현재 상점은 특별 세일중! 매진 임박이니 얼른 사두시길!",
                "상점 내 아이템 가격 10% 감소", Rank.common, Division.shop, 0));

            //상점 내 아이템 수량 1 증가
            itemList.Add(new Item("택배 상자", "소문에 의하면 상점에서 아이템 입고량이 더 많아진다고 한다.",
                "상점 내 아이템 수량 1 증가", Rank.rare, Division.shop, 0));

            //상점 내 아이템 수량 2 감소, 대신 상점 중 랜덤으로 아이템 하나의 가격을 0으로 변경
            itemList.Add(new Item("광대의 상자", "내가 원하는 것을 얻으려면 그만한 대가도 지불해야 한다.",
                "상점 내 아이템 수량 2 감소, 대신 상점 중 랜덤으로 아이템 하나의 가격을 0으로 변경", Rank.epic, Division.shop, 0));

            //상점 내에서 아무것도 사지 않고 나올 경우, N만큼의 골드 획득
            itemList.Add(new Item("무료 서비스 티켓", "내가 사고 싶은 것은 별로 없지만 다음에 오면 생각이 바뀔지도 모른다.",
                "상점 내에서 아무것도 사지 않고 나올 경우, N만큼의 골드 획득", Rank.rare, Division.shop, 0));

            //상점에 진입시 N만큼의 골드 획득
            itemList.Add(new Item("추가 서비스 티켓", "상점에 들어가는 것만으로도 행복하게 만들어준다.",
                "상점에 진입시 N만큼의 골드 획득", Rank.rare, Division.shop, 0));


        }


    }
}
