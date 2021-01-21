using System.Windows.Forms;
using Food_Delivery.Elemente_Grafice;

namespace Food_Delivery
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();

            ElementeGrafice elementeGrafice = new ElementeGrafice(this);
            elementeGrafice.AfisareInitiala(this);
        }
    }
}