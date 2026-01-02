using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;

namespace Gestor_De_Pule.src.Controllers
{
    internal class PuleController
    {
        static public List<Apostador>? Apostadors { get; private set; }
        static public List<Animal>? Animals { get; private set; }
        static public List<Pule>? Pules { get; private set; }
        static public Pule? Pule { get; private set; }
        static public List<Disputa>? Disputas { get; private set; } = null;

        internal static List<Animal>? AttComboBoxAnimais(object disputaSelecionadaUi)
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

        internal static string CadastrarPule(object? apostadorSelecionadoUi, object? pagamentoUi, ListBox.ObjectCollection animaisSelecionadosUi, float valor, int númeroDoPule, object? disputaSelecionadoUi)
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
            sucess = Pule.SavePule(pule);
            sucess = pule.Associete(apostadorSelecionado, animais, disputaSelecionado);

            if (sucess)
                return "Pule Cadastrado Com Sucesso!";
            else return "Algo Deu Errado No Cadastro Do Pule!";

        }

        internal static List<StatusPagamento>? GetStatusPagamento()
        {
            return Enum.GetValues(typeof(StatusPagamento)).Cast<StatusPagamento>().ToList();
        }

        internal static void LoadAnimais()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
        }

        internal static void LoadLists()
        {
            Disputas = Disputa.GetDisputas();
            AnimalController.LoadAnimais();
            ApostadorController.LoadApostadores();
            Apostadors = ApostadorController.Apostadors.ToList();
            Animals = AnimalController.Animals.ToList();
            
        }

        internal static void LoadPule(object puleSelecionadoUi)
        {
            Pule? puleSelecionado = puleSelecionadoUi as Pule;
            if(puleSelecionado is not null)
            {
                Pule = puleSelecionado;
            }
        }

        internal static void LoadPules()
        {
            Pules = new List<Pule>();
            Pules = Pule.ReadPules();
        }

        internal static string RemovePule(object puleSelecionadoUi)
        {
            bool sucess = false;
            Pule? puleSelecionado = puleSelecionadoUi as Pule;
            if(puleSelecionado is not null)
            {
                sucess = Pule.Remove(puleSelecionado);
                if (sucess) return "Pule Removido Com Sucesso!";
                else return "Erro ao Remover O Pule!";
            }
                return "";
        }

        

        internal static string UpdateData(object? apostadorUi, object? pagamentoUi, ListBox.ObjectCollection animaisUi)
        {
            Apostador? apostadorSelecionado = apostadorUi as Apostador;
            StatusPagamento pagamento = StatusPagamento.Pendente;
            List<Animal>? animais = animaisUi.Cast<Animal>().ToList();
            bool sucess = false;
            if(pagamentoUi is StatusPagamento status)
            {
                pagamento = status;
            }
            if (Pule is not null)
            {
                //não pode mudar o apostador apenas status de pagamento e 
                //animais apostados
                //se for do pule igual a pendente ele recebe mesmo se atualizou
                if (Pule.StatusPagamento.Equals(StatusPagamento.Pendente))
                {
                    Pule.StatusPagamento = pagamento;
                }
                //verificar se são os mesmo animais
                bool isEqual = Pule.Animais
                    .Select(a => a.Id)
                    .OrderBy(a => a)
                    .SequenceEqual(animais.Select(a => a.Id).OrderBy(x => x));
                
               sucess =  Pule.Update(Pule, animais, isEqual);
            }
            else
                sucess = false;
            if (sucess)
                return "Pule Atualizado Com Sucesso!";
            else
                return "Erro ao Atualizar o Pule!";
        }
    }
}
