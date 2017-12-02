using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using System.Collections;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class Gramatica:Grammar
    {

       public static ArrayList Errores = new ArrayList();
        public static void concatenar(string linea,string col,string tipoError,string descripcion) {// concatenar las partes de los errores
            string concat =linea+"­±"+col+"±"+tipoError+"±"+descripcion;//± se utilizara luego con un split para poder obtener los datos
            Errores.Add(concat);

        }
        public Gramatica() : base(caseSensitive: true) {
            #region Expresiones Regulares
           
            RegexBasedTerminal numero = new RegexBasedTerminal("num", "[0-9]+");// digitos
            RegexBasedTerminal decimall = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");// digitos
            RegexBasedTerminal cadenas = new RegexBasedTerminal("cadenas", "\"[_|(0-9)|+|#|*|(a-z)|(A-Z)|?|%|&|'|(|)|,|-|.|/|:|;|<|=|>|?|[|@|^|}|{|~]+\"");// Strings=cadenas 
            RegexBasedTerminal chars = new RegexBasedTerminal("chars", "[^­­¿]");// Chars='*'
            IdentifierTerminal id = new IdentifierTerminal("id");
            CommentTerminal comentario = new CommentTerminal("comentario", ">>", "\n", "\r\n");
            CommentTerminal bloque_comentario = new CommentTerminal("bloqueC", "<-", "->");
            #endregion
        

            #region Terminales

            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var div = ToTerm("/");
            var elev = ToTerm("^");
            var boolean =ToTerm("boolean");
            var entero = ToTerm("entero");
            var cadena = ToTerm("cadena");
            var caracter = ToTerm("caracter");
            var doble = ToTerm("doble");
            var conservar = ToTerm("Conservar");
            var verdadero = ToTerm("true");
            var falso = ToTerm("false");
            var arreglo = ToTerm("arreglo");
            var publico = ToTerm("publico");
            var privado = ToTerm("privado");
            var extiende = ToTerm("extiende");
            var lienzo = ToTerm("Lienzo");
            var retorno = ToTerm("retorna");
            var principal = ToTerm("Principal");
            var Si = ToTerm("si");
            var Sino = ToTerm("sino");
            var Para = ToTerm("para");
            var Mientras = ToTerm("mientras");
            var Hacer = ToTerm("hacer");
            var AND = ToTerm("&&");
            var OR = ToTerm("||");
            var NAND = ToTerm("!&&");
            var NOR = ToTerm("!||");
            var XOR = ToTerm("&|");
            var NOT = ToTerm("!");
            var Pintar_P = ToTerm("Pintar_P");
            var Pintar_o = ToTerm("Pintar_OR");
            var apertura = ToTerm("¿");
            #endregion

            #region No Terminales
            //  NonTerminal I = new NonTerminal("I");// Inicio
            NonTerminal LISTA_ATRIBUTOS = new NonTerminal("LISTA_ATRIBUTOS");// lista que contendra a todas las declaraciones
            NonTerminal TIPO = new NonTerminal("TIPO");// para bool,String,int
            NonTerminal ASIG = new NonTerminal("ASIG"); // para operaciones matematicas
            NonTerminal CADENA = new NonTerminal("CADENA"); // para textos o chars
            NonTerminal OP = new NonTerminal("OP");// para +-*/ y decimales
            NonTerminal LOGICO = new NonTerminal("LOGICO");// para true or false
            NonTerminal VARIABLES = new NonTerminal("VARIABLES");
            NonTerminal L_nombres = new NonTerminal("LISTA_NOMBRES");//lista de nombres de variables 
            NonTerminal ID = new NonTerminal("ID");//lista de nombres de variables 
            NonTerminal ID2 = new NonTerminal("ID2");//lista de nombres de variables 
            NonTerminal LISTA_ARREGLO = new NonTerminal("LISTA_ARR");//lista de nombres de variables
            NonTerminal ARREGLOS = new NonTerminal("ARREGLO");
            NonTerminal TAMAÑO = new NonTerminal("TAMAÑO");
            NonTerminal L_DIMENSION = new NonTerminal("L_DIMENSION");
            NonTerminal L_ID = new NonTerminal("L_ID");
 
            NonTerminal LIENZOPRINCIPAL = new NonTerminal("LIENZO");
            NonTerminal INICIO = new NonTerminal("INICIO");
            NonTerminal VISIBILIDAD = new NonTerminal("VISIBILIDAD");
            NonTerminal CUERPO= new NonTerminal("CUERPO");
            NonTerminal LISTA_CUERPO = new NonTerminal("LISTA_CUERPO");
            NonTerminal ATRIBUTOS = new NonTerminal("ATRIBUTO");
            NonTerminal FUNCIONES = new NonTerminal("FUNCIONES");
            NonTerminal PARAMETROS= new NonTerminal("PARAMETROS");
            NonTerminal LI_PARAMETROS = new NonTerminal("LI_PARAMETROS");
           
            NonTerminal RETORNO = new NonTerminal("RETORNO");
            NonTerminal PRINCIPAL = new NonTerminal("PRINCIPAL");
            NonTerminal ASIGNACION = new NonTerminal("ASIGNACION");
            NonTerminal INSTRUCCIONES = new NonTerminal("INSTRUCCIONES");
            NonTerminal L_INSTRUCCIONES = new NonTerminal("L_INSTRUCCIONES");
            NonTerminal A = new NonTerminal("A");
            NonTerminal CICLOS = new NonTerminal("CICLOS");
            NonTerminal EXP_R = new NonTerminal("EXP_R"); // Expresion Relacional
            NonTerminal EXP_L = new NonTerminal("EXP_L"); // Expresion Logica
            NonTerminal LISTA_EXP_L = new NonTerminal("LISTA_EXP_L"); // LISTA de expresiones logicas
            NonTerminal CONDICION = new NonTerminal("CONDICION");
            NonTerminal E = new NonTerminal("E"); // expresiones
            NonTerminal S = new NonTerminal("S");// simbolos relacionales como == =! 
            NonTerminal SL = new NonTerminal("SL");// simbolos logicos como || && 
          
       
            NonTerminal INCREMENTO = new NonTerminal("INCREMENTO"); // incremento de variables
            NonTerminal INICIALIZAR = new NonTerminal("INICIALIZAR");
            NonTerminal INIC = new NonTerminal("INIC");
            NonTerminal INVOCAR = new NonTerminal("INVOCAR"); //invocacion a metodos
            NonTerminal L_PARAMETROS = new NonTerminal("L_PARAMETROS");
            NonTerminal L_PARAM = new NonTerminal("L_PARAM");
            NonTerminal L_PARAM2 = new NonTerminal("L_PARAM2");
          
            NonTerminal NATIVAS = new NonTerminal("NATIVAS");// funciones nativas del lenguaje
            NonTerminal ASIG_MATRIZ = new NonTerminal("ASIG_MATRIZ");
            NonTerminal ID_M = new NonTerminal("ID_M");
            NonTerminal LISTA_ID_M = new NonTerminal("LISTA_ID_M");
            NonTerminal ID_M2 = new NonTerminal("ID_M2");
            NonTerminal LISTA_EXT = new NonTerminal("LISTA_EXT");

            NonTerminal ASIG_ARR = new NonTerminal("ASIG_ARR");
            NonTerminal LISTA_ASIGNACIONES = new NonTerminal("LISTA_ASIGNACIONES");
            NonTerminal ASIGF = new NonTerminal("ASIGF");
            NonTerminal CONDICION2 = new NonTerminal("CONDICION2");
            NonTerminal CONDICION3 = new NonTerminal("CONDICION3");
            NonTerminal SIMPLE = new NonTerminal("SIMPLE");
            NonTerminal ASOCIACION = new NonTerminal("ASOCIACION");

            #endregion


            #region Gramatica
            INICIO.Rule =LIENZOPRINCIPAL
                ;
         
            LIENZOPRINCIPAL.Rule = lienzo + id + ToTerm("¿") + CUERPO + ToTerm("?")
                                  | lienzo + id + extiende + LISTA_EXT + ToTerm("¿") +CUERPO + ToTerm("?")
                                  | VISIBILIDAD + lienzo + id + ToTerm("¿") + CUERPO + ToTerm("?")
                                  | VISIBILIDAD + lienzo + id + extiende + LISTA_EXT + ToTerm("¿")+CUERPO + ToTerm("?")
                                 ;
          

            LISTA_EXT.Rule =MakePlusRule(LISTA_EXT, ToTerm(",") , id)
                       ;

      

            VISIBILIDAD.Rule = publico | privado | SyntaxError;
          
            CUERPO.Rule = MakeStarRule(CUERPO,LISTA_CUERPO);

            LISTA_CUERPO.Rule = ATRIBUTOS
                              | FUNCIONES
                              | PRINCIPAL ;
          

            PRINCIPAL.Rule =principal+ ToTerm("(") + ToTerm(")") + ToTerm("¿")+ INSTRUCCIONES + ToTerm("?")
                         ;
         

            INSTRUCCIONES.Rule = MakeStarRule(INSTRUCCIONES,L_INSTRUCCIONES);

            L_INSTRUCCIONES.Rule = ATRIBUTOS   // declaracion de variables
                                 | ASIGNACION // asignacion e incremento de variables
                                 | CICLOS
                                 | INVOCAR + ToTerm("$")  //invocar metodos()
                                 | NATIVAS;

           


            ATRIBUTOS.Rule =    LISTA_ATRIBUTOS;

            ASIGNACION.Rule =  INICIALIZAR // variables
                             | INCREMENTO + ToTerm("$"); // variables

            INCREMENTO.Rule = id + ToTerm("++")
                             | id + ToTerm("--")
                             | id + ToTerm("+=") + OP
                             | id + ToTerm("-=") + OP
                             | id +ToTerm("=")+ OP
                           ;
         

            INVOCAR.Rule = id + "(" + L_PARAMETROS + ")"
                           | id + "(" +")";

            L_PARAMETROS.Rule = MakePlusRule(L_PARAMETROS,ToTerm(",") , L_PARAM) ;

            L_PARAM.Rule = ASIG
                    //  | id + "[" + OP + "]"
                        |id + L_DIMENSION
                      ;


          



            INICIALIZAR.Rule = id + "=" + A + ToTerm("$")  //x=|true|"cad"|op$
                               | id + "=" + INVOCAR + ToTerm("$")        // x=metodo()$
                               | id + "[" + OP + "]"+ "=" + A + ToTerm("$")
                               | id + "[" + OP + "]" + "=" + INVOCAR + ToTerm("$")
                               | id + "[" + INVOCAR + "]" + "=" + INVOCAR + ToTerm("$")
                               | id + "[" + INVOCAR + "]" + "=" + A + ToTerm("$")
                              ; 

        



            NATIVAS.Rule =Pintar_P + "("+OP+","+ OP +"," + cadenas +","+ OP+ ")"+ToTerm("$")
                 |Pintar_o + "(" + OP + "," + OP + "," +cadenas + "," + OP +","+OP +","+ "'"+"o"+"'" + ")" + ToTerm("$")
                 | Pintar_o + "(" + OP + "," + OP + "," +cadenas+ "," + OP + "," + OP + "," + "'" + "r" + "'" + ")" + ToTerm("$")
                          ;
        

            CICLOS.Rule = Si + ToTerm("(") + CONDICION + ToTerm(")") + ToTerm("¿")+INSTRUCCIONES+ ToTerm("?")
              |Si + ToTerm("(") + CONDICION + ToTerm(")") + ToTerm("¿") +INSTRUCCIONES + ToTerm("?")+ Sino + ToTerm("¿")+ INSTRUCCIONES + ToTerm("?")
              |Para + ToTerm("(")+ INIC +";"+ CONDICION +";"+INCREMENTO + ToTerm(")") + ToTerm("¿")+INSTRUCCIONES + ToTerm("?")
              |Mientras+ ToTerm("(")+ CONDICION + ToTerm(")") + ToTerm("¿")+INSTRUCCIONES + ToTerm("?")
              |Hacer + ToTerm("¿")+INSTRUCCIONES + ToTerm("?") + Mientras +ToTerm("(")+ CONDICION + ToTerm(")") +ToTerm("$");

            CONDICION.Rule = CONDICION2;



            CONDICION2.Rule = EXP_R + CONDICION3
                             |ASOCIACION 
                           ;
     

            CONDICION3.Rule = //SL + EXP_R +CONDICION3 
                             MakeStarRule(CONDICION3,SIMPLE)
                             ;
            SIMPLE.Rule= SL + EXP_R
                       | SL + ASOCIACION ;

            ASOCIACION.Rule =  ToTerm("(") + CONDICION2+ ToTerm(")")
                              | NOT + ToTerm("(") + CONDICION2 + ToTerm(")")
                              | ToTerm("(") + CONDICION2 + ToTerm(")") + SL+CONDICION2
                              |NOT + ToTerm("(") + CONDICION2 + ToTerm(")") + SL + CONDICION2;

            EXP_R.Rule = E + S + E
                       | LOGICO
                       |NOT + E + S + E
                       |NOT + LOGICO
                       ;

            E.Rule = id | CADENA | LOGICO | OP;

            S.Rule = ToTerm("==")
                    | ToTerm("!=")
                    | ToTerm("<=")
                    | ToTerm(">")
                    | ToTerm(">=")
                    | ToTerm("<");

            //EXP_L.Rule = EXP_R + SL + EXP_R //+Lista_SL
            //            | "(" + EXP_R + SL + EXP_R + ")" //+ Lista_SL
            //            | NOT + "(" + EXP_R + SL + EXP_R + ")" //+Lista_SL

                        ;

            SL.Rule = AND
                    | OR
                    | NAND
                    | XOR
                    | NOR;

            INIC.Rule = id + "=" + OP
                       |ToTerm("var")+TIPO+ id + "=" + OP
                       | ToTerm("var") + TIPO + id + "=" + INVOCAR
                       | id + "=" + INVOCAR
                      ;



            A.Rule = id | CADENA | LOGICO ;

   FUNCIONES.Rule = id + ToTerm("(") + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + ToTerm("?")
         | TIPO + id + ToTerm("(") + ToTerm(")") + ToTerm("¿")+ INSTRUCCIONES +RETORNO + ToTerm("?")
         |TIPO+"["+"]" + id + ToTerm("(") + ToTerm(")") + ToTerm("¿")+ INSTRUCCIONES + RETORNO + ToTerm("?")
         |id+ToTerm("(")+PARAMETROS + ToTerm(")") + ToTerm("¿")+ INSTRUCCIONES + ToTerm("?")
         |TIPO + id + ToTerm("(")+ PARAMETROS + ToTerm(")") + ToTerm("¿")+INSTRUCCIONES +RETORNO+ ToTerm("?")
         |TIPO+"["+"]" + id + ToTerm("(")+ PARAMETROS + ToTerm(")") + ToTerm("¿")+ INSTRUCCIONES+ RETORNO+ ToTerm("?")
         | VISIBILIDAD+ id + ToTerm("(") + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + ToTerm("?")
         | VISIBILIDAD+ TIPO + id + ToTerm("(") + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + RETORNO + ToTerm("?")
         | VISIBILIDAD + TIPO + "[" + "]" + id + ToTerm("(") + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + RETORNO + ToTerm("?")
         | VISIBILIDAD + id + ToTerm("(") + PARAMETROS + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + ToTerm("?")
         | VISIBILIDAD + TIPO + id + ToTerm("(") + PARAMETROS + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + RETORNO + ToTerm("?")
         | VISIBILIDAD+ TIPO + "[" + "]" + id + ToTerm("(") + PARAMETROS + ToTerm(")") + ToTerm("¿") + INSTRUCCIONES + RETORNO + ToTerm("?")

         ;


            PARAMETROS.Rule = MakePlusRule(PARAMETROS,ToTerm(","),LI_PARAMETROS);

            LI_PARAMETROS.Rule = TIPO + id;
 

            RETORNO.Rule =  retorno +id+ ToTerm("$") 
                          | retorno + CADENA + ToTerm("$")
                          | retorno + LOGICO + ToTerm("$")
                          | retorno + OP + ToTerm("$")  ;
            

         

            LISTA_ATRIBUTOS.Rule = MakePlusRule(LISTA_ATRIBUTOS, VARIABLES);// TODAS LAS VARIABLES tienen un unico padre que seria lista
      

            VARIABLES.Rule = conservar + ToTerm("var") + TIPO + ID + ToTerm("$")
                         | ToTerm("var") + TIPO + ID + ToTerm("$")
                         |ARREGLOS
                        ;
            // VARIABLES.ErrorRule = SyntaxError + "$";

            ID.Rule = MakePlusRule(ID,ToTerm(","),ID2);
        
                         //  ;

            ID2.Rule =  id + ToTerm("=") + ASIG
                     | id;


            ARREGLOS.Rule = conservar + ToTerm("var") + TIPO + arreglo + L_ID + L_DIMENSION +ASIG_MATRIZ + ToTerm("$")
                           | ToTerm("var") + TIPO + arreglo + L_ID + L_DIMENSION +  ASIG_MATRIZ + ToTerm("$");

            L_ID.Rule = MakePlusRule(L_ID, ToTerm(",") , id)
                     | SyntaxError + id;

            L_DIMENSION.Rule =MakePlusRule(L_DIMENSION ,TAMAÑO);

                    
            TAMAÑO.Rule = ToTerm("[") + OP + ToTerm("]");

            ASIG_MATRIZ.Rule = ToTerm("=") + ASIG_ARR 
                              | ToTerm("=") + INVOCAR
                              |Empty;

            ASIG_ARR.Rule = ToTerm("{")+ LISTA_ID_M + ToTerm("}")
                            | ToTerm("{")+LISTA_ASIGNACIONES + ToTerm("}");

            LISTA_ID_M.Rule = MakePlusRule(LISTA_ID_M, ToTerm(",") , OP);

            LISTA_ASIGNACIONES.Rule =MakePlusRule(LISTA_ASIGNACIONES,ToTerm(","),ASIGF ) ;

            ASIGF.Rule = ToTerm("{") + LISTA_ID_M + ToTerm("}");

            TIPO.Rule = entero | cadena | boolean | caracter | doble |SyntaxError+TIPO;

           // TIPO.ErrorRule = SyntaxError + "$";

            ASIG.Rule = OP    |   CADENA   | LOGICO |  INVOCAR;

            OP.Rule = OP + mas + OP
                     | OP + menos + OP
                     | OP + por + OP
                     | OP + div + OP
                     | OP + elev + OP
                     | ToTerm("(") + OP + ToTerm(")")
                     | numero
                     | decimall
                     | id
                     | SyntaxError + OP;// acepta el error pero se recupera con otra operacion
                 //   ;

           

            CADENA.Rule = cadenas 
                       |  "'" + chars + "'";

            LOGICO.Rule = verdadero | falso;



            #region comentarios
            NonGrammarTerminals.Add(comentario);
            NonGrammarTerminals.Add(bloque_comentario);
            #endregion

            #region  OPTIMIZACION DEL AST
            this.Root = INICIO;
            this.RegisterOperators(1,Associativity.Left,mas,menos);
            this.RegisterOperators(2, Associativity.Left, por, div);
            this.RegisterOperators(2, Associativity.Left, elev);
            this.RegisterOperators(3,Associativity.Right,NOT);
            this.RegisterOperators(4, Associativity.Left, AND,NAND);
            this.RegisterOperators(4, Associativity.Left, OR,NOR,XOR);

            this.MarkTransient(CADENA,LISTA_CUERPO,ATRIBUTOS,L_INSTRUCCIONES,TIPO,ID_M,ID_M2,L_PARAM2,ASIG,VISIBILIDAD,ASIGNACION,LOGICO,ASIG_ARR,ASIGF,ASIG_MATRIZ, A , S ,SL );// funciona para NO TERMINALES  para las transiciones que se pueden reducir a solo una unica transicion..
            this.MarkPunctuation("(",")","=","$","var",",","\"","'","?","{","}","¿","retorna","Principal",";"); // quitar nodos que no son utiles
            this.MarkReservedWords("var","Lienzo","publico","privado","extiende","boolean","entero","cadena","doble","Conservar","arreglo","false","true","retorna","Principal","si","sino","para","mientras","hacer","caracter","Pintar_P","Pintar_OR");
            this.NonGrammarTerminals.Add(comentario);
            //    this.AddToNoReportGroup(LOGICO);
            #endregion

            #endregion

        }
        
        public override void ReportParseError(ParsingContext context)
        {
            base.ReportParseError(context);
            String error = context.CurrentToken.ValueString;
           // context.CurrentParserState()
              //   MessageBox.Show("" + context.);
           // MessageBox.Show("input "+context.CurrentParserInput);
            String tipo;
            int fila;
            int columna;
            string descripcion="";
      
            if (error.Contains("Invalid character")){
                tipo = "Error Lexico";
                fila = context.Source.Location.Line;
                columna = context.Source.Location.Column;

                string delimStr = ":";
                char[] delim = delimStr.ToCharArray();
                string[] division = error.Split(delim, 2);
                division = division[1].Split('.');
                descripcion = "Caracter Invalido " + division[0];
               // MessageBox.Show("lexico : fila "+fila+" col "+columna+"   "+division[0]);
                concatenar(fila.ToString(),columna.ToString(),tipo,descripcion);
            }
            
              
                tipo = "Error Sintactico";
                fila = context.Source.Location.Line;
                columna = context.Source.Location.Column;
                descripcion = "Se esperaba "+context.GetExpectedTermSet().ToString();

               concatenar(fila.ToString(), columna.ToString(),tipo,descripcion);
                //MessageBox.Show("lexico :fila " + fila + " col " + columna);
           

          
        }

    }

   
}
