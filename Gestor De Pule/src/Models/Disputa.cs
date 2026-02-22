using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Gestor_De_Pule.src.Models
{
    internal class Disputa
    {


        public int Id { get; set; }
        public DateTime DataEHora { get; set; } = new DateTime();
        public string Nome { get; set; } = String.Empty;
        public List<Resultado>? ResultadoList { get; set; }
        public List<Pule>? Pules { get; set; }
        public Caixa? Caixa { get; set; }
        public decimal? TotalPago { get; set; }
        public StatusPagamento Pagamento { get; set; }
        public List<Rodada>? Rodadas { get; set; }
        /// <summary>
        /// Initializes a new instance of the Disputa class.
        /// </summary>
        public Disputa() { }
        /// <summary>
        /// Initializes a new instance of the Disputa class with the specified name, date and time, and result.
        /// </summary>
        /// <param name="nome">The name of the dispute.</param>
        /// <param name="dataEHora">The date and time of the dispute.</param>
        /// <param name="resultados">The result to add to the dispute.</param>
        public Disputa(string nome, DateTime dataEHora, Resultado resultados)
        {
            Id = 0;
            DataEHora = dataEHora;
            Nome = nome;
            if (ResultadoList is null)
                ResultadoList = new List<Resultado> { resultados };
            else
                ResultadoList.Add(resultados);
            Pagamento = StatusPagamento.Pendente;
        }
        /// <summary>
        /// Initializes a new instance of the Disputa class with the specified name and date/time.
        /// </summary>
        /// <param name="nome">The name of the dispute.</param>
        /// <param name="dateTime">The date and time of the dispute.</param>
        public Disputa(string nome, DateTime dateTime)
        {
            this.Nome = nome;
            this.DataEHora = dateTime;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object, specifically the value of the <see cref="Nome"/> property.</returns>
        public override string ToString()
        {
            return Nome;
        }
        /// <summary>
        /// Calculates the number of animals associated with the current dispute.
        /// </summary>
        /// <returns>The total count of animals linked to the dispute identified by the current instance's ID.</returns>
        internal int GetNumAnimais()
        {
            int contadorAnimais = 0;
            foreach (var resultado in ResultadoList)
            {
                if (resultado != null)
                {
                    if (resultado.Disputa.Id == Id) contadorAnimais++;
                }
            }
            return contadorAnimais;
        }

        /// <summary>
        /// Updates the positions of animals in ResultadoList by fetching the latest results, sorting them by time, and
        /// assigning sequential positions.
        /// </summary>
        internal void ajustarPosiçãoDosAnimais()
        {
            List<Resultado> resultadoList = new List<Resultado>();
            foreach (var resultadoUi in ResultadoList)
            {
                if (resultadoUi is not null)
                {
                    //buscar os resultados no banco que podem estar atualizados
                    Resultado? resultado = Resultado.BuscarResultado(resultadoUi);
                    //se os resultados não forem nulos buscado no banco.
                    if (resultado is not null)
                    {
                        //agora  adiciona na lista para manipular mais tarde
                        resultadoList.Add(resultado);
                    }
                }
            }
            //agora vamos organizar as posição pelo tempo
            // resultadoList.Sort(); //ORDENA DO MENOR PARA O MAIOR;
            resultadoList = resultadoList.OrderBy(res => res.Tempo).ToList();
            byte pos = 0;
            foreach (var resultado in resultadoList)
            {
                if (resultado is not null)
                {
                    resultado.Posição = ++pos;
                }
            }
            ResultadoList = resultadoList;
        }
        /// <summary>
        /// Updates the current dispute and its related results in the database.
        /// </summary>
        internal void Atualizar()
        {
            using DataBase db = new DataBase();
            try
            {
                var disputaDb = db.Disputas.Include(dis => dis.ResultadoList)
                    .ThenInclude(res => res.Animal).FirstOrDefault(dis => dis.Id == this.Id);
                if (disputaDb is not null)
                {
                    if (disputaDb.Id == this.Id)
                    {
                        foreach (var resultado in disputaDb.ResultadoList)
                        {
                            if (resultado is not null)
                            {
                                if (ResultadoList.Any(res => res.Animal.Id == resultado.Animal.Id))
                                {
                                    int pos = ResultadoList.FindIndex(r => r.Animal.Id == resultado.Animal.Id);
                                    resultado.Posição = ResultadoList[pos].Posição;
                                    db.Update(resultado);
                                }
                            }

                        }
                        db.Update(disputaDb);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        /// <summary>
        /// Retrieves a list of Disputa entities from the database, including related ResultadoList, Animal, Pules, and
        /// Apostador data, filtering out entries with empty or null names.
        /// </summary>
        /// <returns>A list of Disputa objects if any are found; otherwise, null.</returns>

        /// <summary>
        /// Adds the current instance to the database if it does not already exist and saves changes.
        /// </summary>
        /// <returns>True if the operation succeeds; otherwise, false.</returns>
        internal bool save()
        {
            using DataBase db = new DataBase();
            try
            {
                if (this is not null)
                {
                    var disputaDb = db.Disputas.FirstOrDefault(dis => dis.Id == this.Id);
                    if (disputaDb is null)
                    {
                        //quer dizer que não existe essa disputa a inda
                        db.Disputas.Add(this);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Retrieves the name of the winning animal from the results list.
        /// </summary>
        /// <returns>The name of the animal in first position, or "Animal Não encontrado!" if not found.</returns>
        internal string GetNomeAnimalVencedor()
        {
            string mensagem = String.Empty;
            if(Rodadas is null || Rodadas.Count == 0){
                if (ResultadoList is not null && ResultadoList.Count > 0)
                {
                    foreach (var resultado in ResultadoList)
                    {
                        if (resultado is not null)
                        {
                            if (resultado.Posição == 1)
                            {
                                if (resultado.Animal != null && !String.IsNullOrEmpty(resultado.Animal.Nome))
                                {
                                    return resultado.Animal.Nome;
                                }
                            }
                        }
                    }
                }
                else
                {
                    mensagem  = "Animal Não encontrado!";
                }
                
            }
            else
            {
                var vencedorList1 = ResultadoList?.FirstOrDefault(r=> r.Posição==1);
                var vencedorRodadas = Rodadas.SelectMany(r => r.ResultadoDestaRodada)
                    .Where(res => res.Posição == 1)
                    .GroupBy(res => res.Animal.Nome)
                    .Select(g => new { Nome = g.Key, Vitórias = g.Count() })
                    .OrderByDescending(x => x.Vitórias)
                    .First();
               
                if (vencedorList1 is not null && vencedorRodadas is not null)
                    if (String.Equals(vencedorList1.Animal.Nome, vencedorRodadas.Nome, StringComparison.OrdinalIgnoreCase))
                        mensagem = vencedorRodadas.Nome;
                    else
                        mensagem = $"Vencedor da Primeira Rodada {vencedorList1?.Animal.Nome} e Vencedor das Outras Rodadas {vencedorRodadas.Nome}";
                else
                    mensagem = "Erro ao calcular a posição";

            }
            return mensagem;
        }

       

       

        /// <summary>
        /// Counts and returns the number of winning bets where the first animal matches the winning animal.
        /// </summary>
        /// <returns>The total number of winning bets.</returns>
        internal int CntTotalGanhadoresPules()
        {
            //contar e retornar a quantidade de ganhadores de pules ganhadores.
            int idAnimalVencedor = GetIdAnimalVencedor();
            int cntGanhadores = 0;
            if (idAnimalVencedor > -1)
            {
                if (Pules is not null && Pules.Count > 0)
                {
                    foreach (var pule in Pules)
                    {
                        if (pule is not null)
                        {
                            if (pule.Animais.First().Id == idAnimalVencedor)
                            {
                                cntGanhadores++;
                            }
                        }
                    }
                }
                return cntGanhadores;
            }
            return cntGanhadores;
        }
        /// <summary>
        /// Retrieves the ID of the winning animal from the ResultadoList, or -1 if no winner is found.
        /// </summary>
        /// <returns>The ID of the animal in first position, or -1 if not found.</returns>
        private int GetIdAnimalVencedor()
        {
            if (ResultadoList is not null && ResultadoList.Count > 0)
            {
                foreach (var resultado in ResultadoList)
                {
                    if (resultado is not null)
                    {
                        if (resultado.Posição == 1)
                        {
                            if (resultado.Animal != null && !String.IsNullOrEmpty(resultado.Animal.Nome))
                            {
                                return resultado.Animal.Id;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Calculates and returns the net prize amount per winning bet, formatted as currency, based on the total
        /// collected, applicable fee, and number of winning bets.
        /// </summary>
        /// <returns>A string representing the net prize per winning bet in currency format, or an error message if calculation
        /// is not possible.</returns>
        internal string PagamentoPorPule()
        {
            //int quantidadeDePulesVencedores = CntTotalGanhadoresPules();
            int quantidadeDePulesVencedores = 0;
            decimal totalArrecadado = 0.00m;
            decimal valorTaxa = 0.00m;
            decimal prêmioLiquido = 0.00m;
            var caixa = Caixa;
            var resultadoAnimalVencedorList1 = this.ResultadoList.FirstOrDefault(an=> an.Posição==1);
            if (Pules is not null && Pules.Count > 0)
            {
                if(resultadoAnimalVencedorList1 is not null)
                {
                    quantidadeDePulesVencedores = Pules.SelectMany(p => p.Animais)
                        .Where(an => an.Id == resultadoAnimalVencedorList1?.Animal?.Id).Count();

                }
                foreach (var pule in Pules)
                {
                    if (pule is not null)
                        totalArrecadado += (decimal)pule.Valor;
                }
                if (Caixa is not null)
                {
                    valorTaxa = totalArrecadado * Caixa.Taxa;
                    prêmioLiquido = totalArrecadado - valorTaxa;
                    return (prêmioLiquido / quantidadeDePulesVencedores).ToString("C");

                }
                else
                    return "Erro ao calcular!";

            }
            return "Algum Erro encontrado!";

        }
        /// <summary>
        /// Retrieves a Disputa entity by its Id, including related Pules, Animais, and Caixa entities.
        /// </summary>
        /// <param name="disputa">The Disputa instance containing the Id to search for.</param>
        /// <returns>The Disputa entity with related data if found; otherwise, null.</returns>
        internal static Disputa? GetDisputaWithPule(Disputa disputa)
        {
            using DataBase db = new DataBase();
            Disputa? disputaDb = null;
            try
            {
                if (disputa is not null)
                {
                    disputaDb = db.Disputas.Include(dis => dis.Pules).ThenInclude(pu => pu.Animais).Include(dis => dis.Caixa).FirstOrDefault(dis => dis.Id == disputa.Id);
                    if (disputaDb is not null)
                        return disputaDb;
                }


            }
            catch { return disputaDb; }
            return disputaDb;
        }
        /// <summary>
        /// Calculates the total amount collected by summing the 'Valor' of all non-null 'Pules' in the collection.
        /// </summary>
        /// <returns>The total collected amount as a float.</returns>
        internal float GetTotalArrecadado()
        {
            float totalArrecadado = 0;
            if (Pules is not null && Pules.Count > 0)
            {
                foreach (var pule in Pules)
                {
                    if (pule is not null)
                    {
                        totalArrecadado += pule.Valor;
                    }
                }
            }
            return totalArrecadado;
        }
        /// <summary>
        /// Retrieves the value of Caixa.Taxa as a float, or returns 0 if Caixa is null.
        /// </summary>
        /// <returns>The float representation of Caixa.Taxa, or 0 if Caixa is null.</returns>
        internal float GetTaxaToFloat()
        {
            float taxa = 0;
            if (Caixa is not null)
            {
                taxa = (float)Caixa.Taxa;
            }
            return taxa;
        }
        /// <summary>
        /// Calculates the total value associated with the first animal in the provided list by summing the 'Valor' of
        /// all matching entries in 'Pules'.
        /// </summary>
        /// <param name="animais">A list of Animal objects to match against entries in 'Pules'.</param>
        /// <returns>The total value for the matching animal, or 0 if no matches are found.</returns>
        internal float GetTotalAnimal(List<Animal> animais)
        {
            float totalAnimal = 0;
            if (animais is not null)
            {
                var animal = animais.First();
                if (Pules is not null && Pules.Count > 0)
                {
                    foreach (var pule in Pules)
                    {
                        if (pule is not null && pule.Animais is not null)
                        {
                            if (pule.Animais.First().Id == animal.Id)
                            {
                                totalAnimal += pule.Valor;
                            }
                        }
                    }
                }
            }
            return totalAnimal;
        }
        /// <summary>
        /// Retrieves the tax value from Caixa if available; otherwise, returns 0.00.
        /// </summary>
        /// <returns>The tax value from Caixa or 0.00 if Caixa is null.</returns>
        internal decimal GetTaxa()
        {
            decimal total = 0.00m;
            if (Caixa is not null)
                total = Caixa.Taxa;
            return total;
        }
        /// <summary>
        /// Calculates the total value of all non-null Pules in the collection.
        /// </summary>
        /// <returns>The sum of the Valor property of each non-null Pule as a decimal.</returns>
        internal decimal GetTotalValorPule()
        {
            decimal total = 0;
            if (Pules is not null && Pules.Count > 0)
            {
                foreach (var pule in Pules)
                {
                    if (pule is not null)
                    {
                        total += (decimal)pule.Valor;
                    }
                }
            }
            return total;
        }
        /// <summary>
        /// Calculates the total value of all paid items in the Pules collection.
        /// </summary>
        /// <returns>The sum of the Valor property for items with StatusPagamento set to Pago.</returns>
        internal decimal PulesPagos()
        {
            decimal total = decimal.Zero;
            if (Pules is not null)
            {
                foreach (var pule in Pules)
                {
                    if (pule is not null)
                    {
                        if (pule.StatusPagamento == StatusPagamento.Pago)
                        {
                            total += (decimal)pule.Valor;
                        }
                    }
                }
            }
            return total;
        }
        /// <summary>
        /// Updates the current dispute's payment information in the database.
        /// </summary>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        internal bool UpdatePagamentoEpagamento()
        {
            using DataBase db = new DataBase();
            bool sucess = false;
            try
            {
                /*var disputaDb = db.Disputas.FirstOrDefault(dis => dis.Id == this.Id);
                if (disputaDb != null) {
                    disputaDb.Pagamento = this.Pagamento;
                    disputaDb.TotalPago = TotalPago;
                    db.Disputas.Update(disputaDb);
                    db.SaveChanges();
                    sucess = true;
                }*/
                if (this != null)
                {
                    db.Attach(this);
                    db.Disputas.Update(this);
                    db.SaveChanges();
                    sucess = true;
                }
            }
            catch { sucess = false; }
            return sucess;
        }

        /// <summary>
        /// Retrieves the associated Apostador for the specified Pule from the database.
        /// </summary>
        /// <param name="pule">The Pule instance for which to reload the Apostador.</param>
        /// <returns>The Apostador associated with the given Pule, or null if not found.</returns>
        internal static Apostador? ReloadPule(Pule pule)
        {
            using DataBase db = new DataBase();
            Apostador? apostador = null;
            try
            {
                var puleDb = db.Pules
                    .Include(p => p.Apostador)
                    .FirstOrDefault(p => p.Id == pule.Id);
                if (puleDb is not null)
                {
                    apostador = puleDb.Apostador;
                }
            }
            catch { return apostador; }
            return apostador;
        }
        /// <summary>
        /// Associates the specified Resultado with the current object if it is not already present in the
        /// ResultadoList.
        /// </summary>
        /// <param name="resultado">The Resultado instance to associate.</param>
        internal void Associete(Resultado resultado)
        {
            if (this.ResultadoList is null)
            {
                this.ResultadoList = new List<Resultado> { resultado };

            }

            else
            {

                if (!this.ResultadoList.Contains(resultado))
                {
                    this.ResultadoList.Add(resultado);
                }
            }
        }
        /// <summary>
        /// Removes all resultados from the ResultadoList and clears their association with the current disputa.
        /// </summary>
        internal void RemoveResultados()
        {
            var resultados = this.ResultadoList;
            if (resultados != null)
            {
                foreach (var resultado in resultados)
                {
                    if (resultado is not null)
                    {
                        resultado.Disputa = null;
                        this.ResultadoList?.Remove(resultado);

                    }
                }
            }
        }
        /// <summary>
        /// Removes all non-null pule objects from the Pules collection and clears their Disputa references.
        /// </summary>
        internal void RemovePules()
        {
            if (this.Pules != null)
            {
                foreach (var pule in this.Pules)
                {
                    if (pule is not null)
                    {
                        pule.Disputa = null;
                        this.Pules?.Remove(pule);

                    }
                }
            }
        }

        internal void RemoveFromCaixa()
        {
            if (this.Caixa != null)
            {

                this.Caixa?.Disputs?.Remove(this);
                this.Caixa = null;

            }
        }

        internal void ajustarPosiçãoDosAnimais(object? tempoAnimal1, object? tempoAnimal2, bool isResultados= false)
        {
            string? tempo1Str = tempoAnimal1?.ToString()?.Replace(',', '.');
            string? tempo2Str = tempoAnimal2?.ToString()?.Replace(',', '.');
            var rodadas = Rodadas;
            if (isResultados | rodadas is null || rodadas.Count == 0)
            {
                if (!String.IsNullOrEmpty(tempo1Str) && !String.IsNullOrEmpty(tempo2Str))
                {
                    if (TimeSpan.TryParse(tempo1Str, out TimeSpan tempo1) && TimeSpan.TryParse(tempo2Str, out TimeSpan tempo2))
                    {
                        //if(tempo1 < tempo2) {
                        var resultados = this.ResultadoList;
                        if (resultados != null)
                        {
                            
                                if (tempo1 < tempo2)
                                {
                                    resultados[0].Posição = 1;
                                    resultados[1].Posição = 2;
                                }
                                else
                                {
                                    {
                                        resultados[0].Posição = 2;
                                        resultados[1].Posição = 1;
                                    }
                                }
                            
                        }
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(tempo1Str) && !String.IsNullOrEmpty(tempo2Str))
                {
                    if (TimeSpan.TryParse(tempo1Str, out TimeSpan tempo1) && TimeSpan.TryParse(tempo2Str, out TimeSpan tempo2))
                    {
                        foreach (var rodada in rodadas)
                        {
                            if (rodada is not null)
                            {
                                var resultados = rodada.ResultadoDestaRodada;
                                if (resultados is not null)
                                {
                                    if (tempo1 < tempo2)
                                    {
                                        resultados[0].Posição = 1;
                                        resultados[1].Posição = 2;
                                    }
                                    else
                                    {
                                        resultados[0].Posição = 2;
                                        resultados[1].Posição = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal string CntTotalGanhadoresPulesToLists()
        {
            string mensagem = String.Empty;
            int cntVencedoresList1 = this.ResultadoList
                .Count(res => res.Posição == 1);
            int cntVencedorList2 = this.Rodadas.SelectMany(r => r.ResultadoDestaRodada)
                .Count(res => res.Posição == 1);
            mensagem = $"Rodada 1: {cntVencedoresList1} Ganhador , Demais Rodadas {cntVencedorList2} Ganhador(es)";
            return mensagem;

        }

        internal bool RodadasInResultadosIsNull()
        {
            bool isNull = false;
            if (Rodadas is not null && Rodadas.Count > 0)
            {
                foreach (var rodada in Rodadas)
                {
                    if (rodada is not null)
                    {
                        if (rodada.ResultadoDestaRodada is null)
                            isNull = true;
                    }
                }
            }
            else
            {
                isNull = true;
            }
            return isNull;
        }
        /// <summary>
        /// Returns the maximum Nrodadas value from the Rodadas collection.
        /// </summary>
        /// <returns>The highest Nrodadas value, or 0 if none.</returns>
        internal decimal GetNMaiorRodada()
        {
            int maior = 0;
            if(Rodadas is not null)
            {
                maior = Rodadas.Max(rod => rod.Nrodadas);
            }
            return maior;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultado"></param>
        internal void RemoveResultado(Resultado resultado)
        {
            if(Rodadas is not null)
            {
                foreach(var rodada in Rodadas)
                {
                    if(rodada is not null && rodada.ResultadoDestaRodada is not null)
                    {
                        rodada.ResultadoDestaRodada.Remove(resultado);
                    }
                }
            }
        }
        /// <summary>
        /// Remove resultado da rodada
        /// </summary>
        /// <param name="resultado"></param>
        internal void RemoveResultadoRodada(Resultado resultado)
        {
            if(Rodadas?.Count > 0)
            {
               foreach(var rodada in Rodadas)
                {
                    if(rodada is not null && rodada.ResultadoDestaRodada?.Count > 0)
                    {
                        rodada.ResultadoDestaRodada.Remove(resultado);
                    }
                }
            }
        }
        /// <summary>
        /// Adiciona apartir do index o resultado na rodada
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="index"></param>
        internal void AddNewResultadoInRodada(Resultado resultado, int index)
        {
            if(Rodadas is not null && Rodadas.Count > 0)
            {
                if(index < GetNMaiorRodada())
                {
                    Rodadas[index].ResultadoDestaRodada?.Add(resultado);
                }
            }
        }
    }
}
