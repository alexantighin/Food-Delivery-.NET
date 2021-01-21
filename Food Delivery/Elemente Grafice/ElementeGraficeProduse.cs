using Food_Delivery.Baza_de_date;
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Food_Delivery.Elemente
{
    class ElementeGraficeProduse : Produse
    {
        TableLayoutPanel[] tabeleProduse;
        PictureBox[] picturesImagineProduse;
        Label[] labelsNumeProduse;
        Label[] labelsDescriereProduse;
        Label[] labelsPretProduse;
        Guna2Button[] butoaneAddInCos;

        public ElementeGraficeProduse(BazaDeDate bazaDeDate, int tipSortare, int categorieActiva, TableLayoutPanel tabelGeneralProduse) : base(bazaDeDate, tipSortare, categorieActiva)
        {
            InitializareTabelGeneralProduse(tabelGeneralProduse);
            InitializareCuNrProduse(this.nrProduse);
            InitializareInformatiiProduse();
        }

        ~ElementeGraficeProduse()
        {
            Console.WriteLine("\t-->S-a apelat deconstructorul pentru ElementeGraficeProduse.");
        }

        private void InitializareTabelGeneralProduse(TableLayoutPanel tabelGeneralProduse)
        {
            tabelGeneralProduse.Controls.Clear();
            tabelGeneralProduse.Height = 0;
        }

        private void InitializareCuNrProduse(int nrProduse)
        {
            tabeleProduse = new TableLayoutPanel[this.nrProduse];
            picturesImagineProduse = new PictureBox[this.nrProduse];
            labelsNumeProduse = new Label[this.nrProduse];
            labelsDescriereProduse = new Label[this.nrProduse];
            labelsPretProduse = new Label[this.nrProduse];
            butoaneAddInCos = new Guna2Button[this.nrProduse];

            for (int i = 0; i < nrProduse; i++)
            {
                tabeleProduse[i] = new TableLayoutPanel
                {
                    BackColor = Color.Transparent,
                    Name = "tebelProduse_" + this.idProduse[i],
                    ColumnCount = 1,
                    Width = 147,
                    Height = 270,
                    Dock = DockStyle.None,
                    AutoScroll = false,
                    AutoSize = false,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };

                picturesImagineProduse[i] = new PictureBox()
                {
                    Name = "picturesImagineProduse_" + this.idProduse[i],
                    Height = 140,
                    Width = 140,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                labelsNumeProduse[i] = new Label()
                {
                    Name = "labelsNumeProduse_" + this.idProduse[i],
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 9, FontStyle.Bold),
                    Width = 140,
                    Height = 30,
                    BackColor = Color.Transparent

                };

                labelsDescriereProduse[i] = new Label()
                {
                    Name = "labelsDescriereProduse_" + this.idProduse[i],
                    ForeColor = Color.FromArgb(164, 166, 173),
                    Font = new Font("Roboto", 7, FontStyle.Regular),
                    Width = 140,
                    Height = 70,
                    BackColor = Color.Transparent
                };

                labelsPretProduse[i] = new Label()
                {
                    Name = "labelsPretProduse_" + this.idProduse[i],
                    ForeColor = Color.FromArgb(91, 94, 103),
                    Font = new Font("Roboto", 10, FontStyle.Bold),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };

                butoaneAddInCos[i] = new Guna2Button()
                {
                    Name = "butoaneAddInCos_" + this.idProduse[i],
                    Text = "Add",
                    Width = 50,
                    Height = 18,
                    BorderRadius = 8,
                    Margin = new Padding(35, 0, 0, 0),
                    TextAlign = HorizontalAlignment.Center,
                    ForeColor = Color.White,
                    FillColor = Color.FromArgb(238, 92, 109),
                    Font = new Font("Roboto", 8, FontStyle.Regular),
                    Cursor = Cursors.Hand
                };
                butoaneAddInCos[i].HoverState.FillColor = Color.FromArgb(204, 75, 90);
                butoaneAddInCos[i].HoverState.ForeColor = Color.White;
                butoaneAddInCos[i].Click += (sender, EventArgs) => { ClickADD(sender, EventArgs); };
            }
        }

        private void ClickADD(object sender, EventArgs e)
        {
            var button = sender as Guna2Button;
            string idProdusIntrodusInCos = button.Name.Substring("butoaneAddInCos_".Length);
            VariabileGlobale.bazaDeDate.AdaugaProdusInCos(VariabileGlobale.utilizatorActiv.ReturneazaIdUtilizator(), Convert.ToInt32(idProdusIntrodusInCos));
        }

        private void InitializareInformatiiProduse()
        {
            for (int i = 0; i < nrProduse; i++)
            {
                labelsNumeProduse[i].Text = this.numeProduse[i];
                labelsDescriereProduse[i].Text = this.descriereProduse[i];
                labelsPretProduse[i].Text = this.pretProduse[i];
                picturesImagineProduse[i].Image = Image.FromFile(Application.StartupPath + @"\img\produse\" + this.imaginiProduse[i]);
            }
        }

        private void AdaugareInformatiiProduseInTabel(TableLayoutPanel[] tableLayoutPanel)
        {
            for (int i = 0; i < nrProduse; i++)
            {
                tableLayoutPanel[i].Controls.Clear();
                tableLayoutPanel[i].Controls.Add(picturesImagineProduse[i]);
                tableLayoutPanel[i].Controls.Add(labelsNumeProduse[i]);
                tableLayoutPanel[i].Controls.Add(labelsDescriereProduse[i]);

                TableLayoutPanel tabel2Coloane = new TableLayoutPanel()
                {
                    BackColor = Color.Transparent,
                    Name = "tabel2Coloane_" + i,
                    ColumnCount = 2,
                    Width = 140,
                    Height = 30,
                    Dock = DockStyle.None,
                    AutoScroll = false,
                    AutoSize = false,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };
                tabel2Coloane.Controls.Add(labelsPretProduse[i]);
                tabel2Coloane.Controls.Add(butoaneAddInCos[i]);

                tableLayoutPanel[i].Controls.Add(tabel2Coloane);
            }
        }

        public void Afisare(Form form, TableLayoutPanel tabelGeneralProduse)
        {
            AdaugareInformatiiProduseInTabel(tabeleProduse);
            for (int i = 0; i < nrProduse; i++)
            {
                if (i % 6 == 0)
                {
                    tabelGeneralProduse.Height += tabeleProduse[i].Height;
                }
                tabelGeneralProduse.Controls.Add(tabeleProduse[i]);
            }
            tabelGeneralProduse.Height += 20;
            form.Controls.Add(tabelGeneralProduse);
        }
    }
}