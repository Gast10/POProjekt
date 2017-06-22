using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
namespace waz_gra
{




    public abstract class ObiektyBudowa
    {
        protected int X;
        protected int Y;
        protected Rectangle budowa;
        public int UstawX
        {
            get
            { return X; }
            set
            { X = value; }
        }
        public int UstawY
        {
            get
            { return Y; }
            set
            { Y = value; }
        }
        public Rectangle UstawBudowa
        {
            get
            { return budowa; }
 
        }




    }
    class Tułów : ObiektyBudowa
    {


        public Tułów(int x, int y)
        {
            X = x;
            Y = y;
            budowa = new Rectangle();
            budowa.Width = budowa.Height = 15;
            budowa.Fill = Brushes.Black;
        }


    }

    class Głowa : ObiektyBudowa
    {

        public Głowa(int x, int y)
        {
            X = x;
            Y = y;
            budowa = new Rectangle();
            budowa.Width = budowa.Height = 20;
            budowa.Fill = Brushes.Red;
        }

    }

    class jedzenie : ObiektyBudowa
    {
        public jedzenie(int x, int y)
        {
            X = x;
            Y = y;
            budowa = new Rectangle();
            budowa.Width = budowa.Height = 18;
            budowa.Fill = Brushes.Blue;

        }
    }

        class Przeszkoda : ObiektyBudowa
        {
            public Przeszkoda(int x, int y)
            {
                X = x;
                Y = y;
                budowa = new Rectangle();
                budowa.Width = budowa.Height = 20;
                budowa.Fill = Brushes.Green;

            }

        }



        enum TypObiektu
        {
            tułów, glowa, jedzenie, przeszkoda
        }

        static class TworzenieObiektu
    {
        internal static Przeszkoda Przeszkoda
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static jedzenie jedzenie
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static Głowa Głowa
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static Tułów Tułów
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal static TypObiektu TypObiektu
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static ObiektyBudowa Twórz(TypObiektu w, int x, int y)
            {

                ObiektyBudowa element = null;

                switch (w)
                {
                    case TypObiektu.tułów:
                        element = new Tułów(x, y);
                        break;
                    case TypObiektu.glowa:
                        element = new Głowa(x, y);
                        break;
                    case TypObiektu.jedzenie:
                        element = new jedzenie(x, y);
                        break;
                    case TypObiektu.przeszkoda:
                        element = new Przeszkoda(x, y);
                        break;



                }

                return element;
            }

        }
    }

   
