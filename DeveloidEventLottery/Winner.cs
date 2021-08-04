using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloidEventLottery
{
    class Winner
    {
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

                // 상품 목록 만큼 반복
                for (int i = 0; i < Bindings.LIST_ITEM.Count; i++)
                {
                    int count = Bindings.LIST_ITEM[i].Qty;
                    int loop = 0;

                    while (loop < count)
                    {
                        // 전체 참가자 수 범위 내에서 랜덤 숫자 뽑기
                        Random rand = new Random();
                        int r = rand.Next(userAll - 1);

                        string userId = Bindings.LIST_USER[r].ID;

                        // 중복 된 값이 없을 경우 또는 중복 추첨 프로세스 조건 성립
                        if (!pass.Contains(userId) || itemAll > userAll)
                        {
                            string ID_BLIND = userId.Substring(0, 4) + "****";

                            winner.Add(new Bindings.WinnerList(Bindings.LIST_ITEM[i].Item, Bindings.LIST_USER[r].NickName, userId));
                            winner_view.Add(new Bindings.WinnerList(Bindings.LIST_ITEM[i].Item, Bindings.LIST_USER[r].NickName, ID_BLIND));

                            pass.Add(userId);
                            loop++;
                        }
                    }
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
