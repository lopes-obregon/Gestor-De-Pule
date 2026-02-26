using Gestor_De_Pule.Migrations;
using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Models
{
    internal class Rodada
    {
        /// <summary>
        /// id do pule
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de disputa
        /// </summary>
        public int IdDisputa {  get; set; }
        /// <summary>
        /// Navegate
        /// </summary>
        public Disputa? Disputa { get; set; }
        /// <summary>
        /// Nº rodada referente
        /// </summary>
        public byte Nrodadas { get; set; }
        /// <summary>
        ///     List dos pules da rodada referente
        /// </summary>
        public List<Pule>? PulesDestaRodada { get; set; }
        /**
         * Variavel antiga que contia os resultados, agora com a classe rodada Ela fica livre, podendo armazenar os melhores resultados da disputa.
         * 
         * **/
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
            Nrodadas = (byte)quantidadeRodadas;
        }

        public Rodada(Disputa disputa)
        {
            Disputa = disputa;
        }

        public Rodada(Disputa disputa, int nRodada) : this(disputa)
        {
            Nrodadas = (byte)nRodada;
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
