using System;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class ConversorTamanho
    {
        // Tamanho fisico do cartão hidrossensivel
        public const double larguraCm = 7.6;
        public const double alturaCm = 2.6;

        public const double FatorCmParaMicrometro = 10000;

        public static double PixelParaCm2(Mat foto)
        {
            double cmPorPixelLargura = larguraCm / foto.Width;
            double cmPorPixelAltura = alturaCm / foto.Height;
            return cmPorPixelLargura * cmPorPixelAltura;
        }

        public static double PixelParaMicrometro(Mat foto)
        {
            double cmPorPixel = larguraCm / foto.Width;
            return cmPorPixel * FatorCmParaMicrometro;
        }

        public static void DebugConversao(Mat foto)
        {
            double pxMicra = PixelParaMicrometro(foto);
            double pxCm2 = PixelParaCm2(foto);
            Console.WriteLine($"[DEBUG] 1 pixel ≈ {pxMicra:F2} µm");
            Console.WriteLine($"[DEBUG] 1 pixel² ≈ {pxCm2:F6} cm²");
        }
    }
}
