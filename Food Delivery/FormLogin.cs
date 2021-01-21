using System;
using System.Drawing;
using System.Windows.Forms;
using Food_Delivery.Elemente;
using Food_Delivery.Elemente_Grafice;
using Guna.UI2.WinForms;

namespace Food_Delivery
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();            
        }

        public FormLogin(Guna2Button butonLogin, ElementeGrafice elementeGrafice, Form form)
        {
            InitializeComponent();            
            buttonSignIn.Click += (sender, EventArgs) => { Click_SignIn(butonLogin, elementeGrafice, form); };
        }

        private void Click_SignIn(Guna2Button butonLogin, ElementeGrafice elementeGrafice, Form form)
        {
            string username = textBoxUsernameOREmail.Text;
            string parola = textBoxParola.Text;

            int id = VariabileGlobale.bazaDeDate.ReturneazaIdUtilizator(username, parola);

            if (id!=0)
            {
                //pictureBoxAnimatie.Image= Image.FromFile(Application.StartupPath + @"\img\" + "unlock" + ".gif");
                pictureBoxAnimatie.Image = Properties.Resources.unlock;
                pictureBoxAnimatie.Enabled = true;

                VariabileGlobale.utilizatorActiv = new Utilizator(VariabileGlobale.bazaDeDate, id);
                butonLogin.Text = "Logout";
                elementeGrafice.AdaugareImagineLivrari(form);
                elementeGrafice.AdaugareImagineComenzi(form);
                elementeGrafice.AdaugareImagineDetaliiCont(form);
                elementeGrafice.AdaugareImagineCos(form);

                DateTime start = DateTime.Now;
                while ((DateTime.Now - start).TotalMilliseconds < 2000)
                {
                    Application.DoEvents();
                }                
                this.Close();
            }
            else
            {
                //pictureBoxAnimatie.Image = Image.FromFile(Application.StartupPath + @"\img\" + "lock" + ".gif");
                pictureBoxAnimatie.Image = Properties.Resources._lock;
                pictureBoxAnimatie.Enabled = true;
            }
        }

        private void TextBoxParola_IconRightClick(object sender, EventArgs e)
        {
            textBoxParola.UseSystemPasswordChar = !textBoxParola.UseSystemPasswordChar;
        }

        private void TextBox_ApasareEnterORTab(object sender, KeyEventArgs e)
        {
            var textBox = sender as Guna2TextBox;
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonSignIn.PerformClick();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Tab)
            {
                switch(textBox.Name)
                {
                    case "textBoxUsernameOREmail":
                        textBoxParola.Focus();
                        break;
                    case "textBoxParola":
                        textBoxUsernameOREmail.Focus();
                        break;
                    default:
                        break;
                }                    
            }
        }

        private void LabelCreaza_Click(object sender, EventArgs e)
        {
            FormInregistrare formInregistrare = new FormInregistrare();
            formInregistrare.Show();
        }
    }
}