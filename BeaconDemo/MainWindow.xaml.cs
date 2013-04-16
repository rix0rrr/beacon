using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeaconLib;
using BeaconWpfDialog;

namespace BeaconDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Beacon> beacons = new List<Beacon>();
        private Random r = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void serverButton_Click(object sender, RoutedEventArgs e)
        {
            // We need a random port number otherwise all beacons will be the same
            var b = new Beacon("beaconDemo", (ushort) r.Next(2048, 60000)) 
            {
                BeaconData = "Beacon at " + DateTime.Now + " on " + Dns.GetHostName()
            };
            b.Start();
            beacons.Add(b);
        }

        private void clientButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new ConnectionWindow("beaconDemo") { ConnectMessage = "Pick a demo beacon" };
            if (w.ShowDialog() ?? false)
            {
                MessageBox.Show("You selected: " + w.Address);
            }
        }
    }
}
