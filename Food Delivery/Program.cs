using System;
using System.Windows.Forms;
using Food_Delivery.Baza_de_date;
using Food_Delivery.Elemente;

namespace Food_Delivery
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string datasource = "remotemysql.com";
            int port = 3306;
            string username = "714o27gv4s";
            string password = "Doot7n9LlT";
            string database = "714o27gv4s";

            try
            {
                VariabileGlobale.bazaDeDate = new BazaDeDate(datasource, port, username, password, database);
                VariabileGlobale.bazaDeDate.Conectare();
                Application.Run(new FormPrincipal());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}