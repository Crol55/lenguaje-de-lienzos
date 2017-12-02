using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using LenguajeDeLienzos.Gramatica.irony;
namespace LenguajeDeLienzos.Arreglos
{
    class arreglo{
        private Queue IDS;
        private Queue DIMENSIONES;
        private ArrayList VALORES;
        private string tipo ;
        private string Ambito;

        public arreglo(Queue id,Queue dim,ArrayList val,string tipo,string ambito) {
            /*se logran estructuras tales como arreg[2][3]={2,3,4}{1,2,3}*/
            this.IDS = id;
            this.DIMENSIONES = dim;
            this.VALORES = val;
            this.tipo = tipo;
            this.Ambito = ambito;
        }

        private void Dim_1(string tipo){

            object[] auxiliar = new object[Convert.ToInt32(DIMENSIONES.Dequeue())];
            int tamaño = IDS.Count;
            for (int i=0; i<tamaño;i++) {
                RecorrerArbol.TablaDeSimbolos.Add(new tabla(IDS.Dequeue().ToString(), tipo, Ambito, RecorrerArbol.clase, auxiliar, RecorrerArbol.Conservar));


            }

         
        }
            private void Dim_2(string tipo) {
            object[,] auxiliar = new object[Convert.ToInt32(DIMENSIONES.Dequeue()), Convert.ToInt32(DIMENSIONES.Dequeue())];
            int tamaño = IDS.Count;
            for (int i = 0; i < tamaño; i++)
            {
                RecorrerArbol.TablaDeSimbolos.Add(new tabla(IDS.Dequeue().ToString(), tipo, Ambito, RecorrerArbol.clase, auxiliar, RecorrerArbol.Conservar));


            }
          


        }
        private void Dim_3(string tipo)
        {
            object[,,] auxiliar = new object[Convert.ToInt32(DIMENSIONES.Dequeue()), Convert.ToInt32(DIMENSIONES.Dequeue()), Convert.ToInt32(DIMENSIONES.Dequeue())];
              int tamaño = IDS.Count;
            for (int i=0; i<tamaño;i++) {
                RecorrerArbol.TablaDeSimbolos.Add(new tabla(IDS.Dequeue().ToString(), tipo, Ambito, RecorrerArbol.clase, auxiliar, RecorrerArbol.Conservar));


            }
           


        }

        public void Generar_Array(){

            switch (DIMENSIONES.Count) {
                   
                  
                case 1: {
                        Dim_1(tipo); 
                    } break;

                case 2: {
                        Dim_2(tipo);
                    
                    } break;
                case 3: {
                        Dim_3(tipo);

                    } break;
                case 4: {

                    } break;
                case 5: {

                    } break;
                case 6: {

                    } break;
                case 7: {

                    } break;
                case 8: {

                    } break;
                case 9: {

                    } break;
                case 10: {

                    } break;



            }
            
            }
      
    }
}
