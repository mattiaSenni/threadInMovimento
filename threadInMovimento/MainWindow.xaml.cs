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
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace threadInMovimento
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            incremento = 10;
            fine = 610;

            

            
            
            
        }

        private Concorrente c1;
        private Concorrente c2;
        private Concorrente c3;
        private Concorrente c4;
        private Stopwatch cronometro1;
        private Stopwatch cronometro2;
        private Stopwatch cronometro3;
        private Stopwatch cronometro4;
        private List<Tempo> tempi; //sono i tempi che ci hanno messi i concorrenti
        private double incremento;
        private double fine;

        private void start1()
        {
            cronometro1.Start();
            muovi1();
        }
        private void start2()
        {
            cronometro2.Start();
            muovi2();
        }
        private void start3()
        {
            cronometro3.Start();
            muovi3();
        }
        private void start4()
        {
            cronometro4.Start();
            muovi4();
        }

        private void muovi1()
        {
            if (c1.MarginLeft >= fine)
            {
                stop1();
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    concorrente1.Margin = c1.Mossa();
                }));
                Thread.Sleep(50);
                muovi1();
            }
        }

        private void muovi2()
        {
            if (c2.MarginLeft >= fine)
            {
                stop2();
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    concorrente2.Margin = c2.Mossa();
                }));
                Thread.Sleep(50);
                muovi2();
            }
        }
        private void muovi3()
        {
            if (c3.MarginLeft >= fine)
            {
                stop3();
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    concorrente3.Margin = c3.Mossa();
                }));
                Thread.Sleep(50);
                muovi3();
            }
        }
        private void muovi4()
        {
            if (c4.MarginLeft >= fine)
            {
                stop4();
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    concorrente4.Margin = c4.Mossa();
                }));
                Thread.Sleep(50);
                muovi4();
            }
        }

        private void stop1()
        {
            cronometro1.Stop();
            tempi.Add(new Tempo(1, cronometro1.Elapsed));
        }

        private void stop2()
        {
            cronometro2.Stop();
            tempi.Add(new Tempo(2, cronometro1.Elapsed));
        }

        private void stop3()
        {
            cronometro3.Stop();
            tempi.Add(new Tempo(3, cronometro1.Elapsed));
        }

        private void stop4()
        {
            cronometro4.Stop();
            tempi.Add(new Tempo(4, cronometro1.Elapsed));
        }

        class Tempo
        {
            public int Id { get; set; }
            public TimeSpan Time{ get; set; }
            public Tempo(int id, TimeSpan tempo)
            {
                Id = id;
                Time = tempo;
            }
        }

        private void btnFunzioni_Click(object sender, RoutedEventArgs e)
        {
            tempi = new List<Tempo>();

            c1 = new Concorrente(concorrente1.Margin, incremento);
            c2 = new Concorrente(concorrente2.Margin, incremento);
            c3 = new Concorrente(concorrente3.Margin, incremento);
            c4 = new Concorrente(concorrente4.Margin, incremento);

            cronometro1 = new Stopwatch();
            cronometro2 = new Stopwatch();
            cronometro3 = new Stopwatch();
            cronometro4 = new Stopwatch();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente1.Margin = new Thickness(10, concorrente1.Margin.Top, 0, 0);
            }));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente2.Margin = new Thickness(10, concorrente2.Margin.Top, 0, 0);
            }));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente3.Margin = new Thickness(10, concorrente3.Margin.Top, 0, 0);
            }));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente4.Margin = new Thickness(10, concorrente4.Margin.Top, 0, 0);
            }));
            Thread t = new Thread(new ThreadStart(Start));
            t.Start();
        }

        private void Start()
        {
            
            Thread t1 = new Thread(new ThreadStart(start1));
            Thread t2 = new Thread(new ThreadStart(start2));
            Thread t3 = new Thread(new ThreadStart(start3));
            Thread t4 = new Thread(new ThreadStart(start4));


            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                btnFunzioni.Content = "Rigioca";
                btnFunzioni.IsEnabled = false;
            }));
            

            
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            

            //quando hanno finito tutti dalla lista si vede chi ha vinto
            string s = "";
            foreach (Tempo t in tempi)
            {
                TimeSpan ts = t.Time;
                string tempo = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
                s += "id : " + t.Id + ", tempo -> " + tempo + "\n";
            }
            MessageBox.Show(s);
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                
                btnFunzioni.IsEnabled = true;
            }));
        }
    }

}
