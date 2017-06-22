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
    /// Interaction logic for ObszarGry.xaml
    /// </summary>
    public partial class ObszarGry : Window
    {
        private Mapa mapa;
        public ObszarGry(WybórMapy w)
        {   
            InitializeComponent();
            switch(w)
           {
                case WybórMapy.pierwsza:
                     mapa = TworzenieMapy.TwórzMape(WybórMapy.pierwsza, grid);
                      mapa.pobierzSiatke = grid;
                break;
                case WybórMapy.druga:
                    mapa = TworzenieMapy.TwórzMape(WybórMapy.druga, grid);
                    mapa.pobierzSiatke = grid;
                 break;
            }
            


        }
        
          private void Window_KeyDown(object sender, KeyEventArgs e)
         {
             mapa.kierunek(e);
         }

        

    }
}
