using System;
using Food_Delivery.Baza_de_date;

namespace Food_Delivery.Elemente
{
    class Utilizator
    {
        readonly int idUtilizator;
        readonly string userUtilizator;
        readonly string emailUtilizator;
        readonly string parolaUtilizator;
        readonly string numeUtilizator;
        readonly string prenumeUtilizator;
        readonly string adresaUtilizator;
        readonly string telefonUtilizator;
        readonly bool adminUtilizator;

        public Utilizator()
        {
            idUtilizator = 0;
            userUtilizator = null;
            emailUtilizator = null;
            parolaUtilizator = null;
            numeUtilizator = null;
            prenumeUtilizator = null;
            adresaUtilizator = null;
            telefonUtilizator = null;
            adminUtilizator = false;
        }   

        public Utilizator(BazaDeDate bazaDeDate, int idUtilizator)
        {
            string[] rezultatInterogare = bazaDeDate.ReturneazaDateUtilizator(idUtilizator);

            this.idUtilizator = Convert.ToInt32(rezultatInterogare[0]);
            userUtilizator = rezultatInterogare[1];
            emailUtilizator = rezultatInterogare[2];
            parolaUtilizator = rezultatInterogare[3];
            numeUtilizator = rezultatInterogare[4];
            prenumeUtilizator = rezultatInterogare[5];
            adresaUtilizator = rezultatInterogare[6];
            telefonUtilizator = rezultatInterogare[7];
            adminUtilizator = Convert.ToBoolean(rezultatInterogare[8]);
        }

        ~Utilizator()
        {
            Console.WriteLine("S-a apelat deconstructorul pentru Utilizator.");
        }

        public void AfisareUtilizator()
        {
            Console.WriteLine("idUtilizator=\t" + idUtilizator);
            Console.WriteLine("userUtilizator=\t" + userUtilizator);
            Console.WriteLine("emailUtilizator=\t" + emailUtilizator);
            Console.WriteLine("parolaUtilizator=\t" + parolaUtilizator);
            Console.WriteLine("numeUtilizator=\t" + numeUtilizator);
            Console.WriteLine("prenumeUtilizator=\t" + prenumeUtilizator);
            Console.WriteLine("adresaUtilizator=\t" + adresaUtilizator);
            Console.WriteLine("telefonUtilizator=\t" + telefonUtilizator);
            Console.WriteLine("adminUtilizator=\t" + adminUtilizator);
        }

        public int ReturneazaIdUtilizator()
        {
            return idUtilizator;
        }
        public string ReturneazaUserUtilizator()
        {
            return userUtilizator;
        }
        public string ReturneazaEmailUtilizator()
        {
            return emailUtilizator;
        }
        public string ReturneazaParolaUtilizator()
        {
            return parolaUtilizator;
        }
        public string ReturneazaNumeUtilizator()
        {
            return numeUtilizator;
        }
        public string ReturneazaPrenumeUtilizator()
        {
            return prenumeUtilizator;
        }
        public string ReturneazaAdresaUtilizator()
        {
            return adresaUtilizator;
        }
        public string ReturneazaTelefonUtilizator()
        {
            return telefonUtilizator;
        }
        public bool ReturneazaAdminUtilizator()
        {
            return adminUtilizator;
        }
    }
}