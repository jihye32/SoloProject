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
            ClassChain chain = new ClassChain();

            Menu menu = chain.getMenu();
            State state = chain.getState();
            Inventory inventory = chain.getInventory();
            Store store = chain.getStore();
            ItemList itemList = chain.getItemList();

            //서로 연결시켜주기
            menu.getClass(chain);
            inventory.getClass(chain);
            store.getClass(chain);
            itemList.getClass(chain);

            bool gameOver = false;

            while (!gameOver)
            {
                menu.setMenu();
                gameOver = menu.CheckGameOver();
            }
        }
        
        class ClassChain
        {
            public Menu menu = new Menu();
            public State state = new State();
            public Inventory inventory = new Inventory();
            public Store store = new Store();
            public ItemList itemlist = new ItemList();

            public Menu getMenu()
            {
                return menu;
            }
            public State getState()
            {
                return state;
            }
            public Inventory getInventory()
            {
                return inventory;
            }
            public Store getStore()
            {
                return store;
            }
            public ItemList getItemList()
            {
                return itemlist;
            }
        }

        class Menu
        {
            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }
            bool check = false;

            int menu()
            {
                Console.Clear();
                if (chain.state.name == null)
                {
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("스파르타 마을에 입장하기 전에 먼저 이름을 알려주세요.");
                    chain.state.name = Console.ReadLine();
                    setFirstState(chain.state.name);
                    Console.Clear() ;
                    Console.WriteLine($"{chain.state.name}님 환영합니다. 여긴 스파르타 마을입니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                    Console.WriteLine("0. 게임 끝내기\n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                }
                else
                {
                    Console.WriteLine($"{chain.state.name}님 환영합니다. 여긴 스파르타 마을입니다.");
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
                        chain.state.ViewState();
                        break;
                    case 2:
                        //인벤토리
                        Console.Clear();
                        chain.inventory.ViewInventory();
                        break;
                    case 3:
                        //상점
                        Console.Clear();
                        chain.store.ResetStoreItem();
                        chain.store.ViewStore();
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
                chain.state.Lv = 1;
                chain.state.name = name;
                chain.state.Strike = 10;
                chain.state.Depence = 5;
                chain.state.HP = 100;
                chain.state.Gold = 1500;
                chain.state.checkStrike = false;
                chain.state.checkDepence = false;
            }

            public void setMenu()
            {
                menu2(menu());
            }

            public bool CheckGameOver()
            {
                return check;
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
            public bool checkStrike { get; set; }
            public bool checkDepence { get; set; }

            public void ViewState()
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine();
                Console.WriteLine("LV : 0" + Lv); //10이 넘어갔을 때 값 변경 추가 필요
                Console.WriteLine($"Chad ( {name} )");

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
            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }

            public string installnameStrike = "";
            public string installnameDepence = "";

            public void ViewInventory()
            {
                Console.WriteLine("\n인벤토리\n");
                Console.WriteLine("[아이템 목록]\n");
                chain.itemlist.InventoryItemList("-");
                Console.WriteLine("\n1. 장착관리\n");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    Console.Clear();
                }
                else if (n == 1)
                {
                    if (chain.itemlist.inventoryitems[0] == null)
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
                Item[] inventoryitem = chain.itemlist.inventoryitems;

                Console.WriteLine("\n인벤토리 - 장착 관리\n");
                Console.WriteLine("[아이템 목록]\n");
                chain.itemlist.InventoryItemList();
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                int InventoryItemLength = inventoryitem.Length;
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
                    int i = n - 1;
                    if (inventoryitem[i].name.Contains("E")) //장착된 아이템 해제
                    {
                        if (inventoryitem[i].name.Contains("갑옷") || inventoryitem[i].name.Contains("망토"))
                        {
                            inventoryitem[i].name = installnameDepence;
                            installnameDepence = "";
                        }
                        else
                        {
                            inventoryitem[i].name = installnameStrike;
                            installnameStrike = "";
                        }
                        chain.itemlist.RemoveItem(i);
                    }
                    else //아이템 장착
                    {
                        if (inventoryitem[i].name.Contains("갑옷") || inventoryitem[i].name.Contains("망토"))//방어구
                        {
                            if (installnameDepence == "")
                            {
                                installnameDepence = inventoryitem[i].name;
                                chain.itemlist.InstallItem(i);
                            }
                            else
                            {
                                for (int j = 0; j < InventoryItemLength; j++)//단, 다른 아이템 장착 시 장착된 아이템 자동 해제
                                {
                                    if (inventoryitem[j].name.Contains("E") && (inventoryitem[j].name.Contains("갑옷") || inventoryitem[j].name.Contains("망토")))
                                    {
                                        inventoryitem[j].name = installnameDepence;
                                        installnameDepence = inventoryitem[i].name;
                                        chain.itemlist.RemoveItem(j);
                                        chain.itemlist.InstallItem(i);
                                        break;
                                    }
                                }
                            }
                        }
                        else//무기
                        {
                            if (installnameStrike == "")
                            {
                                installnameStrike = inventoryitem[i].name;
                                chain.itemlist.InstallItem(i);
                            }
                            else
                            {
                                for (int j = 0; j < InventoryItemLength; j++)//단, 다른 아이템 장착 시 장착된 아이템 자동 해제
                                {
                                    if (inventoryitem[j].name.Contains("E") && (!inventoryitem[j].name.Contains("갑옷") && !inventoryitem[j].name.Contains("망토")))
                                    {
                                        inventoryitem[j].name = installnameStrike;
                                        installnameStrike = inventoryitem[i].name;
                                        chain.itemlist.RemoveItem(j);
                                        chain.itemlist.InstallItem(i);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Console.Clear();
                    InventoryItemManage();
                }
            }
        }

        class Store
        {
            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }

            public void ViewStore()
            {
                Item[] sellitems = chain.itemlist.inventoryitems;

                Console.WriteLine("\n상점\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{chain.state.Gold}G");
                Console.WriteLine("\n[아이템 목록]\n");
                chain.itemlist.List("-");
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
                    if (sellitems[0] == null)

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
                chain.itemlist.Make(6);
            }

            void StoreBuy()
            {
                Console.WriteLine("\n상점 - 아이템 구매\n");
                Console.WriteLine($"[보유 골드]\n{chain.state.Gold}G\n");
                Console.WriteLine("[아이템 목록]\n");
                chain.itemlist.List();
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
                    int selectgold = chain.itemlist.items[selectItem - 1].goldInt;
                    if (chain.itemlist.items[selectItem - 1].gold == "구매완료")
                    {
                        Console.Clear() ;
                        Console.WriteLine("이미 구매하신 상품입니다.");
                        StoreBuy();
                    }
                    else if (selectgold > chain.state.Gold)
                    {
                        Console.Clear();
                        Console.WriteLine("Gold가 부족합니다.");
                        StoreBuy();
                    }
                    else if (selectgold <= chain.state.Gold)
                    {
                        chain.state.Gold = chain.state.Gold - selectgold;

                        Item buyitem = new Item();
                        buyitem.name = chain.itemlist.items[selectItem - 1].name;
                        buyitem.stats = chain.itemlist.items[selectItem - 1].stats;
                        buyitem.comment = chain.itemlist.items[selectItem - 1].comment;
                        buyitem.goldInt = chain.itemlist.items[selectItem - 1].goldInt * 85;
                        buyitem.goldInt = buyitem.goldInt / 100;
                        buyitem.gold = buyitem.goldInt.ToString() + "G";

                        chain.itemlist.InventoryItem(buyitem);
                        chain.itemlist.items[selectItem - 1].gold = "구매완료";
                        Console.Clear();
                        StoreBuy();
                    }
                }
            }

            void StoreSell()
            {
                Item[] sellitems = chain.itemlist.inventoryitems;

                Console.WriteLine("\n상점 - 아이템 판매\n");
                Console.WriteLine($"[보유 골드]\n{chain.state.Gold}G\n");
                Console.WriteLine("[아이템 목록]\n");
                chain.itemlist.InventoryItemSellList();
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                int InventoryItemLength = sellitems.Length;
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
                    if (sellitems[i].name.Contains("E"))
                    {
                        chain.itemlist.RemoveItem(i);
                        if (sellitems[i].name.Contains("갑옷") || sellitems[i].name.Contains("망토"))
                        {
                            chain.inventory.installnameDepence = "";
                        }
                        else
                        {
                            chain.inventory.installnameStrike = "";
                        }
                    }
                    chain.state.Gold += sellitems[i].goldInt;
                    sellitems[i] = null;
                    for (i = n - 1; i < InventoryItemLength; i++)
                    {
                        if (sellitems[i + 1] != null)
                        {
                            sellitems[i] = sellitems[i + 1];
                            sellitems[i + 1] = null;
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
            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }

            public Item[] items;
            public Item[] inventoryitems = new Item[10];

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

            public void InventoryItem(Item item)
            {
                int i = 0;
                while (true)
                {
                    if (inventoryitems[i]==null)
                    {
                        inventoryitems[i] = item;
                        break;
                    }
                    else if (i > 9)
                    {
                        Console.WriteLine("가득 찼습니다.");
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            public void InventoryItemList(string plus)
            {
                int itemLength = inventoryitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (inventoryitems[i] == null)
                    {
                        break;
                    }
                    else if (inventoryitems[i].name.Contains("갑옷") || inventoryitems[i].name.Contains("망토"))
                    {
                        Console.WriteLine($"{plus} {inventoryitems[i].name}\t| 방어력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($"{plus} {inventoryitems[i].name}\t| 공격력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
                    }
                }
            }
            public void InventoryItemList()
            {
                int itemLength = inventoryitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (inventoryitems[i] == null)
                    {
                        break;
                    }
                    else if (inventoryitems[i].name.Contains("갑옷") || inventoryitems[i].name.Contains("망토"))
                    {
                        Console.WriteLine($" {i+1}. {inventoryitems[i].name}\t| 방어력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i+1}. {inventoryitems[i].name}\t| 공격력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
                    }
                }
            }

            public void InventoryItemSellList()
            {
                int itemLength = inventoryitems.Length;
                for (int i = 0; i < itemLength; i++)
                {
                    if (inventoryitems[i] == null)
                    {
                        break;
                    }
                    else if (inventoryitems[i].name.Contains("갑옷") || inventoryitems[i].name.Contains("망토"))
                    {
                        Console.WriteLine($" {i + 1}. {inventoryitems[i].name}\t| 방어력 +{inventoryitems[i].stats} | {inventoryitems[i].gold} | {inventoryitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i + 1}. {inventoryitems[i].name}\t| 공격력 +{inventoryitems[i].stats} | {inventoryitems[i].gold} | {inventoryitems[i].comment}");
                    }
                }
            }

            public void RemoveItem(int i)
            {
                if (inventoryitems[i].name.Contains("갑옷") || inventoryitems[i].name.Contains("망토"))
                {
                    chain.state.checkDepence = false;
                    chain.state.Depence -= chain.state.PlusDepence;
                    chain.state.PlusDepence = 0;
                }
                else
                {
                    chain.state.checkStrike = false;
                    chain.state.Strike -= chain.state.PlusStrike;
                    chain.state.PlusStrike = 0;
                }
            }

            public void InstallItem(int i)
            {
                inventoryitems[i].name = "[E]" + inventoryitems[i].name;

                if (inventoryitems[i].name.Contains("갑옷") || inventoryitems[i].name.Contains("망토"))
                {
                    chain.state.PlusDepence = inventoryitems[i].stats;
                    chain.state.Depence += chain.state.PlusDepence;
                    chain.state.checkDepence = true;
                }
                else
                {
                    chain.state.PlusStrike = inventoryitems[i].stats;
                    chain.state.Strike += chain.state.PlusStrike;
                    chain.state.checkStrike = true;
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

