using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Models
{
    internal class Rodada
    {
        private int _quantidadeRodadas;

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

        public Rodada(int quantidadeRodadas)
        {
            _quantidadeRodadas = quantidadeRodadas;
        }

        internal void Associete(List<Resultado>? resultadoList)
        {
            if(this is not null)
            {
                if (this.ResultadoDestaRodada is null && resultadoList is not null)
                {
                    this.ResultadoDestaRodada = resultadoList;
                }
                else
                {
                    if(resultadoList is not null)
                        this.ResultadoDestaRodada?.AddRange(resultadoList);
                }
            }
        }

        internal void Associete(Resultado resultado)
        {
            if(this.ResultadoDestaRodada == null)
                this.ResultadoDestaRodada= new List<Resultado>() { resultado };
            else if(!this.ResultadoDestaRodada.Contains(resultado))
                    this.ResultadoDestaRodada.Add(resultado);
        }
    }
}
