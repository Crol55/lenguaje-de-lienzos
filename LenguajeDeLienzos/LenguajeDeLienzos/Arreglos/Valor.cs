using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace LenguajeDeLienzos.Arreglos
{
    class Valor
    {
        private Queue valor;
        public Valor() {
            
           valor = new Queue();
        }

        public void InsertarVal(object val) {

            valor.Enqueue(val);
        }
       

    }
}
