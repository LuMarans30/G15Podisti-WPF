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
    public partial class MainWindow : Window
    {
        private List<Podista> podisti = new List<Podista>();
        private Stickman primo = null;
        private Stickman secondo = null;
        private Stickman terzo = null;
        private int pettorale=1;
        private int i = 0,j, k=0;

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

                if (nome == "" || nome.Any(c => !char.IsLetter(c)))   
                {
                    throw new Exception("Inserisci un nome valido (caratteri speciali o numeri non ammessi)");
                }
                
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
            finally
            {
                txtNome.Clear();
                txtNome.Focus();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(k>0)
                {
                    Podista0.setSource("inizio");
                    Podista1.setSource("inizio");
                    Podista2.setSource("inizio");
                    Podista3.setSource("inizio");
                    Podista4.setSource("inizio");
                    Podista5.setSource("inizio");
                }

                if (podisti.Count < 3) throw new Exception("Prima di iniziare la gara inserire almeno 3 podisti");
                txtNome.IsEnabled = false;
                txtNome.TextChanged -= txtNome_TextChanged;
                btnAggiungi.IsEnabled = false;

                i = 0;
                j = 0;

                //Inizia la gara dei podisti
                foreach (Podista p in podisti)
                {
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
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
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

        private void Da_Completed(object sender, EventArgs e)
        {
            //string source = "../../../Resources/stickman-finish.png";
            AnimationClock ac = sender as AnimationClock;
            DoubleAnimation d = ac.Timeline as DoubleAnimation;
            Stickman podistaArrivato = (Stickman) gridAnimazioni.FindName(d.Name);
            //podistaArrivato.podista.Source = new Uri(source, UriKind.Relative);
            podistaArrivato.setSource("fine"); //new 
            Thread.Sleep(100);
            podistaArrivato.podista.Stop();

            podistaArrivato.setDurata(Convert.ToInt32(d.Duration.TimeSpan.TotalSeconds));

            if (j == 0)
            {
                primo = new Stickman(podistaArrivato.getNome(), podistaArrivato.getDurata());
            }
                
            if (j == 1)
            {
                secondo = new Stickman(podistaArrivato.getNome(), podistaArrivato.getDurata());
            }
                
            if (j == 2)
            {
                terzo = new Stickman(podistaArrivato.getNome(), podistaArrivato.getDurata());
            } 
            
            if (j==podisti.Count()-1)
            {
                visualizzazione(primo, secondo, terzo);
            }

            j++;
        }

        private void visualizzazione(Stickman primo, Stickman secondo, Stickman terzo)
        {
            string messaggio;

            int[] num = new int[3];
            int[] pettorale = new int[3];
            string[] nome = new string[3];
            int[] durata = new int[3];

            num[0] = primo.Name.ElementAt(primo.Name.Length - 1) - '0';
            nome[0] = podisti.ElementAt(num[0]).getNome();
            pettorale[0] = podisti.ElementAt(num[0]).getPettorale();
            durata[0] = primo.getDurata();
            messaggio = "1° Posto: " + nome[0] + " con pettorale " + pettorale[0] + " in " + primo.getDurata() + " secondi";
            
            num[1] = secondo.Name.ElementAt(secondo.Name.Length - 1) - '0';
            nome[1] = podisti.ElementAt(num[1]).getNome();
            pettorale[1] = podisti.ElementAt(num[1]).getPettorale();
            durata[1] = secondo.getDurata();
            messaggio += "\n2° Posto: " + nome[1] + " con pettorale " + pettorale[1] + " in " + secondo.getDurata() + " secondi";

            num[2] = terzo.Name.ElementAt(terzo.Name.Length - 1) - '0';
            nome[2] = podisti.ElementAt(num[2]).getNome();
            pettorale[2] = podisti.ElementAt(num[2]).getPettorale();
            durata[2] = terzo.getDurata();
            messaggio += "\n3° Posto: " + nome[2] + " con pettorale " + pettorale[2] + " in " + terzo.getDurata() + " secondi";

            Ranking r = new Ranking(nome, pettorale, durata);
            r.Show();
            
        }



        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAggiungi.IsEnabled = string.IsNullOrEmpty(txtNome.Text) ? false : true;
        }

    }
}
