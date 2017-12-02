using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LenguajeDeLienzos.Gramatica.irony;
using System.Windows.Forms;

namespace LenguajeDeLienzos.Variables
{
    class variables
    {  //  clase para buscar y modificar (Datos o variables) de la tabla de simbolos
        public static tabla VerificarTipo;// se utilizara en el metodo Buscar_Dato();
        public static string TIPO;// utilizada en la clase Condicion.cs


        public static object Buscar_Dato(string id, string ambito){
            object dato = "";
            bool busqueda = false;
            int conta = 0;
            int var = 0;

            while ((busqueda == false) && (conta < RecorrerArbol.TablaDeSimbolos.Count))
            { // buscar en ambito actual


                tabla aux = (tabla)RecorrerArbol.TablaDeSimbolos[conta];

                if (aux.f_id.Equals(id) && aux.f_claselienzo.Equals(RecorrerArbolPrincipal.ClaseActual) && aux.f_ambito.Equals(ambito))
                {

                   // MessageBox.Show("variable encontrada en estado actual en " + ambito);

                    busqueda = true;
                    dato = aux.f_valor;
                    var = 1;
                    VerificarTipo = aux; // almacenamos el nodo donde se encontro para luego verificar tipo
                    TIPO = aux.f_tipo;// usada en condicion
                }
                conta++;
            } // cierre while

            if (var == 0)
            {
                busqueda = false;
                conta = 0;
                var = 0;
                while ((busqueda == false) && (conta < RecorrerArbol.TablaDeSimbolos.Count))
                {// buscar en Global
                    tabla aux = (tabla)RecorrerArbol.TablaDeSimbolos[conta];
                    if (aux.f_id.Equals(id) && aux.f_claselienzo.Equals(RecorrerArbolPrincipal.ClaseActual) && aux.f_ambito.Equals("Global"))
                    {
                        //MessageBox.Show("Variable encontrada en estado Global");

                        busqueda = true;
                        dato = aux.f_valor;
                        var = 1;
                        VerificarTipo = aux; // almacenamos el nodo donde se encontro para luego verificar tipo
                        TIPO = aux.f_tipo;// usada en condicion
                    }

                    conta++;
                }//  cierre while2


            }

            if (var == 0)
            {
                string mensaje = "La variable de busqueda: " + id + " no existe en el contexto actual";
                MessageBox.Show(mensaje);
                Gramatica.irony.Gramatica.concatenar("-","-","Error Semantico",mensaje);
                tabla au = new tabla("","nulo","","",0,"");
                VerificarTipo = au;
            }

            return dato;
        } // buscar Variables en la tabla de simbolos


        public static void Modificar_Datos(string ambito, string id, object nuevoValor)
        { // modificar datos en la tabla de simbolos
            bool busqueda = false;
            int conta = 0;
            int var = 0;
            while ((busqueda == false) && (conta < RecorrerArbol.TablaDeSimbolos.Count))
            { // buscar en ambito actual
                tabla aux = (tabla)RecorrerArbol.TablaDeSimbolos[conta];

                if (aux.f_id.Equals(id) && aux.f_claselienzo.Equals(RecorrerArbolPrincipal.ClaseActual) && aux.f_ambito.Equals(ambito))
                {

                    // MessageBox.Show("variable encontrada en estado actual en " + ambito);

                    busqueda = true;
                    aux.f_valor = nuevoValor;
                    var = 1;
                    VerificarTipo = aux; // almacenamos el nodo donde se encontro para luego verificar tipo
                }
                conta++;
            } // cierre while1

            if (var == 0)
            {
                busqueda = false;
                conta = 0;
                var = 0;
                while ((busqueda == false) && (conta < RecorrerArbol.TablaDeSimbolos.Count))
                {// buscar en Global
                    tabla aux = (tabla)RecorrerArbol.TablaDeSimbolos[conta];
                    if (aux.f_id.Equals(id) && aux.f_claselienzo.Equals(RecorrerArbolPrincipal.ClaseActual) && aux.f_ambito.Equals("Global"))
                    {
                        //MessageBox.Show("Variable encontrada en estado Global");

                        busqueda = true;
                        aux.f_valor = nuevoValor;
                        var = 1;
                        VerificarTipo = aux; // almacenamos el nodo donde se encontro para luego verificar tipo

                    }

                    conta++;
                }//  cierre while2


            }
            if (var == 0)
            {
                string mensaje = "La variable: " + id + "no se puede modificar,ya que no existe en el contexto actual";
                MessageBox.Show(mensaje);
                Gramatica.irony.Gramatica.concatenar("-", "-", "Error Semantico", mensaje);
            }

        } // inicializar variables en la tabla de simbolos o cambiar su valor actual


    }
}
