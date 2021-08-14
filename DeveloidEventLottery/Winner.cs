using System;
using System.Collections.Generic;

namespace DeveloidEventLottery
{
    class Winner
    {
        public static int RandomNumber(int Max)
        {
            Random rand = new Random();
            return rand.Next(0, Max);
        }

        public static void Get()
        {
            try
            {
                List<Bindings.WinnerList> winner = new List<Bindings.WinnerList>();
                List<Bindings.WinnerList> winner_view = new List<Bindings.WinnerList>();
                List<string> pass = new List<string>();

                // 중복 추첨 프로세스 실행 여부 판단 목적, 전체 상품 개수
                int itemAll = 0;
                for (int i = 0; i < Bindings.LIST_ITEM.Count; i++)
                {
                    itemAll += Bindings.LIST_ITEM[i].Qty;
                }

                // 중복 추첨 프로세스 실행 여부 판단 목적, 전체 회원 수
                int userAll = Bindings.LIST_USER.Count;

                // 중복 참여자 필터링 한 회원 수
                int checkUserAll = Bindings.LIST_USER_VIEW.Count;

                if (userAll != checkUserAll)
                {
                    Console.WriteLine("UserAll -> " + userAll.ToString());
                    Console.WriteLine("CheckUserAll -> " + checkUserAll.ToString());
                }

                // 상품 목록 만큼 반복
                for (int i = 0; i < Bindings.LIST_ITEM.Count; i++)
                {
                    int count = Bindings.LIST_ITEM[i].Qty;
                    int loop = 0;

                    while (loop < count)
                    {
                        // 전체 참가자 수 범위 내에서 랜덤 숫자 뽑기
                        int r = RandomNumber(userAll);
                        if (r > userAll) r = userAll;

                        string userId = Bindings.LIST_USER[r].ID;

                        // 중복 된 값이 없을 경우 또는 중복 추첨 프로세스 조건 성립
                        if (!pass.Contains(userId))
                        {
                            string idBlind = Bindings.UserIdBlind(userId);

                            winner.Add(new Bindings.WinnerList(Bindings.LIST_ITEM[i].Item, Bindings.LIST_USER[r].NickName, userId));
                            winner_view.Add(new Bindings.WinnerList(Bindings.LIST_ITEM[i].Item, Bindings.LIST_USER[r].NickName, idBlind));

                            pass.Add(userId);

                            // 전체 회원 참여 수와 중복 참여자 필터링 된 수가 일치하지 않을 경우
                            if (userAll != checkUserAll)
                            {
                                if (pass.Count == checkUserAll) pass.Clear(); // 중복 제외 대상 값이 필터링 된 회원 수와 같을 경우 초기화
                            }
                            else
                            {
                                if (pass.Count == userAll) pass.Clear(); // 중복 제외 대상 값이 회원 수와 같을 경우 초기화
                            }

                            loop++;
                        }

                        Console.WriteLine("Count -> " + count.ToString());
                        Console.WriteLine("Loop -> " + loop.ToString());
                        Console.WriteLine("Pass -> " + pass.Count.ToString());
                        Console.WriteLine("User -> " + Bindings.LIST_USER.Count.ToString());
                    }

                    // 상품 수가 회원 수 보다 많은 경우 중복 값 제거해서 다음 아이템에서 추첨 될 수 있도록 처리
                    if (itemAll > userAll) pass.Clear();
                }

                Bindings.LIST_WINNER = winner;
                Bindings.LIST_WINNER_VIEW = winner_view;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
