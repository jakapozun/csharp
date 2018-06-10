using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naloga_6
{
    class Izlet : Prodajni
    {
        public string naziv;
        public double cena;
        public string krajOdhoda;
        public List<Termin> poljeTerminov = new List<Termin>();

        public Izlet()
        {
        }

        public Izlet(string naziv, double cena, string krajOdhoda, List<Termin> poljeTerminov)
        {
            this.naziv = naziv;
            this.cena = cena;
            this.krajOdhoda = krajOdhoda;
            this.poljeTerminov = poljeTerminov;
        }

        public void ProdajKarto(Termin termin, Potnik potnik)
        {
            for (int i = 0; i < poljeTerminov.Count; i++)
            {
                if (termin.Equals(poljeTerminov[i]))
                {
                    poljeTerminov[i].PrijavljeniPotniki.Add(potnik.email, potnik);
                    Console.WriteLine("Potnik uspešno dodan.");
                }
            }
        }

        public void PrekličiKarto(Termin termin, Potnik potnik)
        {
            for (int i = 0; i < poljeTerminov.Count; i++)
            {
                if (termin.Equals(poljeTerminov[i]))
                {
                    if (poljeTerminov[i].PrijavljeniPotniki.Remove(potnik.email))
                    {
                        Console.WriteLine("Popotnik je bil uspešno odstranjen.");
                    }
                    else
                    {
                        Console.WriteLine("Odstranjevanje popotnika ni bilo uspešno.");
                    }
                }
            }
        }

        public bool MestoProsto(Termin termin)
        {
            if (termin.PrijavljeniPotniki.Count < termin.avtobus.skupnoŠteviloSedežev)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public double IzračunajCeno(Potnik potnik)
        {
            double novaCena;

            if (potnik.status == Status.Upokojenec)
            {
                novaCena = (cena * 90) / 100;
            }
            else if (potnik.status == Status.Student)
            {
                novaCena = (cena * 85) / 100;
            }
            else
            {
                novaCena = cena;
            }

            return novaCena;
        }

        public void DodajTermin(Termin termin)
        {
            poljeTerminov.Add(termin);
            Console.WriteLine("Termin uspešno dodan.");
        }

        public void OdstraniTermin(Termin termin)
        {
            bool a = poljeTerminov.Remove(termin);
            if (a)
            {
                Console.WriteLine("Termin je bil uspešno odstranjen.");
            }
            else
            {
                Console.WriteLine("Odstranjevanje termina ni bilo uspešno.");
            }
        }

        public Termin VrniNajkasnejsiTermin()
        {
            Termin temp = new Termin();

            for (int i = 0; i < poljeTerminov.Count; i++)
            {
                if (poljeTerminov[i].datumČasOdhoda > temp.datumČasOdhoda)
                {
                    temp = poljeTerminov[i];
                }
            }
            return temp;
        }

        public Potnik NajdiPotnika(string email, Termin termin)
        {
            Potnik temp = new Potnik();

            if (poljeTerminov.Contains(termin))
            {
                for (int i = 0; i < poljeTerminov.Count; i++)
                {
                    if (termin.Equals(poljeTerminov[i]))
                    {
                        if (poljeTerminov[i].PrijavljeniPotniki.ContainsKey(email))
                        {
                            return poljeTerminov[i].PrijavljeniPotniki[email];
                        }
                        else
                        {
                            return temp;
                        }
                    }
                }
                return temp;
            }

            else
            {
                return temp;
            }
        }
    }
}
