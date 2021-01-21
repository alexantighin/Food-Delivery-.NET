using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Food_Delivery.Baza_de_date;
using Food_Delivery.Elemente;

namespace Food_Delivery.Elemente_Grafice
{
    public class ElementeGrafice
    {
        PictureBox imagineLogo;
        Guna2Button butonSpreMeniu;
        Guna2Button butonLogin;

        PictureBox livrariAdmin;
        PictureBox comenzi;
        PictureBox detaliicont;
        PictureBox cos;

        Label labelTipOrdonare;
        Guna2ComboBox comboBoxSchimbareOrdonare;
        TableLayoutPanel tabelGeneralProduse;
        ElementeGraficeCategorii elementeGraficeCategorii;  

        public ElementeGrafice(Form form)
        {
            InitializareimagineLogo(form);
            InitializarebutonSpreMeniu(form);
            InitializarebutonLogin(form);
            InitializarelivrariAdmin();
            Initializarecomenzi();
            Initializaredetaliicont();
            Initializarecos();

            InitializarelabelTipOrdonare();
            InitializarecomboBoxSchimbareOrdonare(form);
            InitializaretabelGeneralProduse();
            InitializareElementeGraficeCategorii(VariabileGlobale.bazaDeDate, form);
        }

        ~ElementeGrafice()
        {
            Console.WriteLine("-->S-a apelat deconstructorul pentru ElementeGrafice.");
        }

