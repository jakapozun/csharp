using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace naloga_6
{
    static class Potovanje
    {
        public static List<Potnik> preberiSeznamCSV(List<Potnik> potniki)
        {
            string[] prebrano = File.ReadAllLines(@"C:\Users\Teja Kolar\Desktop\seznamPotnikov.2018.csv");

            for (int i = 1; i <= prebrano.Length - 1; i++)
            {
                string[] stoplci = new string[6];
                stoplci = prebrano[i].Split(',', ';');
                Potnik nov = new Potnik(stoplci[0], stoplci[1], (Spol)Enum.Parse(typeof(Spol), stoplci[2]), Convert.ToDateTime(stoplci[4]), stoplci[5], (Status)Enum.Parse(typeof(Status), stoplci[3]));
                potniki.Add(nov);
            }

            return potniki;
        }

        public static void zapisiVSeznamCSV(Izlet izleti)
        {
            File.WriteAllText("seznamPopotnikovIzhod.csv", "naziv izleta, datum odhoda, stevilo potnikov \n");

            for (int i = 0; i < izleti.poljeTerminov.Count; i++)
            {
                string podatkiCSV = izleti.naziv + " | " + izleti.poljeTerminov[i].datumČasOdhoda + " | " + izleti.poljeTerminov[i].PrijavljeniPotniki.Count + "\n";
                File.AppendAllText("seznamPopotnikovIzhod.csv", podatkiCSV);
            }
        }

        public static List<Potnik> VrniPotnikeGledeNaStatus(Status statusi, List<Potnik> potniki)
        {
            List<Potnik> seznam = new List<Potnik>();
            for (int i = 0; i < potniki.Count; i++)
            {
                if (potniki[i].status == statusi)
                {
                    seznam.Add(potniki[i]);
                }
            }
            return seznam;
        }
    }
}
