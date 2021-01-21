using System.Windows.Forms;

namespace Food_Delivery
{
    public partial class FormComenziUtilizator : Form
    {
        public FormComenziUtilizator(bool admin)
        {
            InitializeComponent();
            Food_Delivery.Elemente_Grafice.ElementeGraficeComenzi elementeGraficeComenzi = new Elemente_Grafice.ElementeGraficeComenzi(admin, this);
        }
    }
}