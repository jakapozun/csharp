using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace naloga_6
{
    public enum Prednosti
    {
        wifi, TV, klima, parkirišče, bazen
    }

    class Program
    {
        static void Main(string[] args)
        {
            Voznik voznik1 = new Voznik("Tilen", "Cokan", Spol.M, new DateTime(1997, 6, 22), new DateTime(2018, 6, 22));

            Potnik potnik1 = new Potnik("Teja", "Kolar", Spol.Z, new DateTime(1997, 5, 20), "teja.kolar@student.um.si", Status.Student);

            Potnik potnik2 = new Potnik("Tomaž", "Kolar", Spol.M, new DateTime(1989, 12, 5), "tomaz.kolar@student.um.si", Status.Student);

            Potnik potnik3 = new Potnik("Matej", "Kolar", Spol.M, new DateTime(1991, 7, 3), "matej.kolar@student.um.si", Status.Student);

            Potnik potnik4 = new Potnik("Gregor", "Kranjc", Spol.M, new DateTime(1950, 7, 3), "gregor.kranjc@gmail.com", Status.Upokojenec);

            Potnik potnik5 = new Potnik("Urška", "Kranjc", Spol.Z, new DateTime(2005, 5, 3), "urska.kranjc@gmail.com", Status.Otrok);

            Avtomobil avto1 = new Avtomobil(voznik1, "mazda", "limuzina", 5.7, 50, 5, 2);

            Kombi kombi1 = new Kombi(voznik1, "mercedes", "kombi", 9.5, 80, 500, 1000);

            Avtobus bus1 = new Avtobus(voznik1, "MAN", "avtobus", 20, 120, 51, 20, 40);

            Dictionary<string, Potnik> seznamOseb = new Dictionary<string, Potnik>();

            Termin termin1 = new Termin(new DateTime(2018, 5, 10, 6, 0, 0), new DateTime(2018, 5, 19, 23, 0, 0), bus1, seznamOseb);

            Termin termin2 = new Termin(new DateTime(2018, 6, 10, 6, 0, 0), new DateTime(2018, 6, 18, 23, 0, 0), bus1, seznamOseb);

            List<Termin> termini = new List<Termin>();

            Izlet izlet1 = new Izlet("Nizozemska", 480.50, "Celje", termini);

            izlet1.DodajTermin(termin1);

            Izlet izlet2 = new Izlet("Belgija", 600, "Maribor", termini);

            izlet2.DodajTermin(termin2);

            izlet1.ProdajKarto(termin1, potnik1);

            izlet1.ProdajKarto(termin1, potnik2);

            izlet1.ProdajKarto(termin1, potnik3);

            izlet2.ProdajKarto(termin2, potnik4);

            izlet2.ProdajKarto(termin2, potnik5);

            izlet1.PrekličiKarto(termin1, potnik3);

            izlet2.DodajTermin(termin1);


            List<Potnik> potniki = new List<Potnik>();
            Potovanje.preberiSeznamCSV(potniki);

            var poisciKnez = potniki.Find(x => x.priimek.Equals("Knez"));
            Console.WriteLine(poisciKnez.ime + " " + poisciKnez.priimek + " " + poisciKnez.spol + " " + poisciKnez.status + " " + poisciKnez.datumRojstva + " " + poisciKnez.email);
            if (poisciKnez != null) potniki.Remove(poisciKnez);

            var poisciNajstarejsoOsebo = potniki.Find(x => x.datumRojstva == potniki.Min(y => y.datumRojstva));
            Console.WriteLine("Najstarejsi potnik: " + poisciNajstarejsoOsebo.ime + " " + poisciNajstarejsoOsebo.priimek + " " + poisciNajstarejsoOsebo.spol + " " + poisciNajstarejsoOsebo.status + " " + poisciNajstarejsoOsebo.datumRojstva + " " + poisciNajstarejsoOsebo.email);
            if (poisciNajstarejsoOsebo != null) potniki.Remove(poisciNajstarejsoOsebo);

            var dodajOsebo = new Potnik(potnik1.ime, potnik1.priimek, potnik1.spol, potnik1.datumRojstva, potnik1.email, potnik1.status);
            potniki.Add(dodajOsebo);
            Console.WriteLine(dodajOsebo.MojIzpis());

            Termin terminOtroci = new Termin(new DateTime(2018, 6, 20, 7, 0, 0), new DateTime(2018, 6, 27, 22, 0, 0), bus1, new Dictionary<string, Potnik>());
            Termin terminStudenti = new Termin(new DateTime(2018, 7, 20, 7, 0, 0), new DateTime(2018, 6, 27, 22, 0, 0), bus1, new Dictionary<string, Potnik>());
            Termin terminUpokojenci = new Termin(new DateTime(2018, 8, 20, 7, 0, 0), new DateTime(2018, 6, 27, 22, 0, 0), bus1, new Dictionary<string, Potnik>());

            Izlet izlet = new Izlet("Portugalska", 1200, "Celje", new List<Termin> { terminOtroci, terminStudenti, terminUpokojenci });

            List<Potnik> otroci = potniki.FindAll(x => x.status == Status.Otrok);
            otroci = otroci.OrderBy(x => x.priimek).ToList();
            terminOtroci.PrijavljeniPotniki = otroci.ToDictionary(x => x.email);

            List<Potnik> studenti = potniki.FindAll(y => y.status == Status.Student);
            studenti = studenti.OrderBy(y => y.priimek).ToList();
            terminStudenti.PrijavljeniPotniki = studenti.ToDictionary(y => y.email);

            List<Potnik> upokojenci = potniki.FindAll(z => z.status == Status.Upokojenec);
            upokojenci = upokojenci.OrderBy(z => z.priimek).ToList();
            terminUpokojenci.PrijavljeniPotniki = upokojenci.ToDictionary(z => z.email);

            Potovanje.zapisiVSeznamCSV(izlet);

            double povprečnaStarost = potniki.Average(popotnik => 2018 - popotnik.datumRojstva.Year);
            Console.WriteLine("Povprečna starost potnikov je: {0:0.00}", povprečnaStarost);

            List<Potnik> seznamPonikov = Potovanje.VrniPotnikeGledeNaStatus(Status.Otrok, potniki);
            for (int i = 0; i < seznamPonikov.Count; i++)
            {
                Console.WriteLine(seznamPonikov[i].ime + " " + seznamPonikov[i].priimek + " " + seznamPonikov[i].spol + " " + seznamPonikov[i].datumRojstva + " " + seznamPonikov[i].email + " " + seznamPonikov[i].status);
            }
        }
    }
}
