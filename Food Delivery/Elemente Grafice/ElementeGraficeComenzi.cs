using System;
using System.Drawing;
using System.Windows.Forms;
using Food_Delivery.Elemente;
using Guna.UI2.WinForms;

namespace Food_Delivery.Elemente_Grafice
{
    class ElementeGraficeComenzi
    {
        Guna2Panel PanelDetaliiComanda;
        TableLayoutPanel tabelGeneralComenzi;
        Guna2Button[] butoaneComenzi;
        PictureBox[] picturesImagineProdusReprezentativ;
        Label[] labelsDataComanda;
        Label[] labelsPretProdus;
        PictureBox[] picturesLivrat;
        bool[] comandaLivrata;

        public ElementeGraficeComenzi(bool admin, Form form)
        {
            InitializarePanelDetaliiComanda(form);

            int idUtilizatorActiv = VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator();
            string[][] comenziTrecute;
            if (admin == true)
            {
                comenziTrecute = VariabileGlobale.bazaDeDate.ReturneazaComenzi(0);
            }
            else
            {
                comenziTrecute = VariabileGlobale.bazaDeDate.ReturneazaComenzi(idUtilizatorActiv);
            }

            InitializareTabelGeneralComenzi();

            int nrComenzi = comenziTrecute.Length;
            AlocareMemorie(nrComenzi);

            int[] idComenzi = new int[nrComenzi];
            string[] imaginiReprezentativeComenzi = new string[nrComenzi];
            string[] dateComenzi = new string[nrComenzi];
            string[] pretComenzi = new string[nrComenzi];
            string[] livrareComenzi = new string[nrComenzi];
            string[] AdreseComenzi = new string[nrComenzi];
            for (int i = 0; i < nrComenzi; i++)
            {
                idComenzi[i] = Convert.ToInt32(comenziTrecute[i][0]);
                imaginiReprezentativeComenzi[i] = comenziTrecute[i][5];
                dateComenzi[i] = comenziTrecute[i][1];
                pretComenzi[i] = comenziTrecute[i][4];
                livrareComenzi[i] = comenziTrecute[i][3];
                AdreseComenzi[i] = comenziTrecute[i][2];
            }

            InitializareButoaneComenzi(nrComenzi, idComenzi, AdreseComenzi,form);
            InitializarePicturesImagineProdusReprezentativ(nrComenzi, imaginiReprezentativeComenzi);
            InitializareLabelsDataComanda(nrComenzi, dateComenzi);
            InitializareLabelsPretProdus(nrComenzi, pretComenzi);
            InitializarePicturesLivrat(nrComenzi, idComenzi, livrareComenzi, admin);

            AdaugareElemetePentruFiecareComanda(nrComenzi);
        }

        ~ElementeGraficeComenzi()
        {
            Console.WriteLine("\t-->S-a apelat deconstructorul pentru ElementeGraficeComenzi.");
        }

