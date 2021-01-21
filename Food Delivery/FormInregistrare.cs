using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Windows.Forms;
using Food_Delivery.Elemente;

namespace Food_Delivery
{
    public partial class FormInregistrare : Form
    {
        public FormInregistrare()
        {
            InitializeComponent();
            if (VariabileGlobale.utilizatorActiv != null)
            {
                TextBox_Username.Text = VariabileGlobale.utilizatorActiv.ReturneazaUserUtilizator();
                TextBox_Email.Text = VariabileGlobale.utilizatorActiv.ReturneazaEmailUtilizator();
                TextBox_Parola.Text = VariabileGlobale.utilizatorActiv.ReturneazaParolaUtilizator();
                TextBox_Nume.Text = VariabileGlobale.utilizatorActiv.ReturneazaNumeUtilizator();
                TextBox_Prenume.Text = VariabileGlobale.utilizatorActiv.ReturneazaPrenumeUtilizator();
                TextBox_Adresa.Text = VariabileGlobale.utilizatorActiv.ReturneazaAdresaUtilizator();
                TextBox_Telefon.Text = VariabileGlobale.utilizatorActiv.ReturneazaTelefonUtilizator();
            }
        }

        private void TextBox_Parola_IconRightClick(object sender, EventArgs e)
        {
            TextBox_Parola.UseSystemPasswordChar = !TextBox_Parola.UseSystemPasswordChar;
        }

        private void ButonInregistrare_Click(object sender, EventArgs e)
        {
            bool eroare = false;
            bool UserEmailDisponibil = false;
            string textEroare = "";
            label_Eroare.Text = "";
            if (TextBox_Username.Text == "")
            {
                eroare = true;
                textEroare += "un Username ";
            }
            if (TextBox_Email.Text == "")
            {
                eroare = true;
                textEroare += "un Email ";
            }
            if (TextBox_Parola.Text == "")
            {
                eroare = true;
                textEroare += "o Parola ";
            }


            if (eroare == true)
            {
                textEroare += "!";
                label_Eroare.Text = "Nu ai introdus " + textEroare;
            }
            else
            {
                bool Usergasit = VariabileGlobale.bazaDeDate.VerificareExistentaUsernameOREmail(TextBox_Username.Text);
                if (Usergasit == false)
                {
                    bool EmailGasit = VariabileGlobale.bazaDeDate.VerificareExistentaUsernameOREmail(TextBox_Email.Text);
                    if (EmailGasit == false)
                    {
                        if (new EmailAddressAttribute().IsValid(TextBox_Email.Text))
                        {
                            UserEmailDisponibil = true;
                        }
                        else
                        {
                            label_Eroare.Text = "Email-ul introdus nu este valid!";
                        }

                    }
                    else
                    {
                        label_Eroare.Text = "Email-ul introdus deja exista in baza de date!";
                    }
                }
                else
                {
                    label_Eroare.Text = "Username-ul introdus deja exista in baza de date!";
                }
            }

            label_Eroare.Location = new Point(this.Size.Width / 2 - label_Eroare.Size.Width / 2, label_Eroare.Location.Y);
            label_Eroare.Visible = true;

            if (UserEmailDisponibil == true)
            {
                CreazaUserNou(TextBox_Username.Text, TextBox_Email.Text, TextBox_Parola.Text, TextBox_Nume.Text, TextBox_Prenume.Text, TextBox_Adresa.Text, TextBox_Telefon.Text, this);
            }
        }

        private void CreazaUserNou(string Username, string Email, string Parola, string Nume, string Prenume, string Adresa, string Telefon, Form form)
        {
            try
            {
                PictureBox animatie = new PictureBox
                {
                    Name = "animatie",
                    //Image = Image.FromFile(Application.StartupPath + @"\img\waiting.gif"),
                    Image = Properties.Resources.waiting,
                    Width = 80,
                    Height = 80,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Enabled = true
                };
                animatie.Location = new Point(this.Size.Width / 2 - animatie.Size.Width / 2, 480);
                form.Controls.Add(animatie);

                VariabileGlobale.bazaDeDate.AdaugareUtilizator(Username, Email, Parola);
                DateTime start1 = DateTime.Now;
                while ((DateTime.Now - start1).TotalMilliseconds < 2000)
                {
                    Application.DoEvents();
                }
                int idUtilizator = VariabileGlobale.bazaDeDate.ReturneazaIdUtilizator(Username, Parola);
                VariabileGlobale.bazaDeDate.AdaugareDetaliiUtilizator(idUtilizator, Nume, Prenume, Adresa, Telefon);

                //animatie.Image = Image.FromFile(Application.StartupPath + @"\img\unlock.gif");
                animatie.Image = Properties.Resources.unlock;
                animatie.Width = 40;
                animatie.Height = 40;
                animatie.Location = new Point(this.Size.Width / 2 - animatie.Size.Width / 2, 500);
                DateTime start = DateTime.Now;
                while ((DateTime.Now - start).TotalMilliseconds < 2000)
                {
                    Application.DoEvents();
                }
                form.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
