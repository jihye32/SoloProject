using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoloProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            State state = new State();
            Inventory inventory = new Inventory();
            Store store = new Store();
            ItemList itemList = new ItemList();

            //서로 연결시켜주기
            menu.getClass(state, inventory, store);
            inventory.getClass(itemList, state);
            store.getClass(state, itemList);

            bool gameOver = false;

            while (!gameOver)
            {
                menu.setMenu();
                gameOver = menu.CheckGameOver();
            }
        }

        class Menu
        {
            State state;
            Inventory inventory;
            Store store;
            bool check = false;

            int menu()
            {
                Console.Clear();
                if (state.name == null)
                {
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("스파르타 마을에 입장하기 전에 먼저 이름을 알려주세요.");
                    state.name = Console.ReadLine();
                    setFirstState(state.name);
                    Console.Clear() ;
                    Console.WriteLine($"{state.name}님 환영합니다. 여긴 스파르타 마을입니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                    Console.WriteLine("0. 게임 끝내기\n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                }
                else
                {
                    Console.WriteLine($"{state.name}님 환영합니다. 여긴 스파르타 마을입니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                    Console.WriteLine("0. 게임 끝내기\n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                }
                return Number();
            }

            void menu2(int n)
            {
                switch (n)
                {
                    case 0:
                        //게임 끝내기
                        check = true;
                        break;
                    case 1:
                        //상태보기
                        Console.Clear();
                        state.ViewState();
                        break;
                    case 2:
                        //인벤토리
                        Console.Clear();
                        inventory.ViewInventory();
                        break;
                    case 3:
                        //상점
                        Console.Clear();
                        store.ResetStoreItem();
                        store.ViewStore();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        menu2(menu());
                        break;
                }
            }

            void setFirstState(string name)
            {
                state.Lv = 1;
                state.name = name;
                state.Strike = 10;
                state.Depence = 5;
                state.HP = 100;
                state.Gold = 1500;
            }

            public void setMenu()
            {
                menu2(menu());
            }

            public bool CheckGameOver()
            {
                return check;
            }

            public void getClass(State state1, Inventory inventory1, Store store1)
            {
                state = state1;
                inventory = inventory1;
                store = store1;
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

        class State
        {
            public int Lv { get; set; }
            public string name { get; set; }
            public int Strike { get; set; }
            public int Depence { get; set; }
            public int HP { get; set; }
            public int Gold { get; set; }
            public int PlusStrike { get; set; }
            public int PlusDepence { get; set; }
            public bool checkStrike = false;
            public bool checkDepence = false;

            public void ViewState()
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine();
                Console.WriteLine("LV : 0" + Lv); //10이 넘어갔을 때 값 변경 추가 필요
                Console.WriteLine($"Chad ( {name} )"); //이름 받아오기.

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
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewState();
                }
            }
        }

        class Inventory
        {
            ItemList itemlist;
            State state;

            public void ViewInventory()
            {
                Console.WriteLine("\n인벤토리\n");
                Console.WriteLine("[아이템 목록]\n");
                itemlist.InventoryItemList("-");
                Console.WriteLine("\n1. 장착관리\n");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    Console.Clear();
                }
                else if (n == 1)
                {
                    if (itemlist.sellitems[0] == null)
                    {
                        Console.Clear();
                        Console.WriteLine("보유 중인 아이템이 없습니다.\n");
                        ViewInventory();
                    }
                    else
                    {
                        Console.Clear();
                        InventoryItemManage();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewInventory();
                }
            }

            void InventoryItemManage()
            {
                Console.WriteLine("\n인벤토리 - 장착 관리\n");
                Console.WriteLine("[아이템 목록]\n");
                itemlist.InventoryItemList();
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                int InventoryItemLength = itemlist.sellitems.Length;
                if (n == 0)
                {
                    Console.Clear();
                    ViewInventory();
                }
                else if (n > InventoryItemLength || n < 0)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    InventoryItemManage();
                }
                else
                {
                    for (int i = 0; i < InventoryItemLength; i++)
                    {
                        if (n - 1 == i)
                        {
                            if (itemlist.sellitems[i].name.Contains("E"))
                            {
                                Console.Clear();
                                Console.WriteLine("이미 장착하고 있는 아이템입니다.\n");
                                InventoryItemManage();
                            }
                            else
                            {
                                itemlist.sellitems[i].name = "[E]" + itemlist.sellitems[i].name;

                                if (itemlist.sellitems[i].name.Contains("갑옷") || itemlist.sellitems[i].name.Contains("망토"))
                                {
                                    state.PlusDepence = itemlist.sellitems[i].stats;
                                    state.Depence += state.PlusDepence;
                                    state.checkDepence = true;
                                }
                                else
                                {
                                    state.PlusStrike = itemlist.sellitems[i].stats;
                                    state.Strike += state.PlusStrike;
                                    state.checkStrike = true;
                                }
                                Console.Clear();
                                InventoryItemManage();
                            }
                        }
                    }
                }
            }

            public void getClass(ItemList itemlist1, State state1)
            {
                itemlist = itemlist1;
                state = state1;
            }
        }

        class Store
        {
            State state;
            ItemList itemlist;

            public void getClass(State state1, ItemList itemlist1)
            {
                state = state1;
                itemlist = itemlist1;
            }

            public void ViewStore()
            {
                Console.WriteLine("\n상점\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{state.Gold}G");
                Console.WriteLine("\n[아이템 목록]\n");
                itemlist.List("-");
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    Console.Clear();
                }
                else if (n == 1)
                {
                    Console.Clear();
                    StoreBuy();
                }
                else if (n == 2)
                {
                    if (itemlist.sellitems[0] == null)

                    {
                        Console.Clear();
                        Console.WriteLine("판매할 수 있는 아이템이 없습니다.\n");
                        ViewStore();
                    }
                    else
                    {
                        Console.Clear();
                        StoreSell();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    ViewStore();
                }
            }

            public void ResetStoreItem()
            {
                itemlist.Make(6);
            }

            void StoreBuy()
            {
                Console.WriteLine("\n상점 - 아이템 구매\n");
                Console.WriteLine($"[보유 골드]\n{state.Gold}G\n");
                Console.WriteLine("[아이템 목록]\n");
                itemlist.List();
                Console.WriteLine("\n0. 나가기\n");
                int selectItem = Number();
                if (selectItem < 0 || selectItem > 6)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    StoreBuy();
                }
                else if (selectItem == 0)
                {
                    Console.Clear();
                    ViewStore();
                }
                else
                {
                    int selectgold = itemlist.items[selectItem - 1].goldInt;
                    if (itemlist.items[selectItem - 1].gold == "구매완료")
                    {
                        Console.WriteLine("이미 구매하신 상품입니다.");
                        StoreBuy();
                    }
                    else if (selectgold > state.Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        StoreBuy();
                    }
                    else if (selectgold <= state.Gold)
                    {
                        state.Gold = state.Gold - selectgold;

                        itemlist.items[selectItem - 1].gold = "구매완료";
                        itemlist.Sellitem(itemlist.items[selectItem - 1]);
                        Console.Clear();
                        StoreBuy();
                    }
                }
            }

            void StoreSell()
            {
                Console.WriteLine("\n상점 - 아이템 판매\n");
                Console.WriteLine($"[보유 골드]\n{state.Gold}G\n");
                Console.WriteLine("[아이템 목록]\n");
                itemlist.InventoryItemSellList();
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                int InventoryItemLength = itemlist.sellitems.Length;
                if (n == 0)
                {
                    Console.Clear();
                    ViewStore();
                }
                else if (n > InventoryItemLength || n < 0)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                    StoreSell();
                }
                else 
                {
                    int i = n - 1;
                    state.Gold += itemlist.sellitems[i].goldInt;
                    itemlist.sellitems[i] = null;
                    for (i = n - 1; i < InventoryItemLength; i++)
                    {
                        if (itemlist.sellitems[i + 1] != null)
                        {
                            itemlist.sellitems[i] = itemlist.sellitems[i + 1];
                            itemlist.sellitems[i + 1] = null;
                        }
                        else break;
                    }
                    Console.Clear();
                    StoreSell();
                }
            }
        }

        class ItemList
        {
            public Item[] items;
            public Item[] sellitems = new Item[10];

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
                        Console.WriteLine($"{plus} {items[i].name}\t| 방어력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($"{plus} {items[i].name}\t| 공격력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
                    }  
                }
            }
            public void List()
            {
                int itemLength = items.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (items[i].name.Contains("갑옷") || items[i].name.Contains("망토"))
                    {
                        Console.WriteLine($" {i+1}. {items[i].name}\t| 방어력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i+1}. {items[i].name}\t| 공격력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
                    }
                }
            }

            public void Sellitem(Item item)
            {
                int i = 0;
                while (true)
                {
                    if (sellitems[i] == null)
                    {
                        sellitems[i] = item;
                        break;
                    }
                    else if (i > 9)
                    {
                        Console.WriteLine("가득 찼습니다.");
                        break;
                    }
                    else { i++; }
                }
            }

            public void InventoryItemList(string plus)
            {
                int itemLength = sellitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (sellitems[i] == null)
                    {
                        break;
                    }
                    else if (sellitems[i].name.Contains("갑옷") || sellitems[i].name.Contains("망토"))
                    {
                        Console.WriteLine($"{plus} {sellitems[i].name}\t| 방어력 +{sellitems[i].stats} | {sellitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($"{plus} {sellitems[i].name}\t| 공격력 +{sellitems[i].stats} | {sellitems[i].comment}");
                    }
                }
            }
            public void InventoryItemList()
            {
                int itemLength = sellitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (sellitems[i] == null)
                    {
                        break;
                    }
                    else if (sellitems[i].name.Contains("갑옷") || sellitems[i].name.Contains("망토"))
                    {
                        Console.WriteLine($" {i+1}. {sellitems[i].name}\t| 방어력 +{sellitems[i].stats} | {sellitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i+1}. {sellitems[i].name}\t| 공격력 +{sellitems[i].stats} | {sellitems[i].comment}");
                    }
                }
            }

            public void InventoryItemSellList()
            {
                int itemLength = sellitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (sellitems[i] == null)
                    {
                        break;
                    }
                    else if (sellitems[i].name.Contains("갑옷") || sellitems[i].name.Contains("망토"))
                    {
                        sellitems[i].goldInt *= 85;
                        sellitems[i].goldInt /= 100;
                        sellitems[i].gold = (sellitems[i].goldInt + "G").ToString();
                        Console.WriteLine($" {i + 1}. {sellitems[i].name}\t| 방어력 +{sellitems[i].stats} | {sellitems[i].gold} | {sellitems[i].comment}");
                    }
                    else
                    {
                        sellitems[i].goldInt *= 85;
                        sellitems[i].goldInt /= 100;
                        sellitems[i].gold = (sellitems[i].goldInt + "G").ToString();
                        Console.WriteLine($" {i + 1}. {sellitems[i].name}\t| 공격력 +{sellitems[i].stats} | {sellitems[i].gold} | {sellitems[i].comment}");
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
            public int goldInt=0;
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
                        name = firstName[nnn % firstLength] + secondName[nnn % 2];
                    }
                    else { name = firstName[nnn % firstLength] + secondName[nnn % secondLength]; }

                }
                else if (n < 8)
                {
                    if (nn < 5)
                    {
                        name = firstName[nnn % 5] + secondName[nnn % secondLength];
                    }
                    else { name = firstName[nnn % 3] + secondName[nnn % secondLength]; }
                }
                else
                {
                    if (nn < 5)
                    {
                        name = firstName[nnn % 3] + secondName[nnn % secondLength];
                    }
                    else { name = firstName[nnn % 2] + secondName[nnn % secondLength]; }
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
                    goldInt = random1.Next(1, 5) * 100;
                    gold = goldInt.ToString()+"G";
                }
                else if (name.Contains("청동") || name.Contains("수련자"))
                {
                    goldInt = random1.Next(4, 8) * 100;
                    gold = goldInt.ToString() + "G";
                }
                else if (name.Contains("무쇠"))
                {
                    goldInt = random1.Next(5, 11) * 100;
                    gold = goldInt.ToString() + "G";
                }
                else
                {
                    goldInt = random1.Next(6, 13) * 100;
                    gold = goldInt.ToString() + "G";
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

