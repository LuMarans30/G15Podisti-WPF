using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace G15PodistiWPF
{
    public class Podista
    {
        private string nome;
        private int pettorale;
        private Stickman stickman;
        
        public Podista(string nome, int pettorale)
        {
            this.nome = nome;
            this.pettorale = pettorale;
            stickman = new Stickman(nome);
        }
        
        public int getPettorale()
        {
            return pettorale;
        }
        
        public Stickman GetStickman()
        {
            return stickman;
        }

        public string getNome()
        {
            return nome;
        }

        public string toString()
        {
            return nome + " con pettorale " + pettorale;
        }
    }
}
