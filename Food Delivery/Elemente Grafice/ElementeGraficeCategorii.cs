using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Food_Delivery.Baza_de_date;

namespace Food_Delivery.Elemente
{
    class ElementeGraficeCategorii : Categorii
    {
        readonly Guna2Button[] butoaneCategorii;

        public ElementeGraficeCategorii(BazaDeDate bazaDeDate, Form form, TableLayoutPanel tabelGeneralProduse) : base(bazaDeDate)
        {
            butoaneCategorii = new Guna2Button[nrCategorii];
            for (int i = 0; i < this.nrCategorii; i++)
            {
                butoaneCategorii[i] = CreareButon(idCategorii[i], numeCategorii[i], form, tabelGeneralProduse);
            }
            ModificareAspectButoane();
            GenerareProduse(form, tabelGeneralProduse);
        }

        ~ElementeGraficeCategorii()
        {
            Console.WriteLine("\t-->S-a apelat deconstructorul pentru ElementeGraficeCategorii.");
        }

        private Guna2Button CreareButon(int id, string nume, Form form, TableLayoutPanel tabelGeneralProduse)
        {
            Guna2Button buton = new Guna2Button
            {
                Name = "button_categorie_" + id,
                Text = nume,
                Width = 150,
                Height = 50,
                BorderRadius = 15,
                Margin = new Padding(0, 0, 0, 7),
                Cursor = Cursors.Hand
            };
            buton.Click += (sender, EventArgs) => { Click(sender, form, tabelGeneralProduse); };
            return buton;
        }

        private void Click(object sender, Form form, TableLayoutPanel tabelGeneralProduse)
        {
            var button = sender as Guna2Button;
            string id_categorie_activa = button.Name.Substring("button_categorie_".Length);
            VariabileGlobale.categorieActiva = Convert.ToInt32(id_categorie_activa);
            ModificareAspectButoane();
            GenerareProduse(form, tabelGeneralProduse);
        }

        private void ModificareAspectButoane()
        {
            for (int i = 0; i < nrCategorii; i++)
            {
                butoaneCategorii[i].ImageAlign = HorizontalAlignment.Left;
                butoaneCategorii[i].TextAlign = HorizontalAlignment.Left;
                butoaneCategorii[i].TextOffset = new Point(10, 0);
                butoaneCategorii[i].ImageOffset = new Point(10, 0);
                if (VariabileGlobale.categorieActiva == idCategorii[i])
                {

                    butoaneCategorii[i].ForeColor = Color.White;
                    butoaneCategorii[i].FillColor = Color.FromArgb(235, 164, 95);
                    butoaneCategorii[i].HoverState.FillColor = Color.FromArgb(235, 143, 55);
                    butoaneCategorii[i].HoverState.ForeColor = Color.White;
                    butoaneCategorii[i].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    butoaneCategorii[i].Image = Image.FromFile(Application.StartupPath + @"\img\icons\icons_categorii\active\" + butoaneCategorii[i].Text + ".png");
                }
                else
                {
                    butoaneCategorii[i].ForeColor = Color.FromArgb(162, 164, 171);
                    butoaneCategorii[i].FillColor = Color.Transparent;
                    butoaneCategorii[i].HoverState.FillColor = Color.FromArgb(226, 226, 226);
                    butoaneCategorii[i].HoverState.ForeColor = Color.FromArgb(162, 164, 171);
                    butoaneCategorii[i].Font = new Font("Segoe UI", 12, FontStyle.Regular);
                    butoaneCategorii[i].Image = Image.FromFile(Application.StartupPath + @"\img\icons\icons_categorii\inactive\" + butoaneCategorii[i].Text + ".png");
                }
            }
        }

        private void GenerareProduse(Form form, TableLayoutPanel tabelGeneralProduse)
        {
            ElementeGraficeProduse produse = new ElementeGraficeProduse(VariabileGlobale.bazaDeDate, VariabileGlobale.optiuneSortareProduse, VariabileGlobale.categorieActiva, tabelGeneralProduse);
            produse.Afisare(form, tabelGeneralProduse);
        }

        public void AfisareCategorii(Form form)
        {
            try
            {
                TableLayoutPanel tabelCategorii;
                tabelCategorii = new TableLayoutPanel
                {
                    Name = "tabelCategorii",
                    ColumnCount = 1,
                    Location = new Point(50, 85),
                    AutoSize = false,
                    Width = butoaneCategorii[0].Width,
                    Height = 0,
                    BackColor = Color.Transparent,
                };

                for (int i = 0; i < nrCategorii; i++)
                {
                    tabelCategorii.Controls.Add(butoaneCategorii[i]);
                    tabelCategorii.Height += butoaneCategorii[i].Height + butoaneCategorii[i].Margin.Bottom;
                }
                form.Controls.Add(tabelCategorii);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                form.Close();
            }
        }
    }    
}