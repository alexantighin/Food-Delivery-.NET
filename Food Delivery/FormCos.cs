using System;
using System.Drawing;
using System.Windows.Forms;
using Food_Delivery.Elemente;
using Guna.UI2.WinForms;

namespace Food_Delivery
{
    public partial class FormCos : Form
    {
        TableLayoutPanel tabelMare;

        TableLayoutPanel[] tabeleProduse;

        PictureBox[] picturesImagineProduse;
        Label[] labelsNumeProduse;
        Label[] labelsPretProduse;
        PictureBox[] picturesStergereProdus;

        PictureBox[] picturesMinus;
        Guna2TextBox[] textBoxCantitate;
        PictureBox[] picturesplus;

        public FormCos()
        {
            InitializeComponent();
            this.ActiveControl = null;           
            AfisareProduseCos();
        }


        private void AlocareMemorie(int nrProduse)
        {
            tabeleProduse = new TableLayoutPanel[nrProduse];
            picturesImagineProduse = new PictureBox[nrProduse];
            labelsNumeProduse = new Label[nrProduse];
            labelsPretProduse = new Label[nrProduse];
            picturesStergereProdus = new PictureBox[nrProduse];

            picturesMinus = new PictureBox[nrProduse];
            textBoxCantitate = new Guna2TextBox[nrProduse];
            picturesplus = new PictureBox[nrProduse];
        }

        private void InitializareTabelMare()
        {
            tabelMare = new TableLayoutPanel
            {
                Width = 570,
                Height = 0,
                Location = new Point(30, 12),
                BackColor = Color.Transparent,
                ColumnCount = 1,
                Name = "tabelGeneralCos"
            };
        }

