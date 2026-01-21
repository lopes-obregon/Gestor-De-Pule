using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using System.Collections;

namespace Gestor_De_Pule.src.Controllers
{
    internal class PuleController
    {
        

      

        public List<Apostador>? Apostadors { get; private set; }
        public List<Animal>? Animals { get; private set; }
        public List<Pule>? Pules { get; private set; }
        static public Pule? Pule { get; private set; }
        public Pule? PuleLocal { get; private set; } = null;
        public List<Disputa>? Disputas { get; private set; } = null;
        public List<Apostador> ApostadorsLocal { get; private set; }
        public List<Animal> AnimalsLocal { get; private set; }
        public List<Disputa>? DisputasLocal { get; private set; }
        private Caixa? Caixa { get; set; } = null;
        public List<Pule> PulesApostador { get; internal set; }

        //Controllers
        private AnimalController _animalController = new();
        private ApostadorController _apostadorController = new();
        //------------------------------------------------------------//
        //Repository
        private DisputaRepository _disputaRepository = new DisputaRepository();
        private PuleRepository _puleRepository = new PuleRepository();
        private CaixaRepository _caixaRepository = new();

        /// <summary>
        /// Constructor com pule selecionado pela ui
        /// </summary>
        /// <param name="puleSelecionadoUi"></param>
        public PuleController(object puleSelecionadoUi)
        {
            PuleLocal = puleSelecionadoUi as Pule;
            Caixa = (Caixa?)_caixaRepository.GetCaixa();
        }
        public PuleController() { }
        internal  List<Animal>? AttComboBoxAnimais(object disputaSelecionadaUi)
        {
            Disputa? disputaSelecionada = disputaSelecionadaUi as Disputa;
            List<Animal>? animaisSelecionados = null;

            if(disputaSelecionada is not null)
            {
                animaisSelecionados = new List<Animal>();
                foreach(var resultado in disputaSelecionada.ResultadoList)
                {
                    if(resultado is not null && resultado.Animal is not null)
                    {
                        animaisSelecionados.Add(resultado.Animal);
                    }
                }
            }
            return animaisSelecionados;
        }

        internal  string CadastrarPule(object? apostadorSelecionadoUi, object? pagamentoUi, ListBox.ObjectCollection animaisSelecionadosUi, float valor, int númeroDoPule, object? disputaSelecionadoUi)
        {
           Apostador? apostadorUi =apostadorSelecionadoUi as Apostador;
            Apostador? apostadorSelecionado = null;
            StatusPagamento pagamento;
            List<Animal>? animaisUi = animaisSelecionadosUi.Cast<Animal>().ToList();
            Pule? pule =null;
            List<Animal>? animais = new List<Animal>();
            Disputa? disputaUi = disputaSelecionadoUi as Disputa;
            Disputa? disputaSelecionado = null;
            bool sucess = false;
            if (pagamentoUi is StatusPagamento status)
                pagamento = status;
            else
                pagamento = StatusPagamento.Pendente;

            if (apostadorUi is not null)
                apostadorSelecionado = Apostadors.Find(a => a.Id == apostadorUi.Id);
            //pule = new Pule(apostadorSelecionado, pagamento, animais, valor);
            //remap dos animais
            if (disputaUi is not null && Disputas is not null)
                disputaSelecionado = Disputas.Find(d => d.Id == disputaUi.Id);
            var animalIds = animaisUi.Select(a => a.Id).ToHashSet();   
            if(animaisUi is not null && animaisUi.Count > 0)
                animais = Animals.Where(a => animalIds.Contains(a.Id)).ToList();
            pule = new Pule(null, pagamento, null, valor, númeroDoPule);
            //sucess = Pule.Save(pule);
            sucess = _puleRepository.SavePule(pule);
            //sucess = pule.Associete(apostadorSelecionado, animais, disputaSelecionado);
            sucess = _puleRepository.Associete(apostadorSelecionado, animais, disputaSelecionado , pule);

            if (sucess)
                return "Pule Cadastrado Com Sucesso!";
            else return "Algo Deu Errado No Cadastro Do Pule!";

        }

        internal static List<StatusPagamento>? GetStatusPagamento()
        {
            return Enum.GetValues(typeof(StatusPagamento)).Cast<StatusPagamento>().ToList();
        }

