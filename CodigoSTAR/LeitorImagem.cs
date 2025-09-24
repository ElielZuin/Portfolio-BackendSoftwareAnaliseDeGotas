using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CodigoSTAR
{
    internal class LeitorImagem
    {
        public static void InputImagem()
        {
            //colocando a imagem
            string caminhoImagem = "C:/ImagemGotas/gota.jpg";

            Mat foto = Cv2.ImRead(caminhoImagem);

            if (foto.Empty())       //detecta se a imagem existe ou nao
            {
                Console.WriteLine("\n\nErro: imagem não encontrada.");
                return;
            }
            else
            {
                Console.WriteLine("Imagem carregada com sucesso!");
                Console.WriteLine($"Tamanho: {foto.Width} x {foto.Height}");

            }
            //ver se ela nao esta na horizontal pra n buga td
            if (foto.Height > foto.Width)
            {
                Cv2.Rotate(foto, foto, RotateFlags.Rotate90Counterclockwise);
                Console.WriteLine("Imagem rotacionada com sucesso!");
                Console.WriteLine($"Tamanho: {foto.Width} x {foto.Height}");
            }

            //aplicando dado nos conversores
            ConversorTamanho.PixelParaCm2(foto);
            ConversorTamanho.PixelParaMicrometro(foto);

            //chamando processamento da imagem
            ResultadoAnalise resultado = new ResultadoAnalise();
            Processamento.TonsDeCinza(foto, resultado);
            Console.WriteLine(resultado); //vai exibir tudo formatado pelo ToString()
            ConversorTamanho.DebugConversao(foto);

        }
    }
}
