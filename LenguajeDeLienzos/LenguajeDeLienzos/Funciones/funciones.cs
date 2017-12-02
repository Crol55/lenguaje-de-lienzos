using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using LenguajeDeLienzos.Gramatica.irony;
using LenguajeDeLienzos.Variables;
using LenguajeDeLienzos.Ciclos;
using LenguajeDeLienzos.Dibujo;
using System.Collections;

namespace LenguajeDeLienzos.Funciones
{
    class funciones
    {
        public static int tamañoInicial=0;
        public static int tamañoFinal = 0;
        /*almacenan los indices de todas
         *las variables auxiliares creadas de una funcion
        *para luego eliminarlas de la
         *tabla de simbolos....*/

        public static retorno ejecutar(ParseTreeNode raiz, string ambito,retorno ret) {
        
            foreach (ParseTreeNode hijo in raiz.ChildNodes) {
                bool terminar = false;
         
                switch (hijo.ToString()) {
                    #region FUNCIONES 
                    case "FUNCIONES":{

                            #region id  +  INSTRUCCIONES (1)
                            if (hijo.ChildNodes.Count == 2){ // id  +  INSTRUCCIONES (1)

                                string idMetodo = hijo.ChildNodes[0].ToString().Replace(" (id)", "");

                                bool resp = Buscar_funcion(idMetodo, ambito);

                                if (resp == true) {
                                    MessageBox.Show("se ha encontrado el metodo a ejecutar");
                                    ejecutar_instrucciones(hijo.ChildNodes[1], ambito);//---> INSTRUCCIONES.Rule

                                    Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/
                                    terminar = true; // para terminar el foreach
                                }



                            }
                            #endregion

                            #region  TIPO + id + INSTRUCCIONES +RETORNO (2) | VISIBILIDAD + id + PARAMETROS + INSTRUCCIONES (10)
                            else if (hijo.ChildNodes.Count == 4)
                            {// TIPO + id + INSTRUCCIONES +RETORNO (2) | VISIBILIDAD + id + PARAMETROS + INSTRUCCIONES (10)

                                if (hijo.ChildNodes[2].ToString().Contains("PARAMETROS"))
                                {//---->(10)
                                    string accesibilidad = hijo.ChildNodes[0].ToString().Replace(" (Keyword)","");

                                    string idMetodo = hijo.ChildNodes[1].ToString().Replace(" (id)", "");
                                    bool resp = Buscar_funcion(idMetodo, ambito);// determina que metodo ejecutar

                                  

                                    if (resp == true) {
                                        MessageBox.Show("se ha encontrado el metodo a ejecutar");
                                        Inicializar_Parametros(hijo.ChildNodes[2], ambito);//---->PARAMETROS.Rule
                                                                          
                                        ejecutar_instrucciones(hijo.ChildNodes[3], ambito);//---> INSTRUCCIONES.Rule
                                       
                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/

                                        terminar = true;
                                    }


                                }
                                else {//--->(2) 
                                    string[] tipo = hijo.ChildNodes[0].ToString().Split(' ');
                                    string idMetodo = hijo.ChildNodes[1].ToString().Replace(" (id)", "");

                                    bool resp = Buscar_funcion(idMetodo, ambito);

                                    if (resp == true){
                                        ejecutar_instrucciones(hijo.ChildNodes[2], ambito);//---> INSTRUCCIONES.Rule

                                    retorno.ejecutar(hijo.ChildNodes[3],ambito,tipo[0]);//---->RETORNO.Rule

                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/


                                        terminar = true;
                                    }
                                }



                            }
                            #endregion

                            #region TIPO+"["+"]" + id + INSTRUCCIONES + RETORNO(3) | VISIBILIDAD + TIPO + id + PARAMETROS + INSTRUCCIONES + RETORNO(11) 
                            else if (hijo.ChildNodes.Count == 6)
                            {//TIPO+"["+"]" + id + INSTRUCCIONES + RETORNO(3) | VISIBILIDAD + TIPO + id + PARAMETROS + INSTRUCCIONES + RETORNO(11)
                                string visibilidad = hijo.ChildNodes[0].ToString();

                                if (visibilidad.Contains("publico") || visibilidad.Contains("privado"))
                                { //--->(11)
                                   string access = hijo.ChildNodes[0].ToString().Replace(" (Keyword)","");
                                   string[] tipo = hijo.ChildNodes[1].ToString().Split(' ');
                                   string idMetodo = hijo.ChildNodes[2].ToString().Replace(" (id)", "");

                                    bool resp = Buscar_funcion(idMetodo, ambito);

                                    if (resp == true){
                                        MessageBox.Show("se ha encontrado el metodo a ejecutar");
                                        Inicializar_Parametros(hijo.ChildNodes[2], ambito);//---->PARAMETROS.Rule

                                        ejecutar_instrucciones(hijo.ChildNodes[3], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[5],ambito,tipo[0]);
                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/

                                        terminar = true;
                                    }

                                }
                                else
                                { //----->(3)
                                    string[] tip = hijo.ChildNodes[0].ToString().Split(' ');
                                    string tipo = tip[0] + "-arreglo";// le agregamos arreglo para saber q tipo debe retornar 
                                    string idMetodo = hijo.ChildNodes[3].ToString().Replace(" (id)", "");

                                    bool resp = Buscar_funcion(idMetodo, ambito);
                                    if (resp == true) {
                                        ejecutar_instrucciones(hijo.ChildNodes[3], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[5], ambito, tipo);
                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/


                                        terminar = true;
                                    }
                                }

                            }
                            #endregion

                            #region  id + PARAMETROS + INSTRUCCIONES(4) | VISIBILIDAD+ id + INSTRUCCIONES (7)

                            else if (hijo.ChildNodes.Count == 3)
                            { // id + PARAMETROS + INSTRUCCIONES(4) | VISIBILIDAD+ id + INSTRUCCIONES (7)

                                if (hijo.ChildNodes[0].ToString().Contains("id")){ //---->(4)
                             
                                    string idMetodo = hijo.ChildNodes[0].ToString().Replace(" (id)", "");
                                    bool resp = Buscar_funcion(idMetodo, ambito);

                                    if (resp == true){
                                        Inicializar_Parametros(hijo.ChildNodes[1], ambito);//---->PARAMETROS.Rule

                                        ejecutar_instrucciones(hijo.ChildNodes[2], ambito);//---> INSTRUCCIONES.Rule

                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/

                                        terminar = true;
                                    }


                                }else{//---->(7) VISIBILIDAD+ id + INSTRUCCIONES (7)
                                    string accesso = hijo.ChildNodes[0].ToString().Replace(" (Keyword)","");
                                    string idMetodo = hijo.ChildNodes[1].ToString().Replace(" (id)", "");

                                    bool resp = Buscar_funcion(idMetodo, ambito);
                                    if (resp == true){
                                        ejecutar_instrucciones(hijo.ChildNodes[2], ambito);//---> INSTRUCCIONES.Rule

                                        terminar = true;
                                    }


                                }

                            }
                            #endregion

                            #region TIPO + id + PARAMETROS + INSTRUCCIONES +RETORNO (5)  | VISIBILIDAD+ TIPO + id  + INSTRUCCIONES + RETORNO (8)

                            else if (hijo.ChildNodes.Count == 5)
                            {//TIPO + id + PARAMETROS + INSTRUCCIONES +RETORNO (5)  | VISIBILIDAD+ TIPO + id  + INSTRUCCIONES + RETORNO (8)

                                if (hijo.ChildNodes[1].ToString().Equals("id")) { //---->(5)
                                    string[] tipo = hijo.ChildNodes[0].ToString().Split(' ');
                                    string idMetodo = hijo.ChildNodes[1].ToString().Replace(" (id)", "");


                                    bool resp = Buscar_funcion(idMetodo, ambito);
                                    if (resp == true){
                                        Inicializar_Parametros(hijo.ChildNodes[2], ambito);//---->PARAMETROS.Rule

                                        ejecutar_instrucciones(hijo.ChildNodes[3], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[4], ambito, tipo[0]);//---->RETORNO.Rule

                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/


                                        terminar = true;
                                    }


                                }else{// --->(8) //VISIBILIDAD+ TIPO + id  + INSTRUCCIONES + RETORNO (8)
                                    string visi = hijo.ChildNodes[0].ToString().Replace(" (Keyword)", "");
                                    string[] tipo = hijo.ChildNodes[0].ToString().Split(' ');
                                    string idMetodo = hijo.ChildNodes[2].ToString().Replace(" (id)", "");

                                    bool resp = Buscar_funcion(idMetodo, ambito);
                                    if (resp == true){
                                        ejecutar_instrucciones(hijo.ChildNodes[3], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[4], ambito, tipo[0]);//---->RETORNO.Rule

                                        terminar = true;
                                    }
                                }

                            }
                            #endregion

                            #region VISIBILIDAD + TIPO + "[" + "]" + id + INSTRUCCIONES + RETORNO (9)  | TIPO + "[" + "]"+ id +PARAMETROS +INSTRUCCIONES +  RETORNO (6) 

                            else if (hijo.ChildNodes.Count == 7)
                            {// VISIBILIDAD + TIPO + "[" + "]" + id + INSTRUCCIONES + RETORNO (9)  | TIPO + "[" + "]"+ id +PARAMETROS +INSTRUCCIONES +  RETORNO (6) 

                                if (hijo.ChildNodes[1].ToString().Contains("Keyword")){ //---> 9
                                    string visi = hijo.ChildNodes[0].ToString().Replace(" (Keyword)", "");
                                    string[] tip = hijo.ChildNodes[1].ToString().Split(' ');
                                    string tipo = tip[0] + "-arreglo";

                                    string idMetodo = hijo.ChildNodes[4].ToString().Replace(" (id)", "");
                                    bool resp = Buscar_funcion(idMetodo, ambito);

                                    if (resp == true){
                                        ejecutar_instrucciones(hijo.ChildNodes[5], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[6], ambito, tipo);//---->RETORNO.Rule

                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/

                                        terminar = true;
                                    }

                                }else{//---->(6)  
                                    string[] tip = hijo.ChildNodes[0].ToString().Split(' ');
                                    string tipo = tip[0] + "-arreglo";

                                    string idMetodo = hijo.ChildNodes[3].ToString().Replace(" (id)", "");
                                    bool resp = Buscar_funcion(idMetodo, ambito);

                                    if (resp == true){
                                        Inicializar_Parametros(hijo.ChildNodes[4], ambito);//---->PARAMETROS.Rule

                                        ejecutar_instrucciones(hijo.ChildNodes[5], ambito);//---> INSTRUCCIONES.Rule

                                        retorno.ejecutar(hijo.ChildNodes[6], ambito, tipo);//---->RETORNO.Rule

                                        Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/


                                        terminar = true;
                                    }
                                }



                            }
                            #endregion

                            #region  VISIBILIDAD+ TIPO + "[" + "]" + id + PARAMETROS + INSTRUCCIONES + RETORNO 

                            else if (hijo.ChildNodes.Count == 8)
                            {// VISIBILIDAD+ TIPO + "[" + "]" + id + PARAMETROS + INSTRUCCIONES + RETORNO 
                                string visi= hijo.ChildNodes[0].ToString().Replace(" (Keyword)", "");
                                string[] tip = hijo.ChildNodes[1].ToString().Split(' ');
                                string tipo = tip[0] + "-arreglo";

                                string idMetodo = hijo.ChildNodes[4].ToString().Replace(" (id)", "");
                                bool resp = Buscar_funcion(idMetodo, ambito);

                                if (resp == true){
                                    Inicializar_Parametros(hijo.ChildNodes[4], ambito);//---->PARAMETROS.Rule

                                    ejecutar_instrucciones(hijo.ChildNodes[5], ambito);//---> INSTRUCCIONES.Rule

                                    retorno.ejecutar(hijo.ChildNodes[6], ambito, tipo);//---->RETORNO.Rule

                                    Eliminar_variablesAuxiliares();/*Elimina todas las variables creadas luego de haber sido utilizadas*/


                                    terminar = true;
                                }


                            }
                            #endregion

                        }
                        break;
                        #endregion
                }// cierre switch
                if (terminar == true) break;
                ejecutar(hijo, ambito,null);

            }// cierre foreach


            return ret;
        }

