using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Globalization;
using System.Windows.Forms;
using LenguajeDeLienzos.Gramatica.irony;
using LenguajeDeLienzos.Variables;
namespace LenguajeDeLienzos.Ciclos
{
    class Condicion
    {
        struct nodo{
            public string tipo;
            public object valor;
        }

        public static bool ejecutar(ParseTreeNode raiz,string ambito,bool condicion) {

            switch (raiz.ToString()) {
                #region CONDICION
                case "CONDICION":
                    {

                      condicion = ejecutar(raiz.ChildNodes[0], ambito , condicion); //---> CONDICION2.Rule
                       // MessageBox.Show("Determina si la condicion es verdadera "+condicion);
                    }
                    break;
                #endregion

                #region CONDICION2
                case "CONDICION2": { // EXP_R + CONDICION3
                        if (raiz.ChildNodes.Count==2) {
                            condicion = ejecutar(raiz.ChildNodes[0], ambito, condicion);//---> EXP_R.Rule
               
                          condicion= ejecutar(raiz.ChildNodes[1], ambito, condicion);//----> CONDICION3.Rule


                        }else {// ASOCIACION

                            condicion = ejecutar(raiz.ChildNodes[0],ambito,condicion);//---->ASOCIACION.Rule

                        }


                    }
                    break;
                #endregion

                #region EXP_R

                case "EXP_R":
                    {

                       //  MessageBox.Show("EJECUTANDO EXP_R DEL OTRO LADO");

                        if (raiz.ChildNodes.Count == 3) { // E + S + E

                            nodo x = E(raiz.ChildNodes[0], ambito); //-->E.Rule

                            string simb = raiz.ChildNodes[1].ToString().Replace(" (Key symbol)", "");

                            nodo y = E(raiz.ChildNodes[2], ambito); //-->E.Rule

                            condicion = Evaluacion_Relacional(x, simb, y); // nos devuelve, true o false
                         

                        } else if (raiz.ChildNodes.Count == 4) {// NOT + E + S + E

                            nodo x = E(raiz.ChildNodes[1], ambito); //-->E.Rule

                            string simb = raiz.ChildNodes[2].ToString().Replace(" (Key symbol)", "");

                            nodo y = E(raiz.ChildNodes[3], ambito); //-->E.Rule

                            condicion = Evaluacion_Relacional(x, simb, y); // nos devuelve, true o false

                           condicion = NOT(condicion);

                        } else if (raiz.ChildNodes.Count==2) {// NOT + (true | false )

                            string val = raiz.ChildNodes[1].ToString().Replace(" (Keyword)","");
                            condicion = NOT(Convert.ToBoolean(val));
                        }
                        else {// true or false
                            condicion = true;

                        }

                    }
                    break;

                #endregion

                #region CONDICION3
                case "CONDICION3": { 

                        foreach (ParseTreeNode hijo in raiz.ChildNodes) {
                           condicion= ejecutar(hijo,ambito,condicion);//---->SIMPLE.Rule
                        }
                     

                    }
                    break;

                #endregion

                #region SIMPLE
                case "SIMPLE": {//  SL + EXP_R  | SL + ASOCIACION

                        string simb = raiz.ChildNodes[0].ToString().Replace(" (Key symbol)", "");

                        if (raiz.ChildNodes[1].ToString().Contains("EXP_R")) { // SL+ EXP_R

                            bool Anterior = condicion; // condicion anterior
                            condicion= ejecutar(raiz.ChildNodes[1],ambito,condicion);//---->EXP_R

                           condicion= Evaluacion_Logica(Anterior,simb,condicion);
                       

                        }else { // SL+ ASOCIACION

                            bool Anterior = condicion; // almacenamos la condicion anterior

                            condicion= ejecutar(raiz.ChildNodes[1],ambito,condicion);//----> ASOCIACION.Rule

                            condicion = Evaluacion_Logica(Anterior, simb, condicion);

                        }
                    } break;

                #endregion

                #region ASOCIACION

                case "ASOCIACION":{

                        if (raiz.ChildNodes.Count == 1) { // CONDICION2 

                          condicion = ejecutar(raiz.ChildNodes[0],ambito,condicion);//--->CONDICION2.Rule

                        }else if(raiz.ChildNodes.Count==2){//  NOT + CONDICION2 

                            condicion = ejecutar(raiz.ChildNodes[1], ambito, condicion);//--->CONDICION2.Rule

                            condicion = NOT(condicion);

                        }else if(raiz.ChildNodes.Count==3) {// CONDICION2 + SL + CONDICION2
                           
                            string simb = raiz.ChildNodes[1].ToString().Replace(" (Key symbol)", "");
            
                            condicion =ejecutar(raiz.ChildNodes[0],ambito,condicion);//---->CONDICION2.Rule
                            bool Anterior = condicion;
                      

                            condicion= ejecutar(raiz.ChildNodes[2], ambito, condicion);//--->CONDICION2.Rule

                            condicion = Evaluacion_Logica(Anterior, simb, condicion);

                        }else {// NOT +  CONDICION2 + SL + CONDICION2
                            string simb = raiz.ChildNodes[2].ToString().Replace(" (Key symbol)", "");

                            condicion = ejecutar(raiz.ChildNodes[1], ambito, condicion);//---->CONDICION2.Rule
                            bool Anterior = condicion;

                            condicion = ejecutar(raiz.ChildNodes[3], ambito, condicion);//--->CONDICION2.Rule

                            condicion = Evaluacion_Logica(Anterior, simb, condicion);

                            condicion = NOT(condicion); // negamos la condicion obtenida
                        }

                    }break;
                    #endregion




            }



            return condicion;
        }

