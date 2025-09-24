# PORTFOLIO- Backend do Software Analise de Gotas 

- Este código representa a primeira versão de estudo. Algumas métricas (como DV01 e DMV) estão incorretas e foram revisadas em versões posteriores. Mantive aqui apenas para portfólio e análise de estilo de código.
- O código real/atual por motivos ÓBVIOS de segurança e proteção a mim e meu trabalho não sera em nenhuma hipótese divulgado. Como ja foi citado; este foi o primeiro esboço e unico que eu estarei divulgando sobre o software DropSense. 

           RESUMO
Este repositório apresenta um esboço inicial da lógica de backend desenvolvida em C# com OpenCV para análise de cartões hidrossensíveis (utilizados em pulverizações agrícolas). O código aqui publicado não é a versão final usada no software DropSense, mas sim o primeiro protótipo funcional que escrevi durante a concepção do projeto. A lógica foi posteriormente reescrita e corrigida em Java (Android), além de sofrer diversas melhorias para produção. 
Este repositório tem finalidade exclusivamente de portfólio, para demonstrar minha experiência com:
  - Processamento de imagens com OpenCV
  - Estruturação de código modular em C#
  - Aplicação de conceitos matemáticos/estatísticos em problemas reais

          FLUXO DE FUNCIONAMENTO
 1. Entrada de imagem
    - Leitura de um cartão hidrossensível (.jpg) com gotas pulverizadas.
    - Verificação de existência e ajuste de rotação caso necessário.
      
2. Pré-processamento
   - Conversão para escala de cinza.
   - Binarização (threshold automático de Otsu).
   - Remoção de bordas.

3. Detecção e segmentação
   - Localização de contornos (gotas).
   - Cálculo de areas individuais.
   - Conversão de pixels -> micrômetros e cm².
         
4. Cálculos estatísticos
   - D0.1, D0.9, DMV, DMN (valores de referência do diâmetro das gotas).
   - Menor e maior gota detectada.
   - Taxa de recuperação (L/ha).
   - Percentual de cobertura e numero de gotas por cm².

5. Saída
   - Exibição visual das gotas detectadas.
   - Impressão formatada de todos os resultados em console.

          Estrutura do código
  - Program.cs -> ponto de entrada do programa.
  - LeitorImagem.cs -> leitura e validação da imagem de entrada.
  - Processamento.cs -> pipeline de pré-processamento, binarização e contornos.
  - CalculosD0.cs -> funções estatísticas (cálculo de D0.1, DMV, D0.9, DMN etc.).
  - ConversorTamanho.cs -> conversões de pixel para µm e cm².
  - ResultadoAnalise.cs -> classe para organizar e imprimir os resultados.

          Observações importantes 
  - Este código contém erros conhecidos em alguns cálculos (D01 e DMV).
  - Foi o primeiro esboço da lógica de backend do DropSense.
  - Como já foi supracitado, reitéro: código real/atual não será publicado por motivos de segurança e sigilo comercial.


          Objetivo no portfólio
Este repositório serve para demonstrar: 
  - Capacidade de transformar um problema real em software funcional.
  - Habilidade e Experiência em C# + OpenCV.
  - Experiência em cálculo aplicado a visão computacional. 
  - Boas práticas de modularização de código e organização.
