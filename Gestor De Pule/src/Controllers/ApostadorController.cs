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
        private ApostadorRepository _repository = new ApostadorRepository();
        private PuleController _puleController = new PuleController();
        public PuleController PuleController {  get { return _puleController; } }

        internal  string AtualizarApostador(string nome, string contato)
        {
            bool sucess = false;
            if (Apostador is not null)
                if (!String.IsNullOrEmpty(contato) && contato != Apostador.Contato)
                {
                    Apostador.Contato = contato;
                    //sucess = Apostador.Update(Apostador);
                    sucess = _repository.Update(Apostador);
                    if (sucess) return "Contato Atualizado Com Sucesso!";
                    else return "Erro ao Atualizar o Contato!";
                }
            return "";
        }

       

        internal  void LoadApostador(object apostadorSelecionadoUi)
        {
            Apostador? apostador = apostadorSelecionadoUi as Apostador;
            if (Apostadors is null || Apostadors.Count == 0)
                Apostador = _repository.Load(apostador);
            //Apostador = Apostador.Load(apostador);
            else
                if(apostador is not null)
                    Apostador = Apostadors.Find(ap => ap.Id == apostador.Id);

            
        }

        internal  void LoadApostadores()
        {
            Apostadors = Apostador.ReadApostadores().ToList();
        }

        internal  string RemoveApostador(object apostadorSelecionadoUi)
        {
            Apostador? apostador = apostadorSelecionadoUi as Apostador;
            bool sucess = false;
            if(apostador is not null)
                sucess = _repository.Remove(apostador);
                //sucess = Apostador.Remove(apostador);
            if (sucess) return $"Apostador {apostador?.Nome ?? ""} Removido com Sucesso";
            else return $"Erro ao Remover o Apostador {apostador?.Nome} !";
        }

        internal  string SaveApostador(string nome, string contato)
        {
            bool sucss = false;
            Apostador? apostador = new Apostador(nome, contato);
            sucss = _repository.Save(apostador);
            //sucss = Apostador.Save(apostador);
            if (sucss)  return "Apostador Cadastrado com Sucesso!";
            else return "Falha no Cadastro do Apostador!";
        }

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

    }
}
