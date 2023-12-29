using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoloProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            
            //사용하는 전역변수 추가, 현재 지역변수로 사용하는 것들 중에서 전역변수로 나갈 수 있음.
            int selectNumber; //0. 나가기를 입력할 때마다 메인 화면으로 이동할 수 있도록 설정 및 바뀐 화면마다 고르는 숫자에 따른 선택 화변 변경.
            string[] item = null; //보유 중인 아이템 목록. 크기가 가변적이여야하기 때문에 List나 Dictionary 필요.

            //상점에서 판매하는 아이템 목록. 레벨이 올라가거나 던전을 클리어할 때마다 아이템 추가 생성 필요. Class로 뺄 수 있을 확률 높음.
            string[] storeItemName = new string[6]; 
            int[] storeItemStats = new int[6];
            int[] storeItemGold = new int[6];

            //캐릭터 상태 변수
            int Strike = 10;
            int Depence = 5;
            int HP = 100;
            int Gold = 1500;
            int Lv = 1;
            
            Menu2(Menu());
            
            //함수 만드는 법을 배워야함. 임시로 여기에 넣어둠.
            int Menu()
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                return Number();
            }
            int Number()
            {
                int n;
                Console.Write("원하시는 행동을 입력해주세요.\n>>");

                //숫자가 아닌 문자를 입력했을 때 잘못입력했다는 알림 나오게 추가.
                bool check = int.TryParse(Console.ReadLine(), out n);
                int number = check ? n : -1;
                return number;
            }
            void Menu2(int n)//case 안에 있는 내용들 함수로 만들기, 0.나가기만 있을 때 함수도 따로 만들기.
            {
                switch (n)
                {
                    case 1:
                        //상태보기
                        State(n);
                        break;
                    case 2:
                        //인벤토리
                        Inventory(n);
                        break;
                    case 3:
                        //상점
                        Store(n);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Menu2(Menu());
                        //위에 콘솔 내용 삭제.
                        break;
                }
            }
            void outMenu(int number)
            {
                int n;
                Console.WriteLine("0. 나가기\n");
                n = Number();
                if (n == 0)
                {
                    Menu2(Menu());
                }
                else
                {
                    //number번호에 따라 되돌아가는 화면 바꿀 수 있게 설정
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Menu2(number);
                }
            }
            
            void State(int number)
            {
                Console.WriteLine("상태보기");
                Console.WriteLine();
                Console.WriteLine("Lv. 0{0}", Lv); //10이 넘어갔을 때 값 변경 추가 필요
                Console.WriteLine("Chad ( 전사 )"); //직업을 시작 전에 고를 수 잇도록 추가하면 좋을거같음
                Console.WriteLine("공격력 : {0}", Strike); //무기에 따른 추가 값 따로 설정 어떻게 할지 고민
                Console.WriteLine("방어력 : {0}", Depence); //방어구에 따른 추가 값 따로 설정 어떻게 할지 고민
                Console.WriteLine("체  력 : {0}", HP);
                Console.WriteLine("Gold : {0}\n", Gold);

                outMenu(number);
            }
            
            void Inventory(int number)
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine();
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("0.나가기\n");
                int n = Number();
                if (n == 0)
                {
                    Menu2(Menu());
                }
                else if (n == 1)
                {
                    if (item == null)
                    {
                        Console.WriteLine("보유 중인 아이템이 없습니다.\n");
                        Inventory(n);
                    }
                    else
                    {
                        //아이템 리스트 보여주기
                        //0. 나가기 하면 바로 메뉴로 나가게 하는게 맞을까?
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Menu2(2);
                }
            }

            void Store(int number)
            {
                Console.WriteLine("상점");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n{0}G\n", Gold);
                StoreItemCreate(); //상점 리세가 가능해서 상점 리세를 가능하게 할 것인지 아니면 안되게 할 것인지 골라야함.
                StoreItem();
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");
                int n = Number();
                if (n == 0)
                {
                    Menu2(Menu());
                }
                else if (n == 1)
                {
                    Console.WriteLine("[보유 골드]\n{0}G\n", Gold);
                    StoreItem();
                    //아이템을 선택해서 구매하면 gold부분을 구매완료로 바꿀 것
                    //아이템 구매완료일 경우 선택해도 또 구매 못하게 만들 것
                    //아이템 구매한 것은 따로 저장해둘 것. 판매했을 때 대비
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Menu2(3);
                }
            }

            void StoreItemCreate()//상점아이템 생성
            {
                string[] Name = new string[6];
                int[] Stats = new int[6];
                int[] Gold = new int[6];
                for (int i = 0; i < Name.Length; i++)
                {
                    int nameNumber = random.Next(1, 4);
                    if (nameNumber == 1)
                    {
                        Name[i] = "창";
                    }
                    else if (nameNumber == 2)
                    {
                        Name[i] = "검";
                    }
                    else
                        Name[i] = "갑옷";
                }
                for (int i = 0; i < Stats.Length; i++)
                {
                    Stats[i] = random.Next(1 + Lv, 5 + Lv);
                }
                for (int i = 0; i < storeItemGold.Length; i++)
                {
                    Gold[i] = random.Next(100 + Lv * random.Next(0, 10), 1000 + Lv * random.Next(0, 100));
                }
                foreach (string n in Name)
                {
                    storeItemName = Name;
                }
                foreach(int i in Gold)
                {
                    storeItemGold = Gold;
                }
                foreach (int i in Stats)
                {
                    storeItemStats = Stats;
                }
                
            }
            void StoreItem()
            {
                for (int i = 0; i < storeItemStats.Length; i++)
                {
                    if (storeItemName[i] == "창" || storeItemName[i] == "검")
                    {
                        Console.WriteLine("{0} {1}\t| 공격력 +{2} | {3}G", i, storeItemName[i], storeItemStats[i], storeItemGold[i]);
                    }
                    else { Console.WriteLine("{0} {1}\t| 방어력 +{2} | {3}G", i, storeItemName[i], storeItemStats[i], storeItemGold[i]); }
                }
            }
        }
    }
}
