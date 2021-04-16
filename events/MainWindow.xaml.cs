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
using System.Windows.Shapes;
using static events.Publisher;

namespace events
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Subscriber sub;
        Publisher publisher;
        Thread t1;

        public MainWindow()
        {
            InitializeComponent();       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sub = new Subscriber(PrintInfo, ResizeControl, this);
            publisher = new Publisher(1000);
            publisher.DelannoyIsReady += new React(sub.Print);
        }

        private void PrintInfo(int n, int m, int delannoy)
        {
            infoN.Content = infoN.Content.ToString() + n.ToString() + '\n';
            infoM.Content = infoM.Content.ToString() + m.ToString() + '\n';
            infoD.Content = infoD.Content.ToString() + delannoy.ToString() + '\n';
        }

        private void ResizeControl()
        {
            gridforlabels.Height += 22;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {                        
            t1 = new Thread(new ThreadStart(publisher.StartWorking));
            t1.Start();
            start.IsEnabled = false;
            stop.IsEnabled = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            sub.EndCalculating += new Subscriber.Notify(publisher.ShoulStopWorking);
            sub.EndWorking();
            start.IsEnabled = true;
            stop.IsEnabled = false;
        }
    }
}
