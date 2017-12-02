using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace LenguajeDeLienzos.Dibujo
{
    
    class Pintar
    {
        
      public static  PictureBox AreaDibujo;
       static Graphics grafo;
        static SolidBrush brocha;
        //public Pintar() {

        //    grafo = AreaDibujo.CreateGraphics();
        //    Pen lapicero = new Pen(Color.Black);
        //    grafo.DrawLine(lapicero, 10, 50, 150, 50);

        //}
        public static Color color(string col) {
            string colorcode = col;
              int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
        
              Color clr = Color.FromArgb(argb);
         //   MessageBox.Show("fefejfeofjeif  " + clr);
            return clr;
        }
        public static void inicializar() {
            grafo = AreaDibujo.CreateGraphics();
         
        }
   

      

        public static void Dibujar_Punto(int x, int y, string col, int diametro) {
            col = "FF" + col;  
         Color colour= color(col);
           int dif = diametro / 2;
            brocha = new SolidBrush(colour);
            grafo.FillEllipse(brocha,x-dif,y-dif,diametro,diametro);
            
        }

        public static void Dibujar_ovalo(int x, int y, string col, int ancho, int alto)
        {
            col = "FF" + col;
            Color colour = color(col);
        //    MessageBox.Show(x+" "+y+" "+ancho+" "+alto);
            brocha = new SolidBrush(colour);
            grafo.FillEllipse(brocha, x, y, ancho, alto);

        }

        public static void Dibujar_Rectangulo(int x, int y, string col, int ancho, int alto)
        {
            col = "FF" + col;
            Color colour = color(col);

            brocha = new SolidBrush(colour);
            grafo.FillRectangle(brocha,x,y,ancho,alto);

        }




        public  static void ejec() {
            Pen lapicero = new Pen(Color.Black);
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
          // solid
            grafo.DrawLine(lapicero, 10, 70, 150, 50);
        }
    }
}
