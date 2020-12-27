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
            incremento = 20;
            c1 = new Concorrente(concorrente1.Margin, incremento);
            c2 = new Concorrente(concorrente2.Margin, incremento);
            c3 = new Concorrente(concorrente3.Margin, incremento);
            c4 = new Concorrente(concorrente4.Margin, incremento);

            Thread t1 = new Thread(new ThreadStart(start1));
            Thread t2 = new Thread(new ThreadStart(start2));
            Thread t3 = new Thread(new ThreadStart(start3));
            Thread t4 = new Thread(new ThreadStart(start4));

            
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
        }

        private Concorrente c1;
        private Concorrente c2;
        private Concorrente c3;
        private Concorrente c4;
        private double incremento;

        private void start1()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente1.Margin = c1.Mossa();
            }));
        }

        private void start2()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente2.Margin = c2.Mossa();
            }));
        }
        private void start3()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente3.Margin = c3.Mossa();
            }));
        }
        private void start4()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                concorrente4.Margin = c4.Mossa();
            }));
        }



        
    }

}
