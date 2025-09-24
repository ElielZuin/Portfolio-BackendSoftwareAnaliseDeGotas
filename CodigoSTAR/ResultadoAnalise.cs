using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class ResultadoAnalise
    {
        public int TotalGotas { get; set; }
        public double AreaCoberta { get; set; }
        public double CoberturaPercentual { get; set; }
        public double GotasPorCm2 { get; set; }
        public double DMV { get; set; }
        public double DMN { get; set; }
        public double D01 { get; set; }
        public double D09 { get; set; }
        public double TaxaRecuperacao { get; set; }
        public double MenorGota { get; set; }
        public double MaiorGota { get; set; }

        public override string ToString()
        {
            return $@"
----------- RESULTADOS FINAIS -----------
Nº Total de Gotas: {TotalGotas}
Área coberta em cm²: {AreaCoberta:F2} cm²
% de Cobertura: {CoberturaPercentual:F2}%
Gotas por cm²: {GotasPorCm2:F2}
DMV: {DMV:F2} µm
DMN: {DMN:F2} µm
D0.1: {D01:F2} µm
D0.9: {D09:F2} µm
Taxa de recuperação: {TaxaRecuperacao:F6} L/h
Menor gota: {MenorGota:F2} µm
Maior gota: {MaiorGota:F2} µm
----------------------------------------
";
        }
    }
}