using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{

    class Simulyator_ducks
    {

        static void Main(string[] args)
        {


            Random random = new Random();
            int ducks = 44;
            int id = 1;
            int hunt_days = 8;
            int count_lakes = 4;
            

            var lake = new Lake[5];
            lake[0] = new Lake("Байкал");
            lake[1] = new Lake("Кратерное озеро");
            lake[2] = new Lake("Челан");
            lake[3] = new Lake("Ньяса");

            lake[4] = new Farm("BOHIFUT");


            for (int i = 0; i < ducks; i++)
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        {
                            if (random.Next(0, 2) == 0)
                            {
                                lake[0].add_ducks(new Chernety1(ref id));
                            }
                            else
                            {
                                lake[0].add_ducks(new Chernety2(ref id));
                            }
                        }
                        break;
                    case 2:
                        {
                            if (random.Next(0, 2) == 0)
                            {
                                lake[1].add_ducks(new Mramchir(ref id));
                            }
                            else
                            {
                                lake[1].add_ducks(new Shirok1(ref id));
                            }
                        }
                        break;
                    case 3:
                        {
                            if (random.Next(0, 2) == 0)
                            {
                                lake[2].add_ducks(new Shirok2(ref id));
                            }
                            else
                            {
                                lake[2].add_ducks(new Krohali(ref id));
                            }
                        }
                        break;
                    case 4:
                        {
                            if (random.Next(0, 2) == 0)
                            {
                                lake[3].add_ducks(new Chernety3(ref id));
                            }
                            else
                            {
                                lake[3].add_ducks(new Stoneduck(ref id));
                            }
                        }
                        break;
                }

            }

            showLakes(lake);



            for (int i = 1; i < hunt_days + 1; i++)
            {
                int Nitro = 2;
                int sumducks = 0;
                int num_Nitro = random.Next(count_lakes + 1);
                int hunt_ducks = random.Next(1, 9);
                int randomlake = random.Next(count_lakes);


                Console.WriteLine("---------------");
                Console.WriteLine($"День {i}:");

                for (int s = 0; s < count_lakes; s++)
                {
                    sumducks += lake[s].lake.Length;
                }// Chech sumducks
                if (sumducks == 0)
                {
                    Console.WriteLine("Уток на диких озёрах больше нет!");
                    break;

                }

                for (int p = 0; p < count_lakes + 1; p++)
                {
                    if (lake[p].lake_live == false)
                    {
                        if (lake[p].days_Nitro == 0)
                        {
                            lake[p].lake_live = true;
                        }
                        else
                        {
                            lake[p].days_Nitro--;
                        }

                    }
                }//NItroCrack
                if (lake[num_Nitro].lake_live == true)
                {
                    if (random.Next(0, 3) == Nitro)
                    {

                        Console.WriteLine($"О, нет! На озере {lake[num_Nitro].name} появился НитроХряк! Теперь оно и его обитатели уничтожено!");

                        int b = lake[num_Nitro].lake.Length;
                        while (b > 0)
                        {
                            int farm_ducks = random.Next(1, lake[num_Nitro].lake.Length) - 1;
                            Duck exploded_duck = lake[num_Nitro].remove(farm_ducks);
                            b--;
                        }

                        lake[num_Nitro].lake_live = false;
                        lake[num_Nitro].days_Nitro = 1;
                    }
                }





                if (lake[4].lake_live == true)
                {
                    while (true)
                    {
                        if (lake[randomlake].lake_live == true && lake[randomlake].lake.Length != 0)
                        {


                            bool a = true;
                            while (a)
                            {

                                if (lake[randomlake].lake.Length != 0)
                                {
                                    if (hunt_ducks <= lake[randomlake].lake.Length)
                                    {
                                        Console.WriteLine($"Охотник с фермы {lake[4].name} отправился на охоту на озеро {lake[randomlake].name} и поймал {hunt_ducks} уток");
                                        while (hunt_ducks > 0)
                                        {


                                            int lake_ducks = random.Next(1, lake[randomlake].lake.Length) - 1;
                                            Duck catchDuck = lake[randomlake].remove(lake_ducks);
                                            catchDuck.home = lake[4].name;
                                            catchDuck.caught += 1;
                                            lake[4].add_ducks(catchDuck);
                                            lake_ducks--;
                                            hunt_ducks--;

                                            if (lake[randomlake].lake.Length <= 0) hunt_ducks = 0;
                                        }

                                        a = false;
                                    }

                                    else
                                    {
                                        Console.WriteLine($"Охотник с фермы {lake[4].name} отправился на охоту на озеро {lake[randomlake].name} и поймал {lake[randomlake].lake.Length} уток");
                                        int c = lake[randomlake].lake.Length;
                                        while (c > 0)
                                        {
                                            int lake_ducks = random.Next(1, lake[randomlake].lake.Length) - 1;
                                            Duck catchDuck = lake[randomlake].remove(lake_ducks);
                                            catchDuck.home = lake[4].name;
                                            catchDuck.caught += 1;
                                            lake[4].add_ducks(catchDuck);
                                            lake_ducks--;
                                            c--;
                                            if (lake[randomlake].lake.Length <= 0) c = 0;
                                        }

                                        a = false;
                                    }
                                }

                                else
                                {
                                    randomlake = random.Next(count_lakes);
                                }
                            }
                            break;
                        }
                        else if ((lake[randomlake].lake_live == true && lake[randomlake].lake.Length == 0) || (lake[randomlake].lake_live == false))
                        {
                            randomlake = random.Next(count_lakes);
                        }
                        if (lake[4].lake.Length != 0)
                        {
                            Farm farm = (Farm)lake[4];
                            var escaped = new Duck[0];
                            if (farm.escape(ref escaped))
                            {

                                Console.WriteLine($"Сбежало c фермы {lake[4].name} уток: {escaped.Length}");
                                for (int z = 0; z < escaped.Length; z++)
                                {

                                    int id_lake = random.Next(count_lakes);
                                    int w = random.Next(count_lakes);
                                    if (lake[id_lake].lake_live == true)
                                    {
                                        Console.Write("Утка под номером: " + Convert.ToString(escaped[z].ID));
                                        escaped[z].home = lake[id_lake].name;

                                        lake[id_lake].add_ducks(escaped[z]);

                                        Console.WriteLine($" сбежала на озеро {lake[id_lake].name}");
                                    }

                                    else if (w != id_lake)
                                    {

                                        escaped[z].home = lake[w].name;
                                        lake[w].add_ducks(escaped[z]);
                                        Console.WriteLine($" сбежала на озеро {lake[w].name}");
                                    }
                                    else
                                    {
                                        w = random.Next(count_lakes);
                                    }
                                }

                            }
                        }
                    }
                }


                else
                {
                    Console.Write($"Охотник был тяжело ранен НитроХряком восстановится через {lake[4].days_Nitro} ");
                    switch (lake[4].days_Nitro)
                    {
                        case 1:
                            Console.WriteLine("день");
                            break;
                        case 2:
                            Console.WriteLine("дня");
                            break;
                    }

                }

                while (true)
                {
                    Console.WriteLine("Выберите действие:\n1.Показать утку и её свойства и умения\n2.Показать озеро и его обитателей\n3.Следующий день");
                    int move = Convert.ToInt32(Console.ReadLine());
                    
                        if (move == 1)
                        {

                        showDuck(lake);

                        }
                        else if (move == 2)
                        {
                            showLake(lake);
                            
                            
                        }
                        else if (move == 3)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неправильная команда! Введите номер команды ещё раз!");
                            
                        }
                    
                }
            }

            Console.WriteLine("---------------\nСезон охоты закончен!");

            Console.ReadKey();
        }

        
        

        static void showLakes(Lake[] lake)
        {

            for (int i = 0; i < 5; i++)
            {
                lake[i].Lakevoice(i);
            }
            Console.WriteLine("---------------");
        }

        static void showLake(Lake[] lake)
        {
            Console.Write("Введите номер озера: ");
            int lake_id = Convert.ToInt32(Console.ReadLine());

            lake[lake_id].Lakevoice(lake_id);
            
            Console.WriteLine("---------------");
        }

        static void showDuck(Lake[] lake)
        {
            
            Console.Write("Введите номер озера: ");
            int lake_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите номер утки: ");
            int duck_id = Convert.ToInt32(Console.ReadLine());

            lake[lake_id].lake[duck_id].voice();
            
            Console.WriteLine("---------------");
        }


    }
    abstract public class Duck
    {
        public string type = "";
        public bool swim = false;
        public bool knowhome = false;
        public bool fly = false;
        public string home = "";
        public int ID = 0;
        public bool cut;
        public int caught;
        public Duck(ref int id)
        {
            ID = id;
            id++;
        }

        public abstract void voice();
    }

    public static class par
    {
        static Random random = new Random();
        public static string gencolour()
        {
            var colour = new string[] { "красный", "голубой", "зелёный", "жёлтый", "белый", "чёрный" };
            return colour[random.Next(0, 6)];
        }
        public static string gencolourwings()
        {
            var colourwings = new string[] { "красный", "голубой", "зелёный", "жёлтый", "белый", "чёрный" };
            return colourwings[random.Next(0, 6)];
        }
        public static string genformwings()
        {
            var formwings = new string[] { "круглые", "овальные", "вытянутые" };
            return formwings[random.Next(0, 2)];
        }
        public static string geneyecolour()
        {
            var eyecolour = new string[] { "чёрный", "коричневый", "синий" };
            return eyecolour[random.Next(0, 2)];
        }
        public static int genage()
        {
            return random.Next(1, 9);
        }
        public static int genheight()
        {
            return random.Next(5, 15);
        }
        public static string genpaws()
        {
            var formpaws = new string[] { "круглые", "овальные", "вытянутые" };
            return formpaws[random.Next(0, 3)];
        }
        public static string genfavouritedish()
        {
            var favouritedish = new string[] { "травы", "рыба", "корм" };
            return favouritedish[random.Next(0, 3)];
        }
        public static int genwidth()
        {
            return random.Next(5, 10);
        }
        public static string genformbeak()
        {
            var forms = new string[] { "кривой", "овальный", "вытянутый" };
            return forms[random.Next(0, 3)];
        }
        public static int genhealth()
        {
            return random.Next(50, 100);
        }
        public static string gensex()
        {
            var sex = new string[] { "самец", "самка" };
            return sex[random.Next(0, 1)];
        }
        public static string genname()
        {
            var name = new string[] { "Озон", "Атава", "Перди", "Красавка", "Филя", "Мамба", "Алок", "Вин", "Ерон", "Чепчик", "Митч", "Дюна", "Ловкий", "Мариус", "Фредди" };
            return name[random.Next(0, 15)];
        }
        public static int genweight()
        {
            return random.Next(5, 17);
        }
    }
    class Chernety1 : Duck
    {
        public Chernety1(ref int id) : base(ref id)
        {

            type = "Чернети 1";
            home = "Байкал";
            fly = true;
            swim = false;
            knowhome = false;
            cut = false;

        }

        int weight = par.genweight();
        string paws = par.genpaws();
        int height = par.genheight();
        string name = par.genname();

        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nВысота: {height}\nФорма лап: {paws}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += "Не подрезана";
            }
            Console.WriteLine(a);

            Console.WriteLine("---------------");
        }


    }

    class Chernety2 : Duck
    {
        public Chernety2(ref int id) : base(ref id)
        {

            type = "Чернети 2";
            home = "Байкал";
            fly = false;
            knowhome = false;
            swim = true;
            cut = false;
        }

        int weight = par.genweight();
        int age = par.genage();
        string sex = par.gensex();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nПол: {sex}Возраст: {age}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }


    }

    class Chernety3 : Duck
    {
        public Chernety3(ref int id) : base(ref id)
        {

            type = "Чернети 3";
            home = "Ньяса";
            fly = false;
            knowhome = true;
            swim = true;
            cut = false;
        }

        int width = par.genwidth();
        int health = par.genhealth();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nШирина: {width}\nЗдоровье: {health}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            Console.WriteLine(a);
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }

    }

    class Mramchir : Duck
    {
        public Mramchir(ref int id) : base(ref id)
        {

            type = "Мраморная чирка";
            home = "Кратерное озеро";
            fly = false;
            knowhome = false;
            swim = true;
            cut = false;
        }

        string formwings = par.genformbeak();
        string colourwings = par.gencolourwings();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nФорма крыльев: {formwings}\nЦвет крыльев: {colourwings}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }


    }

    class Shirok1 : Duck
    {
        public Shirok1(ref int id) : base(ref id)
        {

            type = "Широконоска 1";
            home = "Кратерное озеро";
            fly = true;
            knowhome = true;
            swim = false;
            cut = false;
        }

        string eyecolour = par.geneyecolour();
        string colour = par.gencolour();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nЦвет глаз: {eyecolour}\nЦвет: {colour}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }


    }

    class Shirok2 : Duck
    {
        public Shirok2(ref int id) : base(ref id)
        {

            type = "Широконоска 2";
            home = "Челан";
            fly = true;
            knowhome = false;
            swim = false;
            cut = false;
        }

        string formwings = par.genformwings();
        int width = par.genwidth();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nФорма крыльев: {formwings}\nШирина: {width}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }


    }

    class Krohali : Duck
    {
        public Krohali(ref int id) : base(ref id)
        {

            type = "Крохали";
            home = "Челан";
            fly = true;
            knowhome = false;
            swim = false;
            cut = false;
        }

        string colourwings = par.gencolourwings();
        int height = par.genheight();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес: {weight}\nОкрас крыльев: {colourwings}\nВысота: {height}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            
            if (cut == true)
            {
                a +="Подрезана";

            }
            else
            {
                a+= "Не подрезана";
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }
    }

    class Stoneduck : Duck
    {
        public Stoneduck(ref int id) : base(ref id)
        {

            type = "Каменушки";
            home = "Ньяса";
            fly = false;
            knowhome = true;
            swim = true;
            cut = false;
        }

        string formbeak = par.genformbeak();
        string favouritedish = par.genfavouritedish();
        int weight = par.genweight();
        string name = par.genname();
        public override void voice()
        {
            Console.WriteLine("---------------");
            string a = $"Имя: {name}\nТип утки: {type}\nВес {weight}\nФорма клюва: {formbeak}\nЛюбимое блюдо: {favouritedish}\n";
            if (fly) a += "Умеет летать\n";
            if (swim) a += "Умеет плавать\n";
            if (knowhome) a += $"Живёт на озере {home}\n";
            
            if (cut == true)
            {
                a += "Подрезана";

            }
            else
            {
                a += ("Не подрезана");
            }
            Console.WriteLine(a);
            Console.WriteLine("---------------");
        }

    }

    class Lake
    {
        Random random = new Random();

        public Duck[] lake = new Duck[0];
        int size = 0;
        public bool lake_live;
        public string name;
        public int days_Nitro;
        public Lake(string name)
        {
            this.name = name;
            this.lake_live = true;
            this.days_Nitro = 1;
        }

        public void add_ducks(Duck duck)
        {
            Array.Resize(ref lake, size + 1);
            lake[size] = duck;
            size++;
        }

        virtual public void Lakevoice(int n)
        {
            int swim = 0;
            int fly = 0;

            for (int i = 0; i < size; i++)
            {
                if (lake[i].swim == true) swim++;
                if (lake[i].fly == true) fly++;

            }

            if (lake_live == true)
            {
                Console.WriteLine("---------------");
                Console.WriteLine($"Озеро {name}");
                if (size > 0)
                {
                    Console.WriteLine($"Всего уток: {size}");
                    if (swim > 0) Console.WriteLine($"Умеют плавать: {swim}");
                    if (fly > 0) Console.WriteLine($"Умеют летать: {fly}");
                }

                else
                {
                    Console.WriteLine("Уток нет");
                }
            }
            else
            {
                Console.WriteLine("----------------\nОзеро уничтожено!");
            }
            Console.WriteLine("---------------");
        }

        public Duck remove(int num)
        {
            Duck duck = lake[num];
            while (num < size - 1)
            {
                lake[num] = lake[num + 1];
                num++;
            }
            Array.Resize(ref lake, size - 1);
            size--;

            return duck;
        }
    }

    class Farm : Lake
    {

        
        int fly = 0;
        public Farm(string name) : base(name)
        {
            this.name = name;
            this.lake_live = true;
            this.days_Nitro = 1;
        }

        public override void Lakevoice(int n)

        {
            int swim = 0;
            int fly = 0;
            for (int i = 0; i < lake.Length; i++)
            {
                if (lake[i].swim == true) swim++;
                if (lake[i].fly == true) fly++;

            }
            Console.WriteLine("---------------");
            if (lake_live == true)
            {
                Console.WriteLine($"Ферма {name}");


                if (lake.Length > 0)
                {
                    Console.WriteLine($"Всего уток: {lake.Length}");
                    if (swim > 0) Console.WriteLine($"Умеют плавать: {swim}");
                    if (fly > 0) Console.WriteLine($"Умеют летать: {fly}");
                }

                else
                {
                    Console.WriteLine("Уток нет");
                }
                Console.WriteLine("---------------");
            }
            else
            {
                Console.WriteLine($"Ферма {this.name} с охотниками уничтожена");
            }
        }

        public bool escape(ref Duck[] duck)
        {
            int i = 0;
            int arrl = 0;
            bool freeducks = false;
            while (i < lake.Length)
            {
                if (lake[i].fly == true && lake[i].knowhome == false && lake[i].cut == false)
                {
                    Array.Resize(ref duck, arrl + 1);
                    duck[arrl] = remove(i);

                    duck[arrl].cut = true;
                    duck[arrl].fly = false;
                    arrl++;
                    fly--;
                    freeducks = true;

                }
                else i++;
            }
            return freeducks;
        }
    }


}


