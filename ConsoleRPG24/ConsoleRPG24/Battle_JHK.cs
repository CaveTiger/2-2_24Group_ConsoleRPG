namespace ConsoleRPG24
{
    //전투 중 아이템 적용 관련 메서드들의 집합
    internal partial class BattleSystem
    {
        
        //배틀 시작 전 효과를 적용시키는 함수
        public void ApplyEffectBeforeBattle(List<Monster> monsterTeam)
        {
            //공격력 관련 아이템
            //3. 신비한 깃털 : 공격력 (적의 현재 수) * 2%
            if (itemList[3].IsOwned && itemList[3].IsEquipped)
            {
                for(int i = 0; i < monsterTeam.Count; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[3], 2);
                }
            }

            //6. 어느 영웅의 대서사시 : 전투 진입시 적의 수가 4명일 경우 + 공격력 12%
            if (itemList[6].IsOwned && itemList[6].IsEquipped)
            {
                if(monsterTeam.Count == 4)
                {
                    player.ApplyItemEffectByTerm(itemList[6], 12);
                }
            }

            //8. 나무 주사위 : 전투 진입시 + 공격력 1~6%
            if (itemList[8].IsOwned && itemList[8].IsEquipped)
            {
                Random random = new Random();
                int randomAtk = random.Next(1,7);
                player.ApplyItemEffectByTerm(itemList[8], randomAtk);
                
            }


            //방어력 관련 아이템
            //13. 영롱한 깃털 : 방어력 (적의 현재 수) * 2%
            if (itemList[13].IsOwned && itemList[13].IsEquipped)
            {
                for (int i = 0; i < monsterTeam.Count; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[13], 2);
                }

            }

            //16. 어느 거인의 첫 발걸음 : 전투 진입시 적의 수가 4명일 경우 + 방어력 12% 
            if (itemList[16].IsOwned && itemList[16].IsEquipped)
            {
                if (monsterTeam.Count == 4)
                {
                    player.ApplyItemEffectByTerm(itemList[16], 12);
                }
            }

            //18. 강철 주사위 : 전투 진입시 + 방어력 1~6%
            if (itemList[18].IsOwned && itemList[18].IsEquipped)
            {
                Random random = new Random();
                int randomDef = random.Next(1, 7);
                player.ApplyItemEffectByTerm(itemList[18], randomDef);

            }


            //최대체력 관련 아이템
            //25. 어느 여행자의 노하우 : 전투 진입시 적의 수가 4명일 경우 + 최대체력 6%
            if (itemList[25].IsOwned && itemList[25].IsEquipped)
            {
                if (monsterTeam.Count == 4)
                {
                    player.ApplyItemEffectByTerm(itemList[25], 6);
                }
            }


            //골드 관련 아이템
            //33. 핏빛 금화주머니 : 현재 보유 골드 X당 공격력 Y% 증가
            if (itemList[33].IsOwned && itemList[33].IsEquipped)
            {
                player.ApplyItemEffectByTerm(itemList[33], (player.Gold / 100) * 5);
            }

            //34. 은빛 금화주머니 : 현재 보유 골드 X당 방어력 Y% 증가
            if (itemList[34].IsOwned && itemList[34].IsEquipped)
            {
                player.ApplyItemEffectByTerm(itemList[34], (player.Gold / 100) * 5);
            }

            //35. 산호빛 금화주머니 : 현재 보유 골드 X당 치피 Y% 증가
            if (itemList[35].IsOwned && itemList[35].IsEquipped)
            {
                player.ApplyItemEffectByTerm(itemList[35], (player.Gold / 100) * 8);
            }

            //36. 영롱한 금화주머니 : 현재 보유 골드 X당 속도 Y 증가
            if (itemList[36].IsOwned && itemList[36].IsEquipped)
            {
                player.ApplyItemEffectByTerm(itemList[36], (player.Gold / 100) * 1);
            }
        }

        //몇턴마다 스택 소멸 같은 기능이 있으면 좋겠지만 시간이 없는 관계상 계속 증가하는 매커니즘으로 결정

        //피격 당한 스택
        public int playerDamagedStack = 0;

        //플레이어가 크리티컬을 성공시킨 스택
        public int playerCHitStack = 0;

        //플레이어가 크리티컬을 실패시킨 스택
        public int playerCHitFailStack = 0;

        //전투 중 적이 죽었는지 여부
        public bool isMonsterDead = false;

        //전투 중 플레이어의 체력이 30% 이하인지의 여부
        public bool isHealthBelowThrity = false;


        //배틀 진행 중 매 턴마다 효과를 적용시키는 함수
        public void ApplyEffectEveryTurn()
        {
            //공격력 관련 아이템
            //4. 한 줌의 분노 : 플레이어가 피격시 + 공격력 2% (최대 + 8%)
            if (itemList[4].IsOwned && itemList[4].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[4], bonusAmount * 2);
                }
            }

            //5. 승승장구의 피리뿔 : 플레이어가 치명타를 터트릴시 공격력 4% (최대 +12%)
            if (itemList[5].IsOwned && itemList[5].IsEquipped)
            {
                int bonusAmount = playerCHitStack > 3 ? 3 : playerCHitStack;

                for (int i = 0; i < playerCHitStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[5], bonusAmount * 4);
                }
            }

            //7. 전투의 흔적 : 적이 전투중 죽었을 경우 + 공격력 8%
            if (itemList[7].IsOwned && itemList[7].IsEquipped)
            {
                if(isMonsterDead)
                {
                    player.ApplyItemEffectByTerm(itemList[7], 8);
                }
            }

            //9. 반격의 서막(수정본) : 플레이어의 체력이 30% 이하가 되었을시 + 공격력 12% 
            if (itemList[9].IsOwned && itemList[9].IsEquipped)
            {
                if (isHealthBelowThrity)
                {
                    player.ApplyItemEffectByTerm(itemList[9], 12);
                }
            }


            //방어력 관련 아이템
            //한 줌의 인내 : 플레이어가 피격시 + 방어력 2% (최대 + 8%)
            if (itemList[14].IsOwned && itemList[14].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[14], bonusAmount * 2);
                }
            }

            //승승장구의 하프 : 플레이어가 치명타를 터트릴시 방어력 4% (최대 +12%)
            if (itemList[15].IsOwned && itemList[15].IsEquipped)
            {
                int bonusAmount = playerCHitStack > 3 ? 3 : playerCHitStack;

                for (int i = 0; i < playerCHitStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[15], bonusAmount * 4);
                }
            }

            //17. 전투의 흔적 : 적이 전투중 죽었을 경우 + 방어력 8%
            if (itemList[17].IsOwned && itemList[17].IsEquipped)
            {
                if (isMonsterDead)
                {
                    player.ApplyItemEffectByTerm(itemList[17], 8);
                }
            }

            //19. 반격의 서막(단행본) : 플레이어의 체력이 30% 이하가 되었을시 + 방어력 12% 
            if (itemList[19].IsOwned && itemList[19].IsEquipped)
            {
                if (isMonsterDead)
                {
                    player.ApplyItemEffectByTerm(itemList[19], 12);
                }
            }


            //최대체력 관련 아이템
            //24. 한 줌의 복수심 : 플레이어가 피격시 + 최대체력 2% (최대 + 8%)
            if (itemList[24].IsOwned && itemList[24].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[24], bonusAmount * 2);
                }
            }


            //치명타 관련 아이템
            //32. 멈추지 않는 회중시계 : 플레이어가 치명타를 터트리지 못할 시 + 치확 4% (최대 +16%)
            if (itemList[32].IsOwned && itemList[32].IsEquipped)
            {
                int bonusAmount = playerCHitFailStack > 4 ? 4 : playerCHitFailStack;

                for (int i = 0; i < playerCHitFailStack; i++)
                {
                    player.ApplyItemEffectByTerm(itemList[32], bonusAmount * 4);
                }
            }
        }


        //배틀 시작 후 효과를 없애는 함수
        public void LoseEffectAfterBattle(List<Monster> monsterTeam)
        {
            //공격력 관련 아이템
            //3. 신비한 깃털 : 공격력 (적의 현재 수) * 2%
            if (itemList[3].IsOwned && itemList[3].IsEquipped)
            {
                for (int i = 0; i < monsterTeam.Count; i++)
                {
                    player.LoseItemEffectByTerm(itemList[3], 2);
                }
            }

            //6. 어느 영웅의 대서사시 : 전투 진입시 적의 수가 4명일 경우 + 공격력 12%
            if (itemList[6].IsOwned && itemList[6].IsEquipped)
            {
                if (monsterTeam.Count == 4)
                {
                    player.LoseItemEffectByTerm(itemList[6], 12);
                }
            }

            //8. 나무 주사위 : 전투 진입시 + 공격력 1~6%
            if (itemList[8].IsOwned && itemList[8].IsEquipped)
            {
                Random random = new Random();
                int randomAtk = random.Next(1, 7);
                player.LoseItemEffectByTerm(itemList[8], randomAtk);
            }


            //방어력 관련 아이템
            //13. 영롱한 깃털 : 방어력 (적의 현재 수) * 2%
            if (itemList[13].IsOwned && itemList[13].IsEquipped)
            {
                for (int i = 0; i < monsterTeam.Count; i++)
                {
                    player.LoseItemEffectByTerm(itemList[13], 2);
                }

            }

            //16. 어느 거인의 첫 발걸음 : 전투 진입시 적의 수가 4명일 경우 + 방어력 12% 
            if (itemList[16].IsOwned && itemList[16].IsEquipped)
            {
                if (monsterTeam.Count == 4)
                {
                    player.LoseItemEffectByTerm(itemList[16], 12);
                }
            }

            //18. 강철 주사위 : 전투 진입시 + 방어력 1~6%
            if (itemList[18].IsOwned && itemList[18].IsEquipped)
            {
                Random random = new Random();
                int randomDef = random.Next(1, 7);
                player.LoseItemEffectByTerm(itemList[18], randomDef);

            }


            //최대체력 관련 아이템
            //25. 어느 여행자의 노하우 : 전투 진입시 적의 수가 4명일 경우 + 최대체력 6%
            if (itemList[25].IsOwned && itemList[25].IsEquipped)
            {
                if (monsterTeam.Count == 4)
                {
                    player.LoseItemEffectByTerm(itemList[25], 6);
                }
            }


            //골드 관련 아이템
            //33. 핏빛 금화주머니 : 현재 보유 골드 X당 공격력 Y% 증가
            if (itemList[33].IsOwned && itemList[33].IsEquipped)
            {
                player.LoseItemEffectByTerm(itemList[33], (player.Gold / 100) * 5);
            }

            //34. 은빛 금화주머니 : 현재 보유 골드 X당 방어력 Y% 증가
            if (itemList[34].IsOwned && itemList[34].IsEquipped)
            {
                player.LoseItemEffectByTerm(itemList[34], (player.Gold / 100) * 5);
            }

            //35. 산호빛 금화주머니 : 현재 보유 골드 X당 치피 Y% 증가
            if (itemList[35].IsOwned && itemList[35].IsEquipped)
            {
                player.LoseItemEffectByTerm(itemList[35], (player.Gold / 100) * 8);
            }

            // 36. 영롱한 금화주머니 : 현재 보유 골드 X당 속도 Y 증가
            if (itemList[36].IsOwned && itemList[36].IsEquipped)
            {
                player.LoseItemEffectByTerm(itemList[36], (player.Gold / 100) * 1);
            }
        }

        //배틀 진행 중 매 턴마다 효과를 없애는 함수
        public void LoseEffectEveryTurn()
        {
            //공격력 관련 아이템
            //4. 한 줌의 분노 : 플레이어가 피격시 + 공격력 2% (최대 + 8%)
            if (itemList[4].IsOwned && itemList[4].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[4], bonusAmount * 2);
                }
            }

            //5. 승승장구의 피리뿔 : 플레이어가 치명타를 터트릴시 공격력 4% (최대 +12%)
            if (itemList[5].IsOwned && itemList[5].IsEquipped)
            {
                int bonusAmount = playerCHitStack > 3 ? 3 : playerCHitStack;

                for (int i = 0; i < playerCHitStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[5], bonusAmount * 4);
                }
            }

            //7. 전투의 흔적 : 적이 전투중 죽었을 경우 + 공격력 8%
            if (itemList[7].IsOwned && itemList[7].IsEquipped)
            {
                if (isMonsterDead)
                {
                    player.LoseItemEffectByTerm(itemList[7], 8);
                }
            }

            //9. 반격의 서막(수정본) : 플레이어의 체력이 30% 이하가 되었을시 + 공격력 12% 
            if (itemList[9].IsOwned && itemList[9].IsEquipped)
            {
                if (isHealthBelowThrity)
                {
                    player.LoseItemEffectByTerm(itemList[9], 12);
                }
            }


            //방어력 관련 아이템
            //한 줌의 인내 : 플레이어가 피격시 + 방어력 2% (최대 + 8%)
            if (itemList[14].IsOwned && itemList[14].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[14], bonusAmount * 2);
                }
            }

            //승승장구의 하프 : 플레이어가 치명타를 터트릴시 방어력 4% (최대 +12%)
            if (itemList[15].IsOwned && itemList[15].IsEquipped)
            {
                int bonusAmount = playerCHitStack > 3 ? 3 : playerCHitStack;

                for (int i = 0; i < playerCHitStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[15], bonusAmount * 4);
                }
            }

            //17. 전투의 흔적 : 적이 전투중 죽었을 경우 + 방어력 8%
            if (itemList[17].IsOwned && itemList[17].IsEquipped)
            {
                if (isMonsterDead)
                {
                    player.LoseItemEffectByTerm(itemList[17], 8);
                }
            }

            //19. 반격의 서막(단행본) : 플레이어의 체력이 30% 이하가 되었을시 + 방어력 12% 
            if (itemList[19].IsOwned && itemList[19].IsEquipped)
            {
                if (isMonsterDead)
                {
                    player.LoseItemEffectByTerm(itemList[19], 12);
                }
            }


            //최대체력 관련 아이템
            //24. 한 줌의 복수심 : 플레이어가 피격시 + 최대체력 2% (최대 + 8%)
            if (itemList[24].IsOwned && itemList[24].IsEquipped)
            {
                int bonusAmount = playerDamagedStack > 4 ? 4 : playerDamagedStack;

                for (int i = 0; i < playerDamagedStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[24], bonusAmount * 2);
                }
            }


            //치명타 관련 아이템
            //32. 멈추지 않는 회중시계 : 플레이어가 치명타를 터트리지 못할 시 + 치확 4% (최대 +16%)
            if (itemList[32].IsOwned && itemList[32].IsEquipped)
            {
                int bonusAmount = playerCHitFailStack > 4 ? 4 : playerCHitFailStack;

                for (int i = 0; i < playerCHitFailStack; i++)
                {
                    player.LoseItemEffectByTerm(itemList[32], bonusAmount * 4);
                }
            }
        }

        //다음 구역 진입시는 Battle이 아닌 던전에서 구현
        //상점 관련은 Battle이 아닌 Shop에서 구현
    }
}
