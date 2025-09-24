using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class CalculosD0
    {
        public static List<double> ListarDiametrosMicrometros(List<double> areas, Mat foto)
        {
            //lista onde vamos armazenar todos os diametros convertidos para micrometros
            List<double> diametrosMicra = new List<double>();

            //para cada area em pixels² na lista recebida
            foreach (double areaPx in areas)
            {
                //calcula o diametro equivalente da gota assumindo forma circular (em pixels)
                double diametroPx = 2 * Math.Sqrt(areaPx / Math.PI);

                //converte esse diametro de pixels para micra
                double diametroMicra = diametroPx * ConversorTamanho.PixelParaMicrometro(foto);


                // adiciona na lista final
                diametrosMicra.Add(diametroMicra);
            }

            //retorna a lista com todos os diametros em micrometros
            return diametrosMicra;
        }

        public static double CalcularD01(List<double> diametros)
        {
            var listaOrdenada = diametros.OrderBy(d => d).ToList();
            double somaTotal = listaOrdenada.Sum();

            double alvo = somaTotal * 0.10;
            double acumulado = 0;

            foreach (var d in listaOrdenada)
            {
                acumulado += d;
                if (acumulado >= alvo)
                    return d;
            }

            return 0;
        }

        public static double CalcularDMV(List<double> diametros)
        {
            //ordena os diametros do menor para o maior
            var listaOrdenada = diametros.OrderBy(d => d).ToList();

            //soma total dos diametros
            double somaTotal = listaOrdenada.Sum();

            // define valor-alvo para 50% da soma
            double alvo = somaTotal * 0.50;

            //acumula ate atingir 50%
            double acumulado = 0;

            foreach (double d in listaOrdenada)
            {
                acumulado += d;

                if (acumulado >= alvo)
                {
                    // d é o DMV baseado na soma dos diametros
                    return d;
                }
            }

            return 0;
        }

        public static double CalcularD09(List<double> diametros)
        {
            var listaOrdenada = diametros.OrderBy(d => d).ToList();
            double somaTotal = listaOrdenada.Sum();

            double alvo = somaTotal * 0.90;
            double acumulado = 0;

            foreach (var d in listaOrdenada)
            {
                acumulado += d;
                if (acumulado >= alvo)
                    return d;
            }

            return 0;
        }

        public static double CalcularDMN(List<double> diametros)
        {
            // ordena a lista de diâmetros em ordem crescente
            var ordenados = diametros.OrderBy(d => d).ToList();

            int n = ordenados.Count;

            if (n == 0)
                return 0;

            // se for ímpar, retorna o valor do meio
            if (n % 2 == 1)
                return ordenados[n / 2];

            // se for par, faz a média dos dois centrais
            double d1 = ordenados[(n / 2) - 1];
            double d2 = ordenados[n / 2];

            return (d1 + d2) / 2;
        }

        public static void CalcularMenorEMaior(List<double> diametros, ResultadoAnalise resultado)
        {
            double menor = double.MaxValue;
            double maior = double.MinValue;

            foreach (double gota in diametros)
            {
                if (gota <= 0) continue; // ignorar gotas invalidas ou vazias

                if (gota < menor)
                    menor = gota;

                if (gota > maior)
                    maior = gota;
            }

            resultado.MenorGota = menor;
            resultado.MaiorGota = maior;
        }

        public static double CalcularTaxaRecuperacaoReal(List<double> diametros)
        {
            if (diametros == null || diametros.Count == 0)
                return 0;

            //area do cartao hidrossensivel em hectares (7.6 cm x 2.6 cm = 19.76 cm² = 0.000001976 ha)
            const double areaCartaoHa = 0.000001976;

            // soma dos volumes em cm³
            double volumeTotalCm3 = 0;

            foreach (double diametroMicra in diametros)
            {
                //converte diametro de micra para cm
                double diametroCm = diametroMicra / 10000.0;

                //volume da gota esférica em cm³
                double volumeCm3 = (Math.PI / 6.0) * Math.Pow(diametroCm, 3);

                volumeTotalCm3 += volumeCm3;
            }

            //converte cm³ para litros
            double volumeLitros = volumeTotalCm3 / 1000.0;

            //taxa de recuperação = Litros / hectare
            double taxaRecuperacao = volumeLitros / areaCartaoHa;

            return taxaRecuperacao;
        }
    }
}
