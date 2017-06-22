using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace waz_gra
{
    //class waz
 //   {
  //      public waz_budowa glowa;
  //      public List<waz_budowa> czesci;
     /*   public waz()
        {   
            glowa = new waz_budowa(20, 0);
            glowa.kwadrat.Width = glowa.kwadrat.Height = 10;
            glowa.kwadrat.Fill = System.Windows.Media.Brushes.Red;

            czesci = new List<waz_budowa>();
            czesci.Add(new waz_budowa(19, 0));
            czesci.Add(new waz_budowa(18, 0));
            czesci.Add(new waz_budowa(17, 0));
            czesci.Add(new waz_budowa(16, 0));
            czesci.Add(new waz_budowa(15, 0));
            czesci.Add(new waz_budowa(14, 0));
            czesci.Add(new waz_budowa(13, 0));
            czesci.Add(new waz_budowa(12, 0));
            czesci.Add(new waz_budowa(11, 0));
            czesci.Add(new waz_budowa(10, 0));

        } */
        class waz
        {
            public TworzenieWeza tworzenieweza = new TworzenieWeza();
            public WazBudowa glowa;
            public WazBudowa[] cialo= new WazBudowa[10];
            public List<WazBudowa> czesci;
            public waz()
             {
            glowa = tworzenieweza.SnakeCreator(TypWeza.czerwony, 20, 0);
            czesci = new List<WazBudowa>();
            for(int i=cialo.Length;i<=1;i--)
            {
                czesci.Add(cialo[i] = tworzenieweza.SnakeCreator(TypWeza.czarny, i+10, 0));      
            }



        }

        public void WazRysuj()
        {
            Grid.SetColumn(glowa.kwadrat, glowa.X);
            Grid.SetRow(glowa.kwadrat, glowa.Y);
          for(int i= cialo.Length; i<=1; i--)
            {
                Grid.SetColumn(cialo[i].kwadrat, cialo[i].X);
                Grid.SetRow(cialo[i].kwadrat, cialo[i].Y);
            }
            
        }
    }
      

    

    abstract class waz2
    {


    }
}
