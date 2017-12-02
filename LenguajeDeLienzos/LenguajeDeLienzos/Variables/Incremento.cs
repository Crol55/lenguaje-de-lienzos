using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using LenguajeDeLienzos.Gramatica.irony;
namespace LenguajeDeLienzos.Variables
{
    class Incremento
    {

        public static void ejecutar(ParseTreeNode raiz,string ambito) {
      
            switch (raiz.ToString()) {
                #region INCREMENTO 
                case "INCREMENTO": {
                       // MessageBox.Show("incrementando variables");

                        if (raiz.ChildNodes.Count == 2) {//  id +('++' o '--')
                            if (raiz.ChildNodes[1].ToString().Contains("OP")) {// id + OP

                                string id = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                
                                RecorrerArbol.Recorrer(raiz.ChildNodes[1],ambito);//---->OP.Rule
                         
                             
                                variables.Modificar_Datos(ambito,id,RecorrerArbol.RESULT);

                             

                            }
                            else {
                                
                                string id = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                                string tipo_incremento = raiz.ChildNodes[1].ToString().Replace(" (Key symbol)", "");

                                object valor = variables.Buscar_Dato(id, ambito);//recuperar valor de la variable

                                tabla auxi = variables.VerificarTipo; //  nos devuelve el tipo de la variable
                                switch (auxi.f_tipo)
                                {

                                    case "entero":
                                        {
                                            int val = Int32.Parse(valor.ToString());

                                            switch (tipo_incremento)
                                            {

                                                case "++": val = val + 1; break;

                                                case "--": val = val - 1; break;

                                            }

                                            variables.Modificar_Datos(ambito, id, val);
                                            //  MessageBox.Show("nuevo valor es  " + val);

                                        }
                                        break;

                                    case "caracter":
                                        {
                                            char c = Convert.ToChar(valor);

                                            switch (tipo_incremento)
                                            {
                                                case "++": c++; break;

                                                case "--": c--; break;
                                            }

                                            variables.Modificar_Datos(ambito, id, c);

                                            //  MessageBox.Show("nuevo valor es  " + c);
                                        }
                                        break;

                                    case "doble":
                                        {
                                            double d = Convert.ToDouble(valor);

                                            switch (tipo_incremento)
                                            {
                                                case "++": d = d + 0.01; break;

                                                case "--": d = d - 0.01; break;
                                            }
                                            variables.Modificar_Datos(ambito, id, d);
                                            // MessageBox.Show("nuevo valor es  " + d);



                                        }
                                        break;

                                    default:
                                        {
                                            MessageBox.Show("Error, el incremento o decremento no se puede aplicar a variables de tipo " + auxi.f_tipo);
                                        }
                                        break;

                                } // cierre switch
                            }
                        }
                        else {//  id + ( '+=' o '-=' ) + OP (3 hijos) 
                         //  MessageBox.Show("incrementos raros");
                            string id = raiz.ChildNodes[0].ToString().Replace(" (id)", "");
                            string tipo_incremento = raiz.ChildNodes[1].ToString().Replace(" (Key symbol)", "");
                            RecorrerArbol.Recorrer(raiz.ChildNodes[2], ambito);//---->OP.Rule
                            double NuevoVal = Convert.ToDouble(RecorrerArbol.RESULT);

                            object valor = variables.Buscar_Dato(id, ambito);//recuperar valor de la variable
                            tabla auxi = variables.VerificarTipo; //  nos devuelve el tipo de la variable
                         
                            switch (auxi.f_tipo)
                            {

                                case "entero":
                                    {
                                        int val = Int32.Parse(valor.ToString());

                                        switch (tipo_incremento)
                                        {

                                            case "+=": val = val + Convert.ToInt32(NuevoVal); break;

                                            case "-=": val = val - Convert.ToInt32(NuevoVal); break;

                                        }

                                        variables.Modificar_Datos(ambito, id, val);
                                        // MessageBox.Show("nuevo valor es  "+val);

                                    }
                                    break;

                                case "doble":
                                    {
                                        double d = Convert.ToDouble(valor);

                                        switch (tipo_incremento)
                                        {

                                            case "+=": d = d + NuevoVal; break;

                                            case "-=": d = d - NuevoVal; break;
                                        }
                                        variables.Modificar_Datos(ambito, id, d);
                                        //  MessageBox.Show("nuevo valor es  " + d);

                                    }
                                    break;

                                default:
                                    {
                                        MessageBox.Show("Error, el incremento o decremento (+= -=) no se puede aplicar a variables de tipo " + auxi.f_tipo);
                                    }
                                    break;

                            }


                        }



                    }
                    break;


                    #endregion


            }
        }


    }
}
