using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using LenguajeDeLienzos.Grafos;
using LenguajeDeLienzos.Gramatica;
using System.Windows.Forms;
using System.Collections;

namespace LenguajeDeLienzos.Gramatica.irony
{
    class sintactico : Grammar
    {
        public static ParseTreeNode RAIZ_ACTUAL;
        public struct raizRoot {
            public ParseTreeNode Raiz;
            public string nombreLienzo;
        }
      public static  ArrayList CLASES = new ArrayList(); // almacenar todas las clases que usara un lienzo.
        
        public static int BuscarExtensiones(string codigo) { 
            CLASES.Clear();
            //------------metodo para verificar si existen extensiones  hacia otras clases-lienzo.-------------------
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(codigo);// Genera arbol a partir de la cadena de entrada
            ParseTreeNode raiz = arbol.Root;
            if (raiz!=null) {
                CargarClase.Recorrer(raiz); //Llena CLASES, si se quiere extender de otro lienzo. paso (1)
            }
        
           

            return CLASES.Count;
        }
        private static void Almacenar_Raiz(ParseTreeNode raiz) {
            raizRoot rut = new raizRoot();
            rut.Raiz = raiz;
            rut.nombreLienzo = Form1.auxiliar;
            Form1.Raices.Add(rut); // almacena raiz y nombre de cada clase extendida
        }
        public static bool analizar(string texto,string condicion) {
          
        
         
        bool func = true;
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(texto);// Genera arbol a partir de la cadena de entrada
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null){
               
                func = false;
                GenerarErroresHTML();
             
            }
            else {
               // MessageBox.Show("si genero arbol");
                if (condicion.Equals("extiende")){
                
                    RecorrerArbol.continuar = true;
                    RecorrerArbol.Recorrer(raiz, "extiende");// Recorrer arbol la primera vez para crear la tabla de simbolos, Comprobacion de tipos 

                    if (RecorrerArbol.continuar==true) {
                        MessageBox.Show("Si almacenare esta Raiz");
                        Almacenar_Raiz(raiz);
 
                    }

                } else {
                    //  MessageBox.Show(CLASES[0].ToString());
                    GenerarImagenAST(raiz);       // Genera imagen del ast.
                    RecorrerArbol.continuar = true;
                    RecorrerArbol.Recorrer(raiz, "");// Recorrer arbol la primera vez para crear la tabla de simbolos, Comprobacion de tipos 
                   
                    RAIZ_ACTUAL = raiz;

                    RecorrerArbolPrincipal.ClaseActual = Form1.clasePrincipal;
                    RecorrerArbolPrincipal.RecorrerPrincipal(raiz,"");// solo para ejecutar metodo principal
                       GenerarErroresHTML();
                    // MessageBox.Show("se ha generado el ast");


                }



            }

            return func;
        }


        private static void GenerarImagenAST(ParseTreeNode raiz) {
            GenerarDot.generarDot(raiz);// metodo de la clase 'GenerarDot' para crear el AST

        }


        public static void GenerarErroresHTML() {

            ArrayList lista = Gramatica.Errores;
            string tabla = "";


            int año = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            int segundo = DateTime.Now.Second;


            tabla += "<html>\n\t<head>\n\t\t<title>Tabla de Errores</title>" + "<meta charset=" + "\"" + "utf-8" + "\"" + ">"
                    + "\n\t\t<link rel=" + "\"" + "stylesheet" + "\"" + "type=" + "\"" + "text/css" + "\"" + " href=" + "\"" + "Estilo.css"
                    + "\"" + ">\n\t</head>\n\t<body>"
                    + "\n\t\t<div style=" + "\"" + "text-align:left;" + "\"" + ">"
                    + "\n\t\t\t<h1>TABLA DE ERRORES</h1>"
                    + "\n\t\t\t<h2>Dia de ejecución:" + dia + " de " + obtenerMes(mes) + " de " + año + "</h2>"
                    + "\n\t\t\t<h2>Hora de ejecución:" + hora + ":" + minuto + ":" + segundo + "</h2>"
                    + "\n\t\t\t<table style=\"margin: margin: 5 auto;\" border=\"2\">\n";
            tabla += "\t\t\t\t<TR>\n\t\t\t\t\t<TH>Linea</TH> <TH>Columna</TH> <TH>Tipo de Error</TH> <TH>Descripcion</TH>\n\t\t\t\t</TR>";

            foreach (string cad in lista) {
                char literal = '±';
                string[] vec = cad.Split(literal);
                //for (int i=0;i<vec.Length;i++) {
                //    MessageBox.Show(vec[i]);


                tabla += "\n\t\t\t\t<TR>";
                tabla += "\n\t\t\t\t\t<TD>" + vec[0] + "</TD>" + "<TD>" + vec[1] + "</TD>" + "<TD>" + vec[2] + "</TD>" + "<TD>" + vec[3] + "</TD>";
                tabla += "\n\t\t\t\t</TR>";

                // }

            }
            tabla += "\n\t\t\t</table>\n\t\t</div>\n\t</body>\n</html>";


            System.IO.StreamWriter w = new System.IO.StreamWriter("TablaErrores.html");
            w.WriteLine(tabla);
            w.Close();
        }

