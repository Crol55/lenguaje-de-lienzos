using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Irony.Parsing;
using LenguajeDeLienzos.Gramatica.irony;
using System.Windows.Forms;
namespace LenguajeDeLienzos.Funciones
{
    class Parametros
    {
        public static Queue parametros = new Queue(); //  se llenara si  al invocar un metodo este utiliza parametros 
       public struct parametro {
            public string tipo;
            public object valor;
        }

        public static void ejecutar(ParseTreeNode raiz,string ambito) {

            switch (raiz.ToString()) {
                case "L_PARAMETROS": {

                        foreach (ParseTreeNode hijo in raiz.ChildNodes) {
                            ejecutar(hijo,ambito);//----> L_PARAM.Rule
                        }
                      //  MessageBox.Show("la cantidad de parametros ingresados son "+parametros.Count);

                    } break;
                case "L_PARAM": {
                        parametro auxiliar = new parametro();
                        

                        if (raiz.ChildNodes.Count==4) {// id + "[" + OP + "]" // ingresar a arreglos
                            



                        }else {// OP, cadenas, chars, true, false, invocar()

                            if (raiz.ChildNodes[0].ToString().Contains("OP")) {

                                RecorrerArbol.Recorrer(raiz.ChildNodes[0],ambito);//---->OP.RULE

                                auxiliar.tipo = "entero"; auxiliar.valor = RecorrerArbol.RESULT;
                                parametros.Enqueue(auxiliar);

                            } else {// cadenas chars, true false o invocar

                                string[] id = raiz.ChildNodes[0].ToString().Split(' ');
                                string tipo = getTipo(id[1]);
                              //  MessageBox.Show("obteniendo el tipo "+tipo);
                                auxiliar.tipo = tipo; auxiliar.valor =id[0];
                                parametros.Enqueue(auxiliar);
                            

                            }

                        }

                    } break;
      

            }

        }

        public static string getTipo(string cambio) {
            string resp = "";

            switch (cambio) {
                case "(num)": { resp = "entero"; }break;
                case "(doble)": { resp = "doble"; } break;
                case "(Keyword)": { resp = "boolean"; } break;
                case "(cadenas)": { resp = "cadena"; } break;
                case "(chars)": { resp = "caracter"; } break;

            }
            return resp;

        }

    }
}
