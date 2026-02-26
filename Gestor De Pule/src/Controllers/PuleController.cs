using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Gestor_De_Pule.src.Controllers
{
    internal class PuleController
    {
        public List<Pule>? Pules { get; private set; }
        public Pule? Pule { get; private set; }
       
        public List<Pule> PulesApostador { get; internal set; }

        //Controllers
        public AnimalController AnimalController { get; private set; }
        public ApostadorController ApostadorController { get; private set; }
        public DisputaController DisputaController { get; private set; }
        private CaixaController _caixaController;
        //------------------------------------------------------------//
        //Repository
        private PuleRepository _puleRepository;
        private Repository _repository;
        /// <summary>
        /// Return context database
        /// </summary>
        public Repository Repository { get { return _repository; } }
        /*private DisputaRepository _disputaRepository = new DisputaRepository();
        private CaixaRepository _caixaRepository = new();*/

        /// <summary>
        /// Constructor com pule selecionado pela ui
        /// </summary>
        /// <param name="puleSelecionadoUi"></param>
        public PuleController(object context)
        {
            _puleRepository = new(context);
            //PuleLocal = puleSelecionadoUi as Pule;
           // Caixa = (Caixa?)_caixaRepository.GetCaixa();
        }
        public PuleController() {
            _repository = new Repository();
            var context = _repository.GetDataBase();
            if (context is not null)
            {
                _puleRepository = new PuleRepository(context);
                AnimalController = new AnimalController(context);
                DisputaController = new(context);
                ApostadorController = new ApostadorController(context);
                _caixaController = new CaixaController(context);
            }
        }
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
            //ApostadorController.LoadApostador(apostadorSelecionadoUi);
            StatusPagamento pagamento;
            List<Animal>? animaisUi = animaisSelecionadosUi.Cast<Animal>().ToList();
            //Pule? pule =null;
            List<Animal>? animais = new List<Animal>();
            //DisputaController.LoadDisputa(disputaSelecionadoUi);
            bool sucess = false;
            if (pagamentoUi is StatusPagamento status)
                pagamento = status;
            else
                pagamento = StatusPagamento.Pendente;

            //if (apostadorUi is not null)
              //  apostadorSelecionado = ApostadorController.FindApostador(apostadorUi);
                //apostadorSelecionado = Apostadors.Find(a => a.Id == apostadorUi.Id);
            //pule = new Pule(apostadorSelecionado, pagamento, animais, valor);
            //remap dos animais
           
                //disputaSelecionado = Disputas.Find(d => d.Id == disputaUi.Id);
            var animalIds = animaisUi.Select(a => a.Id).ToHashSet();   
            if(animaisUi is not null && animaisUi.Count > 0)
                animais = AnimalController.Animals.Where(a => animalIds.Contains(a.Id)).ToList();
            //animais = Animals.Where(a => animalIds.Contains(a.Id)).ToList();

            NovoPule(pagamento, animais, (decimal)valor, númeroDoPule);
            //pule = new Pule(apostadorSelecionado, pagamento, null, valor, númeroDoPule);
            //pule = new Pule(null, pagamento, null, valor, númeroDoPule);
            //sucess = Pule.Save(pule);
            //sucess = _puleRepository.SavePule(pule);
            sucess = false;
            //sucess = pule.Associete(apostadorSelecionado, animais, disputaSelecionado);
            //sucess = _puleRepository.Associete(apostadorSelecionado, animais, disputaSelecionado , pule);
            
            
           
            //sucess = _puleRepository.Save();
            if(Pule is not null)
                sucess = _puleRepository.SavePule(Pule);
            if (sucess)
                return "Pule Cadastrado Com Sucesso!";
            else return "Algo Deu Errado No Cadastro Do Pule!";

        }
        /// <summary>
        /// Registra o novo pule
        /// </summary>
        /// <param name="pagamento"></param>
        /// <param name="animais"></param>
        /// <param name="valor"></param>
        /// <param name="númeroDoPule"></param>
        private void NovoPule(StatusPagamento pagamento, List<Animal> animais, decimal valor, int númeroDoPule)
        {
            var apostador = ApostadorController.Apostador;
            var disputa = DisputaController.Disputa;
            if(apostador is not null){
                //apostador = ApostadorController.IsTrack(apostador);
                if(disputa is not null){
                    _repository.AttachApostador(apostador);
                    _repository.AttachDisputa(disputa);
                    Pule = new(apostador,disputa, pagamento, new List<Animal>(), valor, númeroDoPule);
                    foreach (var animal in animais)
                    {
                        Pule.Animais?.Add(animal);
                    }
                }
               
            }
        }
        internal  void LoadPule(object puleSelecionadoUi)
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

        internal void Dispose()
        {
            _repository.GetDataBase().Dispose();
        }
        /// <summary>
        /// Initializes the apostador controller if necessary and loads apostadores from the database.
        /// </summary>
        internal void LoadApostadors()
        {
            ApostadorController.LoadApostadores();

        }

   

      
        /// <summary>
        /// Loads the specified Pule object and updates the Pule property using the repository.
        /// </summary>
        /// <param name="puleSelecionado">The Pule object to be loaded.</param>
        internal void Load(object puleSelecionado)
        {
            Pule? pule = puleSelecionado as Pule;
            if (pule != null)
            {
                Pule = _puleRepository.IsTrack(pule);
            }
        }
        /// <summary>
        /// Loads the specified pule along with its associated apostador and disputa data.
        /// </summary>
        /// <param name="puleSelecionado">The selected pule object to load.</param>
        internal void LoadFull(object puleSelecionado)
        {
            Load(puleSelecionado);
            if( Pule is not null)
            {
                if(Pule.Apostador is not null)
                {
                    ApostadorController.LoadApostador(Pule.Apostador);

                }
                if(Pule.Disputa is not null)
                {
                    DisputaController.LoadDisputa(Pule.Disputa);
                }

            }

        }
        /// <summary>
        /// Updates the current 'Pule' with the selected animals, payment status, value, and number.
        /// </summary>
        /// <param name="animaisSelecionados">Collection of selected animals to associate with the 'Pule'.</param>
        /// <param name="pagamento">Payment status object to update the 'Pule'.</param>
        /// <param name="valor">New value to set for the 'Pule'.</param>
        /// <param name="númeroDoPule">New number to assign to the 'Pule'.</param>
        /// <returns>A success or error message indicating the result of the update operation.</returns>
        internal string Update(ListBox.ObjectCollection animaisSelecionados, object? pagamento, decimal valor, int númeroDoPule)
        {
            bool sucess = false;
            AnimalController.LoadAnimais(animaisSelecionados);
            _caixaController.LoadCaixa();
            var caixa = _caixaController.Caixa;
            var animais = AnimalController.Animals;
            var pule = Pule;
            if(pule is not null){
                pule.Animais?.Clear();
                foreach(var animal in animais)
                {
                    if(animal is not null)
                    {
                        pule.Animais?.Add(animal);
                    }
                }
                    if (valor != pule.Valor)
                    {
                        pule.Valor = valor;
                    }
                    if(númeroDoPule != pule.Número)
                {
                    pule.Número = númeroDoPule;
                }
                if (pagamento is  StatusPagamento status)
                {
                    
                    if (pule.StatusPagamento != status)
                    {
                        pule.StatusPagamento = status;
                    }
                }
                if(caixa is not null)
                {
                    if(pule.StatusPagamento == StatusPagamento.Pago)
                        caixa.TotalEmCaixa += (decimal) pule.Valor;
                }
                sucess = _puleRepository.Save();
                if (sucess) return "Sucesso ao Atualizar O Pule!";
            }
            
            return "Erro ao Atualizar o Pule!";
            
        }

        internal string Update(List<Animal> animais, object? pagamento, float valor, int númeroDoPule, int rodada)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gera novo pule
        /// </summary>
        /// <param name="apostadorSelecionado"></param>
        /// <param name="pagamento"></param>
        /// <param name="animaisSelecionados"></param>
        /// <param name="valor"></param>
        /// <param name="númeroDoPule"></param>
        /// <param name="disputaSelecionado"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void NovoPule(int apostadorSelecionado, object? pagamentoUi, List<Animal> animaisSelecionados, decimal valor, int númeroDoPule, int disputaSelecionado)
        {
            StatusPagamento pagamento;
            if(pagamentoUi != null && Enum.TryParse(pagamentoUi.ToString(), out StatusPagamento result))
            {
                pagamento = result;
            }
            else
            {
                pagamento = StatusPagamento.Pendente;
            }
            Pule = new Pule(apostadorSelecionado, disputaSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule);
            _puleRepository.AddContext(Pule);

        }

        internal string SaveInContext()
        {
           bool sucess = false;
           sucess  = _puleRepository.Save();
            if (sucess)
                return "Sucesso ao Salvar os Dados no contexto!";
            else return "Erro ao Salvar os Dados no contexto!";
        }
    }
}
