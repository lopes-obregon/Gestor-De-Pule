using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using System.Drawing.Printing;

namespace Gestor_De_Pule.src.Service
{
    internal class ComprovanteService
    {
          private const int Coluna1 = 40;
        private const int Coluna2 = 380;
        internal void PrintPule(object puleSelecionadosUi)
        {
           // var puleSelecionados = Pule.ToPules(puleSelecionadosUi);
            var puleSelecionados = PuleService.ObterPulesSelecionados(puleSelecionadosUi);
            float projeção = 0;
            float totalArrecadado = 0;
            float valorTaxa = 0;
            int indicieAtual = 0;
            PrintDocument printDocument = new PrintDocument();
            if (puleSelecionados != null && puleSelecionados.Count > 0)
            {
                printDocument.PrintPage += (s, e) =>
                {
                    int pulesPorPágina = 4;
                    float y = 40, saveIniY = 0, saveFinalY = 0;
                    bool isSegundaCoulna = false;
                    Font fonte = new Font("Arial", 12);
                    e.Graphics.DrawString("Comprovante de Aposta", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, y); y += 20;
                    float x = Coluna1;
                    for (int i = 0; i < pulesPorPágina && indicieAtual < puleSelecionados.Count; i++)
                    {
                        var pule = puleSelecionados[indicieAtual];
                        var animal = pule.Animais.First();
                        saveIniY = y;

                        if(pule.Disputa is not null)
                        {
                            e.Graphics.DrawString($"Pule Nº: {pule.Número} Disputa: {pule.Disputa.Nome}", fonte, Brushes.Black, x, y); y += 20;

                        }else
                            e.Graphics.DrawString($"Pule Nº: {pule.Número} ", fonte, Brushes.Black, x, y); y += 20;
                        e.Graphics.DrawString($"Apostador: {pule.Apostador}", fonte, Brushes.Black, x , y); y += 20;
                        //e.Graphics.DrawString($"Animal: {string.Join(", ", pule.Animais)}", fonte, Brushes.Black, x, y); y += 20;
                        if(animal != null)
                        {
                            e.Graphics.DrawString($"Animal: {animal.Nome}", fonte, Brushes.Black, x, y); y += 20;
                            e.Graphics.DrawString($"Jockey: {animal.Jockey}", fonte, Brushes.Black, x, y); y += 20;

                        }
                        else
                        {
                            e.Graphics.DrawString($"Animal: Não encontrado", fonte, Brushes.Black, x, y); y += 20;
                            e.Graphics.DrawString($"Jockey: Não encontrado", fonte, Brushes.Black, x, y); y += 20;
                        }
                        e.Graphics.DrawString($"Data: {pule.Date:dd/MM/yyyy HH:mm}", fonte, Brushes.Black, x, y); y += 20;
                        e.Graphics.DrawString($"Status: {pule.StatusPagamento}", fonte, Brushes.Black, x, y); y += 20;
                        e.Graphics.DrawString($"Valor: {pule.Valor.ToString("C")}", fonte, Brushes.Black, x, y); y += 40;
                        if(pule.Disputa is not null)
                        {
                            var disputa = Disputa.GetDisputaWithPule(pule.Disputa);
                            
                            if(disputa is not null)
                            { 
                                totalArrecadado = disputa.GetTotalArrecadado();
                                valorTaxa = totalArrecadado * disputa.GetTaxaToFloat();
                                projeção = totalArrecadado + pule.Valor - valorTaxa;
                                projeção /= disputa.GetTotalAnimal(pule.Animais) + pule.Valor;
                                projeção *= pule.Valor;
                                e.Graphics.DrawString($"Valor Aproximado de Prêmio: {projeção.ToString("C")}", fonte, Brushes.Black, x, y); y += 20;
                                e.Graphics.DrawString($"Taxa: {disputa.GetTaxa().ToString("P")}", fonte, Brushes.Black, x, y); y += 20;
                                e.Graphics.DrawString($"Valor De taxa: {valorTaxa.ToString("C")}", fonte, Brushes.Black, x, y); y += 40;

                            }

                        }
                        saveFinalY = y;
                        indicieAtual++;
                        if (!isSegundaCoulna)
                        {
                            x = Coluna2; // seta x para segunda coluna
                            y = saveIniY;
                            isSegundaCoulna = true;

                        }
                        else
                        {
                            x = Coluna1; //x seta para primeira coluna
                            y = saveFinalY;
                            isSegundaCoulna = false;
                        }
                    }
                    //continuar imprimido se houver pules
                    e.HasMorePages = indicieAtual < puleSelecionados.Count;


                };
                PrintPreviewDialog preview = new();
                preview.Document = printDocument;
                preview.ShowDialog();

            }
        }
    }
}