        private void InitializareForm(Form form)
        {            
            form.BackColor = Color.Black;
            form.BackgroundImage = Properties.Resources.first;

            form.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void InitializareimagineLogo(Form form)
        {
            imagineLogo = new PictureBox
            {
                Image = Properties.Resources.food_delivery_white,
                Width = 70,
                Height = 30,
                Location = new Point(90, 30),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled=false
            };
            imagineLogo.Click += (sender, EventArgs) => { ClickImagineLogo(sender, form); };
        }
        private void ClickImagineLogo(object sender, Form form)
        {
            var pictureBox = sender as PictureBox;
            pictureBox.Image = Properties.Resources.food_delivery_white;
            pictureBox.Cursor = Cursors.Default;
            pictureBox.Enabled = false;
            livrariAdmin.Image = Properties.Resources.livrari_white;
            //comenzi.Image=Image.FromFile(Application.StartupPath + @"\img\icons\" + "comenzi_white" + ".png");
            comenzi.Image = Properties.Resources.comenzi_white;
            //detaliicont.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "detalii_cont_white" + ".png");
            detaliicont.Image = Properties.Resources.detalii_cont_white;
            //cos.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "cos_white" + ".png");
            cos.Image = Properties.Resources.cos_white;
            AfisareInitiala(form);
        }

        private void InitializarebutonSpreMeniu(Form form)
        {
            butonSpreMeniu = new Guna2Button
            {
                Location = new Point(180, 400),
                Height = 40,
                Width = 170,
                Text = "Vezi meniu complet",
                TextAlign = HorizontalAlignment.Center,
                TextOffset = new Point(0, 0),
                ImageOffset = new Point(0, 0),
                ForeColor = Color.White,
                FillColor = Color.Transparent,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BorderColor = Color.White,
                BorderRadius = 12,
                BorderThickness = 1,
                Cursor = Cursors.Hand
            };
            butonSpreMeniu.HoverState.FillColor = Color.FromArgb(40, 40, 40);
            butonSpreMeniu.HoverState.ForeColor = Color.White;
            butonSpreMeniu.Click += (sender, EventArgs) => { ClickButonSpreMeniu(form); };
        }

        private void ClickButonSpreMeniu(Form form)
        {
            form.Controls.Clear();
            form.BackgroundImage = null;
            form.BackColor = Color.FromArgb(245, 245, 246);
            AfisareMeniu(form);
        }

        private void InitializarebutonLogin(Form form)
        {
            butonLogin = new Guna2Button
            {
                Name = "button_Login",
                Text = "Login",
                Width = 80,
                Height = 20,
                BorderRadius = 7,
                Margin = new Padding(0, 0, 0, 0),
                Location = new Point(1100, 35),
                Cursor = Cursors.Hand
            };

            butonLogin.TextAlign = HorizontalAlignment.Center;
            butonLogin.TextOffset = new Point(0, 0);
            butonLogin.ImageOffset = new Point(0, 0);
            butonLogin.ForeColor = Color.White;
            butonLogin.FillColor = Color.FromArgb(239, 157, 126);
            butonLogin.HoverState.FillColor = Color.FromArgb(225, 129, 93);
            butonLogin.HoverState.ForeColor = Color.White;
            butonLogin.Font = new Font("Roboto", 10, FontStyle.Bold);
            butonLogin.Click += (sender, EventArgs) => { Click_ButonLogin(sender, form, this); };
        }

        public void Click_ButonLogin(object sender, Form form, ElementeGrafice elementeGrafice)
        {
            var button = sender as Guna2Button;
            if (button.Text == "Login")
            {
                FormLogin formLogin = new FormLogin(butonLogin, elementeGrafice, form);
                formLogin.Show();
            }
            if(button.Text=="Logout")
            {
                button.Text = "Login";
                /*stergere utilizator*/
                form.Controls.Remove(livrariAdmin);
                form.Controls.Remove(comenzi);
                form.Controls.Remove(detaliicont);
                form.Controls.Remove(cos);
                VariabileGlobale.utilizatorActiv = new Utilizator();
            }
        }

        private void Initializaredetaliicont()
        {
            detaliicont = new PictureBox
            {
                //Image = Image.FromFile(Application.StartupPath + @"\img\icons\"+"detalii_cont_white"+".png"),
                Image = Properties.Resources.detalii_cont_white,
                Width = 20,
                Height = 20,
                Location = new Point(1065, 35),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled = true,
                Cursor=Cursors.Hand
            };
            detaliicont.Click += (sender, EventArgs) => { Clickdetaliicont(); };
        }
        private void Clickdetaliicont()
        {
            FormDetaliiCont formDetaliiCont = new FormDetaliiCont();
            formDetaliiCont.Show();        
        }

        private void Initializarecomenzi()
        {
            comenzi = new PictureBox
            {
                //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "comenzi_white" + ".png"),
                Image = Properties.Resources.comenzi_white,
                Width = 20,
                Height = 20,
                Location = new Point(1000, 35),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled = true,
                Cursor = Cursors.Hand
            };
            comenzi.Click += (sender, EventArgs) => { Clickcomenzi(); };
        }
        private void Clickcomenzi()
        {
            FormComenziUtilizator formComenzi = new FormComenziUtilizator(false);
            formComenzi.Show();
        }

        private void InitializarelivrariAdmin()
        {
            livrariAdmin = new PictureBox
            {
                //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "livrari_white" + ".png"),
                Image = Properties.Resources.livrari_white,
                Width = 26,
                Height = 20,
                Location = new Point(960, 35),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled = true,
                Cursor = Cursors.Hand
            };
            livrariAdmin.Click += (sender, EventArgs) => { ClicklivrariAdmin(); };
        }
        private void ClicklivrariAdmin()
        {
            FormComenziUtilizator formComenzi = new FormComenziUtilizator(true);
            formComenzi.Show();
        }

        private void Initializarecos()
        {
            cos = new PictureBox
            {
                //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "cos_white" + ".png"),
                Image = Properties.Resources.cos_white,
                Width = 16,
                Height = 20,
                Location = new Point(1034, 35),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled = true,
                Cursor = Cursors.Hand
            };
            cos.Click += (sender, EventArgs) => { ClickCos(); };
        }
        private void ClickCos()
        {
            FormCos formCos = new FormCos();
            formCos.Show();
        }

        public void AdaugareImagineDetaliiCont(Form form)
        {
            if (VariabileGlobale.utilizatorActiv != null)
            {
                if (VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator() != 0)
                {
                    form.Controls.Add(detaliicont);
                }
            }
        }

        public void AdaugareImagineComenzi(Form form)
        {
            if (VariabileGlobale.utilizatorActiv != null)
            {
                if (VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator() != 0)
                {
                    form.Controls.Add(comenzi);
                }
            }
        }

        public void AdaugareImagineLivrari(Form form)
        {
            if (VariabileGlobale.utilizatorActiv != null)
            {
                if (VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator() != 0)
                {
                    if(VariabileGlobale.utilizatorActiv.ReturneazaAdminUtilizator()==true)
                    {
                        form.Controls.Add(livrariAdmin);
                    }                   
                }
            }
        }

        public void AdaugareImagineCos(Form form)
        {
            if (VariabileGlobale.utilizatorActiv != null)
            {
                if (VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator() != 0)
                {
                    form.Controls.Add(cos);
                }
            }
        }

        public void AfisareInitiala(Form form)
        {
            form.Controls.Clear();
            form.Controls.Add(imagineLogo);            
            form.Controls.Add(butonSpreMeniu);
            AdaugareImagineLivrari(form);
            AdaugareImagineComenzi(form);
            AdaugareImagineDetaliiCont(form);
            AdaugareImagineCos(form);
            form.Controls.Add(butonLogin);
            InitializareForm(form);            
        }

        public void AfisareMeniu(Form form)
        {
            form.Controls.Clear();
            ModificareimagineLogo(form);
            MofificareImagineLivrari();
            MofificareImagineComenzi();
            MofificareImagineDetaliiCont();
            MofificareImagineCos();
            form.Controls.Add(imagineLogo);
            AdaugareImagineLivrari(form);
            AdaugareImagineComenzi(form);
            AdaugareImagineDetaliiCont(form);
            AdaugareImagineCos(form);
            form.Controls.Add(butonLogin);
            
            form.Controls.Add(labelTipOrdonare);
            form.Controls.Add(comboBoxSchimbareOrdonare);
            form.Controls.Add(tabelGeneralProduse);
            elementeGraficeCategorii.AfisareCategorii(form);
        }

        private void ModificareimagineLogo(Form form)
        {
            //imagineLogo.Image = Image.FromFile(Application.StartupPath + @"\img\icons\food_delivery.png");
            imagineLogo.Image = Properties.Resources.food_delivery;
            imagineLogo.Click += (sender, EventArgs) => { ClickImagineLogo(sender, form); };
            imagineLogo.Cursor = Cursors.Hand;
            imagineLogo.Enabled = true;
        }

        private void MofificareImagineLivrari()
        {
            //livrariAdmin.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "livrari_black" + ".png");
            livrariAdmin.Image = Properties.Resources.livrari_black;
        }

        private void MofificareImagineComenzi()
        {
            //comenzi.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "comenzi_black" + ".png");
            comenzi.Image = Properties.Resources.comenzi_black;
        }

        private void MofificareImagineDetaliiCont()
        {
            //detaliicont.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "detalii_cont_black" + ".png");
            detaliicont.Image = Properties.Resources.detalii_cont_black;
        }

        private void MofificareImagineCos()
        {
            //cos.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "cos_black" + ".png");
            cos.Image = Properties.Resources.cos_black;
        }

        private void InitializarelabelTipOrdonare()
        {
            labelTipOrdonare = new Label
            {
                Location = new Point(275, 102),
                Text = "Ordonează după:",
                Width = 91,
                Height = 13,
                Font = new Font("Roboto", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(162, 164, 171)
            };
        }

        private void InitializarecomboBoxSchimbareOrdonare(Form form)
        {
            string[] texte = new string[] { "Relevanță", "Preț crescător", "Preț descrescător" };
            comboBoxSchimbareOrdonare = new Guna2ComboBox
            {
                Location = new Point(380, 96),
                Width = 175,
                Height = 24,
                BorderRadius = 10,
                ItemHeight = 18,
                FillColor = Color.FromArgb(235, 233, 233),
                BorderColor = Color.FromArgb(217, 221, 226),
                Cursor = Cursors.Hand
            };
            comboBoxSchimbareOrdonare.Items.AddRange(texte);
            comboBoxSchimbareOrdonare.SelectedIndex = VariabileGlobale.optiuneSortareProduse;
            comboBoxSchimbareOrdonare.SelectedIndexChanged += (sender, EventArgs) => { SchimbareIndex(sender, form, tabelGeneralProduse); };
        }

        private void SchimbareIndex(object sender, Form form, TableLayoutPanel tabelGeneralProduse)
        {
            var combobox = sender as Guna2ComboBox;
            VariabileGlobale.optiuneSortareProduse = combobox.SelectedIndex;

            ElementeGraficeProduse produse = new ElementeGraficeProduse(VariabileGlobale.bazaDeDate, VariabileGlobale.optiuneSortareProduse, VariabileGlobale.categorieActiva, tabelGeneralProduse);
            produse.Afisare(form, tabelGeneralProduse);
        }

        private void InitializaretabelGeneralProduse()
        {
            tabelGeneralProduse = new TableLayoutPanel()
            {
                BackColor = Color.Transparent,
                Location = new Point(275, 130),
                Name = "tabelGeneralProduse",
                Width = 917,
                Height = 0,
                ColumnCount = 6
            };
        }

        private void InitializareElementeGraficeCategorii(BazaDeDate bazaDeDate, Form form)
        {
            elementeGraficeCategorii = new ElementeGraficeCategorii(bazaDeDate, form, tabelGeneralProduse);
        }        
    }
}