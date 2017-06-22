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

     public abstract class Mapa
    {
        protected DispatcherTimer _timer;
        protected int _kierunekX = -1;
        protected int _kierunekY = 0;
        protected ElementyMapy _waz = TworzenieElementu.Twórz(TypElementu.wąż, 15, 3);
        protected ElementyMapy __jedzenie = TworzenieElementu.Twórz(TypElementu.jedzenie, 5, 5);
        protected int częśćDodaj;
        protected int licznikCzęsci;
        protected Grid siatka;
        protected int rozmiar = 20;
        protected void timerStart(int czas, bool wł)
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, czas);
            if (wł == true)
                _timer.Start();
            else
                _timer.Stop();
        }
        protected void _timer_Tick(object sender, EventArgs e)
        {
            Ruch(pobierzSiatke);
        }


        protected Mapa(Grid siatka)
        {
            TworzeniePlanszy(siatka);
            twórzWeza(siatka);
            twórzJedzenie(siatka);
            timerStart(100, true);


        }
        public Grid pobierzSiatke
        {
            get
            {
                return siatka;
            }

            set
            {
                siatka = value;
            }
        }
        protected void twórzWeza(Grid a)
        {
            a.Children.Add(_waz.PobierzGłowa.UstawBudowa);
            foreach (ObiektyBudowa wazbudowa in _waz.PobierzCzęści)
                a.Children.Add(wazbudowa.UstawBudowa);
            _waz.Rysuj();
        }

        virtual protected void twórzPrzeszkode(Grid a)
        {

        }


        protected void twórzJedzenie(Grid a)
        {
            a.Children.Add(__jedzenie.PobierzJedzenie.UstawBudowa);
            __jedzenie.Rysuj();

        }
        protected void TworzeniePlanszy(Grid a)
        {

            for (int i = 0; i < a.Width / rozmiar; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();       //definiuje wielkości kolumn
                columnDefinition.Width = new GridLength(rozmiar);
                a.ColumnDefinitions.Add(columnDefinition);
            }

            for (int j = 0; j < a.Height / rozmiar; j++)
            {
                RowDefinition rowDefinition = new RowDefinition();                     //definiuje wielkości rzędów
                rowDefinition.Height = new GridLength(rozmiar);
                a.RowDefinitions.Add(rowDefinition);
            }

        }



        protected void Ruch(Grid a)
        {
            licznikCzęsci = _waz.PobierzCzęści.Count;
            if (częśćDodaj > 0)
            {
                ObiektyBudowa nowaCzesc = TworzenieObiektu.Twórz(TypObiektu.tułów, _waz.PobierzCzęści[_waz.PobierzCzęści.Count - 1].UstawX, _waz.PobierzCzęści[_waz.PobierzCzęści.Count - 1].UstawY);
                a.Children.Add(nowaCzesc.UstawBudowa);
                _waz.PobierzCzęści.Add(nowaCzesc);
                częśćDodaj--;
            }

            for (int i = _waz.PobierzCzęści.Count - 1; i >= 1; i--)
            {
                _waz.PobierzCzęści[i].UstawX = _waz.PobierzCzęści[i - 1].UstawX;
                _waz.PobierzCzęści[i].UstawY = _waz.PobierzCzęści[i - 1].UstawY;
            }



            _waz.PobierzCzęści[0].UstawX = _waz.PobierzGłowa.UstawX;
            _waz.PobierzCzęści[0].UstawY = _waz.PobierzGłowa.UstawY;
            _waz.PobierzGłowa.UstawX += _kierunekX;
            _waz.PobierzGłowa.UstawY += _kierunekY;


            if (Interakcja(pobierzSiatke))
                KoniecGry();
            else
            {
                if (SprawdzJedzenie(pobierzSiatke))
                    __jedzenie.Rysuj();
                _waz.Rysuj();
            }
        }

        protected void KoniecGry()
        {
            _timer.Stop();
            MessageBox.Show("koniec");
             
        }



        protected bool Interakcja(Grid a)
        {
            if (SprawdzKolizjeMapa(a))
                return true;
            if (SprawdzKolizjeMapaWąż())
                return true;
            if (SprawdzKolizjePrzeszkoda())
                return true;
            return false;
        }

        public void kierunek(KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _kierunekX = -1;
                _kierunekY = 0;
            }

            if (e.Key == Key.Right)
            {
                _kierunekX = 1;
                _kierunekY = 0;
            }

            if (e.Key == Key.Up)
            {
                _kierunekX = 0;
                _kierunekY = -1;
            }

            if (e.Key == Key.Down)
            {
                _kierunekX = 0;
                _kierunekY = 1;
            }
        }

        protected bool SprawdzKolizjeMapa(Grid a)
        {
            if (_waz.PobierzGłowa.UstawX < 0 || _waz.PobierzGłowa.UstawX > a.Width / rozmiar)
                return true;
            if (_waz.PobierzGłowa.UstawY < 0 || _waz.PobierzGłowa.UstawY > a.Height / rozmiar)
                return true;
            return false;
        }

        protected bool SprawdzKolizjeMapaWąż()
        {
            foreach (ObiektyBudowa wazbudowa in _waz.PobierzCzęści)
            {
                if (_waz.PobierzGłowa.UstawX == wazbudowa.UstawX && _waz.PobierzGłowa.UstawY == wazbudowa.UstawY)
                    return true;
            }
            return false;
        }

        virtual protected bool SprawdzKolizjePrzeszkoda()
        {
            return false;
        }



        protected bool SprawdzJedzenie(Grid siatka)
        {
            Random rand = new Random();
            if (_waz.PobierzGłowa.UstawX == __jedzenie.PobierzJedzenie.UstawX && _waz.PobierzGłowa.UstawY == __jedzenie.PobierzJedzenie.UstawY)
            {
                częśćDodaj += 1;
                for (int i = 0; i < 50; i++)
                {
                    int x = rand.Next(0, (int)(siatka.Width / rozmiar));
                    int y = rand.Next(0, (int)(siatka.Height / rozmiar));
                    if (CzyWolnePole(x, y))
                    {
                        __jedzenie.PobierzJedzenie.UstawX = x;
                        __jedzenie.PobierzJedzenie.UstawY = y;
                        return true;
                    }
                }
                for (int i = 0; i < siatka.Width / rozmiar; i++)
                    for (int j = 0; j < siatka.Height / rozmiar; j++)
                    {
                        if (CzyWolnePole(i, j))
                        {
                            __jedzenie.PobierzJedzenie.UstawX = i;
                            __jedzenie.PobierzJedzenie.UstawY = j;
                            return true;
                        }
                    }
                KoniecGry();
            }
            return false;
        }
      virtual protected bool CzyWolnePole(int x, int y)
        {
            if (_waz.PobierzGłowa.UstawX == x && _waz.PobierzGłowa.UstawY == y)
                return false;
            foreach (ObiektyBudowa wazbudowa in _waz.PobierzCzęści)
            {
                if (wazbudowa.UstawX == x && wazbudowa.UstawY == y)
                    return false;
            }
            return true;
        }
    }

    public class mapa1 : Mapa
    {
        public mapa1(Grid siatka) : base(siatka)
        {
         
        }
    }


        public class mapa2 : Mapa
    {
        ElementyMapy przeszkoda1 = TworzenieElementu.Twórz(TypElementu.ściana, 15, 7);
        ElementyMapy przeszkoda2 = TworzenieElementu.Twórz(TypElementu.ściana, 15, 22);
        ElementyMapy przeszkoda3 = TworzenieElementu.Twórz(TypElementu.ściana2, 8, 10);
        ElementyMapy przeszkoda4 = TworzenieElementu.Twórz(TypElementu.ściana2, 30, 10);


        public mapa2(Grid siatka) : base(siatka)
            {

                twórzPrzeszkode(siatka);

            }

            override protected void twórzPrzeszkode(Grid a)
            {
            
            foreach (ObiektyBudowa przeszkodabudowa in przeszkoda1.PobierzCzęści)
                a.Children.Add(przeszkodabudowa.UstawBudowa);
            przeszkoda1.Rysuj();
            foreach (ObiektyBudowa przeszkodabudowa in przeszkoda2.PobierzCzęści)
                a.Children.Add(przeszkodabudowa.UstawBudowa);
            przeszkoda2.Rysuj();
            foreach (ObiektyBudowa przeszkodabudowa in przeszkoda3.PobierzCzęści)
                a.Children.Add(przeszkodabudowa.UstawBudowa);
            przeszkoda3.Rysuj();
            foreach (ObiektyBudowa przeszkodabudowa in przeszkoda4.PobierzCzęści)
                a.Children.Add(przeszkodabudowa.UstawBudowa);
            przeszkoda4.Rysuj();

               }

       

          override protected bool SprawdzKolizjePrzeszkoda()
          {
           
           
           foreach (ObiektyBudowa wall in przeszkoda1.PobierzCzęści)
                  {
                      if (_waz.PobierzGłowa.UstawX == wall.UstawX && _waz.PobierzGłowa.UstawY == wall.UstawY)
                          return true;
                  }
            foreach (ObiektyBudowa wall in przeszkoda2.PobierzCzęści)
            {
                if (_waz.PobierzGłowa.UstawX == wall.UstawX && _waz.PobierzGłowa.UstawY == wall.UstawY)
                    return true;
            }
            foreach (ObiektyBudowa wall in przeszkoda3.PobierzCzęści)
            {
                if (_waz.PobierzGłowa.UstawX == wall.UstawX && _waz.PobierzGłowa.UstawY == wall.UstawY)
                    return true;
            }
            foreach (ObiektyBudowa wall in przeszkoda4.PobierzCzęści)
            {
                if (_waz.PobierzGłowa.UstawX == wall.UstawX && _waz.PobierzGłowa.UstawY == wall.UstawY)
                    return true;
            }

            return false;
          }

        override protected bool CzyWolnePole(int x, int y)
        {
            if (_waz.PobierzGłowa.UstawX == x && _waz.PobierzGłowa.UstawY == y)
                return false;
            foreach (ObiektyBudowa wazbudowa in _waz.PobierzCzęści)
            {
                if (wazbudowa.UstawX == x && wazbudowa.UstawY == y)
                    return false;
            }
            foreach (ObiektyBudowa wall in przeszkoda1.PobierzCzęści)
            {
                if ( wall.UstawX==x &&  wall.UstawY==y)
                    return false;
            }
            foreach (ObiektyBudowa wall in przeszkoda2.PobierzCzęści)
            {
                if (wall.UstawX == x && wall.UstawY == y)
                    return false;
            }
            foreach (ObiektyBudowa wall in przeszkoda3.PobierzCzęści)
            {
                if (wall.UstawX == x && wall.UstawY == y)
                    return false;
            }
            foreach (ObiektyBudowa wall in przeszkoda4.PobierzCzęści)
            {
                if (wall.UstawX == x && wall.UstawY == y)
                    return false;
            }

            return true;
        }



    }


    public enum WybórMapy
        {
            pierwsza, druga
        }


        static class TworzenieMapy
    {
        public static mapa1 mapa1
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static mapa2 mapa2
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static WybórMapy WybórMapy
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static Mapa TwórzMape(WybórMapy w, Grid a)
            {

                Mapa element = null;

                switch (w)
                {
                    case WybórMapy.pierwsza:
                        element = new mapa1(a);
                        break;
                    case WybórMapy.druga:
                        element = new mapa2(a);
                        break;




                }

                return element;
            }
        }
    }



