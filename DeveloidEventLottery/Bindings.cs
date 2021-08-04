using System.Collections.Generic;

namespace DeveloidEventLottery
{
    class Bindings
    {
        // 상품 목록
        public static List<ItemList> LIST_ITEM = new List<ItemList>();

        public class ItemList
        {
            public ItemList(string Item, int Qty)
            {
                this.Item = Item;
                this.Qty = Qty;
            }

            public string Item { get; set; }

            public int Qty { get; set; }
        }

        // 회원 목록
        public static List<UserList> LIST_USER = new List<UserList>();
        public static List<UserListView> LIST_USER_VIEW = new List<UserListView>();

        public class UserList
        {
            public UserList(string NickName, string ID)
            {
                this.NickName = NickName;
                this.ID = ID;
            }

            public string NickName { get; set; }

            public string ID { get; set; }
        }

        public class UserListView
        {
            public UserListView(string NickName, string ID, int Overlap, string Percentage)
            {
                this.NickName = NickName;
                this.ID = ID;
                this.Overlap = Overlap;
                this.Percentage = Percentage;
            }

            public string NickName { get; set; }

            public string ID { get; set; }

            public int Overlap { get; set; }

            public string Percentage { get; set; }
        }

        // 당첨자 목록
        public static List<WinnerList> LIST_WINNER = new List<WinnerList>();
        public static List<WinnerList> LIST_WINNER_VIEW = new List<WinnerList>();

        public class WinnerList
        {
            public WinnerList(string Item, string NickName, string ID)
            {
                this.Item = Item;
                this.NickName = NickName;
                this.ID = ID;
            }

            public string Item { get; set; }

            public string NickName { get; set; }

            public string ID { get; set; }
        }
    }
}
