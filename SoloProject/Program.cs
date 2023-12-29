using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoloProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //사용하는 전역변수 추가

            int selectNumber; //0. 나가기를 입력할 때마다 메인 화면으로 이동할 수 있도록 설정 및 바뀐 화면마다 고르는 숫자에 따른 선택 화변 변경.
            string[] item = null; //보유 중인 아이템 목록.

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
                        Console.WriteLine("상태보기");
                        Console.WriteLine();
                        Console.WriteLine("0.나가기\n");
                        n=Number();
                        if (n == 0)
                        {
                            Menu2(Menu());
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.\n");
                            n = 1;
                            Menu2(n);
                        }
                        break;
                    case 2:
                        Inventory(n);
                        break;
                    case 3:
                        Console.WriteLine("상점");
                        Console.WriteLine();
                        Console.WriteLine("0.나가기\n");
                        n = Number();
                        if (n == 0)
                        {
                            Menu2(Menu());
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.\n");
                            n = 3;
                            Menu2(n);
                        }
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Menu2(Menu());
                        //위에 콘솔 내용 삭제.
                        break;
                }
            }
            void outMenu()
            {
                int n;
                Console.WriteLine("0.나가기\n");
                n = Number();
                if (n == 0)
                {
                    Menu2(Menu());
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    //잘못된 입력입니다. 하고 그 전 페이지로 돌아가기 위해서 어떻게 해야할지 생각 필요.
                }
            }
            
            void Inventory(int number)
            {
                int n;
                Console.WriteLine("인벤토리");
                Console.WriteLine();
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("0.나가기\n");
                n = Number();
                if (n == 0) 
                {
                    Menu2(Menu());
                }
                else if (n == 1)
                {
                    if(item == null)
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
                    n = number;
                    Menu2(n);
                }
            }
        }
    }
}
