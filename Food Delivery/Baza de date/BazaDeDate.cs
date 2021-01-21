using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Food_Delivery.Baza_de_date
{
    class BazaDeDate
    {
        readonly string datasource;
        readonly int port;
        readonly string username;
        readonly string password;
        readonly string database;
        MySqlConnection databaseConnection;

        public BazaDeDate(string datasource, int port, string username, string password, string database)
        {
            this.datasource = datasource;
            this.port = port;
            this.username = username;
            this.password = password;
            this.database = database;
        }

        ~BazaDeDate()
        {
            Console.WriteLine("S-a apelat deconstructorul pentru BazaDeDate.");
        }

        public void AfisareConsola()
        {
            Console.WriteLine("------------ Baza de Date ------------");
            Console.WriteLine("-> Datasource= \t" + datasource);
            Console.WriteLine("-> Port= \t" + port);
            Console.WriteLine("-> Username= \t" + username);
            Console.WriteLine("-> Password= \t" + password);
            Console.WriteLine("-> Database= \t" + database);
            Console.WriteLine("--------------------------------------");
        }

        public void Conectare()
        {
            string connectionString = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password + ";database=" + database + ";";
            databaseConnection = new MySqlConnection(connectionString);
        }

        public void Deschidere()
        {
            databaseConnection.Open();
        }

        public void Inchidere()
        {
            databaseConnection.Close();
        }
        
        public string[][] ReturneazaDateCategorii()
        {
            string comandaSQL = "SELECT * FROM categorii ORDER BY id_categorie ASC";
            string[][] rezultat = InterogareSelect2(comandaSQL);
            return rezultat;
        }

        public string[][] ReturneazaDateProduse(int categorieActiva, int tipSortare)
        {
            string comandaSQL = null;
            switch (tipSortare)
            {
                case 0:
                    comandaSQL = "SELECT id_produs, nume_produs, descriere_produs, pret_produs, imagine_produs FROM produse WHERE id_categorie=" + categorieActiva + ";";
                    break;
                case 1:
                    comandaSQL = "SELECT id_produs, nume_produs, descriere_produs, pret_produs, imagine_produs FROM produse WHERE id_categorie=" + categorieActiva + " ORDER BY pret_produs ASC;";
                    break;
                case 2:
                    comandaSQL = "SELECT id_produs, nume_produs, descriere_produs, pret_produs, imagine_produs FROM produse WHERE id_categorie=" + categorieActiva + " ORDER BY pret_produs DESC;";
                    break;
            }
            string[][] rezultat = InterogareSelect2(comandaSQL);
            return rezultat;
        }

        public int ReturneazaIdUtilizator(string UsernameOREmail, string Parola)
        {
            string comandaSQL = "SELECT id_utilizator FROM `utilizatori` WHERE (user_utilizator ='" + UsernameOREmail + "' OR email_utilizator='" + UsernameOREmail + "') AND parola_utilizator = '" + Parola + "';";
            string rezultatInterogare = InterogareSelect(comandaSQL);
            int idUtilizator;
            if (rezultatInterogare == null)
            {
                idUtilizator = 0;
            }
            else
            {
                idUtilizator = Convert.ToInt32(rezultatInterogare);
            }
            return idUtilizator;
        }

        public string[] ReturneazaDateUtilizator(int idUtilizator)
        {
            string comandaSQL = "SELECT U.id_utilizator, U.user_utilizator, U.email_utilizator, U.parola_utilizator, D.nume_utilizator, D.prenume_utilizator, D.adresa_utilizator, D.telefon_utilizator, D.admin_utilizator FROM `detalii_utilizatori` D, utilizatori U WHERE D.id_utilizator = U.id_utilizator AND U.id_utilizator = " + idUtilizator + ";";
            string[] rezultat = InterogareSelect1(comandaSQL);
            return rezultat;
        }

        public bool VerificareExistentaUsernameOREmail(string UsernameOREmail)
        {
            string comandaSQL = "SELECT EXISTS(SELECT * FROM utilizatori WHERE user_utilizator='" + UsernameOREmail + "' OR email_utilizator='" + UsernameOREmail + "');";
            string rezultatInterogare = InterogareSelect(comandaSQL);
            bool ExistentaUsernameOREmail;
            if (Convert.ToInt32(rezultatInterogare) == 1)
            {
                ExistentaUsernameOREmail = true;
            }
            else
            {
                ExistentaUsernameOREmail = false;
            }
            return ExistentaUsernameOREmail;
        }

        public bool VerificareExistentaUsernameOREmailPentruActualizare(string UsernameOREmail, int idUtilizatorActiv)
        {
            string comandaSQL = "SELECT EXISTS(SELECT * FROM utilizatori WHERE (user_utilizator='" + UsernameOREmail + "' OR email_utilizator='" + UsernameOREmail + "') AND id_utilizator!=" + idUtilizatorActiv + ");";
            string rezultatInterogare = InterogareSelect(comandaSQL);
            bool ExistentaUsernameOREmail;
            if (Convert.ToInt32(rezultatInterogare) == 1)
            {
                ExistentaUsernameOREmail = true;
            }
            else
            {
                ExistentaUsernameOREmail = false;
            }
            return ExistentaUsernameOREmail;
        }

        public void AdaugareUtilizator(string Username, string Email, string Parola)
        {
            string comandaSQL = "INSERT INTO utilizatori (user_utilizator,email_utilizator, parola_utilizator) VALUES ('" + Username + "', '" + Email + "', '" + Parola + "');";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public void AdaugareDetaliiUtilizator(int idUtilizator, string Nume, string Prenume, string Adresa, string Telefon)
        {
            string comandaSQL = "INSERT INTO detalii_utilizatori (nume_utilizator,prenume_utilizator,adresa_utilizator,telefon_utilizator,admin_utilizator, id_utilizator) VALUES ('" + Nume + "', '" + Prenume + "', '" + Adresa + "', '" + Telefon + "',false, " + idUtilizator + ");";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public void ActualizareUtilizator(int idUtilizator, string Username, string Email, string Parola, string Nume, string Prenume, string Adresa, string Telefon)
        {
            string comandaSQL = "UPDATE utilizatori SET user_utilizator='" + Username + "', email_utilizator='" + Email + "', parola_utilizator='" + Parola + "' WHERE id_utilizator=" + idUtilizator + ";";
            InterogareInsertUpdateDelete(comandaSQL);
            comandaSQL = "UPDATE detalii_utilizatori SET nume_utilizator='" + Nume + "', prenume_utilizator='" + Prenume + "', adresa_utilizator='" + Adresa + "', telefon_utilizator='" + Telefon + "' WHERE id_utilizator=" + idUtilizator + ";";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public string[][] ReturneazaProduseDinCos(int idUtilizator)
        {
            string comandaSQL = "SELECT produse.id_produs, produse.nume_produs, produse.pret_produs, produse.imagine_produs FROM cos, produse where cos.id_utilizator=" + idUtilizator + " and cos.id_produs=produse.id_produs;";
            string[][] rezultat = InterogareSelect2(comandaSQL);
            return rezultat;
        }

        public string ReturneazaCantitateProdusDinCos(string idProdus, string idUtilizator)
        {
            string comandaSQL = "SELECT cantitate FROM cos WHERE (id_produs=" + idProdus + " AND id_utilizator=" + idUtilizator + ");";
            string cantitate = InterogareSelect(comandaSQL);
            if (cantitate == null)
            {
                cantitate = "0";
            }
            return cantitate;
        }

        public void StergeProdusDinCos(int idUtilizator, int idProdus)
        {
            string comandaSQL = "DELETE FROM `cos` WHERE id_produs=" + idProdus + " AND id_utilizator=" + idUtilizator;
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public void AdaugaProdusInCos(int idUtilizator, int idProdus)
        {
            string comandaSQL = "SELECT EXISTS(SELECT * FROM cos WHERE (id_produs=" + idProdus + " AND id_utilizator=" + idUtilizator + "));";
            int existentaProdusInCos = Convert.ToInt32(InterogareSelect(comandaSQL));

            if (existentaProdusInCos == 0)
            {
                comandaSQL = "INSERT INTO cos (id_produs,cantitate, id_utilizator) VALUES (" + idProdus + ",1," + idUtilizator + ");";
                InterogareInsertUpdateDelete(comandaSQL);
            }
            if (existentaProdusInCos == 1)
            {
                comandaSQL = "UPDATE cos SET cantitate = cantitate + 1 WHERE(id_produs = " + idProdus + " and id_utilizator = " + idUtilizator + ")";
                InterogareInsertUpdateDelete(comandaSQL);
            }
        }

        public void AdaugaCantitateInCos(int idProdus, int idUtilizator)
        {
            string comandaSQL = "UPDATE cos SET cantitate = cantitate + 1  WHERE(id_produs = " + idProdus + " AND id_utilizator = " + idUtilizator + ");";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public void StergeCantitateInCos(int idProdus, int idUtilizator)
        {
            string comandaSQL = "UPDATE cos SET cantitate = cantitate - 1  WHERE(id_produs = " + idProdus + " AND id_utilizator = " + idUtilizator + ");";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        public void AdaugaProduseInComanda(int[] idProduse, int[] cantitati, string adresa, string valoareComanda, string[] preturiProduse, int idUtilizator)
        {
            try
            {
                int nrProduse = idProduse.Length;
                double valoare = Convert.ToDouble(valoareComanda);
                valoareComanda = valoareComanda.Replace(",", ".");

                string comandaSQL = "INSERT into comenzi(data_comanda, adresa, valoare, id_utilizator) VALUES(SYSDATE(), '" + adresa + "', " + valoareComanda + "," + idUtilizator + ");";
                InterogareInsertUpdateDelete(comandaSQL);

                for (int i = 0; i < nrProduse; i++)
                {
                    preturiProduse[i] = preturiProduse[i].Replace(",", ".");
                    comandaSQL = "INSERT INTO produse_din_comanda (id_produs,cantitate, pret, id_comanda) VALUES (" + idProduse[i] + "," + cantitati[i] + "," + preturiProduse[i] + ",(select max(id_comanda) max from comenzi where id_utilizator = " + idUtilizator + "));";
                    InterogareInsertUpdateDelete(comandaSQL);
                }

                comandaSQL = "DELETE FROM cos WHERE id_utilizator= " + idUtilizator + ";";
                InterogareInsertUpdateDelete(comandaSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string[][] ReturneazaComenziFaraImagine(int idUtilizator)
        {
            string comandaSQL;
            if (idUtilizator == 0)
            {
                comandaSQL = "SELECT id_comanda, DATE_FORMAT(data_comanda, '%d.%m.%Y'), adresa, livrata, valoare FROM comenzi ORDER BY id_comanda DESC;";
            }
            else
            {
                comandaSQL = "SELECT id_comanda, DATE_FORMAT(data_comanda, '%d.%m.%Y'), adresa, livrata, valoare FROM comenzi WHERE id_utilizator=" + idUtilizator + " ORDER BY id_comanda DESC;";
            }
            string[][] rezultat = InterogareSelect2(comandaSQL);

            return rezultat;
        }

        public string[][] ReturneazaComenzi(int idUtilizator)
        {
            string[][] comenziFaraImagine = ReturneazaComenziFaraImagine(idUtilizator);
            string[][] final  = new string[comenziFaraImagine.Length][];

            for (int i = 0; i < comenziFaraImagine.Length; i++)
            {
                final[i] = new string[(comenziFaraImagine[i].Length+1)];
                for (int j=0;j< comenziFaraImagine[i].Length;j++)
                {
                    final[i][j] = comenziFaraImagine[i][j];                    
                }
                string idComanda = final[i][0];
                string comandaSQL = "SELECT p.imagine_produs FROM produse_din_comanda pc, produse p WHERE pc.id_comanda=" + idComanda + " and pc.id_produs=p.id_produs ORDER by p.pret_produs DESC LIMIT 1;";
                final[i][comenziFaraImagine[i].Length] = InterogareSelect(comandaSQL);
            }
            return final;
        }

        public string[][] ReturneazaDetaliiComanda(int idComanda)
        {
            string comandaSQL = "SELECT pc.id_produs, p.imagine_produs, p.nume_produs, pc.cantitate, pc.pret FROM produse_din_comanda pc, produse p WHERE pc.id_comanda = " + idComanda + " and pc.id_produs = p.id_produs";
            string[][] rezultat = InterogareSelect2(comandaSQL);
            return rezultat;
        }

        public void ActualizareLivrareComanda(int idComanda)
        {
            string comandaSQL = "UPDATE comenzi SET livrata=1  WHERE id_comanda=" + idComanda + ";";
            InterogareInsertUpdateDelete(comandaSQL);
        }

        private void InterogareInsertUpdateDelete(string comandaSQL)
        {
            try
            {
                Deschidere();
                MySqlCommand commandDatabase = new MySqlCommand(comandaSQL, databaseConnection);
                MySqlDataReader reader;
                reader = commandDatabase.ExecuteReader();
                Inchidere();
            }
            catch (Exception ex)
            {
                Inchidere();
                MessageBox.Show(ex.Message);
            }
        }

        private string InterogareSelect(string comandaSQL)
        {
            string rezultat = null;
            try
            {
                Deschidere();
                MySqlCommand commandDatabase = new MySqlCommand(comandaSQL, databaseConnection);
                MySqlDataReader reader;
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    rezultat = reader.GetString(0);
                }
                reader.Close();
                Inchidere();
            }
            catch (Exception ex)
            {
                Inchidere();
                MessageBox.Show(ex.Message);
            }
            return rezultat;
        }

        private string[] InterogareSelect1(string comandaSQL)
        {
            string[] rezultat = null;
            try
            {
                Deschidere();
                MySqlCommand commandDatabase = new MySqlCommand(comandaSQL, databaseConnection);
                MySqlDataReader reader;

                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    rezultat = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rezultat[i] = reader.GetString(i);
                    }

                }
                reader.Close();
                Inchidere();
            }
            catch (Exception ex)
            {
                Inchidere();
                MessageBox.Show(ex.Message);
            }
            return rezultat;
        }

        private string[][] InterogareSelect2(string comandaSQL)
        {
            string[][] rezultat = new string[0][];
            
            try
            {
                Deschidere();
                MySqlCommand commandDatabase = new MySqlCommand(comandaSQL, databaseConnection);
                MySqlDataReader reader;
                int nrRanduri = 0;

                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nrRanduri++;
                    }
                }
                reader.Close();

                if (nrRanduri > 0)
                {
                    reader = commandDatabase.ExecuteReader();
                    rezultat = new string[nrRanduri][];
                    for (int i = 0; i < nrRanduri; i++)
                    {
                        rezultat[i] = new string[reader.FieldCount];
                    }
                    if (reader.HasRows)
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            for (int j = 0; j < reader.FieldCount; j++)
                            {
                                rezultat[i][j] = reader.GetString(j);
                            }
                            i++;
                        }
                    }
                }
                Inchidere();

            }
            catch (Exception ex)
            {
                Inchidere();
                MessageBox.Show(ex.Message);
            }
            
            return rezultat;
        }

    }
}