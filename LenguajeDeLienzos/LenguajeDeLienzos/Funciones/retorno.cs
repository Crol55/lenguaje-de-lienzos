using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using LenguajeDeLienzos.Gramatica.irony;
using LenguajeDeLienzos.Variables;
namespace LenguajeDeLienzos.Funciones
{
    class retorno
    {
        public static returnn RETORNO;
       public struct returnn {
            public string tipo;
            public object valor;
            
        }
        public static returnn ejecutar(ParseTreeNode raiz,string ambito,string tipo) {
           returnn ret = new returnn();
            switch (raiz.ToString()) {
                case "RETORNO": {// id,caracter,true,false,cadena,OP

                        if (raiz.ChildNodes[0].ToString().Equals("OP")) {
                            RecorrerArbol.Recorrer(raiz.ChildNodes[0], ambito);//---->OP.Rule
                            object x = RecorrerArbol.RESULT;
                            ret.valor = x;
                            ret.tipo = "doble";


                        } else if (raiz.ChildNodes[0].ToString().Contains ("id")) { //  id
                            string id = raiz.ChildNodes[0].ToString().Replace(" (id)","");

                            object val = variables.Buscar_Dato(id,ambito);// devuelve el valor
                            tabla aux = variables.VerificarTipo; //  Se puede buscar su tipo

                            bool seguir = funciones.Coincidir_Tipos(tipo, aux.f_tipo);

                            if (seguir == true){ // ingresa si el tipo de funcion corresponde al tipo de retorno
                                ret.valor = val;
                                ret.tipo = aux.f_tipo;
                            }

                        } else {// caracter, true, false,cadena
                            string[] id = raiz.ChildNodes[0].ToString().Split(' ');
                            string tipoActual = Parametros.getTipo(id[1]);// cambia el tipo devuelve que tipo de valor es

                            bool seguir = funciones.Coincidir_Tipos(tipo, tipoActual);

                            if (seguir==true) { // ingresa si el tipo de funcion corresponde al tipo de retorno
                                ret.valor = id[0];
                                ret.tipo = tipoActual;
                            }

                        }

                    } break;
            }
            RETORNO = ret;
            return ret;
        }

        


    }
}