        public static void ejecutar_instrucciones(ParseTreeNode raiz, string ambito)
        {
        
            switch (raiz.ToString())
            {
                case "INSTRUCCIONES":
                    {

                        foreach (ParseTreeNode hijito in raiz.ChildNodes)
                        {
                            //   MessageBox.Show("si q ingreso aqui "+hijito);
                            ejecutar_instrucciones(hijito, ambito);//---->LISTA_ATRIBUTOS |INICIALIZAR | CICLOS | INVOCAR | NATIVAS.Rule
                        }

                    }
                    break;

                #region LISTA_ATRIBUTOS
                case "LISTA_ATRIBUTOS": {// creacion de variables
                 
                        RecorrerArbol.Recorrer(raiz,ambito);//--->LISTA_ATRIBUTOS.Rule---->VARIABLES.Rule

                    } break;
                #endregion

                #region INICIALIZAR

                case "INICIALIZAR":
                    {// inicializar variables dentro de una funcion

                     
                        RecorrerArbolPrincipal.RecorrerPrincipal(raiz, ambito);//---->INICIALIZAR.Rule

                    }
                    break;

                #endregion

                #region INCREMENTO
                case "INCREMENTO":
                    {
                     
                        Incremento.ejecutar(raiz, ambito);//---->INCREMENTO.Rule en otra clase

                    }
                    break;
                #endregion

                #region CICLOS

                case "CICLOS":
                    {// ejecutar ciclos dentro de una funcion


                        ciclos.ejecutar(raiz, ambito);//---->CICLOS.Rule

                    }
                    break;

                #endregion
                #region NATIVAS
                case "NATIVAS": {

                        Nativas.ejecutar(raiz,ambito);
                    } break;
                #endregion



              
            }


        }
        private static void Inicializar_Parametros(ParseTreeNode raiz, string ambito) {

            switch (raiz.ToString()) {
                case "PARAMETROS": {
                        foreach (ParseTreeNode hijo in raiz.ChildNodes) {
                            Inicializar_Parametros(hijo,ambito);//--->LI_PARAMETROS
                    
                        }
                      
                    } break;
                case "LI_PARAMETROS": { // aca se inicializaran los parametros con los obtenidos en Parametros.cs(clase)
                        /*  TIPO + id*/
                        string[] tipoActual = raiz.ChildNodes[0].ToString().Split(' ');
                        string nombreid = raiz.ChildNodes[1].ToString().Replace(" (id)","");
                        Parametros.parametro aux = (Parametros.parametro)Parametros.parametros.Dequeue();

                   bool coincidencia= Coincidir_Tipos(aux.tipo, tipoActual[0]);
                 
                        if (coincidencia==true) {
                           
                            /*Insertamos el nuevo parametro a la tabla de simbolos*/
                            RecorrerArbol.TablaDeSimbolos.Add(new tabla(nombreid,aux.tipo,ambito,RecorrerArbol.clase,aux.valor,"0"));

                        }
                        
                    } break;

            }
        }

