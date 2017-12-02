using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using LenguajeDeLienzos.Variables;
using LenguajeDeLienzos.Funciones;
using LenguajeDeLienzos.Dibujo;
using LenguajeDeLienzos.Ciclos;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class RecorrerArbolPrincipal
    {
        public static string ClaseActual; // se llena en sintactico.
        public static bool principal = false;

        public static void RecorrerPrincipal(ParseTreeNode raiz, string ambito) {
        
            switch (raiz.ToString()) {

                case "INICIO":
                    {
                        RecorrerPrincipal(raiz.ChildNodes[0], ambito);//--->LIENZO.Rule

                    } break;
                #region LIENZO
                case "LIENZO":
                    {

                        if (raiz.ChildNodes.Count == 3)
                        {//   lienzo + id + CUERPO 


                            RecorrerPrincipal(raiz.ChildNodes[2], ambito);  //--->CUERPO.Rule

                        }

                        else if (raiz.ChildNodes.Count == 5)
                        {//  lienzo + id + extiende + LISTA_EXT +CUERPO



                            RecorrerPrincipal(raiz.ChildNodes[4], ambito);  //--->CUERPO.Rule
                        }

                        else if (raiz.ChildNodes.Count == 4)
                        {//  VISIBILIDAD + lienzo + id  + CUERPO



                            RecorrerPrincipal(raiz.ChildNodes[3], ambito);  //--->CUERPO.Rule

                        }

                        else if (raiz.ChildNodes.Count == 6)
                        {//VISIBILIDAD + lienzo + id + extiende + LISTA_EXT  +  CUERPO 


                            RecorrerPrincipal(raiz.ChildNodes[5], ambito);  //--->CUERPO.Rule

                        }

                    }
                    break;
                #endregion

                #region CUERPO
                case "CUERPO":
                    {
                        foreach (ParseTreeNode hijo in raiz.ChildNodes)
                        {
                            RecorrerPrincipal(hijo, ambito);  //--->PRINCIPAL.Rule

                        }

                    }
                    break;

                #endregion

                #region PRINCIPAL

                case "PRINCIPAL":
                    {
                        principal = true;
                        ambito = "principal";
                        RecorrerPrincipal(raiz.ChildNodes[0], ambito);//--->INSTRUCCIONES.Rule


                    }
                    break;

                #endregion

                #region INSTRUCCIONES
                case "INSTRUCCIONES":
                    { // LISTA_ATRIBUTOS INICIALIZAR | INCREMENTO | CICLOS | INVOCAR | NATIVAS.Rule

                        //MessageBox.Show("volviendo a instrucciones");
                   
                        foreach (ParseTreeNode hijo in raiz.ChildNodes)
                        {

                            RecorrerPrincipal(hijo, ambito); //---> INICIALIZAR | INCREMENTO | CICLOS | INVOCAR | NATIVAS.Rule

                        }

                    }
                    break;

                #endregion

                case "LISTA_ATRIBUTOS": {
                        if (principal==true) {
                            RecorrerArbol.Recorrer(raiz.ChildNodes[0], ambito);//---->VARIABLES.Rule
                        }principal = false;
                      

                    } break;
                #region  INICIALIZAR
                case "INICIALIZAR":
                    {
                      
                        //   MessageBox.Show("INICIALIZAR en arbol principal");
                        if (raiz.ChildNodes.Count == 2) {// id + ( A )
                          
                            string id = raiz.ChildNodes[0].ToString().Replace(" (id)", "");

                            if (raiz.ChildNodes[1].ToString().Equals("OP")) {
                                
                                RecorrerArbol.Recorrer(raiz.ChildNodes[1],ambito); // metodo de otra clase que calcula el resultado
                                MessageBox.Show(""+RecorrerArbol.RESULT);
                                MessageBox.Show(""+ambito+" "+id);
                                variables.Modificar_Datos(ambito, id, RecorrerArbol.RESULT);
                                RecorrerArbol.RESULT = 0;
                           
                            } else if (raiz.ChildNodes[1].ToString().Contains("(cadenas)")) {

                                object valor = raiz.ChildNodes[1].ToString().Replace(" (cadenas)", "");
                                variables.Modificar_Datos(ambito, id, valor); // inicializar

                            }else if (raiz.ChildNodes[1].ToString().Contains("(Keyword)")) {

                                object valor = raiz.ChildNodes[1].ToString().Replace(" (Keyword)", "");
                                variables.Modificar_Datos(ambito, id, valor); // inicializar

                            }
                            else if (raiz.ChildNodes[1].ToString().Contains("(chars)"))
                            {

                                object valor = raiz.ChildNodes[1].ToString().Replace(" (chars)", "");
                                variables.Modificar_Datos(ambito, id, valor); // inicializar

                            }
                            else
                            {// A->id
                             /*Si es id, debe ir por el valor y luego asignarlo. */
                                ParseTreeNode hijo = raiz.ChildNodes[1];//---> A.Rule 
                                string idAsig = hijo.ChildNodes[0].ToString().Replace(" (id)", "");

                                object valor = variables.Buscar_Dato(idAsig, ambito);// buscar 
                                if (!valor.ToString().Equals(""))
                                {
                                    variables.Modificar_Datos(ambito, id, valor); // inicializar
                                }

                            }


                            // object[] nuevoDato = raiz.ChildNodes[1].ToString().Split(' ');


                        }
                        else if (raiz.ChildNodes.Count == 5)
                        {// id + "[" + OP + "]"+ A


                        }

                    }
                    break;

                #endregion


                #region INVOCAR 
                case "INVOCAR":{ // al invocar se debe ir a buscar el metodo y ejecutarlo
                    
                        if (raiz.ChildNodes.Count == 2){ //id +  L_PARAMETROS 
                          ambito= raiz.ChildNodes[0].ToString().Replace(" (id)", "");

                            Parametros.ejecutar(raiz.ChildNodes[1],ambito);//---->L_PARAMETROS (almacenar parametros)

                            funciones.tamañoInicial = RecorrerArbol.TablaDeSimbolos.Count;
                         
                            funciones.ejecutar(sintactico.RAIZ_ACTUAL, ambito, null);//---->FUNCIONES.Rule
                          
                            ambito = "principal";
                        }
                        else {//  id ()

                            // el ambito siempre es el nombre de la funcion a invocar
                            ambito = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                            funciones.tamañoInicial = RecorrerArbol.TablaDeSimbolos.Count;
                            funciones.ejecutar(sintactico.RAIZ_ACTUAL, ambito, null);//---->FUNCIONES.Rule
                            
                       
                           
                            ambito = "principal";
                        }


                    }
                    break;

                #endregion

                #region INCREMENTO

                case "INCREMENTO": {
                    
                        Incremento.ejecutar(raiz,ambito);// INCREMENTO.Rule en otra clase
                    } break;
                #endregion

                #region CICLOS 

                case "CICLOS": {
                      //  MessageBox.Show("maldita sea se supone qingrese aqui");
                        ciclos.ejecutar(raiz,ambito);   //---->CICLOS.Rule

                    } break;
                #endregion

                #region NATIVAS
                case "NATIVAS": {
                  
                        Nativas.ejecutar(raiz,ambito);
                    } break;
               
                    #endregion


            }


        }

      


    


    }
}
