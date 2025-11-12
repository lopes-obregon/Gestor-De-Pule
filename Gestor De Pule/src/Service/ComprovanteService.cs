using Gestor_De_Pule.src.Model;
using System.Drawing.Printing;

namespace Gestor_De_Pule.src.Service
{
    internal class ComprovanteService
    {
        internal void PrintPule(object puleSelecionadoUi)
        {
            var puleSelecionado = Pule.ToPule(puleSelecionadoUi);
            if (puleSelecionado != null)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (s, ev) =>
                {
                    ev.Graphics.DrawString("Comprovante de Aposta", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, 20);
                    ev.Graphics.DrawString($"Nº do Pule: {puleSelecionado.Id}", new Font("Arial", 12), Brushes.Black, 20, 60);
                    ev.Graphics.DrawString($"Animais: {string.Join(", ", puleSelecionado.Animais)}", new Font("Arial", 12), Brushes.Black, 20, 90);
                    ev.Graphics.DrawString($"Data: {puleSelecionado.Date:dd/MM/yyyy HH:mm}", new Font("Arial", 12), Brushes.Black, 20, 120);
                    ev.Graphics.DrawString($"Status: {puleSelecionado.StatusPagamento}", new Font("Arial", 12), Brushes.Black, 20, 150);
                    ev.Graphics.DrawString($"Valor Apostado R$: {puleSelecionado.Valor.ToString("C")}", new Font("Arial", 12), Brushes.Black, 20, 180);

                };
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDocument;
                preview.ShowDialog();

            }
        }
    }
}
