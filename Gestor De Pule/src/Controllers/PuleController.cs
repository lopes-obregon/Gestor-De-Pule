using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Controllers
{
    internal class PuleController
    {
        static public List<Apostador>? Apostadors { get; private set; }
        static public List<Animal>? Animals { get; private set; }
        static public List<Pule>? Pules { get; private set; }
        static public Pule? Pule { get; private set; }
        internal static string CadastrarPule(object? apostadorSelecionadoUi, object? pagamentoUi, ListBox.ObjectCollection animaisSelecionadosUi)
        {
           Apostador? apostador =apostadorSelecionadoUi as Apostador;
            StatusPagamento pagamento;
            List<Animal>? animais = animaisSelecionadosUi.Cast<Animal>().ToList();
            Pule pule =null;
            bool sucess = false;
            if (pagamentoUi is StatusPagamento status)
                pagamento = status;
            else
                pagamento = StatusPagamento.Pendente;

                pule = new Pule(apostador, pagamento, animais);
            sucess = Pule.Save(pule);
            if (sucess)
                return "Pule Cadastrado Com Sucesso!";
            else return "Algo Deu Errado No Cadastro Do Pule!";

        }

        internal static List<StatusPagamento>? GetStatusPagamento()
        {
            return Enum.GetValues(typeof(StatusPagamento)).Cast<StatusPagamento>().ToList();
        }

        internal static void LoadLists()
        {
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
