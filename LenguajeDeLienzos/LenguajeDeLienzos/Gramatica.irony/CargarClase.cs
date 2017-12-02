using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class CargarClase{
        public static void Recorrer(ParseTreeNode raiz) {


            switch (raiz.ToString()){
                case "INICIO": {
                        Recorrer(raiz.ChildNodes[0]);//-->LIENZO.rule

                    } break;

                case "LIENZO": {
                        if (raiz.ChildNodes.Count == 5) {//  lienzo + id + extiende + LISTA_EXT +CUERPO 

                            Recorrer(raiz.ChildNodes[3]);//-->LISTA_EXT.rule

                        } else if (raiz.ChildNodes.Count == 6){// VISIBILIDAD + lienzo + id + extiende + LISTA_EXT +CUERPO 

                            Recorrer(raiz.ChildNodes[4]);//-->LISTA_EXT.rule

                        }

                    } break;

                case "LISTA_EXT": {
                        foreach (ParseTreeNode hijo in raiz.ChildNodes) {
                            string claseExt = hijo.ToString().Replace(" (id)","");
                            sintactico.CLASES.Add(claseExt);
                         //   MessageBox.Show(claseExt);
                        }

                    } break;

              }
            }

    }
}