        private void InitializareTabeleProduse(int nrProduse, int[] idProduseDinCos)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                tabeleProduse[i] = new TableLayoutPanel
                {
                    ColumnCount = 7,
                    BackColor = Color.Transparent,
                    Width = 565,
                    Height = 110,
                    Name = "tabelCosProdus_" + idProduseDinCos[i],
                    AutoScroll = false,
                    AutoSize = false,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.None,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows
                };
                tabelMare.Controls.Add(tabeleProduse[i]);
                tabelMare.Height += tabeleProduse[i].Height + 10;
            }
        }

        private void InitializarePicturesImagineProduse(int nrProduse, int[] idProduseDinCos)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                picturesImagineProduse[i] = new PictureBox()
                {
                    Height = 105,
                    Width = 105,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(1, 1, 30, 1),
                    BackColor = Color.Transparent,
                    Name = "pozaProdus_" + idProduseDinCos[i]
                };
            }
        }

        private void InitializareLabelsNumeProduse(int nrProduse, int[] idProduseDinCos)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                labelsNumeProduse[i] = new Label()
                {
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 11, FontStyle.Bold),
                    AutoSize = false,
                    Width = 150,
                    Height = 50,
                    Margin = new Padding(0, 45, 40, 0),
                    BackColor = Color.Transparent,
                    Name = "labelNumeProdus_" + idProduseDinCos[i]
                };
            }
        }

        private void InitializarePicturesMinus(int nrProduse, int[] idProduseDinCos, int idUtilizatorActiv)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                int nrOrdine = i;
                picturesMinus[i] = new PictureBox()
                {
                    Height = 2,
                    Width = 10,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(0, 105 / 2 - 1, 0, 0),
                    BackColor = Color.Transparent,
                    Name = "pozaMinus_" + idProduseDinCos[i],
                    Cursor = Cursors.Hand,
                    //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "minus" + ".png")
                    Image = Properties.Resources.minus
                };
                picturesMinus[i].Click += (sender, EventArgs) => { ClickMinus(sender, EventArgs, nrOrdine, idUtilizatorActiv); };
            }
        }

        private void InitializareTextBoxCantitate(int nrProduse, int[] idProduseDinCos, int idUtilizatorActiv)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                textBoxCantitate[i] = new Guna2TextBox()
                {
                    Height = 20,
                    Width = 37,
                    Margin = new Padding(10, 105 / 2 - 20 / 2, 10, 0),
                    BackColor = Color.Transparent,
                    Name = "cantitate_" + idProduseDinCos[i],
                    BorderRadius = 3,
                    TextAlign = HorizontalAlignment.Center,
                    FillColor = Color.FromArgb(245, 245, 246),
                    ReadOnly = false
                };
                textBoxCantitate[i].HoverState.BorderColor = Color.FromArgb(255, 224, 192);
                textBoxCantitate[i].FocusedState.BorderColor = Color.FromArgb(255, 192, 128);
                textBoxCantitate[i].Text = VariabileGlobale.bazaDeDate.ReturneazaCantitateProdusDinCos(idProduseDinCos[i].ToString(), idUtilizatorActiv.ToString());
            }
        }

        private void InitializarePicturesplus(int nrProduse, int[] idProduseDinCos, int idUtilizatorActiv)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                int nrOrdine = i;
                picturesplus[i] = new PictureBox()
                {
                    Height = 10,
                    Width = 10,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(0, 105 / 2 - 10 / 2, 30, 0),
                    BackColor = Color.Transparent,
                    Name = "pozaPlus_" + idProduseDinCos[i],
                    Cursor = Cursors.Hand,
                    //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "plus" + ".png")
                    Image = Properties.Resources.plus
                };
                picturesplus[i].Click += (sender, EventArgs) => { ClickPlus(sender, EventArgs, nrOrdine, idUtilizatorActiv); };
            }
        }

        private void InitializareLabelsPretProduse(int nrProduse, int[] idProduseDinCos)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                labelsPretProduse[i] = new Label()
                {
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 12, FontStyle.Regular),
                    AutoSize = false,
                    Width = 80,
                    Height = 20,
                    Margin = new Padding(0, 105 / 2 - 20 / 2, 20, 0),
                    BackColor = Color.Transparent,
                    Name = "labelPretProdus_" + idProduseDinCos[i]
                };
            }
        }

        private void InitializarePicturesStergereProdus(int nrProduse, int[] idProduseDinCos, int idUtilizatorActiv)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                int nrOrdine = i;
                picturesStergereProdus[i] = new PictureBox()
                {
                    Height = 15,
                    Width = 15,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(0, 105 / 2 - 15 / 2, 0, 0),
                    BackColor = Color.Transparent,
                    Name = "pozaStergereProdus__" + idProduseDinCos[i],
                    Cursor = Cursors.Hand,
                    //Image = Image.FromFile(Application.StartupPath + @"\img\icons\" + "remove" + ".png")
                    Image = Properties.Resources.remove
                };
                picturesStergereProdus[i].Click += (sender, EventArgs) => { ClickStergere(sender, EventArgs, idUtilizatorActiv, idProduseDinCos[nrOrdine], nrOrdine); };
            }
        }

        private void AdaugareElementeInTabeleProduse(int nrProduse)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                tabeleProduse[i].Controls.Add(picturesImagineProduse[i]);
                tabeleProduse[i].Controls.Add(labelsNumeProduse[i]);

                tabeleProduse[i].Controls.Add(picturesMinus[i]);
                tabeleProduse[i].Controls.Add(textBoxCantitate[i]);
                tabeleProduse[i].Controls.Add(picturesplus[i]);

                tabeleProduse[i].Controls.Add(labelsPretProduse[i]);

                tabeleProduse[i].Controls.Add(picturesStergereProdus[i]);
            }
        }

        private void AdaugareProduseInForm(Form form)
        {
            form.Controls.Add(tabelMare);
        }

        private void AtribuiriDetaliiCos(int nrProduse, string[] imaginiProduse, string[] numeProduse, string[] pretProduse, string adresaCos, string telefonCos, string numePrenumeCos)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                picturesImagineProduse[i].Image = Image.FromFile(Application.StartupPath + @"\img\produse\" + imaginiProduse[i]);
                labelsNumeProduse[i].Text = numeProduse[i];
                labelsPretProduse[i].Text = pretProduse[i];
            }
            TextBox_Adresa.Text = adresaCos;
            TextBox_Telefon.Text = telefonCos;
            TextBox_NumePrenume.Text = numePrenumeCos;
        }


        private void ClickStergere(object sender, EventArgs e, int idUtilizator,int idProdus,  int nrOrdine)
        {
            try
            {
                double pretTotalProduseCos = Convert.ToDouble(label_Pret.Text);
                pretTotalProduseCos -= Convert.ToDouble(labelsPretProduse[nrOrdine].Text)*Convert.ToDouble(textBoxCantitate[nrOrdine].Text);
                
                label_Pret.Text = pretTotalProduseCos.ToString("0.00");
                label_Pret.Location = new Point(guna2Panel1.Size.Width - label_Pret.Size.Width - 30, label_Pret.Location.Y);
                VariabileGlobale.bazaDeDate.StergeProdusDinCos(idUtilizator, idProdus);
                tabelMare.Controls.Remove(tabeleProduse[nrOrdine]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Click_ButonComanda(object sender, EventArgs e, int[] idProduse, int idUtilizator)
        {
            int[] cantitati = new int[textBoxCantitate.Length];
            string[] preturiProduse = new string[labelsPretProduse.Length];
            for (int j = 0; j < textBoxCantitate.Length; j++)
            {
                cantitati[j] = Convert.ToInt32(textBoxCantitate[j].Text);
                preturiProduse[j] = labelsPretProduse[j].Text;
            }
            string valoareComanda =label_Pret.Text;
            string adresa = TextBox_NumePrenume.Text + "\n" + TextBox_Adresa.Text + "\n" + TextBox_Telefon.Text;
            VariabileGlobale.bazaDeDate.AdaugaProduseInComanda(idProduse, cantitati, adresa, valoareComanda, preturiProduse, idUtilizator);

            for(int i=0;i< tabeleProduse.Length;i++)
            {
                tabelMare.Controls.Remove(tabeleProduse[i]);
            }

            double pretTotalProduseCos = 0;
            label_Pret.Text = pretTotalProduseCos.ToString("0.00");
            label_Pret.Location = new Point(guna2Panel1.Size.Width - label_Pret.Size.Width - 30, label_Pret.Location.Y);
        }

        private void ClickMinus(object sender, EventArgs e, int nrOrdine, int idUtilizator )
        {
            try
            {
                var poza = sender as PictureBox;               
                int cantitate = Convert.ToInt32(textBoxCantitate[Convert.ToInt32(nrOrdine)].Text);
                if(cantitate>=2)
                {
                    string idProdus = poza.Name.Substring("pozaMinus_".Length);
                    VariabileGlobale.bazaDeDate.StergeCantitateInCos(Convert.ToInt32(idProdus), idUtilizator);

                    cantitate -= 1;
                    textBoxCantitate[Convert.ToInt32(nrOrdine)].Text = cantitate.ToString();

                    double pretTotal = Convert.ToDouble(label_Pret.Text);
                    pretTotal -= Convert.ToDouble(labelsPretProduse[nrOrdine].Text);
                    label_Pret.Text = pretTotal.ToString("0.00");
                    label_Pret.Location = new Point(guna2Panel1.Size.Width - label_Pret.Size.Width - 30, label_Pret.Location.Y);
                }             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void ClickPlus(object sender, EventArgs e, int nrOrdine, int idUtilizator)
        {
            try
            {
                var poza = sender as PictureBox;
                int cantitate = Convert.ToInt32(textBoxCantitate[Convert.ToInt32(nrOrdine)].Text);
                if (cantitate >= 1)
                {
                    string idProdus = poza.Name.Substring("pozaPlus_".Length);
                    VariabileGlobale.bazaDeDate.AdaugaCantitateInCos(Convert.ToInt32(idProdus), idUtilizator);

                    cantitate += 1;
                    textBoxCantitate[Convert.ToInt32(nrOrdine)].Text = cantitate.ToString();

                    double pretTotal = Convert.ToDouble(label_Pret.Text);
                    pretTotal += Convert.ToDouble(labelsPretProduse[nrOrdine].Text);
                    label_Pret.Text = pretTotal.ToString("0.00");
                    label_Pret.Location = new Point(guna2Panel1.Size.Width - label_Pret.Size.Width - 30, label_Pret.Location.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AfisareProduseCos()
        {
            string[][] ProduseDinCos = VariabileGlobale.bazaDeDate.ReturneazaProduseDinCos(VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator());
            int nrProduse = ProduseDinCos.Length;
            InitializareTabelMare();
            AlocareMemorie(nrProduse);

            int[] idProduseDinCos = new int[nrProduse];
            for (int i = 0; i < nrProduse; i++)
            {
                idProduseDinCos[i] = Convert.ToInt32(ProduseDinCos[i][0]);
            }
            InitializareTabeleProduse(nrProduse, idProduseDinCos);

            InitializarePicturesImagineProduse(nrProduse, idProduseDinCos);
            InitializareLabelsNumeProduse(nrProduse, idProduseDinCos);
            InitializarePicturesMinus(nrProduse, idProduseDinCos, VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator());
            InitializareTextBoxCantitate(nrProduse, idProduseDinCos, VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator());
            InitializarePicturesplus(nrProduse, idProduseDinCos, VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator());
            InitializareLabelsPretProduse(nrProduse, idProduseDinCos);
            InitializarePicturesStergereProdus(nrProduse, idProduseDinCos, VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator());

            AdaugareElementeInTabeleProduse(nrProduse);
            AdaugareProduseInForm(this);


            string[] imaginiProduse = new string[nrProduse];
            string[] numeProduse = new string[nrProduse];
            string[] pretProduse = new string[nrProduse];

            for (int i = 0; i < nrProduse; i++)
            {
                imaginiProduse[i] = ProduseDinCos[i][3];
                numeProduse[i] = ProduseDinCos[i][1];
                pretProduse[i] = ProduseDinCos[i][2];
            }
            string adresaCos = VariabileGlobale.utilizatorActiv.ReturneazaAdresaUtilizator();
            string telefonCos = VariabileGlobale.utilizatorActiv.ReturneazaTelefonUtilizator();
            string numePrenumeCos = VariabileGlobale.utilizatorActiv.ReturneazaNumeUtilizator() + " " + VariabileGlobale.utilizatorActiv.ReturneazaPrenumeUtilizator();

            AtribuiriDetaliiCos(nrProduse, imaginiProduse, numeProduse, pretProduse, adresaCos, telefonCos, numePrenumeCos);

            CalculPretTotal(nrProduse, pretProduse);
            ButonComanda.Click += (sender, EventArgs) => { Click_ButonComanda(sender, EventArgs, idProduseDinCos, VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator()); };
        }

        private void CalculPretTotal(int nrProduse, string[] pretProduse)
        {
            double pretTotalProduseCos = 0;
            for (int i = 0; i < nrProduse; i++)
            {
                pretTotalProduseCos += Convert.ToDouble(pretProduse[i]) * (Convert.ToInt32(textBoxCantitate[i].Text));
            }
            label_Pret.Text = pretTotalProduseCos.ToString("0.00");
            label_Pret.Location = new Point(guna2Panel1.Size.Width - label_Pret.Size.Width - 30, label_Pret.Location.Y);
        }
    }
}