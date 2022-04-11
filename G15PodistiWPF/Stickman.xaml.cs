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
using System.Windows.Navigation;

namespace G15PodistiWPF
{
    /// <summary>
    /// Logica di interazione per Stickman.xaml
    /// </summary>
    public partial class Stickman : UserControl
    {
        private string inizio = "../../../Resources/stickman-running.gif", 
                         fine = "../../../Resources/stickman-finish.png";

        public Stickman()
        {
            InitializeComponent();
        }
        
        public Stickman(string nome)
        {
            InitializeComponent();
            this.Name = nome;
        }
        
        private void podista_MediaEnded(object sender, RoutedEventArgs e)
        {
            podista.Position = new TimeSpan(0, 0, 1);
            podista.Play();
        }

        public void setSource(int i)
        {
            podista.Source = new Uri(i == 0 ? inizio : fine, UriKind.Relative);
        }
    }
}
