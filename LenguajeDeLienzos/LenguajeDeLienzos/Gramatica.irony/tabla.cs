using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class tabla
    {
       private string id;
       private  string conservar;
       private  string ambito;
       private  string claselienzo;
       private object valor;
       private string tipo;
       private ArrayList arregloval;
        public  tabla(string idd,string tip,string ambitoo,string clase,object val,string conservarr) {
            this.id = idd;
            this.conservar = conservarr;
            this.ambito = ambitoo;
            this.claselienzo = clase;
            this.valor = val;
            this.tipo = tip;
            
        }
        public tabla(string idd, string tip, string ambitoo, string clase, ArrayList valorr, string conservarr)
        {
            this.id = idd;
            this.conservar = conservarr;
            this.ambito = ambitoo;
            this.claselienzo = clase;
            this.arregloval = valorr;
            this.tipo = tip;
           // Dimension auxiliar=(Dimension)arregloval[0];
            this.valor =arregloval.Count+"Dimensiones";
        }


        #region  funciones get y set 

        public object f_arreglo {
            get { return this.arregloval; }

        }
        public string f_id
        {

            get { return id;  }
            set { id = value; } // value es una funcion que c# trae por defecto....


        }
        public string f_conservar
        {

            get { return conservar;  }
            set { conservar = value; }


        }



        public string f_ambito
        {

            get { return ambito;  }
            set { ambito = value; }


        }

        public string f_claselienzo
        {

            get { return claselienzo;  }
            set { claselienzo = value; }


        }

        public object f_valor
        {

            get { return valor;  }
            set { valor = value; }


        }

        public string f_tipo
        {

            get { return tipo;  }
            set { tipo = value; }


        }





        #endregion



    }




}