        public static string obtenerMes(int valor) {
            string MES = "";
            switch (valor)
            {

                case 1: { MES = "Enero"; } break;
                case 2: { MES = "Febrero"; } break;
                case 3: { MES = "Marzo"; } break;
                case 4: { MES = "Abril"; } break;
                case 5: { MES = "Mayo"; } break;
                case 6: { MES = "Junio"; } break;
                case 7: { MES = "Julio"; } break;
                case 8: { MES = "Agosto"; } break;
                case 9: { MES = "Septiembre"; } break;
                case 10: { MES = "Octubre"; } break;
                case 11: { MES = "Noviembre"; } break;
                case 12: { MES = "Diciembre"; } break;

            }

            return MES;
        }

        public static void GenerarTablaDeSimbolosHtml() {
            string tabla = "";


            int año = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            int segundo = DateTime.Now.Second;


            tabla += "<html>\n\t<head>\n\t\t<title>Tabla de Simbolos</title>" + "<meta charset=" + "\"" + "utf-8" + "\"" + ">"
                    + "\n\t\t<link rel=" + "\"" + "stylesheet" + "\"" + "type=" + "\"" + "text/css" + "\"" + " href=" + "\"" + "Estilo.css"
                    + "\"" + ">\n\t</head>\n\t<body>"
                    + "\n\t\t<div style=" + "\"" + "text-align:left;" + "\"" + ">"
                    + "\n\t\t\t<h1>TABLA DE Simbolos</h1>"
                    + "\n\t\t\t<h2>Dia de ejecución:" + dia + " de " + obtenerMes(mes) + " de " + año + "</h2>"
                    + "\n\t\t\t<h2>Hora de ejecución:" + hora + ":" + minuto + ":" + segundo + "</h2>"
                    + "\n\t\t\t<table style=\"margin: margin: 5 auto;\" border=\"2\">\n";
            tabla += "\t\t\t\t<TR>\n\t\t\t\t\t<TH>ID</TH> <TH>TIPO</TH> <TH>Ambito</TH> <TH>Lienzo</TH><TH>Valor</TH><TH>Conservar</TH>\n\t\t\t\t</TR>";
            //MessageBox.Show("Generando xml xml xml xml " + RecorrerArbol.TablaDeSimbolos.Count);

            for (int i = 0; i < RecorrerArbol.TablaDeSimbolos.Count; i++) {

                tabla aux = (tabla)RecorrerArbol.TablaDeSimbolos[i];
                tabla += "\n\t\t\t\t<TR>";
                tabla += "\n\t\t\t\t\t<TD>" + aux.f_id + "</TD>" + "<TD>" + aux.f_tipo + "</TD>" + "<TD>" + aux.f_ambito + "</TD>" + "<TD>" + aux.f_claselienzo + "</TD>" + "<TD>" + aux.f_valor + "</TD>" + "<TD>" + aux.f_conservar + "</TD>";
                tabla += "\n\t\t\t\t</TR>";

            }


            tabla += "\n\t\t\t</table>\n\t\t</div>\n\t</body>\n</html>";


            System.IO.StreamWriter w = new System.IO.StreamWriter("TablaDeSimbolos.html");
            w.WriteLine(tabla);
            w.Close();

        }
        
    }

    

}
