using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace waz_gra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>



public partial class MainWindow : Window

    {
        public ObszarGry wnd;
    
        public MainWindow()
        {         
                  InitializeComponent();

               

        }
        public void button_Click(object sender, RoutedEventArgs e)
        {
            wnd = new ObszarGry(WybórMapy.pierwsza);
            wnd.Show();
           

    
        }

        public void button2_Click(object sender, RoutedEventArgs e)
        {
            wnd = new ObszarGry(WybórMapy.druga);
            wnd.Show();
        }


        
    }
}
