using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using LenguajeDeLienzos.Variables;
using LenguajeDeLienzos.Gramatica.irony;
using System.Globalization;

namespace LenguajeDeLienzos.Ciclos
{
    class ciclos
    {
        private static bool condicion = false;
       // private static int contadorr = 0;
        public static void ejecutar(ParseTreeNode raiz, string ambito)
        {
            //   MessageBox.Show("mierda "+raiz);

            switch (raiz.ToString())
            {

                case "CICLOS":{

                        string[] simb = raiz.ChildNodes[0].ToString().Split(' ');

                        switch (simb[0])
                        {

                            #region si o si else
                            case "si":
                                {
                                    if (raiz.ChildNodes.Count == 3) {// Si + CONDICION +INSTRUCCIONES


                                      condicion= Condicion.ejecutar(raiz.ChildNodes[1], ambito,false); //--->CONDICION.Rule

                                      //  MessageBox.Show("la evaluacion de la condicion me dice q: " + condicion);
                                  
                                        if (condicion == true)
                                        {

                                            // MessageBox.Show("la condicion se ha cumplido ");
                                            //    ejecuta instrucciones
                                            RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[2], ambito);//---->INSTRUCCIONES.Rule

                                            condicion = false;
                                        }



                                    }
                                    else{// Si  + CONDICION  +INSTRUCCIONES + Sino+  INSTRUCCIONES
                                       Condicion.ejecutar(raiz.ChildNodes[1], ambito,false); //--->CONDICION.Rule

                                      //  MessageBox.Show("la evaluacion de la condicion me dice q: " + condicion);
                                        
                                        if (condicion == true)
                                        { // si la condicion no se cumple realizar else
                                   
                                            //    ejecuta instrucciones
                                            RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[2], ambito);//---->INSTRUCCIONES.Rule

                                            condicion = false;

                                        }
                                        else
                                        {

                                            RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[4], ambito);//---->INSTRUCCIONES.Rule

                                        }



                                    }

                                }
                                break;

                            #endregion

                            #region para
                            case "para":
                                {//Para + INIC +";"+ CONDICION +";"+INCREMENTO+INSTRUCCIONES 

                                    string ambit = ambito;
                                    //  ambito = "for";
                                    ejecutar(raiz.ChildNodes[1], ambito);//----> INIC.Rule

                                    condicion=Condicion.ejecutar(raiz.ChildNodes[2], ambito,false);//---> CONDICION.Rule 
                                   
                                    while (condicion == true){
                                      

                                      RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[3], ambito);//---> INCREMENTO.

                                      condicion= Condicion.ejecutar(raiz.ChildNodes[2], ambito,condicion);//---> CONDICION.Rule
                                    

                                        RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[4], ambito);//---->INSTRUCCIONES.Rule
                                    }// cierre while
                                    sintactico.GenerarTablaDeSimbolosHtml();
                                    MessageBox.Show("Ya sali del ciclo FOR");
                                    ambito = ambit;
                                }
                                break;

                            #endregion

                            #region  hacer_mientras
                            case "hacer":
                                {// Hacer +INSTRUCCIONES + Mientras + CONDICION 

                                    do
                                    {
                                        /*Almenos ejecuta una vez, luego verifica la condicion*/
                                        RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[1], ambito);//---->INSTRUCCIONES.Rule

                                        condicion = Condicion.ejecutar(raiz.ChildNodes[3], ambito,condicion);//---> CONDICION.Rule 
                                     


                                    } while (condicion == true);
                                    MessageBox.Show("salgo de el do while");


                                }
                                break;

                            #endregion

                            #region mientras
                            case "mientras":{// Mientras+ CONDICION +INSTRUCCIONES
                                    sintactico.GenerarTablaDeSimbolosHtml();
                                    MessageBox.Show("ingresando a while");
                                    condicion = Condicion.ejecutar(raiz.ChildNodes[1], ambito,false);//---> CONDICION.Rule 
                                

                                    while (condicion == true){

                                   //     MessageBox.Show("zayayay1");
                                        RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[2], ambito);//---->INSTRUCCIONES.Rule

                                       condicion = Condicion.ejecutar(raiz.ChildNodes[1], ambito, condicion); //--->CONDICION.Rule

                                    }
                                    MessageBox.Show("saliendo while");
                                    sintactico.GenerarTablaDeSimbolosHtml();
                                    MessageBox.Show("consultar tabla");
                                }
                                break;

                                #endregion


                        }


                    }
                    break;
                #region INIC
                case "INIC":
                    {
                        if (raiz.ChildNodes.Count == 2)
                        { // id + OP  o   id + INVOCAR
                          /*Modificar una variable ya declarada*/
                           
                            string id = raiz.ChildNodes[0].ToString().Replace(" (id)", "");

                            RecorrerArbol.Recorrer(raiz.ChildNodes[1], ambito);//---->OP

                            variables.Modificar_Datos(ambito, id, RecorrerArbol.RESULT); RecorrerArbol.RESULT = 0;

                        }
                        else
                        {// TIPO+ id + OP   o  + TIPO + id + INVOCAR
              
                            string tip = raiz.ChildNodes[0].ToString().Replace(" (Keyword)", "");
                            string id = raiz.ChildNodes[1].ToString().Replace(" (id)", "");
                            RecorrerArbol.Recorrer(raiz.ChildNodes[2], ambito);

                            Crear_Variables(id, tip, ambito, RecorrerArbol.RESULT);
                           // MessageBox.Show("creando variables");
                        }


                    }
                    break;

                    #endregion





            }

        }


      

        private static void Crear_Variables(string id, string tipo, string ambito, object valor)
        {

            switch (tipo)
            {
                case "entero":
                    {

                        RecorrerArbol.TablaDeSimbolos.Add(new tabla(id, "entero", ambito, RecorrerArbol.clase, valor, "0"));


                    }
                    break;

                case "doble":
                    {

                        RecorrerArbol.TablaDeSimbolos.Add(new tabla(id, "doble", ambito, RecorrerArbol.clase, valor, "0"));


                    }
                    break;
            }



        }
 


    }
}
