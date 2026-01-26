using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Models
{
    internal class Rodada
    {
        public int Id { get; set; }
        public Disputa? Disputa { get; set; }
        public byte Nrodadas { get; set; }
        public List<Pule>? PulesDestaRodada { get; set; }
        public List<Resultado>? ResultadoDestaRodada {get; set;}
        public Rodada() { }

        public Rodada(Disputa disputa, int quantidadeRodadas, List<Pule>? pules, List<Resultado>? resultadoList)
        {
            Disputa = disputa;
            Nrodadas = (byte)quantidadeRodadas;
            PulesDestaRodada = pules;
            ResultadoDestaRodada = resultadoList;
        }
    }
}
