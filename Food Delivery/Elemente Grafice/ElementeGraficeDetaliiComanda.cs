using Food_Delivery.Elemente;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Food_Delivery.Elemente_Grafice
{
    class ElementeGraficeDetaliiComanda
    {
        TableLayoutPanel tabelGeneralProduse;
        TableLayoutPanel[] tabelProduseComanda;
        PictureBox[] picturesImagineProdusComanda;
        Label[] numeProduseComanda;
        Guna2TextBox[] cantitateProduseComanda;
        Label[] pretProduseComanda;        

        public ElementeGraficeDetaliiComanda(Form form, Guna2Panel PanelDetaliiComanda, int idComanda, string PretComanda, string Adresa)
        {
            PanelDetaliiComanda.Visible = false;
            DateTime start = DateTime.Now;
            PictureBox animatie = new PictureBox
            {
                Name = "animatie",
                //Image = Image.FromFile(Application.StartupPath + @"\img\waiting.gif"),
                Image = Properties.Resources.waiting,
                Width = 120,
                Height = 120,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Enabled = true
            };
            animatie.Location = new Point(800 - animatie.Size.Width / 2, 235);
            form.Controls.Add(animatie);

            string[][] DetaliiComanda = VariabileGlobale.bazaDeDate.ReturneazaDetaliiComanda(Convert.ToInt32(idComanda));
            int nrProduseComanda = DetaliiComanda.Length;
            string[] imaginiProduseComanda = new string[nrProduseComanda];
            string[] numeProduse = new string[nrProduseComanda];
            string[] CantitatiProduse = new string[nrProduseComanda];
            string[] preturiProduse = new string[nrProduseComanda];
            for (int i = 0; i < nrProduseComanda; i++)
            {
                imaginiProduseComanda[i] = DetaliiComanda[i][1];
                numeProduse[i] = DetaliiComanda[i][2];
                CantitatiProduse[i] = DetaliiComanda[i][3];
                preturiProduse[i] = DetaliiComanda[i][4];
            }

            PanelDetaliiComanda.Controls.Clear();
            PanelDetaliiComanda.Height = 30;

            InitializareTabelGeneralProduse(PanelDetaliiComanda.Width);
            AlocareMemorie(nrProduseComanda);
            InitializareTabelProduseComanda(nrProduseComanda);        
            InitializarePicturesImagineProdusComanda(nrProduseComanda, imaginiProduseComanda);
            InitializareNumeProduseComanda(nrProduseComanda, numeProduse);
            InitializareCantitateProduseComanda(nrProduseComanda, CantitatiProduse);
            InitializarePretProduseComanda(nrProduseComanda, preturiProduse);

            AdaugareElementeInTabele(nrProduseComanda);
            PanelDetaliiComanda.Controls.Add(tabelGeneralProduse);
            PanelDetaliiComanda.Height += tabelGeneralProduse.Height + 60;

            Label labelDetaliiLivrare = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Roboto", 9, FontStyle.Bold),
                Width = 100,
                Height = 20,
                BackColor = Color.Transparent,
                Location = new Point(15, tabelGeneralProduse.Height + 60),
                Text = "Detalii Livrare: "
            };
            PanelDetaliiComanda.Controls.Add(labelDetaliiLivrare);
            PanelDetaliiComanda.Height += labelDetaliiLivrare.Height;

            Label labelAdresaComanda = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Roboto", 8, FontStyle.Regular),
                Width = 262,
                Height = 60,
                BackColor = Color.Transparent,
                Location = new Point(15, labelDetaliiLivrare.Location.Y + 30),
                Text = Adresa
            };
            PanelDetaliiComanda.Controls.Add(labelAdresaComanda);
            PanelDetaliiComanda.Height += labelAdresaComanda.Height;

            Label labelPretComanda = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Roboto", 12, FontStyle.Bold),
                AutoSize = true,
                Width = 100,
                Height = 40,
                BackColor = Color.Transparent,
                Text = "Total: " + PretComanda
            };
            labelPretComanda.Location = new Point(PanelDetaliiComanda.Width / 2 - labelPretComanda.Width / 2, labelAdresaComanda.Location.Y + labelAdresaComanda.Height + 50);
            PanelDetaliiComanda.Controls.Add(labelPretComanda);
            PanelDetaliiComanda.Height += labelPretComanda.Height + 50;
            PanelDetaliiComanda.Location = new Point(657, 12);

            while ((DateTime.Now - start).TotalMilliseconds < 2000)
            {
                Application.DoEvents();
            }
            PanelDetaliiComanda.Visible = true;
            form.Controls.Remove(animatie);            
        }

        ~ElementeGraficeDetaliiComanda()
        {
            Console.WriteLine("\t-->S-a apelat deconstructorul pentru ElementeGraficeDetaliiComanda.");
        }

        private void InitializareTabelGeneralProduse(int PanelDetaliiComanda_Width)
        {
            tabelGeneralProduse = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                Name = "tebelProduse",
                ColumnCount = 1,
                Width = 292 - 30,
                Height = 0,
                Dock = DockStyle.None,
                AutoScroll = false,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            tabelGeneralProduse.Location = new Point(PanelDetaliiComanda_Width / 2 - tabelGeneralProduse.Width / 2, 30);
        }

        private void AlocareMemorie(int nrProduseComanda)
        {
            tabelProduseComanda = new TableLayoutPanel[nrProduseComanda];
            picturesImagineProdusComanda = new PictureBox[nrProduseComanda];
            numeProduseComanda = new Label[nrProduseComanda];
            cantitateProduseComanda = new Guna2TextBox[nrProduseComanda];
            pretProduseComanda = new Label[nrProduseComanda];
        }

        private void InitializareTabelProduseComanda(int nrProduseComanda)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                tabelProduseComanda[i] = new TableLayoutPanel
                {
                    BackColor = Color.Transparent,
                    Name = "tebelProduse",
                    ColumnCount = 4,
                    Width = 258,
                    Height = 70,
                    Dock = DockStyle.None,
                    AutoScroll = false,
                    AutoSize = false,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };
                tabelGeneralProduse.Height += tabelProduseComanda[i].Height + 10;
            }
        }

        private void InitializarePicturesImagineProdusComanda(int nrProduseComanda, string[] ImagineProduseComanda)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                picturesImagineProdusComanda[i] = new PictureBox()
                {
                    Height = 64,
                    Width = 64,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(2, 70 / 2 - 64 / 2),
                    Image = Image.FromFile(Application.StartupPath + @"\img\produse\" + ImagineProduseComanda[i])
                };
            }
        }

        private void InitializareNumeProduseComanda(int nrProduseComanda, string[] numeProduse)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                numeProduseComanda[i] = new Label()
                {
                    ForeColor = Color.White,
                    Font = new Font("Roboto", 7, FontStyle.Bold),
                    AutoSize = false,
                    Width = 100,
                    Height = 40,
                    BackColor = Color.Transparent,
                    Text = numeProduse[i],
                    Margin = new Padding(0, 70 / 2 - 15 / 2, 0, 0)
                };
            }
        }

        private void InitializareCantitateProduseComanda(int nrProduseComanda, string[] CantitatiProduse)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                cantitateProduseComanda[i] = new Guna2TextBox()
                {
                    AutoSize = false,
                    Width = 30,
                    Height = 19,
                    Margin = new Padding(8, 70 / 2 - 19 / 2, 8, 0),
                    Name = "cantitate_",
                    BorderRadius = 5,
                    TextAlign = HorizontalAlignment.Center,
                    FillColor = Color.FromArgb(47, 49, 55),
                    ReadOnly = true,
                    Text = CantitatiProduse[i],
                    ForeColor = Color.White
                };
            }
        }

        private void InitializarePretProduseComanda(int nrProduseComanda, string[] preturiProduse)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                pretProduseComanda[i] = new Label()
                {
                    ForeColor = Color.White,
                    Font = new Font("Roboto", 7, FontStyle.Regular),
                    Width = 70,
                    Height = 15,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0, 70 / 2 - 15 / 2, 0, 0),
                    Text = preturiProduse[i]
                };
            }
        }

        private void AdaugareElementeInTabele(int nrProduseComanda)
        {
            for (int i = 0; i < nrProduseComanda; i++)
            {
                tabelProduseComanda[i].Controls.Add(picturesImagineProdusComanda[i]);
                tabelProduseComanda[i].Controls.Add(numeProduseComanda[i]);
                tabelProduseComanda[i].Controls.Add(cantitateProduseComanda[i]);
                tabelProduseComanda[i].Controls.Add(pretProduseComanda[i]);

                tabelGeneralProduse.Controls.Add(tabelProduseComanda[i]);
            }
        }
    }
}