        internal  void LoadAnimais()
        {
            //AnimalController.LoadAnimais();
            _animalController.LoadAnimais();
            Animals = _animalController.Animals.ToList();
            //Animals = AnimalController.Animals.ToList();
        }

        internal  void LoadLists()
        {
            //Disputas = Disputa.GetDisputas();
            Disputas = _disputaRepository.GetDisputas();
            //AnimalController.LoadAnimais();
            _animalController.LoadAnimais();
            //ApostadorController.LoadApostadores();
            _apostadorController.LoadApostadores();
            //Apostadors = ApostadorController.Apostadors.ToList();
            Apostadors = _apostadorController.Apostadors.ToList();
            //Animals = AnimalController.Animals.ToList();
            Animals = _animalController.Animals.ToList();
            
        }
        
        
        internal static void LoadPule(object puleSelecionadoUi)
        {
            Pule? puleSelecionado = puleSelecionadoUi as Pule;
            if(puleSelecionado is not null)
            {
                Pule = puleSelecionado;
            }
        }
        /// <summary>
        /// Loads the list of Pule objects from the repository into the Pules collection.
        /// </summary>
        internal  void LoadPules()
        {
            Pules = new List<Pule>();
            Pules = _puleRepository.ReadPules().ToList();
        }

        internal  string RemovePule(object puleSelecionadoUi)
        {
            bool sucess = false;
            Pule? puleSelecionado = puleSelecionadoUi as Pule;
            if(puleSelecionado is not null)
            {
               // sucess = Pule.Remove(puleSelecionado);
                sucess = _puleRepository.Remove(puleSelecionado);
                if (sucess) return "Pule Removido Com Sucesso!";
                else return "Erro ao Remover O Pule!";
            }
                return "";
        }

        

        internal  string UpdateData(object? apostadorUi, object? pagamentoUi, ListBox.ObjectCollection animaisUi)
        {
            Apostador? apostadorSelecionado = apostadorUi as Apostador;
            StatusPagamento pagamento = StatusPagamento.Pendente;
            List<Animal>? animais = animaisUi.Cast<Animal>().ToList();
            bool sucess = false;
            if(pagamentoUi is StatusPagamento status)
            {
                pagamento = status;
            }
            if (PuleLocal is not null)
            {
                //não pode mudar o apostador apenas status de pagamento e 
                //animais apostados
                //se for do pule igual a pendente ele recebe mesmo se atualizou
                if (PuleLocal.StatusPagamento.Equals(StatusPagamento.Pendente))
                {
                    PuleLocal.StatusPagamento = pagamento;
                }
                //verificar se são os mesmo animais
                bool isEqual = PuleLocal.Animais
                    .Select(a => a.Id)
                    .OrderBy(a => a)
                    .SequenceEqual(animais.Select(a => a.Id).OrderBy(x => x));
                
              //sucess =  Pule.Update(PuleLocal, animais, isEqual);
               sucess =  _puleRepository.Update(PuleLocal, animais, isEqual);
                if(sucess)
                {
                   if(Caixa is not null)
                    {
                        Caixa.TotalEmCaixa += (decimal)PuleLocal.Valor;
                        sucess = Caixa.Update();
                    }
                    else
                        sucess = false;

                }
            }
            else
                sucess = false;
            if (sucess)
                return "Pule Atualizado Com Sucesso!";
            else
                return "Erro ao Atualizar o Pule!";
        }

        internal object? FindPule(Pule pule)
        {
            if (Pules is not null && Pules.Count > 0)
                return Pules.Find(p => p.Id == pule.Id);
            return null;
        }

        internal static Pule? ToPule(object? v)
        {
            return (Pule?)v;
        }
        internal  List<int> GetAttrNumPule()
        {
            if (Pules is not null && Pules.Count > 0)
            {
                return Pules.Select(p => p.Número)
                    .Distinct()
                    .ToList();
            }
            return new List<int>();
        }

        internal List<Pule> PulesSelecionados(object puleSelecionadoUi)
        {
            int? puleSelecionado = puleSelecionadoUi as int?;
            if (puleSelecionado != null)
            {
                if (Pules is not null && Pules.Count > 0)
                {
                    return Pules.Select(p => p)
                        .Where(p => p.Número == puleSelecionado).ToList();
                }
            }
            return new List<Pule>();
        }
    }
}
