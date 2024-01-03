using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoloProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StoreItem item = new StoreItem();
            item.StoreItemList("-");
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
               // Store store = new Store();

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
                       // store.ViewStore();
                        break;
                    default:
                        //Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        setMenu();
                        break;
                }
            }

            public void setMenu()
            {
                //Console.Clear();
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
                    //if (name == null)
                    //{
                    //    Console.Clear();
                    //    Console.WriteLine("보유 중인 아이템이 없습니다.");
                    //    ViewInventory();
                    //}
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

        class Store : Menu
        {
            //    public void ViewStore();

            //    public void CreateItem();

            //    public void ResetStoreItem()
            //    {
            //        //item.StoreItemCreate();
            //    }
            //    //    void StoreBuy()
            //    //    {
            //    //        Console.Clear();
            //    //        Console.WriteLine("[보유 골드]\n{0}G\n", state.Gold);
            //    //        item.ViewStoreItem(0);
            //    //        //아이템 구매한 것은 따로 저장해둘 것. 판매했을 때 대비
            //    //        Console.WriteLine("\n0. 나가기\n");
            //    //        int selectItem = Number();
            //    //        if (selectItem < 0 || selectItem > 6)
            //    //        {
            //    //            Console.WriteLine("잘못된 입력입니다.\n");
            //    //            StoreBuy();
            //    //        }
            //    //        else if (selectItem == 0)
            //    //        {
            //    //            StoreBuy();
            //    //        }
            //    //        else
            //    //        {
            //    //            //int selectGold = int.Parse(storeItemGold[selectItem - 1]);
            //    //            //if (storeItemGold[selectItem - 1] == "구매완료")
            //    //            //{
            //    //            //    Console.WriteLine("이미 구매하신 상품입니다.");
            //    //            //    StoreBuy();
            //    //            //}
            //    //            //else if (selectGold > Gold)
            //    //            //{
            //    //            //    Console.WriteLine("Gold가 부족합니다.");
            //    //            //    StoreBuy();
            //    //            //}
            //    //            //else if (selectGold <= Gold)
            //    //            //{
            //    //            //    Gold = Gold - selectGold;

            //    //            //    itemName.Add(storeItemName[selectItem - 1]);
            //    //            //    itemStats.Add(storeItemStats[selectItem - 1]);
            //    //            //    itemGold.Add(storeItemGold[selectItem - 1]);

            //    //            //    storeItemGold[selectItem - 1] = "구매완료";
            //    //            //    StoreBuy();
            //    //            //}
            //    //        }
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

        class StoreItem
        {
            ItemList itemlist = new ItemList();

            void StoreItemMake()
            {
                itemlist.Make(6);
            }
            
            public void StoreItemList(string plus)
            {
                StoreItemMake();
                itemlist.List("-");
            }
        }

        class ItemList
        {
            private Item[] items;
            public void Make(int count)
            {
                items = new Item[count];
                for(int i = 0; i < count; i++)
                {
                    items[i] = new Item();
                }
            }
            public void List(string plus)
            {
                int itemLength = items.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if(items[i].name.Contains("갑옷")|| items[i].name.Contains("망토"))
                    {
                        Console.WriteLine($"{plus} {items[i].name}\t| 방어력 +{items[i].stats} | {items[i].gold}G | {items[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($"{plus} {items[i].name}\t| 공격력 +{items[i].stats} | {items[i].gold}G | {items[i].comment}");
                    }  
                }
            }
        }

        class Item
        {
            State state = new State();
            
            Random random1 = new Random();

            public string name="";
            public int stats=0;
            public string gold="";
            public string comment="";
            private string[] firstName = { "나무", "낡은 ", "수련자", "청동", "무쇠", "스파트라의 " };
            private string[] secondName = { "갑옷", "망토", "검", "창", "도끼"};
            private string firstComment = "";
            private string SecondComment = "";

            public Item()
            {
                MakeName();
                MakeStats();
                MakeGold();
                MakeCommand();
            }

            void MakeName() 
            {
                int n = random1.Next(10);
                int nn = random1.Next(10);
                int nnn = random1.Next(10);

                int firstLength = firstName.Length;
                int secondLength = secondName.Length;

                if (n < 5)
                {
                    if (nn < 5)
                    {
                        name = firstName[nnn % secondLength] + secondName[nnn % 2];
                    }
                    else { name = firstName[nnn % secondLength] + secondName[nnn % firstLength]; }

                }
                else if (n < 8)
                {
                    if (nn < 5)
                    {
                        name = firstName[nnn % 5] + secondName[nnn % firstLength];
                    }
                    else { name = firstName[nnn % 3] + secondName[nnn % firstLength]; }
                }
                else
                {
                    if (nn < 5)
                    {
                        name = firstName[nnn % 3] + secondName[nnn % firstLength];
                    }
                    else { name = firstName[nnn % 2] + secondName[nnn % firstLength]; }
                }
            }

            void MakeStats() 
            {
                if (name.Contains("나무") || name.Contains("낡은"))
                {
                    int n = random1.Next(1, 3);
                    stats = n;

                }
                else if (name.Contains("청동") || name.Contains("수련자"))
                {
                    int n = random1.Next(2 + state.Lv, 5 + state.Lv);
                    stats = n;
                }
                else if(name.Contains("무쇠"))
                {
                    int n = random1.Next(4 + state.Lv, 7 + state.Lv);
                    stats = n;
                }
                else
                {
                    int n = random1.Next(6 + state.Lv, 8 + state.Lv);
                    stats = n;
                }
            }
            void MakeGold() 
            {
                if (name.Contains("나무") || name.Contains("낡은"))
                {
                    int n = random1.Next(1, 5) * 100;
                    gold = n.ToString();
                }
                else if (name.Contains("청동") || name.Contains("수련자"))
                {
                    int n = random1.Next(4, 8) * 100;
                    gold = n.ToString();
                }
                else if (name.Contains("무쇠"))
                {
                    int n = random1.Next(5, 11) * 100;
                    gold = n.ToString();
                }
                else
                {
                    int n = random1.Next(6, 13) * 100;
                    gold = n.ToString();
                }
            }
            void MakeCommand() 
            {
                int itemLength = name.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (name.Contains("나무"))
                    {
                        firstComment = "초보자가 쓰기 좋은 ";
                    }
                    else if (name.Contains("낡은"))
                    {
                        firstComment = "쉽게 볼 수 있는 낡은 ";
                    }
                    else if (name.Contains("청동"))
                    {
                        firstComment = "어디선가 사용됐던거 같은 ";
                    }
                    else if (name.Contains("무쇠"))
                    {
                        firstComment = "무쇠로 만들어져 튼튼한 ";
                    }
                    else if (name.Contains("스파"))
                    {
                        firstComment = "스파르타의 전사들이 사용했다는 전설의 ";
                    }
                    else if (name.Contains("수련자"))
                    {
                        firstComment = "수련에 도움을 주는 ";
                    }

                    if (name.Contains("갑옷"))
                    {
                        SecondComment = "갑옷입니다.";
                    }
                    else if (name.Contains("검"))
                    {
                        SecondComment = "검입니다.";
                    }
                    else if (name.Contains("창"))
                    {
                        SecondComment = "창입니다.";
                    }
                    else if (name.Contains("도끼"))
                    {
                        SecondComment = "도끼입니다.";
                    }
                    else if (name.Contains("망토"))
                    {
                        SecondComment = "망토입니다.";
                    }

                    comment = firstComment + SecondComment;
                }
            }

            
        }
    }
}

