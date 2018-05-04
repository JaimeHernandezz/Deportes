using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deportes.GUI
{
    /// <summary>
    /// Lógica de interacción para Estadisticos.xaml
    /// </summary>
    public partial class Estadisticos : Window
    {
        public Estadisticos()
        {
            InitializeComponent();
        }

		private void btnCalcular_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnSalir_Click(object sender, RoutedEventArgs e)
		{
			MainWindow Miventana = new MainWindow();
			Miventana.Show();
			this.Close();
		}
	}
}
