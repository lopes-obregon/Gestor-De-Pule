using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Models
{
    class Disputa
    {
        private string nomeDisputa;
        private DateTime dateTime;

        public int Id { get; set; }
        public DateTime DataEHora { get; set; } = new DateTime();
        public string Nome { get; set; } = String.Empty;
        public List<Resultado>? ResultadoList { get; set; }
        public List<Pule>? Pules { get; set; }
        public Caixa? Caixa { get; set; }
        public Disputa() { }
        public Disputa(string nome,  DateTime dataEHora,Resultado resultados)
        {
            Id = 0;
            DataEHora = dataEHora;
            Nome = nome;
            ResultadoList.Add(resultados);
        }

        public Disputa(string nome, DateTime dateTime)
        {
            this.Nome = nome;
            this.dateTime = dateTime;
        }

        /// <summary>
        /// Verifica se essa disputa já foi criada!
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <returns></returns>
        internal  static Disputa? isCreate(string nomeDisputa)
        {
            Disputa? disputaDb;
            using DataBase db = new DataBase();
            disputaDb =  db.Disputas.FirstOrDefault(dis=> dis.Nome == nomeDisputa);
            if (disputaDb == null)
                return null;
            else
                return disputaDb;
            
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
                if(resultado != null)
                {
                    if(resultado.Disputa.Id == Id) contadorAnimais++;
                }
            }
            return contadorAnimais;
        }
        /// <summary>
        /// Updates the tempo for a specific animal in the current dispute.
        /// </summary>
        /// <remarks>This method updates the tempo for the specified animal in the current dispute if the
        /// animal's name matches and the result is found in the database. Changes are saved to the database upon
        /// successful update.</remarks>
        /// <param name="animalNome">The name of the animal whose tempo is to be updated. Must match the name of an existing animal in the
        /// dispute.</param>
        /// <param name="tempoUi">The new tempo value to set for the animal. Represents the time duration to be updated.</param>
        /// <param name="resUi">The result object containing the animal and dispute information. Must not be null and should correspond to
        /// an existing result in the database.</param>
        internal void UpdateTempo(object animalNome, TimeSpan tempoUi, Resultado resUi)
        {
           // string? animalNomeStr = animalNome.ToString();
            using DataBase db = new DataBase();
            try
            {
                var disputaDb = db.Disputas
                    .Include(dis => dis.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .FirstOrDefault(dis=> dis.Id == Id);
                if(disputaDb is not null)
                {
                    var resultadoDb = db.Resultados
                        .Include(res => res.Animal)
                        .Include(res => res.Disputa)
                        .FirstOrDefault(res => res.Id == resUi.Id && res.Disputa.Id == Id);
                    if (resultadoDb is not null)
                    {
                        if (resultadoDb.Animal.isAnimalMesmoNome(animalNome))
                        {
                            resultadoDb.Tempo = tempoUi;
                            db.Resultados.Update(resultadoDb);
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }

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
           resultadoList = resultadoList.OrderBy(res=> res.Tempo).ToList();
            byte pos = 0;
            foreach (var resultado in resultadoList) { 
                if(resultado is not null)
                {
                    resultado.Posição = ++pos;
                }
            }
            ResultadoList = resultadoList;
        }

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

        internal static List<Disputa>? GetDisputas()
        {
           using DataBase db = new DataBase();
            try
            {
                var disputasDb = db.Disputas
                    .Include(d=> d.ResultadoList)
                    .ThenInclude(r=> r.Animal)
                    .Where(d=> !String.IsNullOrEmpty(d.Nome))
                    .ToList();
                if(disputasDb is null || disputasDb.Count == 0)
                {
                    return null;
                }
                else
                {
                    return disputasDb;
                }
            }
            catch { return null; }
        }

        internal bool save()
        {
            using DataBase db = new DataBase();
            try
            {
                if(this is not null)
                {
                    var disputaDb = db.Disputas.FirstOrDefault(dis => dis.Id == this.Id);
                    if(disputaDb is null)
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

        internal static Disputa? Reload(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                    var disputaDb = db.Disputas.Include(dis => dis.Pules)
                        .ThenInclude(pu => pu.Animais).Include(dis => dis.Caixa).Include(dis => dis.ResultadoList)
                        .ThenInclude(res => res.Animal).Include(dis => dis.Pules).ThenInclude(pu => pu.Apostador).FirstOrDefault(dis => dis.Id == disputa.Id);
                    if(disputaDb != null)
                    {
                        return disputaDb;

                    }
                }
                return null;
            }catch { return null; }
        }

        internal string GetNomeAnimalVencedor()
        {
            if(ResultadoList is not null &&  ResultadoList.Count > 0)
            {
                foreach(var resultado in ResultadoList)
                {
                    if(resultado is not null)
                    {
                        if(resultado.Posição == 1)
                        {
                            if(resultado.Animal != null && !String.IsNullOrEmpty(resultado.Animal.Nome))
                            {
                                return resultado.Animal.Nome;
                            } 
                        }
                    }
                }
            }
            return "Animal Não encontrado!";
        }

        internal int CntTotalGanhadoresPules()
        {
            //contar e retornar a quantidade de ganhadores de pules ganhadores.
            int idAnimalVencedor = GetIdAnimalVencedor();
            int cntGanhadores = 0;
            if(idAnimalVencedor > -1)
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

        internal string PagamentoPorPule()
        {
            int quantidadeDePulesVencedores = CntTotalGanhadoresPules();
            float totalArrecadado = 0.0f;
            float valorTaxa = 0.0f;
            float prêmioLiquido = 0.0f; 
            if (Pules is not null && Pules.Count > 0)
            {
                foreach (var pule in Pules)
                {
                    if (pule is not null)
                        totalArrecadado += pule.Valor;
                }
                if (Caixa is not null)
                {
                    valorTaxa = totalArrecadado * (float)Caixa.Taxa;
                    prêmioLiquido = totalArrecadado - valorTaxa;
                    return (prêmioLiquido / quantidadeDePulesVencedores).ToString("C");

                }
                else
                    return "Erro ao calcular!";

            }
            return "Algum Erro encontrado!";

        }
    }
}
