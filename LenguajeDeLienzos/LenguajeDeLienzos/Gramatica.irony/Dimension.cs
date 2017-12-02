using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
namespace LenguajeDeLienzos.Gramatica.irony
{
    class Dimension
    {
        ArrayList listaDatos = new ArrayList();  
        public object dimension;

        public Dimension(object tamaño)
        {
            this.dimension = tamaño;

        }
        public void addValor(object dat)
        {
            listaDatos.Add(dat);
        }

        public object getValor(int indice)
        {

            return listaDatos[indice];
        }

       public void setDato(int indice, object valor)
        {
            listaDatos[indice] = valor;
        }


    }
}
