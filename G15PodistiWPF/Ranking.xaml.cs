using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace G15PodistiWPF
{
    /// <summary>
    /// Logica di interazione per Ranking.xaml
    /// </summary>
    public partial class Ranking 
    {

        public Ranking(string[] nomi, int[] pettorale, int[] durata)
        {
            InitializeComponent();
            string label;
            
            nome1.Content = nomi[0];
            nome2.Content = nomi[1];
            nome3.Content = nomi[2];

            pettorale1.Content = pettorale[0].ToString();
            pettorale2.Content = pettorale[1].ToString();
            pettorale3.Content = pettorale[2].ToString();

            label = (string) durata1.Content;
            durata1.Content = label + durata[0].ToString();
            label = (string) durata2.Content;
            durata2.Content = label + durata[1].ToString();
            label = (string) durata3.Content;
            durata3.Content = label + durata[2].ToString();
        }

    }
}
