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


    public abstract class ElementyMapy
    {
        protected ObiektyBudowa _głowa;
        protected ObiektyBudowa _tułów;
        protected ObiektyBudowa _jedzenie;
        protected List<ObiektyBudowa> _części;
       
        public ObiektyBudowa PobierzGłowa
        {
            get { return _głowa; }
            set { }
        }
        public ObiektyBudowa PobierzTułów
        {
            get { return _tułów; }
            set { }
        }
        public ObiektyBudowa PobierzJedzenie
        {
            get { return _jedzenie; }
            set { }
        }
        public List<ObiektyBudowa> PobierzCzęści
        {
            get { return _części; }
            set { }
        }
        abstract public void Rysuj();


    }

    class Wąż : ElementyMapy
    {
        public Wąż(int x, int y=0)
        {
            
            _głowa = TworzenieObiektu.Twórz(TypObiektu.glowa, x, y);    
            _części = new List<ObiektyBudowa>();

            for (int i = 0; i < 10; i++)                                         // for (int i=0;i< cialo.Length; i++)
            {

                _części.Add(TworzenieObiektu.Twórz(TypObiektu.tułów, i + x+1, y));
            }

        }

        override public void Rysuj()
        {
            Grid.SetColumn(_głowa.UstawBudowa, _głowa.UstawX);
            Grid.SetRow(_głowa.UstawBudowa, _głowa.UstawY);
            foreach (ObiektyBudowa wazbudowa in _części)
            {
                Grid.SetColumn(wazbudowa.UstawBudowa, wazbudowa.UstawX);     
                Grid.SetRow(wazbudowa.UstawBudowa, wazbudowa.UstawY);
            }
        }
    }

    class Jedzenie : ElementyMapy
    {
        public Jedzenie()
        {
            _jedzenie = TworzenieObiektu.Twórz(TypObiektu.jedzenie, 8, 8);
        }
        public override void Rysuj()
        {
            Grid.SetColumn(_jedzenie.UstawBudowa, _jedzenie.UstawX);
            Grid.SetRow(_jedzenie.UstawBudowa, _jedzenie.UstawY);
        }
    }

    class Sciana : ElementyMapy
    {
        public Sciana(int x, int y)
        {
            _części = new List<ObiektyBudowa>();
            for (int j = 0; j < 2; j++)   
            {
                for (int i = 0; i < 10; i++)                                           
                {

                    _części.Add(TworzenieObiektu.Twórz(TypObiektu.przeszkoda, i+x, j+y));
                }
            }
        }

        public override void Rysuj()
        {
            foreach (ObiektyBudowa przeszkodyBudowa in _części)
            {
                Grid.SetColumn(przeszkodyBudowa.UstawBudowa, przeszkodyBudowa.UstawX);     
                Grid.SetRow(przeszkodyBudowa.UstawBudowa, przeszkodyBudowa.UstawY);
            }
        }
    }

    class Sciana2 : ElementyMapy
    {
        public Sciana2(int x, int y)
        {
            _części = new List<ObiektyBudowa>();
            for (int i = 0; i < 2; i++)
            { 
                for (int j = 0; j < 10; j++)                                         
                {

                    _części.Add(TworzenieObiektu.Twórz(TypObiektu.przeszkoda, i + x, j + y));
                }
            }
        }

        public override void Rysuj()
        {
            foreach (ObiektyBudowa przeszkodyBudowa in _części)
            {
                Grid.SetColumn(przeszkodyBudowa.UstawBudowa, przeszkodyBudowa.UstawX);     
                Grid.SetRow(przeszkodyBudowa.UstawBudowa, przeszkodyBudowa.UstawY);
            }
        }
    }


        enum TypElementu
    {
        wąż, jedzenie, ściana, ściana2, 
        }
    static class TworzenieElementu
    {
        internal static Sciana2 Sciana2
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static Wąż Wąż
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static Jedzenie Jedzenie
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static Sciana Sciana
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static TypElementu TypElementu
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static ElementyMapy Twórz(TypElementu w, int x, int y)
        {

            ElementyMapy element = null;

            switch (w)
            {
                case TypElementu.wąż:
                    element = new Wąż(x,y);
                    break;
                case TypElementu.jedzenie:
                    element = new Jedzenie();
                    break;
                    case TypElementu.ściana:
                    element = new Sciana(x,y);
                    break;
                    case TypElementu.ściana2:
                    element = new Sciana2(x, y);
                    break;


                }

            return element;
        }

    }
}