        private void InitializarePanelDetaliiComanda(Form form)
        {
            PanelDetaliiComanda = new Guna2Panel
            {
                Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right))),
                BackColor = System.Drawing.Color.Transparent,
                BorderColor = System.Drawing.Color.Transparent,
                BorderRadius = 15,
                FillColor = Color.FromArgb(47, 49, 55),
                Location = new Point(657, 12),
                Name = "PanelDetaliiComanda",
                Size = new Size(292, 30),
                Visible = false
            };
            PanelDetaliiComanda.ShadowDecoration.BorderRadius = 15;
            PanelDetaliiComanda.ShadowDecoration.Depth = 5;
            PanelDetaliiComanda.ShadowDecoration.Enabled = true;
            PanelDetaliiComanda.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 6);

            form.Controls.Add(PanelDetaliiComanda);
        }

        private void InitializareTabelGeneralComenzi()
        {
            tabelGeneralComenzi = new TableLayoutPanel()
            {
                BackColor = Color.Transparent,
                Location = new Point(30, 15),
                Name = "tabelGeneralProduse",
                Width = 595,
                Height = 0,
                ColumnCount = 1
            };
        }

        private void AlocareMemorie(int nrComenzi)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                butoaneComenzi = new Guna2Button[nrComenzi];
                picturesImagineProdusReprezentativ = new PictureBox[nrComenzi];
                labelsDataComanda = new Label[nrComenzi];
                labelsPretProdus = new Label[nrComenzi];
                picturesLivrat = new PictureBox[nrComenzi];
                comandaLivrata = new bool[nrComenzi];
            }
        }

        private void InitializareButoaneComenzi(int nrComenzi, int[] idComenzi, string[] AdreseComenzi, Form form)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                butoaneComenzi[i] = new Guna2Button
                {
                    Width = 590,
                    Height = 120,
                    BorderRadius = 15,
                    Dock = DockStyle.None,
                    AutoSize = false,
                    FillColor = Color.Transparent,
                    Name = "panel_" + i,
                    Cursor = Cursors.Hand,
                    Animated = true

                };
                butoaneComenzi[i].BorderRadius = 15;

                string idComanda = idComenzi[i].ToString();
                int nrOrdine = i;
                butoaneComenzi[i].Click += (sender, EventArgs) => { Click_comanda_buton(sender, idComanda, AdreseComenzi[nrOrdine], form); };

                tabelGeneralComenzi.Controls.Add(butoaneComenzi[i]);
                tabelGeneralComenzi.Height += butoaneComenzi[i].Height + 10;
            }
            form.Controls.Add(tabelGeneralComenzi);
        }

        private void InitializarePicturesImagineProdusReprezentativ(int nrComenzi, string[] imaginiReprezentativeComenzi)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                picturesImagineProdusReprezentativ[i] = new PictureBox()
                {
                    Height = 100,
                    Width = 100,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(20, 120 / 2 - 100 / 2),
                    Image = Image.FromFile(Application.StartupPath + @"\img\produse\" + imaginiReprezentativeComenzi[i])
                };
            }
        }

        private void InitializareLabelsDataComanda(int nrComenzi, string[] dateComenzi)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                labelsDataComanda[i] = new Label()
                {
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 10, FontStyle.Bold),
                    AutoSize = false,
                    Width = 100,
                    Height = 25,
                    BackColor = Color.Transparent,
                    Text = dateComenzi[i],
                    Location = new Point(190, 120 / 2 - 25 / 2)
                };
            }
        }

        private void InitializareLabelsPretProdus(int nrComenzi, string[] pretComenzi)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                labelsPretProdus[i] = new Label()
                {
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 10, FontStyle.Bold),
                    Width = 70,
                    Height = 25,
                    BackColor = Color.Transparent,
                    Location = new Point(360, 120 / 2 - 25 / 2),
                    Text = pretComenzi[i]
                };
            }
        }

        private void InitializarePicturesLivrat(int nrComenzi, int[] idComenzi, string[] livrareComenzi, bool rolAdmin)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                picturesLivrat[i] = new PictureBox()
                {
                    Width = 36,
                    Height = 25,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "delivering_black" + ".png"),
                    Image = Properties.Resources.delivering_black,
                    Location = new Point(500, 120 / 2 - 25 / 2),
                    Name = "pozaLivrare_" + idComenzi[i]
                };
                comandaLivrata[i] = false;

                if (rolAdmin == true && livrareComenzi[i] == "False")
                {
                    int nrOrdine = i;
                    picturesLivrat[i].Click += (sender, EventArgs) => { ClickLivrareComanda(sender, nrOrdine); };
                    picturesLivrat[i].Cursor = Cursors.Hand;
                }
                if (livrareComenzi[i] == "True")
                {
                    //picturesLivrat[i].Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "delivered" + ".png");
                    picturesLivrat[i].Image = Properties.Resources.delivered;
                    picturesLivrat[i].Width = 25;
                    picturesLivrat[i].Height = 25;
                    picturesLivrat[i].Enabled = false;
                    comandaLivrata[i] = true;
                }
            }
        }

        private void AdaugareElemetePentruFiecareComanda(int nrComenzi)
        {
            for (int i = 0; i < nrComenzi; i++)
            {
                butoaneComenzi[i].Controls.Add(picturesImagineProdusReprezentativ[i]);
                butoaneComenzi[i].Controls.Add(labelsDataComanda[i]);
                butoaneComenzi[i].Controls.Add(labelsPretProdus[i]);
                butoaneComenzi[i].Controls.Add(picturesLivrat[i]);
            }
        }

        private void Click_comanda_buton(object sender, string idComanda, string Adresa, Form form)
        {
            var categorie = sender as Guna2Button;
            int nrPanelActiv = Convert.ToInt32(categorie.Name.Substring("panel_".Length));

            for (int i = 0; i < butoaneComenzi.Length; i++)
            {
                if (i == nrPanelActiv)
                {
                    butoaneComenzi[nrPanelActiv].FillColor = Color.FromArgb(47, 49, 55);
                    labelsDataComanda[nrPanelActiv].ForeColor = Color.White;
                    labelsPretProdus[nrPanelActiv].ForeColor = Color.White;
                    if (comandaLivrata[nrPanelActiv] == false)
                    {
                        //picturesLivrat[nrPanelActiv].Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "delivering_white" + ".png");
                        picturesLivrat[nrPanelActiv].Image = Properties.Resources.delivering_white;
                    }
                }
                else
                {
                    butoaneComenzi[i].FillColor = Color.Transparent;
                    labelsDataComanda[i].ForeColor = Color.FromArgb(91, 94, 103);
                    labelsPretProdus[i].ForeColor = Color.FromArgb(91, 94, 103);
                    if (comandaLivrata[i] == false)
                    {
                        //picturesLivrat[i].Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "delivering_black" + ".png");
                        picturesLivrat[i].Image = Properties.Resources.delivering_black;
                    }
                }
            }

            AfisareProduseDinComanda(Convert.ToInt32(idComanda), labelsPretProdus[nrPanelActiv].Text, Adresa, form);
        }

        private void AfisareProduseDinComanda(int idComanda, string PretComanda, string Adresa, Form form)
        {
            Food_Delivery.Elemente_Grafice.ElementeGraficeDetaliiComanda elementeGraficeDetaliiComanda = new Elemente_Grafice.ElementeGraficeDetaliiComanda(form, PanelDetaliiComanda, idComanda, PretComanda, Adresa);
        }

        private void ClickLivrareComanda(object sender, int nrOrdine)
        {
            var imagine = sender as PictureBox;
            imagine.Enabled = false;
            imagine.Visible = false;

            string idComanda = imagine.Name.Substring("pozaLivrare_".Length);
            VariabileGlobale.bazaDeDate.ActualizareLivrareComanda(Convert.ToInt32(idComanda));
            comandaLivrata[nrOrdine] = true;

            imagine.Width = 25;
            imagine.Height = 25;
            //imagine.Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "delivered" + ".png");
            imagine.Image = Properties.Resources.delivered;
            imagine.Cursor = Cursors.Default;
            imagine.Enabled = false;
            imagine.Visible = true;
        }
    }
}