using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Gestor_De_Pule.src.Model
{
        public  enum StatusPagamento
        {
            Pendente,
            Pago,
            Cancelado
        }
     internal class Pule
    {
        /// <summary>
        /// Pule id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id Apostador
        /// </summary>
        public int? ApostadorId { get; set; }
        /// <summary>
        /// Navegate Apostador
        /// </summary>
        public Apostador? Apostador { get; set; }
        /// <summary>
        /// sustatus pagamento 
        /// </summary>
        public StatusPagamento StatusPagamento { get; set; }
        /// <summary>
        /// Dia que foi criado
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Navegate Animais
        /// </summary>
        public List<Animal>? Animais { get; set; }
        /// <summary>
        /// Id Disputa
        /// </summary>
        public int? DisputaId { get; set; }
        /// <summary>
        /// Disputa navegate
        /// </summary>
        public Disputa? Disputa { get; set; }
        public int Número { get; set; }
        public decimal Valor {  get; set; }
        //Propriedade para chamar no listVew
        public string NomeAnimais => (string)AnimaisToString();
        public string ValorFormatado => Valor.ToString("C2");
        /// <summary>
        /// Rodada id
        /// </summary>
        public int? RodadaId { get; set; }
        /// <summary>
        /// Rodada navegate
        /// </summary>
        public Rodada? Rodada { get; set; }
        //construct
        public Pule() { }

        public Pule(Apostador? apostador, Disputa disputa, StatusPagamento pagamento, List<Animal>? animais, decimal valor, int númeroDoPule)
        {
            Apostador = apostador;
            Disputa = disputa;
            StatusPagamento = pagamento;
            Date = DateTime.Now;
            Animais = animais;
            Valor = valor;
            Número = númeroDoPule;
        }

        public Pule(int apostadorId, int disputaId, StatusPagamento pagamento, List<Animal>? animais, decimal valor, int númeroDoPule)
        {
            this.ApostadorId = apostadorId;
            this.DisputaId = disputaId;
            this.StatusPagamento = pagamento;
            this.Animais = animais;
            this.Valor = valor;
            this.Número = númeroDoPule;
            Date = DateTime.Now;
        }
        internal String AnimaisToString()
        {
            string nomeAnimal = String.Empty;
            if (Animais != null)
            {
                for (int i = 0; i < Animais.Count; i++)
                {
                    if (Animais[i] is not null)
                    {
                        nomeAnimal += Animais[i].Nome;
                        if (i + 1 < Animais.Count)
                        {
                            nomeAnimal += ", ";
                        }
                    }
                }
            }
            return nomeAnimal;
        }
        

       
        public override string ToString()
        {
            return Número.ToString();
        }
       
        /// <summary>
        /// verifica se o item fornecido é um apostador e verifica se os ids são iguais
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true or false</returns>
        internal bool IsEqualsApostador(object item)
        {
            Apostador? apostador = (Apostador?)item;
            if(apostador != null)
                if(ApostadorId == apostador.Id) return true;
            return false;
        }
        /// <summary>
        /// Set in new pule data
        /// </summary>
        /// <returns>A new <see cref="Pule"/>.</returns>
        internal Pule Clone()
        {
            return new Pule
            {
                Apostador = this.Apostador,
                Disputa = this.Disputa,
                StatusPagamento = this.StatusPagamento,
                Date = this.Date,
                Animais = this.Animais,
                Valor = this.Valor,
                Número = this.Número
            };
        }
    }
}
