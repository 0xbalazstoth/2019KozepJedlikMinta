using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace _2019MintaJedlikKozep
{
    class Felf
    {
        public static List<Felf> Adat = new List<Felf>(300);

        public string ElsoSor;
        public int Sorszam;
        public string Ev;
        public string Nev;
        public string Vegyjel;
        public string Rendszam;
        public string Felfedezo;
        public static string megadottVegyjel { get; set; }

        public Felf(string elsoSor, int sorszam, string ev, string nev, string vegyjel, string rendszam, string felfedezo)
        {
            ElsoSor = elsoSor;
            Sorszam = sorszam;
            Ev = ev;
            Nev = nev;
            Vegyjel = vegyjel;
            Rendszam = rendszam;
            Felfedezo = felfedezo;
        }

        public static void MasodikFeladat()
        {
            using (StreamReader olvas = new StreamReader(@"felfedezesek.csv"))
            {
                string elsoSor = olvas.ReadLine();
                int sor = 0;
                while (!olvas.EndOfStream)
                {
                    sor++;

                    string[] split = olvas.ReadLine().Split(';');
                    string ev = split[0];
                    string nev = split[1];
                    string vegyjel = split[2];
                    string rendszam = split[3];
                    string felfedezo = split[4];

                    Felf felf = new Felf(elsoSor, sor, ev, nev, vegyjel, rendszam, felfedezo);

                    Adat.Add(felf);
                }
            }
        }

        public static void HarmadikFeladat() => Console.WriteLine($"3. feladat: Elemek száma: {Adat.Count}");
        public static void NegyedikFeladat() => Console.WriteLine($"4. feladat: Felfedezések száma az ókorban: {Adat.Count(x => x.Ev == "Ókor")}");
        public static void OtodikFeladat()
        {
            bool hiba = true;

            while (hiba)
            {
                Console.Write("5. feladat: Kérek egy vegyjelet: ");
                megadottVegyjel = Console.ReadLine().ToLower();

                hiba = false;

                var isMatches = Regex.IsMatch(megadottVegyjel, @"^[A-Z-a-z]\w{1}\b$");

                if (megadottVegyjel.Length != 2)
                    hiba = true;
                else if (isMatches == false)
                    hiba = true;
            }
        }

        public static void HatodikFeladat()
        {
            int isLetezik = 0;
            string vegyjel = "";
            string nev = "";
            string rendszam = "";
            string ev = "";
            string felfedezo = "";

            Console.WriteLine("6. feladat: Keresés");
            for (int i = 0; i < Adat.Count; i++)
            {
                if (megadottVegyjel == Adat[i].Vegyjel.ToLower())
                {
                    vegyjel += Adat[i].Vegyjel;
                    nev += Adat[i].Nev;
                    rendszam += Adat[i].Rendszam;
                    ev += Adat[i].Ev;
                    felfedezo += Adat[i].Felfedezo;

                    isLetezik += 1;
                }
                else if (megadottVegyjel != Adat[i].Vegyjel)
                {
                    isLetezik += 0;
                }
            }

            if(isLetezik == 1)
                
                Console.WriteLine($"\tAz elem vegyjele: {vegyjel}\n\tAz elem neve: {nev}\n\tRendszáma: {rendszam}\n\tFelfedezés éve: {ev}\n\tFelfedező: {felfedezo}");
            else if (isLetezik == 0)
                Console.WriteLine("\tNincs ilyen elem az adatforrásban!");

        }
        public static void HetedikFeladat()
        {
            List<int> rendezes = new List<int>();

            var reOkor = Adat.SkipWhile(x => x.Ev == "Ókor").Select(x => x.Ev).OrderByDescending(x => x).ToList();

            var elsoEv = Convert.ToInt32(reOkor[reOkor.Count - 2]);
            var masodikEv = Convert.ToInt32(reOkor[reOkor.Count - 1]);

            Console.WriteLine($"7. feladat: {elsoEv - masodikEv} év volt a leghosszabb időszak két elem felfedezése között.");
        }

        public static void NyolcadikFeladat()
        {
            Console.WriteLine("8. feladat: Statisztika");

            var reOkor = Adat.SkipWhile(x => x.Ev == "Ókor").ToList();
            List<string> legt = new List<string>();

            var rendezes = new SortedDictionary<string, int>();
            for (int i = 0; i < reOkor.Count; i++)
            {
                if (rendezes.ContainsKey(reOkor[i].Ev))
                    rendezes[reOkor[i].Ev]++;
                else
                    rendezes[reOkor[i].Ev] = 1;
            }

            foreach (var statisztika in rendezes.OrderByDescending(x => x.Value))
            {
                legt.Add($"{statisztika.Key}: {statisztika.Value} db");
            }

            Console.WriteLine($"\t{legt[0]}\n\t{legt[1]}\n\t{legt[2]}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Felf.MasodikFeladat();
            Felf.HarmadikFeladat();
            Felf.NegyedikFeladat();
            Felf.OtodikFeladat();
            Felf.HatodikFeladat();
            Felf.HetedikFeladat();
            Felf.NyolcadikFeladat();
        }
    }
}
