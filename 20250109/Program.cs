using Microsoft.EntityFrameworkCore;
using System;

namespace _20250109
{
    public class Autok
    {
        public int ID { get; set; }
        public string Rendszam { get; set; }
        public virtual List<Adat> Adatok { get; } = new();
    }
    public class Adat
    {
        public int AdatID { get; set; }
        public string Marka { get; set; }
        public string Tipus { get; set; }
        public int Evjarat { get; set; }
        public int ID { get; set; }
        public virtual Autok Auto { get; set; }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Autok> Autok { get; set; }
        public DbSet<Adat> Adatok { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=auto;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    

    internal class Program
    {
        
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {

                Console.WriteLine("Válassz opciót:");
            Console.WriteLine("1. Autó hozzáadása");
            Console.WriteLine("2. Autók/Adatok listázása");
            Console.WriteLine("3. Autók/Adatok változtatása");
            Console.WriteLine("4. Autók/Adatok törlése");
            int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.Write("Rendszam: ");
                        string rendszam = Console.ReadLine();
                        Console.Write("Marka: ");
                        string marka = Console.ReadLine();
                        Console.Write("Tipus: ");
                        string tipus = Console.ReadLine();
                        Console.Write("Evjarat: ");
                        int evjarat = Convert.ToInt32(Console.ReadLine());
                        var auto = new Autok { Rendszam = rendszam };
                        var adat = new Adat { Marka = marka, Tipus = tipus, Evjarat = evjarat, Auto = auto };
                        db.Autok.Add(auto);
                        db.Adatok.Add(adat);
                        db.SaveChanges();
                        break;
                    case 2:
                        var query = from b in db.Autok
                                    orderby b.ID
                                    select b;
                        Console.WriteLine("Autok:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.ID + ":  " + item.Rendszam);
                        }
                        Console.WriteLine("adatok:");
                        var query2 = from b in db.Adatok
                                     orderby b.AdatID
                                     select b;
                        foreach (var item in query2)
                        {
                            Console.WriteLine(item.ID + ":  " + item.Marka);
                            Console.WriteLine(item.ID + ":  " + item.Tipus);
                            Console.WriteLine(item.ID + ":  " + item.Evjarat);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Rendszámok:");
                        var query3 = from b in db.Autok
                                     orderby b.ID
                                     select b;
                        foreach (var item in query3)
                        {
                            Console.WriteLine(item.ID + ":  " + item.Rendszam);
                        }
                        Console.WriteLine("Válassz egy rendszámot:");
                        string rendszam2 = Console.ReadLine();
                        var query4 = from b in db.Autok
                                     where b.Rendszam == rendszam2
                                     select b;
                        int id = 0;
                        foreach (var item in query4)
                        {
                            id = item.ID;
                        }
                        Console.WriteLine("Melyik adatot szeretnéd változtatni?");
                        Console.WriteLine("1. Marka");
                        Console.WriteLine("2. Tipus");
                        Console.WriteLine("3. Evjarat");
                        int valasztas = Convert.ToInt32(Console.ReadLine());
                        switch (valasztas)
                        {
                            case 1:
                                Console.WriteLine("Új Marka:");
                                string marka2 = Console.ReadLine();
                                var query6 = from b in db.Adatok
                                             where b.ID == id
                                             select b;
                                foreach (var item in query6)
                                {
                                    item.Marka = marka2;
                                }
                                db.SaveChanges();
                                break;
                            case 2:
                                Console.WriteLine("Új Tipus:");
                                string tipus2 = Console.ReadLine();
                                var query7 = from b in db.Adatok
                                             where b.ID == id
                                             select b;
                                foreach (var item in query7)
                                {
                                    item.Tipus = tipus2;
                                }
                                db.SaveChanges();
                                break;
                            case 3:
                                Console.WriteLine("Új Evjarat:");
                                int evjarat2 = Convert.ToInt32(Console.ReadLine());
                                var query8 = from b in db.Adatok
                                             where b.ID == id
                                             select b;
                                foreach (var item in query8)
                                {
                                    item.Evjarat = evjarat2;
                                }
                                db.SaveChanges();
                                break;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Rendszámok:");
                        var query9 = from b in db.Autok
                                     orderby b.ID
                                     select b;
                        foreach (var item in query9)
                        {
                            Console.WriteLine(item.ID + ":  " + item.Rendszam);
                        }
                        Console.WriteLine("Válassz egy rendszámot:");
                        string rendszam3 = Console.ReadLine();
                        var query10 = from b in db.Autok
                                      where b.Rendszam == rendszam3
                                      select b;
                        //delete the selected rendszam from the db
                        foreach (var item in query10)
                        {
                            db.Autok.Remove(item);
                        }
                        db.SaveChanges();

                        break;
                }
            }
        }
    }
}
