using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoloProject
{
    internal class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.setMenu();
        }

        class Menu
        {
            int menu()
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                return Number();
            }
            void menu2(int n)
            {
                State state = new State();
                Invetory inventory = new Invetory();
                Store store = new Store();

                Console.Clear();
                switch (n)
                {
                    case 1:
                        //상태보기
                        state.ViewState();
                        break;
                    case 2:
                        //인벤토리
                        Console.Clear();
                        inventory.ViewInventory();
                        break;
                    case 3:
                        //상점
                        //store.ResetStoreItem();
                        store.ViewStore();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        setMenu();
                        break;
                }
            }

            public void setMenu()
            {
                Console.Clear();
                menu2(menu());
            }
        }
        static int Number()
        {
            int n;
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            //숫자가 아닌 문자를 입력했을 때 잘못입력했다는 알림 나오게 추가.
            bool check = int.TryParse(Console.ReadLine(), out n);
            int number = check ? n : -1;
            return number;
        }

        //static void outMenu(int number)
        //{
        //    int n;
        //    Console.WriteLine("0. 나가기\n");
        //    n = Number();
        //    if (n == 0)
        //    {
        //        Menu2(Menu());
        //    }
        //    else
        //    {
        //        //number번호에 따라 되돌아가는 화면 바꿀 수 있게 설정
        //        Console.WriteLine("잘못된 입력입니다.\n");
        //        Menu2(number);
        //    }
        //}

        class State : Menu
        {
            int lv = 1;
            public int Lv
            {
                get { return lv; }
                set { lv = value; }
            }
            int Strike = 10;
            int Depence = 5;
            int HP = 100;
            int gold = 1500;
            public int Gold
            {
                get { return gold; }
                set { gold = value; }
            }

            int PlusStrike { get; set; }
            int PlusDepence { get; set; }

            bool checkStrike = false;
            bool checkDepence = false;

            public void ViewState()
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine();
                Console.WriteLine("LV : 0" + Lv); //10이 넘어갔을 때 값 변경 추가 필요
                Console.WriteLine("Chad ( 전사 )"); //이름 받아오기.

                if (checkStrike)
                {
                    Console.WriteLine("공격력 : {0} (+{1})", Strike, PlusStrike);
                }
                else Console.WriteLine("공격력 : {0}", Strike);

                if (checkDepence)
                {
                    Console.WriteLine("방어력 : {0} (+{1})", Depence, PlusDepence);
                }
                else Console.WriteLine("방어력 : {0}", Depence);

                Console.WriteLine("체  력 : {0}", HP);
                Console.WriteLine("Gold : {0}G\n", Gold);

                int n;
                Console.WriteLine("0. 나가기\n");
                n = Number();
                if (n == 0)
                {
                    Console.Clear();
                    setMenu();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewState();
                }
            }
        }

        class Invetory : Menu
        {
            public string[] name;
            public string[] stats;
            public int[] gold;
            public string[] comment;

            public void ViewInventory()
            {
                Console.WriteLine("인벤토리\n");
                Console.WriteLine("[아이템 목록]\n");
                //InventoryItem(0);
                Console.WriteLine("\n1. 장착관리");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    setMenu();
                }
                else if (n == 1)
                {
                    if (name == null)
                    {
                        Console.Clear();
                        Console.WriteLine("보유 중인 아이템이 없습니다.");
                        ViewInventory();
                    }
                    //else
                    //{
                    //    InventoryItem(1);
                    //}
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewInventory();
                }
            }

            void InventoryItem()
            {

            }
        }

        class InventoryItem : Invetory
        {
            //struct item
            //{
            //    public string name;
            //    string stats;
            //    int gold;
            //    string comment;
            //}

            //public void ViewInventoryItem()
            //{
            //    if (name == null)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < itemName.Count; i++)
            //        {
            //            if (itemName[i] == "창" || itemName[i] == "검")
            //            {
            //                Console.WriteLine("- {0}\t| 공격력 + {1} |", itemName[i], itemStats[i]);
            //            }
            //            else
            //            {
            //                Console.WriteLine("- {0}\t| 방어력 + {1} |", itemName[i], itemStats[i]);
            //            }
            //        }
            //    }
            //}
        }

        //static void InventoryItem(int n)
        //{
        //    switch (n)
        //    {
        //        //아이템 보여주기
        //        case 0:
        //            if (itemName == null)
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                for (int i = 0; i < itemName.Count; i++)
        //                {
        //                    if (itemName[i] == "창" || itemName[i] == "검")
        //                    {
        //                        Console.WriteLine("- {0}\t| 공격력 + {1} |", itemName[i], itemStats[i]);
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("- {0}\t| 방어력 + {1} |", itemName[i], itemStats[i]);
        //                    }
        //                }
        //            }
        //            break;
        //        //아이템 착용 시 [E] 추가
        //        case 1:
        //            Console.WriteLine("인벤토리 - 장착 관리\n");
        //            Console.WriteLine("[아이템 목록]\n");
        //            for (int i = 0; i < itemName.Count; i++)
        //            {
        //                if (itemName[i] == "창" || itemName[i] == "검")
        //                {
        //                    Console.WriteLine("{0} {1}\t| 공격력 + {2} |", i + 1, itemName[i], itemStats[i]);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("{0} {1}\t| 방어력 + {2} |", i + 1, itemName[i], itemStats[i]);
        //                }
        //            }
        //            Console.WriteLine("\n0. 나가기\n");
        //            int number = Number();
        //            for (int i = 0; i < itemName.Count; i++)
        //            {
        //                if (number < 0 || number > itemName.Count)
        //                {
        //                    Console.WriteLine("잘못된 입력입니다.\n");
        //                    InventoryItem(1);
        //                }
        //                else if (number == 0)
        //                {
        //                    Inventory(2);
        //                }
        //                else if (i + 1 == number)
        //                {
        //                    if (itemName[i] == "갑옷")
        //                    {
        //                        PlusDepence = int.Parse(itemStats[i]);
        //                        Depence += PlusDepence;
        //                        checkDepence = true;
        //                    }
        //                    else
        //                    {
        //                        PlusStrike = int.Parse(itemStats[i]);
        //                        Strike += PlusStrike;
        //                        checkStrike = true;
        //                    }
        //                    string name = "[E]" + itemName[i];
        //                    itemName[i] = name;
        //                }
        //            }
        //            InventoryItem(1);
        //            break;
        //        //착용된 아이템 값을 상태 값에 추가
        //        case 2:
        //            break;
        //    }
        //}

        class Store : Menu
        {
            State state = new State();
            StoreItem item = new StoreItem();

            public void ViewStore()
            {
                Console.WriteLine("상점");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n{0}G\n", state.Gold);
                //        item.ViewStoreItem("-");
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    setMenu();
                }
                else if (n == 1)
                {
                    //StoreBuy();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewStore();
                }
            }

            public void ResetStoreItem()
            {
                //item.StoreItemCreate();
            }
            //    void StoreBuy()
            //    {
            //        Console.Clear();
            //        Console.WriteLine("[보유 골드]\n{0}G\n", state.Gold);
            //        item.ViewStoreItem(0);
            //        //아이템 구매한 것은 따로 저장해둘 것. 판매했을 때 대비
            //        Console.WriteLine("\n0. 나가기\n");
            //        int selectItem = Number();
            //        if (selectItem < 0 || selectItem > 6)
            //        {
            //            Console.WriteLine("잘못된 입력입니다.\n");
            //            StoreBuy();
            //        }
            //        else if (selectItem == 0)
            //        {
            //            StoreBuy();
            //        }
            //        else
            //        {
            //            //int selectGold = int.Parse(storeItemGold[selectItem - 1]);
            //            //if (storeItemGold[selectItem - 1] == "구매완료")
            //            //{
            //            //    Console.WriteLine("이미 구매하신 상품입니다.");
            //            //    StoreBuy();
            //            //}
            //            //else if (selectGold > Gold)
            //            //{
            //            //    Console.WriteLine("Gold가 부족합니다.");
            //            //    StoreBuy();
            //            //}
            //            //else if (selectGold <= Gold)
            //            //{
            //            //    Gold = Gold - selectGold;

            //            //    itemName.Add(storeItemName[selectItem - 1]);
            //            //    itemStats.Add(storeItemStats[selectItem - 1]);
            //            //    itemGold.Add(storeItemGold[selectItem - 1]);

            //            //    storeItemGold[selectItem - 1] = "구매완료";
            //            //    StoreBuy();
            //            //}
            //        }
        }

        class StoreItem
        {
            //Random rand = new Random();
            //State state = new State();

            //string[] storeItemName = new string[6];
            //string[] storeItemStats = new string[6];
            //string[] storeItemGold = new string[6];

            public void StoreItemCreate()//상점아이템 생성
            {
                //        string[] Name = new string[6];
                //        string[] Stats = new string[6];
                //        string[] Gold = new string[6];
                //        for (int i = 0; i < Name.Length; i++)
                //        {
                //            int nameNumber = rand.Next(1, 4);
                //            if (nameNumber == 1)
                //            {
                //                Name[i] = "창";
                //            }
                //            else if (nameNumber == 2)
                //            {
                //                Name[i] = "검";
                //            }
                //            else
                //                Name[i] = "갑옷";
                //        }
                //        for (int i = 0; i < Stats.Length; i++)
                //        {
                //            Stats[i] = rand.Next(1 + state.Lv, 5 + state.Lv).ToString("N0");
                //        }
                //        for (int i = 0; i < Gold.Length; i++)
                //        {
                //            Gold[i] = rand.Next(100 + state.Lv * rand.Next(0, 10), 1000 + state.Lv * rand.Next(0, 100)).ToString("N0");
                //        }
                //        foreach (string n in Name)
                //        {
                //            storeItemName = Name;
                //        }
                //        foreach (string i in Gold)
                //        {
                //            storeItemGold = Gold;
                //        }
                //        foreach (string i in Stats)
                //        {
                //            storeItemStats = Stats;
                //        }
            }

            public void ViewStoreItem(string bonus)
            {
                //        for (int i = 0; i < storeItemStats.Length; i++)
                //        {
                //            if (storeItemName[i] == "창" || storeItemName[i] == "검")
                //            {
                //                Console.WriteLine("{0} {1}\t| 공격력 +{2} | {3}G", bonus, storeItemName[i], storeItemStats[i], storeItemGold[i]);
                //            }
                //            else { Console.WriteLine("{0} {1}\t| 방어력 +{2} | {3}G", bonus, storeItemName[i], storeItemStats[i], storeItemGold[i]); }
                //        }
            }

            public void ViewStoreItem(int number)
            {
                //        for (int i = 0; i < storeItemStats.Length; i++)
                //        {
                //            if (storeItemName[i] == "창" || storeItemName[i] == "검")
                //            {
                //                Console.WriteLine("{0} {1}\t| 공격력 +{2} | {3}G", i + 1, storeItemName[i], storeItemStats[i], storeItemGold[i]);
                //            }
                //            else { Console.WriteLine("{0} {1}\t| 방어력 +{2} | {3}G", i + 1, storeItemName[i], storeItemStats[i], storeItemGold[i]); }
                //        }

            }
        }
    }
}

