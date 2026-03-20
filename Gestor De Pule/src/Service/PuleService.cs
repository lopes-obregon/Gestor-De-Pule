using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System.Security.Cryptography.X509Certificates;

namespace Gestor_De_Pule.src.Service
{
    class PuleService
    {
        /// <summary>
        /// Repository
        /// </summary>
        private readonly PuleRepository _repository;
        /// <summary>
        /// cache com os pules
        /// </summary>
        public List<Pule> Pules;
        public PuleService(object context)
        {
            _repository = new PuleRepository(context);
        }

        internal static List<Pule>? ObterPulesSelecionados(object puleSelecionadosUi)
        {
            var pules = PuleRepository.BuscarPorIds(puleSelecionadosUi);
            if (pules == null) return null;
            else
                return pules;
        }
        /// <summary>
        /// Get pule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Pule or null</returns>
        internal Pule? GetById(int id)
        {
            return _repository.GetPuleById(id);
        }

        internal bool Remove(int idPule, List<Pule>? pules, List<Animal> animals)
        {
            var pule = pules?.FirstOrDefault(p => p.Id == idPule) ?? _repository.GetPuleById(idPule);
            if (pule == null) return false;

            //remover pule do animal
            RemovePuleAnimal(pule, pules, animals);
            //remove relação apostador
            pule.Apostador = null;
            //pule.ApostadorId = null;
            pule.Disputa = null;
            pule.Rodada = null;
            if (pule.DisputaId != null || pule.RodadaId != null || pule.ApostadorId != null)
                pule.DisputaId = pule.RodadaId = pule.ApostadorId = null;

            return _repository.Remove(pule);
        }
        /// <summary>
        /// Remove pule do animal e animal do pule
        /// </summary>
        /// <param name="pule"></param>
        private void RemovePuleAnimal(Pule pule, List<Pule>? pules, List<Animal> animals)
        {
            //var animais = _repository.GetAnimaisToPule(pule.Id);
            var animais = animals.Where(an => an.Pules.Any(p => p.Id == pule.Id)).ToList();
            if (animais == null) return;
            foreach (var animal in animais)
            {
                if (animal is not null)
                {
                    if (animal.Pules is null)
                        animal.Pules = _repository.GetPulesToAnimal(animal.Id);
                    if (pule.Animais is null)
                        pule.Animais = _repository.GetAnimaisToPule(pule.Id);
                    if (animal.Pules.Count > 0)
                    {
                        animal.Pules.Remove(pule);
                        pule.Animais.Remove(animal);
                        if (animais.Count == 0)
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Update pule 
        /// </summary>
        /// <param name="animais"></param>
        /// <param name="pagamentoUi"></param>
        /// <param name="valor"></param>
        /// <param name="númeroDoPule"></param>
        /// <param name="rodadaId"></param>
        /// <param name="pule"></param>
        /// <returns>Menssage sucess or error</returns>
        internal string Update(List<Animal> animais, object? pagamentoUi, decimal valor, int númeroDoPule, int rodadaId, Pule? pule)
        {
            // StatusPagamento pagamento;
            string mensagem = String.Empty;
            bool sucss = false;
            if (pule is not null)
            {
                if (pagamentoUi != null)
                {
                    if (pagamentoUi is StatusPagamento)
                    {
                        var pagamento = (StatusPagamento)pagamentoUi;
                        if (pule.StatusPagamento != pagamento)
                        {
                            pule.StatusPagamento = pagamento;
                        }
                    }
                }

                RemoveAnimalDiferente(animais, pule);
                if (pule.Valor != valor)
                    pule.Valor = valor;
                if (pule.Número != númeroDoPule)
                    pule.Número = númeroDoPule;

                AddAnimalToPule(animais, pule);
                sucss = _repository.Save();
                if (sucss) mensagem = $"Sucesso ao atualiza o Pule Nº {pule.Número}";
                else mensagem = $"Erro ao atualiza o Pule Nº {pule.Número}";
            }
            return mensagem;
        }
        /// <summary>
        /// Add animal in pule
        /// </summary>
        /// <param name="animais"></param>
        /// <param name="pule"></param>
        private void AddAnimalToPule(List<Animal> animais, Pule pule)
        {
            foreach (Animal animal in animais)
            {
                if (animal != null)
                {
                    if (animal.Pules is null)
                        animal.Pules = new();
                    animal.Pules.Add(pule);
                    if (pule.Animais is null)
                        pule.Animais = new List<Animal>();
                    pule.Animais.Add(animal);
                }
            }
        }

        /// <summary>
        /// Remove o animal do pule atual
        /// </summary>
        /// <param name="animais"></param>
        /// <param name="pule"></param>
        private void RemoveAnimalDiferente(List<Animal> animais, Pule? pule)
        {
            if (pule is not null)
            {
                if (pule.Animais is null)
                    pule.Animais = _repository.GetAnimaisToPule(pule.Id);
                foreach (var animal in pule.Animais)
                {
                    if (animal is not null)
                    {
                        //remove a relação de animal com pule
                        animal.Pules.Remove(pule);
                        //remove a relação de pule com o animal
                        pule.Animais.Remove(animal);
                    }
                    if (pule.Animais.Count == 0)
                        break;
                }
            }
        }
        /// <summary>
        /// Seta no Cahce os pules relacionados ao id da disputa que tem relação com o id da disputa
        /// </summary>
        /// <param name="idDisputa"></param>>
        internal List<Pule>? GetPulesByIdWithIdDisputs(int idDisputa)
        {
            List<Pule>? pules = null;
            if (Pules is not null && Pules.Count > 0)
            {
                pules = Pules.Where(p => p.DisputaId == idDisputa).ToList();
            }
            else
            {
                pules = _repository.GetPulesWithIdDisputs(idDisputa);
                Pules = pules ?? new List<Pule>();
            }
            return pules;
        }
        /// <summary>
        /// Carrega os pules em memória
        /// </summary>
        internal void LoadPules()
        {
            Pules = new List<Pule>();
            Pules = _repository.ReadPules().ToList();
        }



        /// <summary>
        /// Verifica in memory pulus existes with relação id
        /// </summary>
        /// <param name="idDisputa"> identificador unico da disputa</param>
        /// <returns>List with pules search</returns>
        internal List<Pule>? LoadPulesWithAnimals(int idDisputa)
        {
            List<Pule> pules;
            if (Pules is not null)
            {
                pules = Pules.Where(p => p.DisputaId == idDisputa).ToList();
            }
            else
            {
                pules = _repository.GetPulesWithIdDisputsAndAnimalInclude(idDisputa);
            }
            return pules;
        }
        /// <summary>
        /// Set cache pules para consultas futuras
        /// </summary>
        /// <param name="id"> identificador da disputa que deseja encontrar os pules associados</param>
        internal void LoadPulesWithDisputaById(int id)
        {
            var pules = _repository.GetPulesWithIdDisputs(id);
            if (pules is null)
                Pules = new List<Pule>();
            else
                Pules = pules;
        }
        /// <summary>
        /// A new Pule with data pule info
        /// </summary>
        /// <param name="pule"> To copy data</param>
        /// <returns> new pule</returns>
        internal Pule? NovoPule(Pule pule)
        {
            Pule novoPule = pule.Clone();
            _repository.AddContext(novoPule);
            if(Pules is not null)
                Pules.Add(novoPule);
            return novoPule;
            
        }
    }
}