        private static bool NOT(bool exp) { // se le cambia el tipo booleano
            //MessageBox.Show("ingreso con    "+exp);
            bool expr = false;
            expr = !exp;
           // MessageBox.Show("y salio con el valor de "+expr);
            return expr;

        }

        private static bool Evaluacion_Logica(bool exp1, string simbolo, bool exp2)
        {
            bool ret = false;
            // MessageBox.Show(exp1 +" "+simbolo+" "+exp2);
            switch (simbolo){
                case "&&":{ // ambas deben ser verdaderas

                        if (exp1 == true && exp2 == true)
                        {
                            ret = true;
                        }

                    }
                    break;

                case "||": { // solo una o ambas verdaderas
                        if (exp1 == true) {
                            ret = true;
                        }
                        else if (exp2 == true) {
                           // MessageBox.Show("malidfeflefieofjeiofeiijeifeoejief eyo me ejjfioejfioejfioe");
                            ret = true;
                        }

                    }
                    break;

                case "!&&":{// si ambas son verdaderas retorna false;
                        if (exp1 == true && exp2 == true)
                        {
                            ret = false;
                        }
                        else
                        {
                            ret = true;
                        }

                    } break;

                case "!||":{// si almenos 1 es verdadera, retorna false; (ambas falsas==true)
                        if (exp1 == false && exp2 == false){
                            ret = true;
                        }
                        else{
                            ret = false;
                        }

                    } break;

                case "&|":{// solo una de las 2 puede ser verdadera

                        if (exp1 == true) {

                            if (exp2 == false){
                                ret = true;
                            }

                        }
                        else if (exp2 == true){

                            if (exp1 == false){
                                ret = true;
                            }
                        }

                    } break;             

            }
            return ret;
        }

        private static bool Evaluacion_Relacional(nodo val1, string simbolo, nodo val2){
            bool retorno = false;

           string tip = Verificar_AmbosTipos(val1.tipo,val2.tipo);// devuelve que tipo de comparacion deberia ser y verifica si se pueden comparar
        
            switch (tip) {

                case "entero": {
                       retorno = Comparar_Enteros(Convert.ToInt32( val1.valor) ,simbolo, Convert.ToInt32(val2.valor));
                    } break;

                case "doble": {
                      retorno=Comparar_Dobles(double.Parse(val1.valor.ToString(), CultureInfo.InvariantCulture),simbolo,double.Parse(val2.valor.ToString(),CultureInfo.InvariantCulture));
                    } break;

                case "boolean": {

                       retorno= Comparar_booleans(Convert.ToBoolean(val1.valor),simbolo,Convert.ToBoolean(val2.valor));
                      //  MessageBox.Show("COMPARANDO   "+retorno);
                    } break;

                case "cadena": {

                        retorno = Comparar_Cadenas(Convert.ToString(val1.valor),simbolo,Convert.ToString(val2.valor));

                    } break;

                case "caracter": {
                        retorno = Comparar_Caracteres(Convert.ToChar(val1.valor) ,simbolo, Convert.ToChar(val2.valor));

                    } break;

            }
            






            return retorno;
        }
        public static string Verificar_AmbosTipos(string tipo1,string tipo2) {
            string resp = "";
            if (tipo1.Equals("entero") && tipo2.Equals("entero"))
            {
                resp = "entero";
               // MessageBox.Show("entero con enter");
            }
            else if (tipo1.Equals("entero") && tipo2.Equals("doble"))
            {
                resp = "doble";
              //  MessageBox.Show("entero con doble");

            }
            else if (tipo1.Equals("doble") && tipo2.Equals("entero"))
            {
                resp = "doble";
               // MessageBox.Show("doble con entero");

            }
            else if (tipo1.Equals("doble") && tipo2.Equals("doble"))
            {
                resp = "doble";
               // MessageBox.Show("doble con doble");
            }
            else if ((tipo1.Equals("(Keyword)") || tipo1.Equals("boolean")) && (tipo2.Equals("(Keyword)") || tipo2.Equals("boolean")))
            {
                resp = "boolean";
               // MessageBox.Show("boolean con boolean");
            }
            else if ((tipo1.Equals("(chars)") || tipo1.Equals("caracter")) && (tipo2.Equals("(chars)") || tipo2.Equals("caracter"))) {
                resp = "caracter";
               // MessageBox.Show("char con char");

            } else if ((tipo1.Equals("(cadenas)") || tipo1.Equals("cadena")) && (tipo2.Equals("(cadenas)") || tipo2.Equals("cadena"))) {
                resp = "cadena";
               // MessageBox.Show("cadena con cadena");
            }
            else {

                MessageBox.Show("El tipo: "+tipo1+" no se puede comparar con: "+tipo2);
            }
            return resp;
        }

