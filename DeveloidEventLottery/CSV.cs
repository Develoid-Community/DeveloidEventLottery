using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DeveloidEventLottery
{
    class CSV
    {
        // CSV 양식 저장
        public static void FormSave()
        {
            try
            {
                // 폴더 경로 가져오기
                string FolderPath = Files.GetSaveFolderPath();
                //Console.WriteLine(FilePath);

                if (FolderPath is null) return;

                // 상품 목록 파일 정보 생성
                string ItemPath = Path.Combine(FolderPath, "디벨로이드_이벤트_추첨기_상품목록.csv");
                string ItemForm = "상품,수량";

                // 회원 목록 파일 정보 생성
                string UserPath = Path.Combine(FolderPath, "디벨로이드_이벤트_추첨기_회원목록.csv");
                string UserForm = "닉네임,ID";

                // 파일 생성
                File.WriteAllText(ItemPath, ItemForm, Encoding.UTF8);
                File.WriteAllText(UserPath, UserForm, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // 이벤트 상품 가져오기
        public static void SetItemList()
        {
            try
            {
                List<Bindings.ItemList> itemList = new List<Bindings.ItemList>();

                Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                string FilePath = Files.GetOpenFilePath();

                if (FilePath is null) return;

                bool oneLine = true;

                using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] fields = regex.Split(line);

                        if (oneLine)
                        {
                            if (!fields[0].Contains("상품"))
                            {
                                MessageBox.Show("올바른 파일을 선택해주세요.", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                                return;
                            }
                            if (!fields[1].Contains("수량"))
                            {
                                MessageBox.Show("올바른 파일을 선택해주세요.", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                                return;
                            }
                            oneLine = false;
                        }
                        else
                        {
                            itemList.Add(new Bindings.ItemList(fields[0], Convert.ToInt32(fields[1])));
                        }

                    }
                }

                Bindings.LIST_ITEM = itemList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // 참여 회원 가져오기
        public static void SetUserList()
        {
            try
            {
                List<Bindings.UserList> userList = new List<Bindings.UserList>();

                Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                string FilePath = Files.GetOpenFilePath();

                if (FilePath is null) return;

                bool oneLine = true;

                // 회원 목록 가져오기
                using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] fields = regex.Split(line);

                        if (oneLine)
                        {
                            if (!fields[0].Contains("닉네임"))
                            {
                                MessageBox.Show("올바른 파일을 선택해주세요.", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                                return;
                            }
                            if (!fields[1].Contains("ID"))
                            {
                                MessageBox.Show("올바른 파일을 선택해주세요.", "정지", MessageBoxButton.OK, MessageBoxImage.Stop);
                                return;
                            }
                            oneLine = false;
                        }
                        else
                        {
                            userList.Add(new Bindings.UserList(fields[0], fields[1]));
                        }

                    }
                }

                Bindings.LIST_USER = userList;

                // 화면 출력을 위한 중복 정리 프로세스
                List<Bindings.UserListView> overlapList = new List<Bindings.UserListView>();
                List<string> pass = new List<string>();

                for (int i = 0; i < userList.Count; i++)
                {
                    string userId = userList[i].ID;
                    int count = 0;

                    // 중복된 값이 없을 경우 진행
                    if (!pass.Contains(userId))
                    {
                        // 동일한 값 찾아서 중복 등록 처리
                        Parallel.For(0, userList.Count, (loop) =>
                        {
                            string checkId = userList[loop].ID;

                            if (userId.Equals(checkId))
                            {
                                count++;
                                pass.Add(checkId);
                            }
                        });

                        // 데이터 생성 처리
                        string idBlind = Bindings.UserIdBlind(userId);
                        
                        double percent = (((double)count / (double)userList.Count) * 100);
                        string percentage = string.Format("{0:0.00}", percent) + "%";

                        overlapList.Add(new Bindings.UserListView(userList[i].NickName, idBlind, count, percentage));
                    }
                }

                Bindings.LIST_USER_VIEW = overlapList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // 당첨자 목록 저장
        public static void SaveWinner()
        {
            try
            {
                string FileName = "디벨로이드_이벤트_추첨기_당첨자_목록_"
                                + DateTime.Now.ToString("yyyyMMddHHmmss")
                                + DateTime.Now.Millisecond.ToString("000");

                string FilePath = Files.GetSaveFilePath(FileName);
                if (FilePath is null) return;

                // 헤더 생성
                string str = "상품,닉네임,ID" + Environment.NewLine;

                for (int i = 0; i < Bindings.LIST_WINNER.Count; i++)
                {
                    str += string.Format("{0},", Bindings.LIST_WINNER[i].Item);
                    str += string.Format("{0},", Bindings.LIST_WINNER[i].NickName);
                    str += string.Format("{0}", Bindings.LIST_WINNER[i].ID);
                    str += Environment.NewLine;
                }

                str = str.Trim();

                File.WriteAllText(FilePath, str, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
