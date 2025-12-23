using Gestor_De_Pule.src.Model;
using System.Drawing.Printing;

namespace Gestor_De_Pule.src.Service
{
    internal class ComprovanteService
    {
        internal void PrintPule(object puleSelecionadosUi)
        {
           // var puleSelecionados = Pule.ToPules(puleSelecionadosUi);
            var puleSelecionados = PuleService.ObterPulesSelecionados(puleSelecionadosUi);
            
            int indicieAtual = 0;
            PrintDocument printDocument = new PrintDocument();
            if (puleSelecionados != null && puleSelecionados.Count > 0)
            {
                printDocument.PrintPage += (s, e) =>
                {
                    int pulesPorPágina = 4;
                    float y = 40;
                    Font fonte = new Font("Arial", 12);
                    e.Graphics.DrawString("Comprovante de Aposta", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, y); y += 20;

                    for (int i = 0; i < pulesPorPágina && indicieAtual < puleSelecionados.Count; i++)
                    {
                        var pule = puleSelecionados[indicieAtual];
                        e.Graphics.DrawString($"Pule Nº: {pule.Id}", fonte, Brushes.Black, 20, y); y += 20;
                        e.Graphics.DrawString($"Animais: {string.Join(", ", pule.Animais)}", fonte, Brushes.Black, 20, y); y += 20;
                        e.Graphics.DrawString($"Data: {pule.Date:dd/MM/yyyy HH:mm}", fonte, Brushes.Black, 20, y); y += 20;
                        e.Graphics.DrawString($"Status: {pule.StatusPagamento}", fonte, Brushes.Black, 20, y); y += 20;
                        e.Graphics.DrawString($"Status: {pule.Valor.ToString("C")}", fonte, Brushes.Black, 20, y); y += 40;
                        indicieAtual++;
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
