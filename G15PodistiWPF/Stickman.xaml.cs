using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public partial class Stickman : UserControl
    {
        private string inizio = "../../Resources/stickman-running.gif", 
                         fine = "../../Resources/stickman-finish.png";

        private int durata;

        public Stickman()
        {
            InitializeComponent();
            durata = 0;
            setSource("inizio");
        }
        
        public Stickman(string nome):this()
        {
            this.Name = nome;
        }

        public Stickman(string nome, int durata):this()
        {
            InitializeComponent();
            this.Name = nome;
            this.durata = durata;
        }

        public void setDurata(int durata)
        {
            this.durata = durata;
        }

        public void setNome(string nome)
        {
            this.Name = nome;
        }

        public string getNome()
        {
            return this.Name;
        }

        public int getDurata()
        {
            return durata;
        }
        
        private void podista_MediaEnded(object sender, RoutedEventArgs e)
        {
            podista.Position = new TimeSpan(0, 0, 1);
            podista.Play();
        }

        public void setSource(string stringa)
        {
            podista.Source = new Uri(stringa == "inizio" ? inizio : fine, UriKind.Relative);
        }
    }
}