        private static nodo E(ParseTreeNode raizE, string ambito)
        {
            // object resultado = "";
            nodo auxiliar = new nodo() ;
            ParseTreeNode hijoE = raizE.ChildNodes[0];

            if (hijoE.ToString().Equals("OP")){ //  OP

             
                RecorrerArbol.Recorrer(hijoE, "Global"); //OP.Rule en otra clase, y devuelve el result

                auxiliar.valor = RecorrerArbol.RESULT;
                auxiliar.tipo = "doble";

            } else {// id , true o false, cadena o char

              
                if (hijoE.ToString().Contains("id")){
                    /*si contiene (id) debe ir a traer su valor a la tabla de simbolos*/
                    string idd = hijoE.ToString().Replace(" (id)","") ;

                    auxiliar.valor = variables.Buscar_Dato( idd , ambito);
                    auxiliar.tipo = variables.TIPO;


                }
                else
                {// logico o cadena o character
                    string []idd = hijoE.ToString().Split(' ');

                    auxiliar.valor = idd[0];
                    auxiliar.tipo = idd[1];
                }
            }
            return auxiliar;
        }

        private static bool Comparar_Enteros(int val1,string simbolo,int val2) {
            bool retorn=false;
            switch (simbolo)
            {
                case "==": {
                        if (val1 == val2) { retorn = true; }
                    }
                    break;

                case "!=":
                    {
                        if (val1 != val2) { retorn = true; }
                    }
                    break;

                case "<=":
                    {

                        if (val1 <= val2){ retorn = true; }
                    }
                    break;

                case ">":{
                      
                        if (val1 > val2){  retorn = true;  }

                    }
                    break;

                case ">=":
                    {
                        if (val1 >= val2) { retorn = true;   }
                    }
                    break;
                case "<":
                    {

                        if (val1 < val2){ retorn = true; }
                    }
                    break;


            }
            return retorn;
        }

        private static bool Comparar_Dobles(double val1, string simbolo, double val2) {
            bool retorn = false;
            switch (simbolo)
            {
                case "==":
                    {
                        if (val1 == val2) { retorn = true; }
                    }
                    break;

                case "!=":
                    {
                        if (val1 != val2) { retorn = true; }
                    }
                    break;

                case "<=":
                    {

                        if (val1 <= val2) { retorn = true; }
                    }
                    break;

                case ">":
                    {

                        if (val1 > val2) { retorn = true; }

                    }
                    break;

                case ">=":
                    {
                        if (val1 >= val2) { retorn = true; }
                    }
                    break;
                case "<":
                    {

                        if (val1 < val2) { retorn = true; }
                    }
                    break;


            }
            return retorn;


        }

        private static bool Comparar_booleans(bool val1,string simbolo,bool val2) {
            bool retorn = false;
            switch (simbolo) {
                case "==": {
                        if (val1 == val2) { retorn = true; }
                    } break;
                case "!=": {
                        if (val1 != val2) { retorn = true; }
                    } break;
                default: {
                        MessageBox.Show("no se pueden utilizar "+simbolo+" para comparar (boolean) y (boolean)");
                    } break;
            }
       
            return retorn;
        }

        private static bool Comparar_Cadenas(string cad1, string simbolo, string cad2)
        {
            bool retorn = false;
            switch (simbolo)
            {
                case "==":{
                        if (cad1 == cad2) { retorn = true; }
                    }
                    break;
                case "!=":{
                        if (cad1 != cad2) { retorn = true; }
                    }
                    break;
                default:{
                        MessageBox.Show("no se pueden utilizar " + simbolo + " para comparar (cadena) y (cadena)");
                    }
                    break;
            }

            return retorn;
        }

        private static bool Comparar_Caracteres(char car1,string simbolo, char car2) {
            bool retorn = false;
            switch (simbolo){
                case "==":{
                        if (car1 == car2) { retorn = true; }
                    }
                    break;

                case "!=": {
                        if (car1 != car2) { retorn = true; }
                    }
                    break;

                case "<=":{

                        if (car1 <= car2) { retorn = true; }
                    }
                    break;

                case ">": {

                        if (car1 > car2) { retorn = true; }

                    }
                    break;

                case ">=":{
                        if (car1 >= car2) { retorn = true; }
                    }
                    break;

                case "<": {

                        if (car1 < car2) { retorn = true; }
                    }
                    break;


            }

            return retorn;
        }
    }
}

