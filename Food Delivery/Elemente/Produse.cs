using System;
using Food_Delivery.Baza_de_date;

namespace Food_Delivery.Elemente
{
    class Produse
    {
        protected int[] idProduse;
        protected string[] numeProduse;
        protected string[] descriereProduse;
        protected string[] pretProduse;
        protected string[] imaginiProduse;
        protected int nrProduse;

        public Produse(BazaDeDate bazaDeDate, int tipSortare, int categorieActiva)
        {
            string[][] rezultatInterogare = bazaDeDate.ReturneazaDateProduse(categorieActiva, tipSortare);
            nrProduse = rezultatInterogare.Length;

            idProduse = new int[nrProduse];
            numeProduse = new string[nrProduse];
            descriereProduse = new string[nrProduse];
            pretProduse = new string[nrProduse];
            imaginiProduse = new string[nrProduse];

            for (int i = 0; i < nrProduse; i++)
            {
                idProduse[i] = Convert.ToInt32(rezultatInterogare[i][0]);
                numeProduse[i] = rezultatInterogare[i][1];
                descriereProduse[i] = rezultatInterogare[i][2];
                pretProduse[i] = rezultatInterogare[i][3];
                imaginiProduse[i] = rezultatInterogare[i][4];
            }
        }

        ~Produse()
        {
            Console.WriteLine("\t\t-->S-a apelat deconstructorul pentru Produse.");
        }

        public void AfisareConsola()
        {
            Console.WriteLine("------------ Produse ( " + nrProduse + " )------------");
            for (int i = 0; i < nrProduse; i++)
            {
                Console.WriteLine("--> Id= \t" + idProduse[i]);
                Console.WriteLine("--> Nume= \t" + numeProduse[i]);
                Console.WriteLine("--> Descriere= \t" + descriereProduse[i]);
                Console.WriteLine("--> Pret= \t" + pretProduse[i]);
                Console.WriteLine("--> Imagine= \t" + imaginiProduse[i]);
            }
            Console.WriteLine("--------------------------------------");
        }

        public int[] ReturneazaIdProduse()
        {
            return idProduse;
        }

        public string[] ReturneazaNumeProduse()
        {
            return numeProduse;
        }
        public string[] ReturneazaDescriereProduse()
        {
            return descriereProduse;
        }
        public string[] ReturneazaPretProduse()
        {
            return pretProduse;
        }
        public string[] ReturneazaImaginiProduse()
        {
            return imaginiProduse;
        }
        public int ReturneazaNrProduse()
        {
            return nrProduse;
        }
    }
}
