using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Controllers
{
    internal class ApostadorController
    {
        public Apostador? Apostador { get; private set; }
        public List<Apostador> Apostadors { get; private set; }
       //controllers
        private Repository _repository;
        private ApostadorRepository _apostadorRepository;
        private PuleController _puleController;
        public PuleController PuleController {  get { return _puleController; } }
        /// <summary>
        /// Initializes a new instance of the ApostadorController class and sets up its dependencies.
        /// </summary>
        public ApostadorController()
        {
            _repository = new Repository();
            _apostadorRepository = new ApostadorRepository(_repository.GetDataBase());
            _puleController = new PuleController(_repository.GetDataBase());
            Apostadors = new List<Apostador>();

        }

        public ApostadorController(object apostadorSelecionado)
        {
            _repository = new Repository();
            _apostadorRepository = new ApostadorRepository(_repository.GetDataBase());
            _puleController = new PuleController(_repository.GetDataBase());
            Apostadors = new List<Apostador>();
            LoadApostador(apostadorSelecionado);
        }

        /// <summary>
        /// Updates the contact information of the current bettor if the provided contact is different and not empty.
        /// </summary>
        /// <param name="nome">The name of the bettor.</param>
        /// <param name="contato">The new contact information to update.</param>
        /// <returns>A message indicating whether the contact was successfully updated, an error occurred, or an empty string if
        /// no update was performed.</returns>
        internal  string AtualizarApostador(string nome, string contato)
        {
            bool sucess = false;
            if (Apostador is not null)
                if (!String.IsNullOrEmpty(contato) && contato != Apostador.Contato)
                {
                    Apostador.Contato = contato;
                    //sucess = Apostador.Update(Apostador);
                    sucess = _apostadorRepository.Update(Apostador);
                    if (sucess) return "Contato Atualizado Com Sucesso!";
                    else return "Erro ao Atualizar o Contato!";
                }
            return "";
        }

       /// <summary>
       /// Loads the selected Apostador from the repository or from the existing list based on the provided UI object.
       /// </summary>
       /// <param name="apostadorSelecionadoUi">The UI object representing the selected Apostador.</param>

        internal  void LoadApostador(object apostadorSelecionadoUi)
        {
            Apostador? apostador = apostadorSelecionadoUi as Apostador;
            if (Apostadors is null || Apostadors.Count == 0)
                Apostador = _apostadorRepository.Load(apostador);
            //Apostador = Apostador.Load(apostador);
            else
                if(apostador is not null)
                    Apostador = Apostadors.Find(ap => ap.Id == apostador.Id);

            
        }
        /// <summary>
        /// Loads the list of apostadores from the repository if it is null or empty.
        /// </summary>
        internal  void LoadApostadores()
        {
            if (Apostadors is null || Apostadors.Count == 0)
                Apostadors = _apostadorRepository.ReadApostadores();
            //Apostadors = Apostador.ReadApostadores().ToList();
        }
        /// <summary>
        /// Removes the specified Apostador and returns a status message indicating success or failure.
        /// </summary>
        /// <param name="apostadorSelecionadoUi">The selected Apostador object to be removed.</param>
        /// <returns>A message indicating whether the Apostador was successfully removed or if an error occurred.</returns>
        internal  string RemoveApostador(object apostadorSelecionadoUi)
        {
            Apostador? apostador = apostadorSelecionadoUi as Apostador;
            string mensagem = String.Empty;
            bool sucess = false;
            if(apostador is not null)
                sucess = _apostadorRepository.Remove(apostador);
            //sucess = Apostador.Remove(apostador);
            if (sucess)
            {
                mensagem = $"Apostador {apostador?.Nome ?? ""} Removido com Sucesso";
                Apostadors.Remove(apostador);
            }
            else mensagem =  $"Erro ao Remover o Apostador {apostador?.Nome} !";
            return mensagem ;
        }
        /// <summary>
        /// Registers a new Apostador with the specified name and contact information.
        /// </summary>
        /// <param name="nome">The name of the Apostador to register.</param>
        /// <param name="contato">The contact information of the Apostador.</param>
        /// <returns>A message indicating whether the registration was successful or failed.</returns>
        internal  string SaveApostador(string nome, string contato)
        {
            bool sucss = false;
            Apostador? apostador = new Apostador(nome, contato);
            sucss = _apostadorRepository.Save(apostador);
            //sucss = Apostador.Save(apostador);
            if (sucss)  return "Apostador Cadastrado com Sucesso!";
            else return "Falha no Cadastro do Apostador!";
        }
        /// <summary>
        /// Loads apostadores and pules lists.
        /// </summary>
        internal void LoadLists()
        {
            LoadApostadores();
           
            _puleController.LoadPules();
           
        }

        internal void LoadPulesDoApostador()
        {
            var pules = _puleController.Pules;
            if (pules is not null && pules.Count > 0)
            {
                if (Apostador is not null)
                    _puleController.PulesApostador = pules.Where(p => p.Apostador != null && p.Apostador.Id == Apostador.Id).ToList();
            }
        }

        internal List<Pule> LoadPulesDoApostador(Apostador apostador)
        {
            var pules = Apostador?.Pules;
            if(pules is not null && pules.Count == 0)
                pules = _apostadorRepository.GetPules(apostador.Id);
            if (pules is null)
                return new List<Pule>();
            else
                return pules;
        }
        /// <summary>
        /// Releases resources used by the underlying database connection.
        /// </summary>
        internal void Dispose()
        {
            _repository.GetDataBase().Dispose();
        }

        internal string UpdateApostador(string nome, string contato)
        {
            string mensagem = String.Empty;
            //vericiar o rastreio de apostador
            Apostador = _apostadorRepository.isTrack(Apostador);
            if(Apostador is not null)
            {
                if(Apostador.Nome != nome)
                    Apostador.Nome = nome;
                if(Apostador.Contato != contato)
                    Apostador.Contato = contato;
                var sucess = _apostadorRepository.Save();
                if(sucess != null && sucess == true)
                {
                    mensagem = "Sucesso ao Salvar o apostador:" + Apostador.Nome;
                }
                else
                {
                    mensagem = "Erro ao Salvar o apostador:" + nome;
                }
              

               
            }
            else
            {
                mensagem = "Erro ao Atualizar o apostador:" + nome;
            }
            return mensagem;
        }

        internal Apostador? FindApostador(Apostador apostadorUi)
        {
            Apostador? apostador = null;
            if (Apostadors is null || Apostadors.Count == 0)
                Apostadors = _apostadorRepository.GetApostadores();
           
            apostador = Apostadors?.Find(a=> a.Id == apostadorUi.Id);
            return apostador;
        }

        internal Apostador? IsTrack(Apostador apostadorUi)
        {
            Apostador? apostdor = null;
            apostdor = _apostadorRepository.isTrack(apostadorUi);
            return apostdor;
        }
        internal Apostador? IsTrack(object? apostadorUi)
        {
            Apostador? apostadorTrack = apostadorUi as Apostador;
            Apostador? apostdor = null;

            apostdor = _apostadorRepository.isTrack(apostadorTrack);
            return apostdor;
        }
    }
}
