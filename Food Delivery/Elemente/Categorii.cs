using System;
using Food_Delivery.Baza_de_date;

namespace Food_Delivery.Elemente
{
    class Categorii
    {
        protected int[] idCategorii;
        protected string[] numeCategorii;
        protected int nrCategorii;

        public Categorii(BazaDeDate bazaDeDate)
        {
            string[][] rezultatInterogare = bazaDeDate.ReturneazaDateCategorii();
            nrCategorii = rezultatInterogare.Length;
            idCategorii = new int[nrCategorii];
            numeCategorii = new string[nrCategorii];

            for (int i = 0; i < nrCategorii; i++)
            {
                idCategorii[i] = Convert.ToInt32(rezultatInterogare[i][0]);
                numeCategorii[i] = rezultatInterogare[i][1];
            }
        }

        ~Categorii()
        {
            Console.WriteLine("\t\t-->S-a apelat deconstructorul pentru Categorii.");
        }

        public void AfisareConsola()
        {
            Console.WriteLine("------------ Categorii ( " + nrCategorii + " )------------");
            for (int i = 0; i < nrCategorii; i++)
            {
                Console.WriteLine("--> Id= \t" + idCategorii[i]);
                Console.WriteLine("--> Nume= \t" + numeCategorii[i]);
            }
            Console.WriteLine("--------------------------------------");
        }

        public int[] ReturneazaIdCategorii()
        {
            return idCategorii;
        }

        public string[] ReturneazaNumeCategorii()
        {
            return numeCategorii;
        }

        public int ReturneazaNumarCategorii()
        {
            return nrCategorii;
        }
    }
}
