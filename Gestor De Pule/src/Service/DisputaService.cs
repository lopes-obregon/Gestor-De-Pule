using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class DisputaService
    {
       
        private DisputaRepository _disputaRepository;

        public DisputaService(DataBase data)
        {
            _disputaRepository = new DisputaRepository(data);
        }
        internal string AtualizarDados(Disputa? disputa,string nomeDisputa, DateTime? date, List<int> idsAnimais, int quantidadeRodadas)
        {
            
            int nRodada = disputa?.Rodadas?.Count ?? 0;
            bool sucss = false;
            string mensagem = String.Empty;
            int i = 0;
            if (disputa != null)
            {
                if (!String.IsNullOrEmpty(nomeDisputa))
                    if (!String.Equals(nomeDisputa, disputa.Nome))//se são diferentes
                        disputa.Nome = nomeDisputa;
                if (date != null)
                    if (!disputa.DataEHora.Equals(date))
                        disputa.DataEHora = (DateTime)date; // se data diferente troca
                //ver se mudou o animal
                if(disputa.MudouAnimalRodada(idsAnimais))
                    RemoveAnimais(disputa, idsAnimais);
                if (disputa.GetNMaiorRodada() < quantidadeRodadas)//considerando que a disputa a inda ta com as rodadas antigas;
                {
                    //devo criar as rodadas
                    while (nRodada < quantidadeRodadas)
                    {
                        Rodada novaRodada = new Rodada();
                        _disputaRepository?.AddContext(novaRodada);
                        novaRodada.DisputaId = disputa.Id;
                        novaRodada.Nrodadas = (byte)(1 + nRodada);
                        foreach (int animalId in idsAnimais)
                        {
                            Resultado novoResultado = new Resultado();
                            _disputaRepository?.AddContext(novoResultado);

                            novoResultado.DisputaId = disputa.Id;
                            novoResultado.AnimalId = animalId;
                            if (novaRodada.ResultadoDestaRodada is null)
                                novaRodada.ResultadoDestaRodada = new();
                            if (!novaRodada.ResultadoDestaRodada.Contains(novoResultado))
                                novaRodada.ResultadoDestaRodada.Add(novoResultado);
                        }
                        if (disputa.Rodadas is null)
                            disputa.Rodadas = new();
                        if (!disputa.Rodadas.Contains(novaRodada))
                            disputa.Rodadas.Add(novaRodada);
                        nRodada++;


                    }
                }
                else if (disputa.GetNMaiorRodada() > quantidadeRodadas)

                {
                    //se diminui a quantidade de rodadas
                    while (disputa.Rodadas?.Count > quantidadeRodadas)
                    {
                        var rodada = disputa.Rodadas.Last();
                        if (rodada is not null)
                        {
                            rodada.Disputa = null;
                            if (rodada.ResultadoDestaRodada is not null)
                            {
                                foreach (var resultado in rodada.ResultadoDestaRodada)
                                {
                                    if (resultado is not null)
                                    {
                                        resultado.Disputa = null;
                                    }
                                }
                            }
                            disputa.Rodadas.Remove(rodada);
                        }
                    }
                }
                else
                {
                    //quero o que não contem na lista da disputa
                    
                    while(i < disputa.Rodadas?.Count)
                    {
                        var rodada = disputa.Rodadas[i];
                        if (rodada is not null)
                        {
                            //quero o que não contem na lista da disputa
                            //retornar os animais cujo o id não aparece em resultados
                            if (rodada.ResultadoDestaRodada is not null)
                            {
                                var animais = idsAnimais.Where(id => !rodada.ResultadoDestaRodada.Any(res => res.AnimalId == id)).ToList();
                                //criar novo resultado
                                foreach (var animal in animais)
                                {
                                    var resultado = new Resultado();
                                    if (resultado is not null)
                                    {
                                        _disputaRepository.AddContext(resultado);
                                        resultado.AnimalId = animal;
                                        resultado.Disputa = disputa;
                                        rodada.ResultadoDestaRodada?.Add(resultado);


                                    }

                                }
                            }
                        }
                        i++;
                    }
                }
                    sucss = _disputaRepository?.Save() ?? false;
            }
            if (sucss)
                mensagem = "Disputa Atualizada com sucesso!";
            else
                mensagem = "Erro Ao atualizar a disputa!";
            return mensagem;
        }
        /// <summary>
        /// Lê do banco a disputa
        /// </summary>
        /// <param name="idDisputa"></param>
        /// <returns>Disputa ou null se não encontrar a disputa</returns>
        internal Disputa? GetById(int idDisputa)
        {
            var disputa = _disputaRepository.GetById(idDisputa);
            return disputa;
        }

        /// <summary>
        /// Veja qual animal foi removido e ajusta a relação
        /// </summary>
        /// <param name="disputa"></param>
        /// <param name="idsAnimais"></param>
        private void RemoveAnimais(Disputa disputa, List<int> idsAnimais)
        {
            if(disputa.Rodadas != null)
            {
                foreach(var rodada in disputa.Rodadas)
                {
                    if(rodada is not null)
                    {
                        var resultados = rodada.ResultadoDestaRodada.Where(res=> !idsAnimais.Contains(res.AnimalId)).ToList();
                        if(resultados is not null)
                        {
                            foreach(var resultado in resultados)
                            {
                                if(resultado is not null)
                                {
                                    resultado.Disputa = null;
                                    rodada.ResultadoDestaRodada.Remove(resultado);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
