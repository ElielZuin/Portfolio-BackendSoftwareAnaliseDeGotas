using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("º-- Análise de Gotas --º");
            Console.WriteLine("------------------------\n");

            LeitorImagem.InputImagem();
            
        }
    }
}
