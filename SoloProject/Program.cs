using System;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Claims;
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
            Dungeon dungeon = chain.getDungeon();
            Heal heal = chain.getHeal();

            //서로 연결시켜주기
            menu.getClass(chain);
            inventory.getClass(chain);
            store.getClass(chain);
            itemList.getClass(chain);
            dungeon.getClass(chain);
            heal.getClass(chain);

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
            public Dungeon dungeon = new Dungeon();
            public Heal heal = new Heal();

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
            public Dungeon getDungeon()
            {
                return dungeon;
            }
            public Heal getHeal()
            {
                return heal;
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
                    Console.WriteLine("0. 게임 끝내기\n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n");
                }
                else
                {
                    Console.WriteLine($"{chain.state.name}님 환영합니다. 여긴 스파르타 마을입니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                    Console.WriteLine("0. 게임 끝내기\n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n");
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
                    case 4:
                        //던전
                        Console.Clear();
                        chain.dungeon.ViewDungeon();
                        break;
                    case 5:
                        //휴식
                        Console.Clear();
                        chain.heal.ViewHeal();
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
            public float Strike { get; set; }
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
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                if (Lv < 10)
                {
                    Console.WriteLine("LV : 0" + Lv);
                }
                else Console.WriteLine("LV : " + Lv);
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
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
                        ViewState();
                        break;
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

            public string? installnameStrike = null;
            public string? installnameDepence = null;

            public void ViewInventory()
            {
                Console.WriteLine("\n인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]\n");
                chain.itemlist.InventoryItemList("-");
                Console.WriteLine("\n1. 장착관리\n");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
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
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        ViewInventory();
                        break;
                }
            }

            void InventoryItemManage()
            {
                Item[] inventoryitem = chain.itemlist.inventoryitems;

                Console.WriteLine("\n인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
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
                            installnameDepence = null;
                        }
                        else
                        {
                            inventoryitem[i].name = installnameStrike;
                            installnameStrike = null;
                        }
                        chain.itemlist.RemoveItem(i);
                    }
                    else //아이템 장착
                    {
                        if (inventoryitem[i].name.Contains("갑옷") || inventoryitem[i].name.Contains("망토"))//방어구
                        {
                            if (installnameDepence == null)
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
                            if (installnameStrike == null)
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

                Console.WriteLine("\n상점");
                Console.WriteLine("필요한 아이템을 얻거나 가지고 있는 아이템을 팔 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{chain.state.Gold}G");
                Console.WriteLine("\n[아이템 목록]\n");
                chain.itemlist.List("-");
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        StoreBuy();
                        break;
                    case 2:
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
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        ViewStore();
                        break;
                }
            }

            public void ResetStoreItem()
            {
                chain.itemlist.Make(6);
            }

            void StoreBuy()
            {
                Console.WriteLine("\n상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
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
                        Item buyitem = new Item(5,3,1);

                        if (chain.itemlist.inventoryitems.Length < 10)
                        {
                            chain.state.Gold = chain.state.Gold - selectgold;
                            buyitem.name = chain.itemlist.items[selectItem - 1].name;
                            buyitem.stats = chain.itemlist.items[selectItem - 1].stats;
                            buyitem.comment = chain.itemlist.items[selectItem - 1].comment;
                            buyitem.goldInt = chain.itemlist.items[selectItem - 1].goldInt * 85;
                            buyitem.goldInt = buyitem.goldInt / 100;
                            buyitem.gold = buyitem.goldInt.ToString() + "G";

                            chain.itemlist.InventoryItem(buyitem);
                            chain.itemlist.items[selectItem - 1].gold = "구매완료";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("인벤토리가 가득 차 구매하실 수 없습니다.");
                            Console.WriteLine("인벤토리 아이템을 판매하시고 구매하시기 바랍니다.\n");
                            ViewStore();
                        }
                        
                        Console.Clear();
                        StoreBuy();
                    }
                }
            }

            void StoreSell()
            {
                Item[] sellitems = chain.itemlist.inventoryitems;

                Console.WriteLine("\n상점 - 아이템 판매");
                Console.WriteLine("필요 없는 아이템을 팔 수 있는 상점입니다.\n");
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
                            chain.inventory.installnameDepence = null;
                        }
                        else
                        {
                            chain.inventory.installnameStrike = null;
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

        class Dungeon
        {
            Random random = new Random();

            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }

            int clearCount = 0;

            public void ViewDungeon()
            {
                Console.WriteLine("\n던전 - 난이도 선택");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("[공격력]");
                Console.WriteLine($"   {chain.state.Strike}");
                Console.WriteLine("[방어력]");
                if (chain.state.Depence < 10)
                {
                    Console.WriteLine($"    {chain.state.Depence}");
                }
                else Console.WriteLine($"   {chain.state.Depence}");
                Console.WriteLine(" [체력]");
                Console.WriteLine($"   {chain.state.HP}");

                Console.WriteLine("\n1. 쉬움");
                Console.WriteLine("2. 보통");
                Console.WriteLine("3. 어려움");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        EasyDungeon();
                        break;
                    case 2:
                        Console.Clear();
                        NomalDungeon();
                        break;
                    case 3:
                        Console.Clear();
                        HardDungeon();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        ViewDungeon();
                        break;
                }
            }

            void EasyDungeon()
            {
                int needStrike = 10 + chain.state.Lv;
                int needDepence = 5 + chain.state.Lv;
                int needHP = 20 + random.Next(6);
                int clearpercent = ClearPercent(0, needStrike, needDepence) * 10;
                Console.WriteLine("\n 던전 - [쉬움]\n");
                Console.WriteLine("난이도 쉬움 던전입니다. \n");
                Console.WriteLine("[공격력]  [권장 공격력]");
                Console.WriteLine($"   {chain.state.Strike}\t      {needStrike}");
                Console.WriteLine("[방어력]  [권장 방어력]");
                if (chain.state.Depence < 10)
                {
                    Console.WriteLine($"    {chain.state.Depence}\t       {needDepence}");
                }
                else Console.WriteLine($"   {chain.state.Depence}\t      {needDepence}");
                Console.WriteLine(" [체력]\t [필요한 체력]");
                Console.WriteLine($"  {chain.state.HP}\t      {needHP}");
                Console.WriteLine($"\n현재 클리어 확률 : {clearpercent}%");
                Console.WriteLine("\n1. 입장");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        ViewDungeon();
                        break;
                    case 1:
                        Console.Clear();
                        if (chain.state.HP <= needHP)
                        {
                            Console.Clear();
                            Console.WriteLine("\n체력이 부족해 입장하실 수 없습니다.\n던전을 나갑니다.");
                            Console.ReadLine();
                        }
                        else
                        {
                            EnterDungeon(0, clearpercent, needHP);
                            ViewDungeon();
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        EasyDungeon();
                        break;
                }
            }

            void NomalDungeon()
            {
                int needStrike = 12 + chain.state.Lv + random.Next(2);
                int needDepence = 7 + chain.state.Lv + random.Next(2);
                int needHP = 40 + random.Next(11);
                int clearpercent = ClearPercent(1, needStrike, needDepence) * 10;
                Console.WriteLine("\n 던전 - [보통]\n");
                Console.WriteLine("[공격력]  [권장 공격력]");
                Console.WriteLine($"   {chain.state.Strike}\t      {needStrike}");
                Console.WriteLine("[방어력]  [권장 방어력]");
                if (chain.state.Depence < 10)
                {
                    Console.WriteLine($"    {chain.state.Depence}\t       {needDepence}");
                }
                else Console.WriteLine($"   {chain.state.Depence}\t      {needDepence}");
                Console.WriteLine(" [체력]\t [필요한 체력]");
                Console.WriteLine($"   {chain.state.HP}\t      {needHP}");
                Console.WriteLine($"\n현재 클리어 확률 : {clearpercent}%");
                Console.WriteLine("\n1. 입장");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        ViewDungeon();
                        break;
                    case 1:
                        Console.Clear();
                        if (chain.state.HP <= needHP)
                        {
                            Console.Clear();
                            Console.WriteLine("\n체력이 부족해 입장하실 수 없습니다.\n던전을 나갑니다.");
                            Console.ReadLine();
                        }
                        else
                        {
                            EnterDungeon(1, clearpercent, needHP);
                            ViewDungeon();
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        NomalDungeon();
                        break;
                }
            }
            void HardDungeon()
            {
                int needStrike = 15 + chain.state.Lv + random.Next(1, 5);
                int needDepence = 10 + chain.state.Lv + random.Next(1, 5);
                int needHP = 60 + random.Next(11);
                int clearpercent = ClearPercent(2, needStrike, needDepence) * 10;
                Console.WriteLine("\n 던전 - [어려움]\n");
                Console.WriteLine("[공격력]  [권장 공격력]");
                Console.WriteLine($"   {chain.state.Strike}\t      {needStrike}");
                Console.WriteLine("[방어력]  [권장 방어력]");
                if (chain.state.Depence < 10)
                {
                    Console.WriteLine($"    {chain.state.Depence}\t       {needDepence}");
                }
                else Console.WriteLine($"   {chain.state.Depence}\t      {needDepence}");
                Console.WriteLine(" [체력]\t [필요한 체력]");
                Console.WriteLine($"  {chain.state.HP}\t      {needHP}");
                Console.WriteLine($"\n현재 클리어 확률 : {clearpercent}%");
                Console.WriteLine("\n1. 입장");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        ViewDungeon();
                        break;
                    case 1:
                        Console.Clear();
                        if (chain.state.HP <= needHP)
                        {
                            Console.Clear();
                            Console.WriteLine("\n체력이 부족해 입장하실 수 없습니다.\n던전을 나갑니다.");
                            Console.ReadLine();
                        }
                        else
                        {
                            EnterDungeon(2, clearpercent, needHP);
                            ViewDungeon();
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        HardDungeon();
                        break;
                }
            }

            int ClearPercent(int level, int Strike, int Depence)
            {
                int clearPercent = 6;
                int plusPercent = 1;
                int needStrike = Strike;
                int needDepence = Depence;

                float strike = chain.state.Strike;
                int depence = chain.state.Depence;

                switch (level)
                {
                    case 0:
                        if (strike >= needStrike)
                        {
                            clearPercent += plusPercent;
                            if (strike - needStrike > 5)
                            {
                                clearPercent += plusPercent;
                            }
                        }
                        if (depence >= needDepence)
                        {
                            clearPercent += plusPercent;
                            if (depence - needDepence > 5)
                            {
                                clearPercent += plusPercent;
                            }
                        }
                        break;
                    case 1:
                        if (strike >= needStrike)
                        {
                            clearPercent += plusPercent;
                            if (strike - needStrike > 5)
                            {
                                clearPercent += plusPercent;
                            }
                            else if (needStrike - strike > 5)
                            {
                                clearPercent -= plusPercent;
                            }
                        }
                        if (depence >= needDepence)
                        {
                            clearPercent += plusPercent;
                            if (depence - needDepence > 5)
                            {
                                clearPercent += plusPercent;
                            }
                            else if (needDepence - depence > 5)
                            {
                                clearPercent -= plusPercent;
                            }
                        }
                        break;
                    case 2:
                        if (strike >= needStrike)
                        {
                            clearPercent += plusPercent;
                            if (strike - needStrike > 5)
                            {
                                clearPercent += plusPercent;
                            }
                            else if (needStrike - strike > 2)
                            {
                                clearPercent -= plusPercent;
                                if (needStrike - strike > 5)
                                {
                                    clearPercent -= plusPercent;
                                }
                            }
                        }
                        if (depence >= needDepence)
                        {
                            clearPercent += plusPercent;
                            if (depence - needDepence > 5)
                            {
                                clearPercent += plusPercent;
                            }
                            else if (needDepence - depence > 2)
                            {
                                clearPercent -= plusPercent;
                                if (needDepence - depence > 5)
                                {
                                    clearPercent -= plusPercent;
                                }
                            }
                        }
                        break;
                }
                return clearPercent;
            }

            void EnterDungeon(int level, int clearpercent, int hp)
            {
                int percent = random.Next(0, 10);
                int needHp = hp;
                int clearGold;

                switch (level)
                {
                    case 0:
                        if (percent < clearpercent)
                        {
                            clearGold = 500 + (random.Next(2) + chain.state.Lv) * 100;
                            Clear(10, clearGold, 1);
                            chain.state.HP -= 10;
                            chain.state.Gold += clearGold;
                        }
                        else
                        {
                            NonClear(needHp);
                            chain.state.HP -= hp;
                        }
                        break;
                    case 1:
                        if (percent < clearpercent)
                        {
                            clearGold = 700 + (random.Next(5) + chain.state.Lv) * 100;
                            Clear(20, clearGold, 2);
                            chain.state.HP -= 20;
                            chain.state.Gold += clearGold;
                        }
                        else
                        {
                            NonClear(needHp);
                            chain.state.HP -= hp;
                        }
                        break;
                    case 2:
                        if (percent < clearpercent)
                        {
                            clearGold = 1000 + (random.Next(10) + chain.state.Lv) * 100;
                            Clear(40, clearGold, 3);
                            chain.state.HP -= 40;
                            chain.state.Gold += clearGold;
                        }
                        else
                        {
                            NonClear(needHp);
                            chain.state.HP -= hp;
                        }
                        break;
                }

                void Clear(int hp, int gold, int difficult)//확인
                {
                    Console.WriteLine("\n던전 클리어\n");
                    Console.WriteLine("[탐험 결과]\n");
                    Console.WriteLine($"체력 {chain.state.HP} -> {chain.state.HP - hp}");
                    Console.WriteLine($"골드 {chain.state.Gold} -> {chain.state.Gold + gold}\n");
                    ClearCount(difficult);
                    Console.WriteLine("\n0. 나가기\n");
                    int n = Number();
                    switch (n)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 입력입니다.\n");
                            Clear(hp, gold, difficult);
                            break;
                    }
                }

                void NonClear(int hp)
                {
                    Console.WriteLine("\n던전 클리어 실패\n");
                    Console.WriteLine("[탐험 결과]\n");
                    Console.WriteLine($"체력 {chain.state.HP} -> {chain.state.HP - hp}");
                    Console.WriteLine("\n0. 나가기\n");
                    int n = Number();
                    switch (n)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 입력입니다.\n");
                            NonClear(hp);
                            break;
                    }
                }

                void ClearCount(int difficult)
                {
                    int levelup = 10 * chain.state.Lv;

                    clearCount += difficult;
                    switch (difficult)
                    {
                        case 1:
                            Item easydropitem = new Item(7, 2.5f, 0.5f);
                            easydropitem.goldInt *= 85;
                            easydropitem.goldInt /= 100;
                            easydropitem.gold = $"{easydropitem.goldInt}G";
                            Console.WriteLine("\n아이템을 얻었습니다.\n");
                            Console.WriteLine($"[ {easydropitem.name}  | + {easydropitem.stats}  |  {easydropitem.comment} ]");
                            chain.itemlist.InventoryItem(easydropitem);
                            break;
                        case 2:
                            Item nomaldropitem = new Item(3, 4, 2);
                            nomaldropitem.goldInt *= 85;
                            nomaldropitem.goldInt /= 100;
                            nomaldropitem.gold = $"{nomaldropitem.goldInt}G";
                                Console.WriteLine("\n아이템을 얻었습니다.\n");
                                Console.WriteLine($"[{nomaldropitem.name} | +{nomaldropitem.stats} | {nomaldropitem.comment}]");
                            chain.itemlist.InventoryItem(nomaldropitem);
                            break;
                        case 3:
                            Item harddropitem = new Item(0, 2, 6);
                            harddropitem.goldInt *= 85;
                            harddropitem.goldInt /= 100;
                            harddropitem.gold = $"{harddropitem.goldInt}G";
                            Console.WriteLine("\n아이템을 얻었습니다.\n");
                            Console.WriteLine($"[{harddropitem.name} | +{harddropitem.stats} | {harddropitem.comment}]");
                            chain.itemlist.InventoryItem(harddropitem);
                            break;
                    }
                    if (clearCount > levelup)
                    {
                        LevelUp();
                        chain.state.Lv++;
                        chain.state.Strike += 0.5f;
                        chain.state.Depence += 1;
                        chain.state.HP += 10;
                        clearCount = 0;
                    }
                }

                void LevelUp()
                {
                    Console.WriteLine("\n축하합니다! 레벨이 오르셨습니다!\n");
                    Console.WriteLine($"LV : {chain.state.Lv} -> LV : {chain.state.Lv + 1}");
                    chain.state.Lv++;
                    chain.state.Strike += 0.5f;
                    chain.state.Depence += 1;
                    chain.state.HP += 10;
                    clearCount = 0;
                }
            }
        }

        class Heal
        {
            ClassChain chain;
            public void getClass(ClassChain classchain)
            {
                chain = classchain;
            }
            public void ViewHeal()
            {
                Console.WriteLine("\n휴식하기");
                Console.WriteLine("500G를 내면 체력을 회복할 수 있습니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{chain.state.Gold}G");
                Console.WriteLine("\n1. 휴식하기");
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        CompleteHeal();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        ViewHeal();
                        break;
                }
            }

            void CompleteHeal()
            {
                int hp = 100;
                Console.WriteLine("휴식이 완료되었습니다.");
                Console.WriteLine($"{chain.state.HP} -> {hp}");
                chain.state.HP = hp;
                chain.state.Gold -= 500;
                Console.WriteLine("\n0. 나가기\n");
                int n = Number();
                switch (n)
                {
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.WriteLine("체력이 회복되었으므로 나갑니다.\n");
                        break;
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
                    items[i] = new Item(6-chain.state.Lv/3, 3 - chain.state.Lv/2, 1 + chain.state.Lv/2);
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
                        Console.WriteLine($" {i+1}.{items[i].name}\t| 방어력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i+1}.{items[i].name}\t| 공격력 +{items[i].stats} | {items[i].gold} | {items[i].comment}");
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
                        Console.WriteLine("인벤토리가 가득 찼습니다.");
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
                        Console.WriteLine($" {i+1}.{inventoryitems[i].name}\t| 방어력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i+1}.{inventoryitems[i].name}\t| 공격력 +{inventoryitems[i].stats} | {inventoryitems[i].comment}");
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
                        Console.WriteLine($" {i + 1}.{inventoryitems[i].name}\t| 방어력 +{inventoryitems[i].stats} | {inventoryitems[i].gold} | {inventoryitems[i].comment}");
                    }
                    else
                    {
                        Console.WriteLine($" {i + 1}.{inventoryitems[i].name}\t| 공격력 +{inventoryitems[i].stats} | {inventoryitems[i].gold} | {inventoryitems[i].comment}");
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
            private string[] firstName = { "나무 ", "낡은 ", "수련자 ", "청동", "무쇠", "스파트라의 " };
            private string[] secondName = { "갑옷", "망토", "검", "창", "도끼"};
            private string firstComment = "";
            private string SecondComment = "";

            public Item(float a, float b, float c)
            {
                MakeName(a,b,c);
                MakeStats();
                MakeGold();
                MakeCommand();
            }

            void MakeName(float a, float b, float c) 
            {
                int n = random1.Next(10);
                int nn = random1.Next(10);
                int nnn = random1.Next(5);

                int secondLength = secondName.Length;

                if (n < a)//나무, 낡은 아이템 드랍
                {
                    if (nn < 2)//갑옷, 망토 아이템만 드랍
                    {
                        name = firstName[nnn % 2] + secondName[nnn % 2];
                    }
                    else { name = firstName[nnn % 2] + secondName[nnn % secondLength]; }

                }
                else if (n < a+b)//수련자, 청동 아이템 드랍
                {
                    if (nn < 2)//갑옷, 망토 아이템만 드랍
                    {
                        name = firstName[nnn % 2 + 2] + secondName[nnn % 2];
                    }
                    else { name = firstName[nnn % 2 + 2] + secondName[nnn % secondLength]; }
                }
                else if (n < a+b+c)//무쇠 아이템 드랍
                {
                    if (nn < 2)//갑옷, 망토 아이템만 드랍
                    {
                        name = firstName[4] + secondName[nnn % 2];
                    }
                    else { name = firstName[4] + secondName[nnn % secondLength]; }
                }
                else //스파르타 아이템 드랍
                {
                    if (nn < 2)//갑옷, 망토 아이템만 드랍
                    {
                        name = firstName[5] + secondName[nnn % 2];
                    }
                    else { name = firstName[5] + secondName[nnn % secondLength]; }
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

