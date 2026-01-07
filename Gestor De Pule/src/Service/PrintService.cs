using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Views.Relatórios.Animal;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Service
{
    internal class PrintService
    {
        internal static void PrintAnimal(Animal? animal, List<Pule>? pules)
        {
            PrintDocument printDocument = new PrintDocument();
            int pageLinha = 80, pageColuna = 20, cntTotalPule =0, cntTotalApostadores =0;
            float cntTotalApostado = 0.0f;
            Font font = new Font("Arial", 12);
            if (animal is null)
            {
                printDocument.PrintPage += (s, e) => {

                    e.Graphics.DrawString("Algo Deu Errado!", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, pageLinha);

                };
            }
            else
            {
                printDocument.PrintPage += (s, e) => {
                    e.Graphics.DrawString("Relatório Do Animal", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, 20, 20);
                    e.Graphics.DrawString($"Nome Do Animal: {animal.Nome}  \t Nº {animal.Número}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                    e.Graphics.DrawString($"Apostadores e Pules", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                    e.Graphics.DrawLine(new Pen(Color.Black, 2),e.MarginBounds.Left, pageLinha, e.MarginBounds.Right, pageLinha); pageLinha +=20;
                    foreach(var pule in animal.Pules)
                    {
                        var puleBuscado = pules.Find(pu => pu.Id == pule.Id);
                        if(puleBuscado != null)
                        {
                            if(puleBuscado.Apostador is not null)
                            {
                                e.Graphics.DrawString($"\n Nome: {puleBuscado.Apostador.Nome}\t Contato: {puleBuscado.Apostador.Contato}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                                e.Graphics.DrawString($"\n Nº Pule: {puleBuscado.Número}\t R$ {puleBuscado.Valor.ToString("C")}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                                cntTotalApostado += puleBuscado.Valor;
                                cntTotalPule++;
                                cntTotalApostadores++;

                            }
                        }
                        
                    }
                    pageLinha += 20;
                    e.Graphics.DrawLine(new Pen(Color.Black, 2), e.MarginBounds.Left, pageLinha, e.MarginBounds.Right, pageLinha); pageLinha += 20;
                    e.Graphics.DrawString($"\n Total Apostado R$:{cntTotalApostado.ToString("C")}\t Total De Apostas: {cntTotalPule}\t Total Apostadores: {cntTotalApostadores}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                };
            }
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            try{
                printPreviewDialog.ShowDialog();
            }
            catch { PrintAnimal(animal, pules); }
        }

        internal static void PrintRelatórioApostador(Apostador apostadorUi, List<Pule>? pulesUi, string totalDePules, string valorTotalApostado)
        {
            Apostador? apostador = Apostador.GetApostador(apostadorUi);
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            int pageLinha = 80, pageColuna = 20;
            if(apostador is  null)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (s, e) =>
                {
                    Font font = new Font("Arial", 12);
                    e.Graphics.DrawString("Relatório Do Apostador", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, 20);
                    e.Graphics.DrawString("Algo Deu Errado!", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, pageLinha);
                };
               
                printPreviewDialog.Document = printDocument;
                //printPreviewDialog.ShowDialog();
            }
            else
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (s, e) =>
                {
                    Font font = new Font("Arial", 12);
                    e.Graphics.DrawString("Relatório Do Apostador", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 20, 20);
                    e.Graphics.DrawString($"Nome Do Apostador: {apostador.Nome}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                    e.Graphics.DrawString($"Contato Do Apostador: {apostador.Contato}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 40;
                    e.Graphics.DrawString($"Pules Do Apostador:", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, pageColuna, pageLinha); pageLinha += 30;
                    foreach(var pule in apostador.Pules)
                    {
                        e.Graphics.DrawString($"Nº: {pule.Número}, Data: {pule.Date}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                        e.Graphics.DrawString($"Animal: {pule.Animais.First().Nome}, Valor {pule.Valor.ToString("C")}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 20;
                        e.Graphics.DrawString($"Status Pagamento: {pule.StatusPagamento}", font, Brushes.Black, pageColuna, pageLinha); pageLinha += 40;
                    }
                };
               
                printPreviewDialog.Document = printDocument;
            }
            try
            {
                printPreviewDialog.ShowDialog();

            }
            catch (Exception ex) { PrintRelatórioApostador(apostadorUi, pulesUi, totalDePules, valorTotalApostado); }
        }

        internal static void PrintRelatórioPule(List<Pule> pules, object numPuleUi)
        {
            int? numPule = numPuleUi as int?;
            PrintDocument printDocument = new PrintDocument();
            Font font = new Font("Arial", 12);
            int pagLinha = 80, pagColuna = 20;
            if (pules is null || pules.Count == 0)
            {
                printDocument.PrintPage += (s, e) =>
                {
                    e.Graphics.DrawString("Algo Deu Errado!", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, pagLinha);
                };
            }
            else
            {
                printDocument.PrintPage += (s, e) => {
                    e.Graphics.DrawString($"Relatório Pule {numPule}", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, pagColuna, pagLinha); pagLinha += 20;
                    foreach (Pule pule in pules)
                    {
                        if (pule is not null)
                        {
                            // e.Graphics.DrawString($"\n Nº {pule.Número}", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, pagColuna, pagLinha); pagLinha += 20;
                           // e.Graphics.DrawString($"\n Apostador:{pule.Apostador} ", font, Brushes.Black, pagColuna, pagLinha); pagLinha += 20;
                            e.Graphics.DrawString($"\n Apostador:{pule.Apostador}\t Data:{pule.Date}\t Animal:{pule.Animais.First().Nome}\n Valor Apostado:{pule.Valor.ToString("C")}\t Status Pagamento:{pule.StatusPagamento}", font, Brushes.Black, pagColuna, pagLinha); pagLinha += 80;
                        }
                    }
                };
            }
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            try
            {
                printPreviewDialog.ShowDialog();

            }
            catch (Exception ex) { PrintRelatórioPule(pules, numPuleUi); }

        }
    }
}
