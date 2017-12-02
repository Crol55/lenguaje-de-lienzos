using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using LenguajeDeLienzos.Arreglos;
using LenguajeDeLienzos.Funciones;
using LenguajeDeLienzos.Variables;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class RecorrerArbol
    {
        public static bool ext = false;
        private static int contadorP = 0;
        public static double RESULT = 0;
        private static object valorInicializador;
        public static string clase;
        private static string TIPO;
        public static bool continuar = true;

  
        public static string Conservar;
        public static ArrayList TablaDeSimbolos= new ArrayList();
       // private static ArrayList ClasesActivas = new ArrayList();
        //public static ArrayList Clases = new ArrayList();
        //----------------Vectores-----------------------------------------------
        private static Queue Dimensiones_ = new Queue(); //  lo usa ->L_DIMENSION
        private static Queue Lista_ID_arreglo = new Queue(); //  lo usa ->L_ID
        private static ArrayList Valores_ = new ArrayList();// lo utiliza LISTA_ID_M
        private static ArrayList arreg = new ArrayList();// lo utiliza LISTA_ID_M
        //--------------------------------------------------------------------------- 

       

        static string Verificar_tipoCorrecto(string tipo, string valor) {
               // Parte del analisis Semantico, para verficar si los tipos son correctos
           string respuesta = "error";
            if (valor.Equals("OP")) { valor = "OP"+ " "+ "OP"; }
            
                string[] tip = tipo.Split(' ');   // entero (keyword) | boolean (keyword)
                string[] val = valor.Split(' ');  // cadenas como--> true (Keyword) |  10 (num)


              //  MessageBox.Show("recibiendo "+tip[0]+" "+val[1]);

                switch (tip[0]) {

                    case "entero": {

                            if (val[1].Equals("OP")){
                                respuesta = "entero";
                            }
                            else {
                              string msg = "Error semantico-no se puede agregar " + val[1] + " a una variable tipo ENTERO";
                              MessageBox.Show(msg);
                              Gramatica.concatenar("-", "-", "Error Semantico", msg);
                          }
                        } break;

                    case "doble": {

                            if (val[1].Equals("OP")) {
                                respuesta = "doble";
                            }
                            else{
                              string msg = "Error semantico-no se puede agregar " + val[1] + " a una variable tipo DOBLE";
                              MessageBox.Show(msg);
                              Gramatica.concatenar("-", "-", "Error Semantico", msg);
                           }
                        } break;

                    case "boolean": {
                            if (val[1].Equals("(Keyword)")) {
                                respuesta = "boolean";
                            }
                            else {
                              string msg = "Error semantico-no se puede agregar " + val[1] + " a una variable tipo BOOLEAN";
                              MessageBox.Show(msg);
                              Gramatica.concatenar("-", "-", "Error Semantico", msg);
                           }
                        } break;

                    case "cadena": {

                            if (val[1].Equals("(cadenas)")) {
                                respuesta = "cadena";
                            }
                             else {
                                string msg = "Error semantico-no se puede agregar " + val[1] + " a una variable tipo CADENA";
                                MessageBox.Show(msg);
                                Gramatica.concatenar("-", "-", "Error Semantico", msg);
                        }
                        } break;

                    case "caracter": {

                            if (val[1].Equals("(chars)")) {
                                respuesta = "caracter";
                            }
                            else {
                                string msg = "Error semantico-no se puede agregar " + val[1] + " a una variable tipo CARACTER";
                                MessageBox.Show(msg);
                                Gramatica.concatenar("-", "-", "Error Semantico", msg);

                        }
                    } break;

                }
     

            return respuesta;
        }
        public static void Recorrer(ParseTreeNode raiz, string ambito) {

            /* si la clase es una extension, IMPORTA el tipo de visibilidad que posea por lo tanto
             *el ambito vendra con la frase extension para verificar si se puede ejecutar dicha clase

            */
            if (continuar == true)
            {// permite seguir ejecutando 


                switch (raiz.ToString())
                {
                    case "INICIO":
                        {
                            //  ambito = "Global";
                            // TablaDeSimbolos.Add(new tabla("algo", "1", "Global", "clase", 10, "void"));
                            // tabla t = (tabla)TablaDeSimbolos[0];
                            //MessageBox.Show(t.f_id);

                            Recorrer(raiz.ChildNodes[0], ambito);   // ---> LIENZO.Rule

                        }
                        break;


                    #region LIENZO
                    case "LIENZO":
                        {
                         
                            if (raiz.ChildNodes.Count == 3)
                            {//   lienzo + id + CUERPO 


                                clase = raiz.ChildNodes[1].ToString().Replace(" (id)", "");




                                Recorrer(raiz.ChildNodes[2], ambito);  //--->CUERPO.Rule

                            }

                            else if (raiz.ChildNodes.Count == 5)
                            {//  lienzo + id + extiende + LISTA_EXT +CUERPO

                                clase = raiz.ChildNodes[1].ToString().Replace(" (id)", "");





                                Recorrer(raiz.ChildNodes[4], ambito);  //--->CUERPO.Rule
                            }

                            else if (raiz.ChildNodes.Count == 4)
                            {//  VISIBILIDAD + lienzo + id  + CUERPO
                               
                                string visibilidad = raiz.ChildNodes[0].ToString().Replace(" (Keyword)", "");
                                MessageBox.Show("si ingreso a LIENZO " + ambito+" "+visibilidad);
                                if (ambito.Equals("extiende") && visibilidad.Equals("privado"))
                                {
                                    continuar = false;
                                    MessageBox.Show("La clase " + clase + "de la cual se quiere extender tiene visibilidad 'PRIVADO'");
                                }
                                ambito = "Global";
                                clase = raiz.ChildNodes[2].ToString().Replace(" (id)", "");




                                Recorrer(raiz.ChildNodes[3], ambito);  //--->CUERPO.Rule

                            }

                            else if (raiz.ChildNodes.Count == 6)
                            {//VISIBILIDAD + lienzo + id + extiende + LISTA_EXT  +  CUERPO 

                                string visibilidad = raiz.ChildNodes[0].ToString().Replace(" (Keyword)", "");

                                if (ambito.Equals("extiende") && visibilidad.Equals("privado"))
                                {
                                    continuar = false;
                                    MessageBox.Show("La clase " + clase + "de la cual se quiere extender tiene visibilidad 'PRIVADO'");
                                }
                                ambito = "Global";

                                clase = raiz.ChildNodes[2].ToString().Replace(" (id)", "");


                                Recorrer(raiz.ChildNodes[5], ambito);  //--->CUERPO.Rule

                            }


                        }
                        break;
                    #endregion


                    #region CUERPO 
                    case "CUERPO":
                        {
                            // MessageBox.Show("CUERPO en case  "+clase +" extensiones "+ClasesActivas.Count);
                    
                            foreach (ParseTreeNode hijo in raiz.ChildNodes) { 

                                Recorrer(hijo, ambito); //--> LISTA_ATRIBUTOS | FUNCIONES  | PRINCIPAL

                            }


                        }
                        break;

                    #endregion



                    #region LISTA_ATRIBUTOS 
                    case "LISTA_ATRIBUTOS":
                        {
                            // MessageBox.Show("LISTA_ATRIBUTOS en case");

                            foreach (ParseTreeNode hijo in raiz.ChildNodes){    // infinito de ser necesario
                
                                Recorrer(hijo, ambito); // ---> VARIABLES.RULE
                                Conservar = "";
                                TIPO = "";
                            }


                        }
                        break;
                    #endregion



                    #region VARIABLES
                    case "VARIABLES":
                        {
                            // MessageBox.Show("VARIABLES en case");
                            if (raiz.ChildNodes.Count == 3){ //  conservar + TIPO + ID 


                                Conservar = "1";

                                TIPO = raiz.ChildNodes[1].ToString();

                                Recorrer(raiz.ChildNodes[2], ambito);  // --> ID.Rule


                            }
                            else if (raiz.ChildNodes.Count == 2)
                            { //   TIPO + ID 
                                Conservar = "0";

                                TIPO = raiz.ChildNodes[0].ToString();

                                Recorrer(raiz.ChildNodes[1], ambito);  // --> ID.Rule

                            }
                            else{ // arreglos

                                Recorrer(raiz.ChildNodes[0], ambito); //--> ARREGLO.Rule

                                arreglo arr = new arreglo(Lista_ID_arreglo,Dimensiones_,Valores_,TIPO,ambito);
                            }


                        }
                        break;
                    #endregion

                    #region ID
                    case "ID":
                        {  // MessageBox.Show("ID en case");

                            foreach (ParseTreeNode hijo in raiz.ChildNodes)
                            {
                                Recorrer(hijo, ambito);// ----> ID2.Rule
                            }

                        }
                        break;

                    #endregion


                    #region LISTA_ID
                    case "LISTA_ID":
                        {
                            //  MessageBox.Show("LISTA_ID en case");

                            foreach (ParseTreeNode hiji in raiz.ChildNodes)
                            {
                                Recorrer(hiji, ambito); //--> ID2.Rule

                            }


                        }
                        break;

                    #endregion


                    #region ID2
                    case "ID2":
                        {
                          
                            if (raiz.ChildNodes.Count == 2){ //id + OP
                               
                                string[] id = raiz.ChildNodes[0].ToString().Split(' ');
                                if (raiz.ChildNodes[1].ToString().Contains("INVOCAR")) {
                                   
                                    RecorrerArbolPrincipal.RecorrerPrincipal(raiz.ChildNodes[1],ambito);//---->INVOCAR.Rule
                                    retorno.returnn varv=retorno.RETORNO;

                                    Asignar_invocacion(id[0], varv.valor,varv.tipo,ambito);

                                }else{
                             
                                    //  MessageBox.Show("no ingrese a invocar");
                                    TablaSimb_Variables(id[0], ambito, raiz.ChildNodes[1]); // llena tabla de simbolos con variables creadas

                                    //   MessageBox.Show("el resultado seria " + RESULT);
                                    //    RESULT = 0;
                                }

                            }
                            else { // id +
                         
                                string[] id = raiz.ChildNodes[0].ToString().Split(' ');
                                TablaSimb_variables2(TIPO, ambito, id[0]);
                                // Verificar_tipoCorrecto(TIPO, id);

                            }

                        }
                        break;

                    #endregion




                    #region OP
                    case "OP": {  //    OP + (+-*/) + OP
                           //  MessageBox.Show("OP en case");
                            if (raiz.ChildNodes.Count == 3){

                                Recorrer(raiz.ChildNodes[0], ambito);//---->OP.rule
                                double x = RESULT;
                               

                                Recorrer(raiz.ChildNodes[2], ambito);//---->OP
                                double y = RESULT;
                               

                                switch (DeterminarOperacion(raiz.ChildNodes[1].ToString()))
                                {
                                    case "+": { RESULT = x + y; } break;
                                    case "-": { RESULT = x - y; } break;
                                    case "*": { RESULT = x * y; } break;
                                    case "/": { RESULT = x / y; } break;
                                    case "^": { RESULT = Math.Pow(x, y); } break;


                                }


                            }
                            else{ // num | decimal | id

                                if (raiz.ChildNodes[0].ToString().Contains("id")){// para id 

                                    string identificador = raiz.ChildNodes[0].ToString().Replace(" (id)","");
                                    /* Si viene un id, se debe ir a traer su valor a la tabla de simbolos
                                     ya sea que la variable sea Global o del ambito en la que se aplica */
                                   // MessageBox.Show("pujujujujujuteee "+ambito+" LLamada ");
                                    object val = variables.Buscar_Dato(identificador,ambito);

                                    RESULT = operacion(val.ToString());
                                }
                                else
                                {// solo (num) y (decimal)
                                    RESULT = operacion(raiz.ChildNodes[0].ToString());
                                }


                            }


                        }
                        break;

                    #endregion



                    #region ARREGLO
                    case "ARREGLO":
                        {
                              MessageBox.Show("ARREGLO en case");

                            if (raiz.ChildNodes.Count == 5) { // TIPO + arreglo + L_ID + L_DIMENSION +  LISTA_ASIGNACIONES
                                Conservar = "0";
                                TIPO = raiz.ChildNodes[0].ToString().Replace(" (Keyword)", "") + "-arreglo";

                                Recorrer(raiz.ChildNodes[2], ambito);// ---> L_ID

                                Recorrer(raiz.ChildNodes[3], ambito); // ---> L_DIMENSION
                            

                                Recorrer(raiz.ChildNodes[4], ambito); // ---> LISTA_ASIGNACIONES
                                           
                              
                              
                                  //  TablaDeSimbolos.Add(new tabla(Lista_ID_arreglo.Dequeue().ToString(), TIPO, ambito, clase, arreg, Conservar));
                                
               


                            }
                            else if (raiz.ChildNodes.Count == 6)
                            {// conservar + TIPO + arreglo + L_ID + L_DIMENSION +LISTA_ASIGNACIONES

                                Conservar = "1";
                                TIPO = raiz.ChildNodes[1].ToString().Replace(" (Keyword)", "") + "-arreglo";

                                Recorrer(raiz.ChildNodes[3], ambito);// ---> L_ID  // llenar lista de ID.

                                Recorrer(raiz.ChildNodes[4], ambito); // ---> L_DIMENSION
                                                                      // MessageBox.Show("matriz de dimensiones " + Dimensiones_.Count);

                                Recorrer(raiz.ChildNodes[5], ambito); // ---> LISTA_ASIGNACIONES

                            }
                            else
                            {// la matriz NO posee asignacion == TIPO + arreglo + L_ID + L_DIMENSION

                                Conservar = "0";
                                TIPO = raiz.ChildNodes[0].ToString().Replace(" (Keyword)", "") + "-arreglo";

                                Recorrer(raiz.ChildNodes[2], ambito);// ---> L_ID

                                Recorrer(raiz.ChildNodes[3], ambito); // ---> L_DIMENSION
                                                                      //---------------llenar matriz vacia--------------------------
                                llenar_matrizSinAsignacion();
                          

                                //------------------------------------------------------------
                           



                            }

                        }
                        break;
                    #endregion

                    #region  L_ID
                    case "L_ID":{// id de todos los arreglos

                         //  MessageBox.Show("L_ID en case");
                            foreach (ParseTreeNode hijito in raiz.ChildNodes)
                            { // id
                                string id = hijito.ToString().Replace(" (id)", "");
                                Lista_ID_arreglo.Enqueue(id);
                            }



                        }
                        break;

                    #endregion




                    #region L_DIMENSION
                    case "L_DIMENSION":{//   ToTerm("[") + OP + ToTerm("]");

                            MessageBox.Show("L_DIMENSION en case,RecorrerArbol");
                            foreach (ParseTreeNode hijito in raiz.ChildNodes){

                                Recorrer(hijito, ambito); //----> TAMAÑO.Rule

                                Dimensiones_.Enqueue(RESULT);
                              //  MessageBox.Show("Dimension " + RESULT);

                            }



                        }
                        break;

                    #endregion

                    #region TAMAÑO
                    case "TAMAÑO":
                        {//  ToTerm("[") + OP + ToTerm("]");
                            Recorrer(raiz.ChildNodes[1], ambito); //----> OP.Rule
                        }
                        break;


                    #endregion

                    #region LISTA_ASIGNACIONES
                    case "LISTA_ASIGNACIONES":
                        {
                            //  MessageBox.Show("LISTA_ASIGNACIONES en case");
                            foreach (ParseTreeNode hijito in raiz.ChildNodes){

                                Recorrer(hijito, ambito);// -----> LISTA_ID_M.Rule
                            }

                        } break;

                    #endregion

                    #region LISTA_ID_M
                    case "LISTA_ID_M":{// MessageBox.Show("LISTA_ID_M en case");
                            Valor aux = new Valor();//  instanceamos la lista para almacenar valores

                            foreach (ParseTreeNode hijo in raiz.ChildNodes) {

                                Recorrer(hijo,ambito);//---->OP.Rule
                                aux.InsertarVal(RESULT);
                               

                            }
                            Valores_.Add(aux);// se agregan nodos valor de la forma {2,3,4}


                        }
                        break;
                    #endregion






                    #region L_PARAMETROS

                    case "L_PARAMETROS":
                        {
                           // MessageBox.Show("L_PARAMETROS en case,RecorrerArbol");

                            if (raiz.ChildNodes.Count == 3)
                            { //  id + "[" + OP + "]"+L_PARAM
                                string idMetodo = raiz.ChildNodes[0].ToString();

                                Recorrer(raiz.ChildNodes[1], ambito); //--> OP.Rule

                                Recorrer(raiz.ChildNodes[2], ambito); //--> L_PARAM.Rule

                            }
                            else
                            { //ASIG +L_PARAM
                                Recorrer(raiz.ChildNodes[0], ambito); //--> ASIG.Rule=  OP    |   CADENA   | LOGICO |  INVOCAR;

                                Recorrer(raiz.ChildNodes[1], ambito); //--> L_PARAM.Rule 

                            }

                        }
                        break;

                    #endregion



                    #region FUNCIONES
                    //case "FUNCIONES":
                    //    {// MessageBox.Show("FUNCIONES en case");

                    //        if (raiz.ChildNodes.Count == 2)
                    //        { // id  +  INSTRUCCIONES (1)

                    //            string idMetodo = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                    //            ambito = idMetodo;

                    //            Recorrer(raiz.ChildNodes[1], ambito);//----> INSTRUCCIONES.Rule

                    //        }

                    //        else if (raiz.ChildNodes.Count == 4)
                    //        {// TIPO + id + INSTRUCCIONES +RETORNO (2) | VISIBILIDAD + id + PARAMETROS + INSTRUCCIONES (10)

                    //            if (raiz.ChildNodes[2].ToString().Contains("PARAMETROS"))
                    //            {//---->(10)

                    //                string idMetodo = raiz.ChildNodes[1].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;
                    //                Recorrer(raiz.ChildNodes[2], ambito);//----> INSTRUCCIONES.Rule


                    //            }
                    //            else
                    //            {//--->(2)

                    //                string idMetodo = raiz.ChildNodes[1].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;
                    //                Recorrer(raiz.ChildNodes[2], ambito);//----> INSTRUCCIONES.Rule

                    //            }





                    //        }

                    //        else if (raiz.ChildNodes.Count == 6)
                    //        {//TIPO+"["+"]" + id + INSTRUCCIONES + RETORNO(3) | VISIBILIDAD + TIPO + id + PARAMETROS + INSTRUCCIONES + RETORNO(11)
                    //            string visibilidad = raiz.ChildNodes[0].ToString();

                    //            if (visibilidad.Contains("publico") || visibilidad.Contains("privado"))
                    //            { //--->(11)
                    //                string idMetodo = raiz.ChildNodes[2].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[4], ambito);//----> INSTRUCCIONES.Rule

                    //            }
                    //            else
                    //            { //----->(3)
                    //                string idMetodo = raiz.ChildNodes[3].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[4], ambito);//----> INSTRUCCIONES.Rule

                    //            }

                    //        }
                    //        else if (raiz.ChildNodes.Count == 3)
                    //        { // id + PARAMETROS + INSTRUCCIONES(4) | VISIBILIDAD+ id + INSTRUCCIONES (7)

                    //            if (raiz.ChildNodes[0].ToString().Contains("id"))
                    //            { //---->(4)

                    //                string idMetodo = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[2], ambito);//----> INSTRUCCIONES.Rule


                    //            }
                    //            else
                    //            {//---->(7)

                    //                string idMetodo = raiz.ChildNodes[1].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[2], ambito);//----> INSTRUCCIONES.Rule


                    //            }

                    //        }
                    //        else if (raiz.ChildNodes.Count == 5)
                    //        {//TIPO + id + PARAMETROS + INSTRUCCIONES +RETORNO (5)  | VISIBILIDAD+ TIPO + id  + INSTRUCCIONES + RETORNO (8)

                    //            if (raiz.ChildNodes[1].ToString().Equals("id"))
                    //            { //---->(5)

                    //                string idMetodo = raiz.ChildNodes[1].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[3], ambito);//----> INSTRUCCIONES.Rule


                    //            }
                    //            else
                    //            {// --->(8)

                    //                string idMetodo = raiz.ChildNodes[2].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[3], ambito);//----> INSTRUCCIONES.Rule


                    //            }

                    //        }
                    //        else if (raiz.ChildNodes.Count == 7)
                    //        {// VISIBILIDAD + TIPO + "[" + "]" + id + INSTRUCCIONES + RETORNO (9)  | TIPO + "[" + "]"+ id +PARAMETROS +INSTRUCCIONES +  RETORNO (6) 

                    //            if (raiz.ChildNodes[1].ToString().Contains("Keyword"))
                    //            { //---> 9

                    //                string idMetodo = raiz.ChildNodes[4].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[6], ambito);//----> INSTRUCCIONES.Rule

                    //            }
                    //            else
                    //            {//---->(6)

                    //                string idMetodo = raiz.ChildNodes[3].ToString().Replace(" (id)", "");
                    //                ambito = idMetodo;

                    //                Recorrer(raiz.ChildNodes[5], ambito);//----> INSTRUCCIONES.Rule

                    //            }



                    //        }
                    //        else if (raiz.ChildNodes.Count == 8)
                    //        {// VISIBILIDAD+ TIPO + "[" + "]" + id + PARAMETROS + INSTRUCCIONES + RETORNO 

                    //            string idMetodo = raiz.ChildNodes[4].ToString().Replace(" (id)", "");
                    //            ambito = idMetodo;

                    //            Recorrer(raiz.ChildNodes[7], ambito);//----> INSTRUCCIONES.Rule


                    //        }


                    //    }
                    //    break;
                        #endregion

                }// cierre switch principal
            }// cierre if(continuar)



        }
        public static void llenar_matrizSinAsignacion()
        {
            int tamano = Dimensiones_.Count;

            for (int x = 0; x < tamano; x++)
            {
                object tamaño = Dimensiones_.Dequeue();
                Dimension dimen = new Dimension(tamaño);//Creamos el arreglo y su dimension

                for (int i = 0; i < Int32.Parse(tamaño.ToString()); i++) // se ejecutara el tamaño de su dimension
                {// llenar matriz con null
                    dimen.addValor("null");//llenar arreglo actual 

                }
                arreg.Add(dimen);

            }


        }
        public static double operacion(string op) { // encargado de separar => 10 (num) | 3.1416 (decimal)
          
            string[] sp = op.Split(' ');
 
            double resultado = double.Parse(sp[0], CultureInfo.InvariantCulture);
          
            return resultado;
        }
        public static string DeterminarOperacion(string simbolo) {

            string[] simb = simbolo.Split(' ');

            return simb[0];

        }
        static void TablaSimb_Variables( string id , string ambito, ParseTreeNode raiz) {
         
            switch (Verificar_tipoCorrecto(TIPO, raiz.ToString())){
                case "entero": {

                        Recorrer(raiz, ambito); //--> OP.Rule 
                       // MessageBox.Show("el resultado doble seria " + RESULT);
                        TablaDeSimbolos.Add(new tabla(id, "entero", ambito, clase, RESULT, Conservar));
                        RESULT = 0;

                    }
                    break;

                case "doble":
                    {
                        Recorrer(raiz, ambito); //---> OP.Rule 
                      //  MessageBox.Show("el resultado seria " + RESULT);
                        TablaDeSimbolos.Add(new tabla(id, "doble", ambito, clase, RESULT, Conservar));
                        RESULT = 0;
                    }
                    break;

                case "boolean":
                    {
                        string[] valor = raiz.ToString().Split(' '); //separar---> true (Keyword) | false (Kw) 
                        TablaDeSimbolos.Add(new tabla(id, "boolean", ambito, clase, valor[0], Conservar));

                    }
                    break;

                case "cadena":
                    {
                        string[] valor = raiz.ToString().Split(' '); //separar---> "flajfeios" (cadenas) | false (cademas) 
                        TablaDeSimbolos.Add(new tabla(id, "cadena", ambito, clase, valor[0], Conservar));

                    }
                    break;

                case "caracter":
                    {
                        string[] valor = raiz.ToString().Split(' '); //separar---> 'a' (chars) 
                        TablaDeSimbolos.Add(new tabla(id, "caracter", ambito, clase, valor[0], Conservar));


                    }
                    break;

                case "error":
                    {
                        MessageBox.Show("ERROR de tipo por lo tanto no agrega a la tabla de simbolos");
                    }
                    break;
            }
        }

        static void TablaSimb_variables2(string tipo,string ambito, string id) {
       
            string[] ti = tipo.Split(' ');
            switch (ti[0])
            {
                case "entero": {

                        TablaDeSimbolos.Add(new tabla(id, "entero", ambito, clase, 0 , Conservar));
                       
                    } break;
                case "doble": {
                        TablaDeSimbolos.Add(new tabla(id, "doble", ambito, clase, 0.0 , Conservar));
                    } break;
                case "boolean": {
                        TablaDeSimbolos.Add(new tabla(id, "boolean", ambito, clase, false, Conservar));
                    } break;
                case "cadena": {

                        TablaDeSimbolos.Add(new tabla(id, "cadena", ambito, clase, "", Conservar));
                    } break;
                case "caracter": {
                        TablaDeSimbolos.Add(new tabla(id, "caracter", ambito, clase, "", Conservar));
                    } break;


            }
        }


        public static void Verificar_MetodoPrincipal() {
            if (contadorP > 1) {

                String concat = "-" + "­±" + "-" + "±" + "Error Semantico" + "±" + "Existe mas de 1 metodo principal";//± se utilizara luego con un split para poder obtener los datos
                Gramatica.Errores.Add(concat);


            }
        }


        public static void Asignar_invocacion(string id,object valorRetornado, string tipoRetornado,string ambito) {
          //  MessageBox.Show("El valora asignar ahorita es "+valorRetornado );
            TablaDeSimbolos.Add(new tabla(id, tipoRetornado, ambito, clase, valorRetornado,"0"));
        }


    }

  
    
}
