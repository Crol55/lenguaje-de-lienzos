using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using System.Windows.Forms;
using System.Threading.Tasks;
using LenguajeDeLienzos.Gramatica.irony;
namespace LenguajeDeLienzos.Dibujo
{
    class Nativas
    {

        public static void ejecutar(ParseTreeNode raiz, string ambito) {

            switch (raiz.ToString()) {
                case "NATIVAS": {
                        if (raiz.ChildNodes.Count==5) {// Pintar_P + OP +  OP + cadenas + OP                               
                     
                            RecorrerArbol.Recorrer(raiz.ChildNodes[1],ambito);
                            
                            int x = Convert.ToInt32(RecorrerArbol.RESULT);
                  
                            RecorrerArbol.Recorrer(raiz.ChildNodes[2], ambito);
                            int y = Convert.ToInt32(RecorrerArbol.RESULT);
                     
                            RecorrerArbol.Recorrer(raiz.ChildNodes[4], ambito);
                            int diametro = Convert.ToInt32(RecorrerArbol.RESULT);
            
                            string[] color = raiz.ChildNodes[3].ToString().Replace("\"","").Split(' ');
                            string colnuevo = color[0].Replace("#", "");
                            

                            Pintar.Dibujar_Punto(x,y,colnuevo, diametro);
                          
                        }
                        else {// Pintar_o +  OP + OP  +cadenas + OP + OP+ "o"(1)   |  Pintar_o + OP  + OP  +cadenas + OP + OP+"r" (2)
                            string a = raiz.ChildNodes[6].ToString().Replace(" (Keyword)","");
                            switch (a) {
                                case "o": {
                            
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[1], ambito);
                                        int x = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[2], ambito);
                                        int y = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[4], ambito);
                                        int ancho = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[5], ambito);
                                        int alto = Convert.ToInt32(RecorrerArbol.RESULT);
                                        string[] color = raiz.ChildNodes[3].ToString().Replace("\"", "").Split(' ');
                                        string colnuevo = color[0].Replace("#", "");
                                        int dif = ancho / 2;
                                        int dif2 = alto / 2;
                                        Pintar.Dibujar_ovalo(x-dif, y-dif2, colnuevo, ancho, alto);


                                    }
                                    break;
                                case "r": {
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[1], ambito);
                                        int x = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[2], ambito);
                                        int y = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[4], ambito);
                                        int ancho = Convert.ToInt32(RecorrerArbol.RESULT);
                                        RecorrerArbol.Recorrer(raiz.ChildNodes[5], ambito);
                                        int alto = Convert.ToInt32(RecorrerArbol.RESULT);
                                        string[] color = raiz.ChildNodes[3].ToString().Replace("\"", "").Split(' ');
                                        string colnuevo = color[0].Replace("#", "");

                                        Pintar.Dibujar_Rectangulo(x, y, colnuevo, ancho, alto);
                                    } break;

                            }

                          
                       
                          
                            
                    
                                
                              

                            


                        }


                    } break;
              
            
            }

        }
    }
}
