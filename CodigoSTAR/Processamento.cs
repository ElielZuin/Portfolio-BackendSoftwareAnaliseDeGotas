using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class Processamento
    {
        public static void TonsDeCinza(Mat foto, ResultadoAnalise resultado)
        {
            //convertendo ela pra tom de cinza
            Mat cinza = new Mat();
            Cv2.CvtColor(foto, cinza, ColorConversionCodes.BGR2GRAY);

            Binarizar(cinza, foto, resultado);
        }

        public static void Binarizar(Mat cinza, Mat foto, ResultadoAnalise resultado)
        {
            //aplicar threshold na foto e deixando ela preto e branco
            Mat binarizada = new Mat();
            Cv2.Threshold(cinza, binarizada, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

            DetectarContornos(binarizada, foto, resultado);
        }

        public static void DetectarContornos(Mat binarizada, Mat foto, ResultadoAnalise resultado)
        {
            Point[][] contornos;
            HierarchyIndex[] hierarchy;

            // Adiciona borda branca ao redor
            Cv2.CopyMakeBorder(binarizada, binarizada, 2, 2, 2, 2, BorderTypes.Constant, Scalar.White);
            Cv2.FindContours(binarizada, out contornos, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            Mat binarizadaInversa = new Mat();
            Cv2.BitwiseNot(binarizada, binarizadaInversa);

            Mat visualizacao = new Mat();
            Cv2.CvtColor(binarizada, visualizacao, ColorConversionCodes.GRAY2BGR);

            int totalGotas = 0;
            double somaPixelsPretos = 0;

            List<double> areas = new List<double>(); // Lista para guardar as aareas das gotas

            for (int i = 0; i < contornos.Length; i++)
            {
                Rect boundingBox = Cv2.BoundingRect(contornos[i]);

                if (boundingBox.Width >= binarizada.Width - 4 || boundingBox.Height >= binarizada.Height - 4)
                    continue;

                Mat mascara = Mat.Zeros(binarizada.Size(), MatType.CV_8UC1);
                Cv2.DrawContours(mascara, contornos, i, Scalar.White, -1);

                Mat resultadoBin = new Mat();
                Cv2.BitwiseAnd(binarizadaInversa, mascara, resultadoBin);

                int pixelsPretos = Cv2.CountNonZero(resultadoBin);

                //calcula e salva a area da gota
                double area = Cv2.ContourArea(contornos[i]);
                areas.Add(area);

                totalGotas++;
                if (pixelsPretos > 0)
                {
                    somaPixelsPretos += pixelsPretos;
                    Cv2.DrawContours(visualizacao, contornos, i, new Scalar(0, 0, 255), 1);
                    Console.WriteLine($"Contorno {i} - Pixels Pretos (gota): {pixelsPretos}");
                }
            }
            // aqui converte todas as areas para diametros micra
            List<double> diametros = CalculosD0.ListarDiametrosMicrometros(areas, foto);
            
            //jogar la pra estrutura dos results
            resultado.D01 = CalculosD0.CalcularD01(diametros);
            resultado.DMV = CalculosD0.CalcularDMV(diametros);
            resultado.D09 = CalculosD0.CalcularD09(diametros);
            resultado.DMN = CalculosD0.CalcularDMN(diametros);
            CalculosD0.CalcularMenorEMaior(diametros, resultado);


            // Conversão de pixel para cm²
            double pixelParaCm2 = ConversorTamanho.PixelParaCm2(foto);
            double areaFisicaCm2 = 19.76;
            double areaGotasCm2 = somaPixelsPretos * pixelParaCm2;
            double cobertura = (areaGotasCm2 / areaFisicaCm2) * 100.0;
            double gotasPorCm2 = totalGotas / areaFisicaCm2;

            //preenchendo os dados na estrutura ResultadoAnalise
            resultado.TotalGotas = totalGotas;
            resultado.AreaCoberta = areaGotasCm2;
            resultado.TaxaRecuperacao = CalculosD0.CalcularTaxaRecuperacaoReal(diametros);
            resultado.CoberturaPercentual = cobertura;
            resultado.GotasPorCm2 = gotasPorCm2;

            // exibicao na tela
            Cv2.ImShow("Contornos sobre imagem original", visualizacao);
            Cv2.ImShow("Imagem binarizada", binarizada);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

    }
}
