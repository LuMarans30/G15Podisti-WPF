using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace G15PodistiWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Podista> podisti = new();
        int pettorale=1;
        string messaggio = "";
        int i = 0,j, k=0;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "G15Podisti";
            btnAggiungi.IsEnabled = false;
            txtNome.Focus();
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (podisti.Count == 6)
                {
                    txtNome.IsEnabled = false;
                    txtNome.TextChanged -= txtNome_TextChanged;
                    btnAggiungi.IsEnabled = false;
                    throw new Exception("Hai raggiunto il numero massimo di podisti");
                }

                string nome = txtNome.Text;

                podisti.Add(new Podista(nome, pettorale));

                pettorale++;

                MediaElement mediaelement = podisti[i].GetStickman().podista;

                switch (i)
                {
                    case 0:
                        Podista0.Visibility = Visibility.Visible;
                        Podista0.podista.Play();
                        Thread.Sleep(150);
                        Podista0.podista.Stop();
                        break;
                    case 1:
                        Podista1.Visibility = Visibility.Visible;
                        Podista1.podista.Play();
                        Thread.Sleep(150);
                        Podista1.podista.Stop();
                        break;
                    case 2:
                        Podista2.Visibility = Visibility.Visible;
                        Podista2.podista.Play();
                        Thread.Sleep(150);
                        Podista2.podista.Stop();
                        break;
                    case 3:
                        Podista3.Visibility = Visibility.Visible;
                        Podista3.podista.Play();
                        Thread.Sleep(150);
                        Podista3.podista.Stop();
                        break;
                    case 4:
                        Podista4.Visibility = Visibility.Visible;
                        Podista4.podista.Play();
                        Thread.Sleep(150);
                        Podista4.podista.Stop();
                        break;
                    case 5:
                        Podista5.Visibility = Visibility.Visible;
                        Podista5.podista.Play();
                        Thread.Sleep(150);
                        Podista5.podista.Stop();
                        break;
                }

                i++;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRORE: INPUT NON VALIDO");
            }

            txtNome.Clear();
            txtNome.Focus();
        }

        private void Podista0_MediaEnded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(k>0)
                {
                    Podista0.setSource(0);
                    Podista1.setSource(0);
                    Podista2.setSource(0);
                    Podista3.setSource(0);
                    Podista4.setSource(0);
                    Podista5.setSource(0);
                }
                
                if (podisti.Count == 0) throw new Exception("Prima di iniziare la gara inserire qualche podista");
                txtNome.IsEnabled = false;
                txtNome.TextChanged -= txtNome_TextChanged;
                btnAggiungi.IsEnabled = false;

                i = 0;
                j = 0;

                //Inizia la gara dei podisti
                foreach (Podista p in podisti)
                {
                    //gridAnimazioni.RowDefinitions.Add(new RowDefinition());
                    Parallel.Invoke(animazione);
                }

                k++;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRORE");
            }
        }

        private void animazione()
        {
            Random rnd = new Random();
            var da = new DoubleAnimation(0, 370, TimeSpan.FromSeconds(rnd.Next(7,12)));
            da.AccelerationRatio = rnd.NextDouble();
            da.Completed += Da_Completed;

            switch (i)
            {
                case 0:
                    da.Name = "Podista0";
                    Podista0.BeginAnimation(Canvas.LeftProperty, da);
                    Podista0.podista.Play();
                    break;
                case 1:
                    da.Name = "Podista1";
                    Podista1.BeginAnimation(Canvas.LeftProperty, da);
                    Podista1.podista.Play();
                    break;
                case 2:
                    da.Name = "Podista2";
                    Podista2.BeginAnimation(Canvas.LeftProperty, da);
                    Podista2.podista.Play();
                    break;
                case 3:
                    da.Name = "Podista3";
                    Podista3.BeginAnimation(Canvas.LeftProperty, da);
                    Podista3.podista.Play();
                    break;
                case 4:
                    da.Name = "Podista4";
                    Podista4.BeginAnimation(Canvas.LeftProperty, da);
                    Podista4.podista.Play();
                    break;
                case 5:
                    da.Name = "Podista5";
                    Podista5.BeginAnimation(Canvas.LeftProperty, da);
                    Podista5.podista.Play();
                    break;
            }

            i++;

        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnAggiungi_Click(sender,e);
            }
        }

        private void Da_Completed(object? sender, EventArgs e)
        {
            //string source = "../../../Resources/stickman-finish.png";
            AnimationClock? ac = sender as AnimationClock;
            DoubleAnimation? d = ac.Timeline as DoubleAnimation;

            Stickman podistaArrivato = (Stickman) gridAnimazioni.FindName(d.Name);
            //podistaArrivato.podista.Source = new Uri(source, UriKind.Relative);
            podistaArrivato.setSource(1); //new 
            Thread.Sleep(100);
            podistaArrivato.podista.Stop();  

            if (j<3) {
                int numArrivato = podistaArrivato.Name.ElementAt(podistaArrivato.Name.Length-1) - '0';
                string nome = podisti.ElementAt(numArrivato).getNome();
                int pettorale = podisti.ElementAt(numArrivato).getPettorale();
                if(j==0)
                    messaggio = "1° Posizione: " + nome + " con pettorale " + pettorale  + " in " + d.Duration + " secondi";
                if(j==1)
                    messaggio += "\n2° Posizione: " + nome + " con pettorale " + pettorale + " in " + d.Duration + " secondi";
                if (j == 2)
                    messaggio += "\n3° Posizione: " + nome + " con pettorale " + pettorale + " in " + d.Duration + " secondi";
            }

            if (j==podisti.Count()-1)
            {
                MessageBox.Show(messaggio);
            }
            //stickman.podista.Source = new Uri(source);
            j++;
        }

        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAggiungi.IsEnabled = string.IsNullOrEmpty(txtNome.Text) ? false : true;
        }

    }
}
