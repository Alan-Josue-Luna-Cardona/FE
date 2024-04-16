using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace NaveEspacial
{
    internal class Nave
    {
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> PosicionesNave { get; set; }
        public List<Bala> Balas { get; set; }
        public float MuniDer { get; set; }
        public bool LimDer { get; set; }
        public float MuniIzq { get; set; }
        public bool LimIzq { get; set; }

        public float Super { get; set; }
        public bool ColisionBala { get; set; }
        public List<Enemigo> Enemigos { get; set; }
        public ConsoleColor ColorAux { get; set; }
        public DateTime TiempoColision { get; set; }

        public Nave(Point posicion, ConsoleColor color, Ventana ventana)
        {
            Posicion = posicion;
            Color = color;
            VentanaC = ventana;
            Vida = 100;
            MuniDer = 100;
            MuniIzq = 100;
            PosicionesNave = new List<Point>();
            Balas = new List<Bala>();
            ColisionBala = false;
            Enemigos = new List<Enemigo>();
            ColorAux = color;
            TiempoColision = DateTime.Now;
        }
        public void Dibujar()
        {
            if (DateTime.Now > TiempoColision.AddMilliseconds(1000))
                Console.ForegroundColor = Color;
            else
                Console.ForegroundColor = ColorAux;


            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 3, y);
            Console.Write("♦");
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<(♥)>");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("«♦W W♦»");

            PosicionesNave.Clear();

            PosicionesNave.Add(new Point(x + 3, y));

            PosicionesNave.Add(new Point(x + 1, y + 1));
            PosicionesNave.Add(new Point(x + 2, y + 1));
            PosicionesNave.Add(new Point(x + 3, y + 1));
            PosicionesNave.Add(new Point(x + 4, y + 1));
            PosicionesNave.Add(new Point(x + 5, y + 1));

            PosicionesNave.Add(new Point(x, y + 2));
            PosicionesNave.Add(new Point(x + 1, y + 2));
            PosicionesNave.Add(new Point(x + 2, y + 2));
            PosicionesNave.Add(new Point(x + 4, y + 2));
            PosicionesNave.Add(new Point(x + 5, y + 2));
            PosicionesNave.Add(new Point(x + 6, y + 2));
        }
        public void Borrar()
        {
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }
        public void Teclado(ref Point distancia, int velocidad)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.W)
                distancia = new Point(0, -1);
            if (tecla.Key == ConsoleKey.S)
                distancia = new Point(0, 1);
            if (tecla.Key == ConsoleKey.D)
                distancia = new Point(1, 0);
            if (tecla.Key == ConsoleKey.A)
                distancia = new Point(-1, 0);

            distancia.X *= velocidad;
            distancia.Y *= velocidad;

            if (tecla.Key == ConsoleKey.L)
            {
                if (!LimDer)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 6, Posicion.Y + 2),
                   ConsoleColor.White, TipoBala.Normal);
                    Balas.Add(bala);

                    MuniDer -= 0.8f;
                    if (MuniDer <= 0)
                    {
                        LimDer = true;
                        MuniDer = 0;
                    }
                }
            }
            if (tecla.Key == ConsoleKey.J)
            {
                if (!LimIzq)
                {
                    Bala bala = new Bala(new Point(Posicion.X, Posicion.Y + 2),
                   ConsoleColor.White, TipoBala.Normal);
                    Balas.Add(bala);

                    MuniIzq -= 0.8f;
                    if (MuniIzq <= 0)
                    {
                        LimIzq = true;
                        MuniIzq = 0;
                    }
                }

            }
            if (tecla.Key == ConsoleKey.I)
            {
                if (Super >= 100)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 2, Posicion.Y - 2),
                  ConsoleColor.White, TipoBala.Especial);
                    Balas.Add(bala);
                    Super = 0;
                }

            }

        }
        public void Colisiones(Point distancia)
        {
            Point posicionAux = new Point(Posicion.X + distancia.X, Posicion.Y + distancia.Y);
            if (posicionAux.X <= VentanaC.LimiteSuperior.X)
                posicionAux.X = VentanaC.LimiteSuperior.X + 1;
            if (posicionAux.X + 6 >= VentanaC.LimiteInferior.X)
                posicionAux.X = VentanaC.LimiteInferior.X - 7;
            if (posicionAux.Y <= (VentanaC.LimiteSuperior.Y) + 15)
                posicionAux.Y = (VentanaC.LimiteSuperior.Y + 1) + 15;
            if (posicionAux.Y + 2 >= VentanaC.LimiteInferior.Y)
                posicionAux.Y = VentanaC.LimiteInferior.Y - 3;

            Posicion = posicionAux;
        }
        public void Informacion()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X, VentanaC.LimiteSuperior.Y - 1);
            Console.Write("VIDA: " + (int)Vida + " %  ");

            if (MuniDer >= 100)
                MuniDer = 100;
            else
                MuniDer += 0.0009f;

            if (MuniDer >= 25)
                LimDer = false;

            if (LimDer)
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 40, VentanaC.LimiteSuperior.Y - 1);
            Console.Write("MunicionDerecha: " + (int)MuniDer + " %  ");

            if (MuniIzq >= 100)
                MuniIzq = 100;
            else
                MuniIzq += 0.0009f;

            if (MuniIzq >= 25)
                LimIzq = false;

            if (LimDer)
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 13, VentanaC.LimiteSuperior.Y - 1);
            Console.Write("MunicionIzquierda: " + (int)MuniIzq + " %  ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 70, VentanaC.LimiteSuperior.Y - 1);
            Console.Write("BALA ESPECIAL: " + (int)Super + " %  ");
            if (Super >= 100)
                Super = 100;
            else
                Super += 0.009f;
        }
        public void Mover(int velocidad)
        {
            if (Console.KeyAvailable)
            {
                Borrar();
                Point distancia = new Point();
                Teclado(ref distancia, velocidad);
                Colisiones(distancia);
            }
            Dibujar();
            Informacion();
        }
        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, VentanaC.LimiteSuperior.Y, Enemigos))
                {
                    Balas.Remove(Balas[i]);
                }

            }
        }
        public void Muerte()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("X");
                Thread.Sleep(200);
            }
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
                Thread.Sleep(200);
            }
        }
        
    }
}