using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
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

            Apostador = _repository.Load(apostador);
            //Apostador = Apostador.Load(apostador);
            
        }

        internal  void LoadApostadores()
        {
            Apostadors = Apostador.ReadApostadores();
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
    }
}