        public static bool Coincidir_Tipos(string recibido, string actual) {
            bool ret = false;

            if (recibido.Equals(actual)) {
                ret = true;
            } else if (recibido.Equals("doble") && actual.Equals("entero")) {
                ret = true;
            } else if (recibido.Equals("entero") && actual.Equals("doble")) {
                ret = true;
            } else {

                MessageBox.Show("El tipo de parametro enviado " + recibido + " no coincide con el esperado '" + actual + "'");
            }

            return ret;
        }
        
        public static bool Buscar_funcion(string id, string ambito)
        {// determina que funcion deberia ejecutar
            bool retorno = false;
            MessageBox.Show( id + " encontrando funcion " + ambito);
            if (id.Equals(ambito))
            {
                retorno = true;
            }
            return retorno;

        }

        private static void Eliminar_variablesAuxiliares() {//  elimina todas las variables creadas por los metodos
          // sintactico.GenerarTablaDeSimbolosHtml();
            
          //  MessageBox.Show("se ha creado la tabla de simbolos de prueba");
            tamañoFinal = RecorrerArbol.TablaDeSimbolos.Count;
            int estatica = tamañoInicial;/*para eliminar siempre la misma posicion ya que lo nodos se mueven de lugar*/
            while (tamañoInicial<tamañoFinal ) {

                RecorrerArbol.TablaDeSimbolos.RemoveAt(estatica);

                tamañoInicial++;
            }

               



        }


    }
}
