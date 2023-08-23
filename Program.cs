using System.Numerics;
using static Textgame.Program;

namespace Textgame
{
    internal class Program
    {
        private static Character player;
        private static Item ironarmor;
        private static Item oldsword;
        private static Item leatherboots;
        private static List<Item> items;

        private static int equipatk; // 착용한 장비의 총 공격력
        private static int equipdef; // 착용한 장비의 총 방어력

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            ironarmor = new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5, false);
            oldsword = new Item("낡은 검","쉽게 볼 수 있는 낡은 검입니다.", 2, 0, false);
            leatherboots = new Item("가죽장화", "튼튼하진 않지만 멋진 장화입니다.", 0, 1, false);

            items = new List<Item>();
            items.Add(ironarmor);
            items.Add(oldsword);
            items.Add(leatherboots);
            
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 : {player.Atk}{CheckEquipStats(equipatk)}");
            Console.WriteLine($"방어력 : {player.Def}{CheckEquipStats(equipdef)}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");


            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            ShowItemList(items);

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    Displayequipment();
                    break;
            }
        }

        static void Displayequipment()
        {
            // 인벤토리 화면 + 번호 붙이기

            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            ShowEquipList(items);

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, items.Count);
           
            for (int i = 0; i < items.Count; i++)
            {
                if (input == 0)
                {
                    DisplayGameIntro();
                    break;
                }

                if (input - 1 == i)
                {
                    if (!items[i].Equipflag)
                    {
                        items[i].Equipflag = true;
                        ReflectStats(items[i]);

                    }
                    else
                    {
                        items[i].Equipflag = false;
                        ReflectStats(items[i]);
                    }

                    Displayequipment();
                    break;
                }
               
            }

        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void CheckItemStats(Item item)
        {
            List<string> statsname;

            if (item.Atk != 0)
            {
                Console.Write($"공격력 +{item.Def} | ");
            }
            if (item.Def != 0)
            {
                Console.Write($"방어력 +{item.Def} | ");
            }
        }

        static void ReflectStats(Item item)
        {
            if (item.Equipflag)
            {
                player.Atk += item.Atk;
                player.Def += item.Def;

                equipatk += item.Atk;
                equipdef += item.Def;

            } else
            {
                player.Atk -= item.Atk;
                player.Def -= item.Def;

                equipatk -= item.Atk;
                equipdef -= item.Def;
            }
        }

        static string CheckEquipStats(int equipstat)
        {
            if (equipstat != 0) return $" +({equipstat.ToString()})";
            else return "";
        }

        static void ShowItemList(List<Item> items)
        {
            foreach (Item item in items)
            {
                Console.Write("- ");
                Console.Write(CheckEquipflaq(item.Equipflag));
                Console.Write($" {item.Name}      | ");

                CheckItemStats(item);

                Console.WriteLine(item.Explain);
            }

        }

        static void ShowEquipList(List<Item> items)
        {
            int i = 1;
            foreach (Item item in items)
            {
                Console.Write("- ");
                Console.Write(i.ToString() + " ");
                Console.Write(CheckEquipflaq(item.Equipflag));
                Console.Write($" {item.Name}      | ");

                CheckItemStats(item);

                Console.WriteLine(item.Explain);
                
                i++;
            }
        }

        static string CheckEquipflaq(bool equipflag)
        {
            string eflag = "   ";

            if (equipflag == true) eflag = "[E]";

            return eflag;
        }

        public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

        public class Item
        {
            public string Name { get; }
            public string Explain { get; }
            public int Atk { get; }
            public int Def { get; }
            public bool Equipflag { get; set; }

            //// 나중에 추가되면 좋을 것 같은 사항들
            //public string Job { get; }
            //public int Level { get; }
            //public int Gold { get; }

            public Item(string name, string explain, int atk, int def, bool equipflag)
            {
                Name = name;
                Explain = explain;
                Atk = atk;
                Def = def;
                Equipflag = equipflag;
            }
        }
    }
}