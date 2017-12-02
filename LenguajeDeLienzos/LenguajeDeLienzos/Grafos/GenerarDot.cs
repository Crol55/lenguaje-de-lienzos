using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace LenguajeDeLienzos.Grafos
{
    class GenerarDot
    {

        private static int contador;
        private static string texto;

            public static void GenerarImagenAST(string archivoDot,string nombreArchivo) {
            Process a = new Process();
            a.StartInfo.FileName = "\"C:\\release\\bin\\dot.exe\"";
            a.StartInfo.Arguments = "-Tpng " + archivoDot + " -o" + nombreArchivo;
            a.StartInfo.UseShellExecute = false;
            a.Start();
            a.WaitForExit();
        }


        public static void generarDot(ParseTreeNode raiz){
           
            texto += "digraph G{\n";
            texto+= "nodo0[label=\""+ escapar(raiz.ToString()) +"\"];\n";
         
            contador = 1;
            recorrerAST("nodo0",raiz);
            texto += "}";
            //-----------------------Generamos el archivo .dot, de lo que concateno texto-------
            StreamWriter w = new StreamWriter("AST.dot");
           // string aux = "digraph G{\nnodo0[label = \"I\"];\nnodo1[label = \"LISTA\"];\nnodo0->nodo1;\nnodo2[label = \"var (Keyword)\"];\nnodo1->nodo2;\nnodo3[label = \"TIPO\"];\nnodo1->nodo3;\nnodo4[label = \"boolean (Keyword)\"];\nnodo3->nodo4;\n nodo5[label = \"algo (id)\"];\nnodo1->nodo5;\nnodo6[label = \"= (Key symbol)\"];\nnodo1->nodo6;\nnodo7[label = \"ASIG\"];\nnodo1->nodo7;\nnodo8[label = \"OP\"];\nnodo7->nodo8;\nnodo9[label = \"T\"];\nnodo8->nodo9;\nnodo10[label = \"F\"];\nnodo9->nodo10;\nnodo11[label = \"true (id)\"];\nnodo10->nodo11;\nnodo12[label = \"$(Key symbol)\"];\nnodo1->nodo12;\n}";

            w.WriteLine(texto);
            w.Close();
            GenerarImagenAST("AST.dot", "AST.png"); // Generamos la imagen 'REFERENCIA:linea 19'
            //----------------------------------------------------------------------------------
       }


        public static void recorrerAST(String padre,ParseTreeNode hijos) { // 'REF: linea 34'

            foreach (ParseTreeNode hijo in hijos.ChildNodes) {
              //  MessageBox.Show(hijo.ToString());
                string nombrehijo = "nodo" + contador.ToString();
                texto += nombrehijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                
                texto += padre + "->" + nombrehijo + ";\n";
                contador++;
                recorrerAST(nombrehijo, hijo);
            }
        }
        private static string escapar(string cadena) {

            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");

             //cadena = cadena.Replace(" (id)","");
             //cadena = cadena.Replace(" (num)", "");
             //cadena = cadena.Replace(" (decimal)", "");
             //cadena = cadena.Replace(" (cadenas)", "");
             //cadena = cadena.Replace(" (chars)", "");
           // MessageBox.Show("al escapar" + cadena);
            return cadena;
        }
    }
}
