using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Repository
{
    class DisputaRepository
    {
        internal static Disputa? Exist(string nomeDisputa)
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas.Include(dis => dis.ResultadoList).First(dis => dis.Nome == nomeDisputa);
            }
            catch { return null; }
        }

        internal static Disputa? ReadDisputa(Disputa disputaSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                if (disputaSelecionado == null) return null;
                return db.Disputas.Include(d => d.ResultadoList).First();
                
            }
            catch { return null; }
        }

        internal static List<Disputa> ReadDisputas()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas
                    .Include(d => d.ResultadoList)
                    .Where(dis=> !String.IsNullOrEmpty(dis.Nome))
                    .ToList();
            }
            catch { return new List<Disputa>(); }
        }

        internal static bool Remove(Disputa disputaSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                var disputaDb = db.Disputas
                    .Include(d => d.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .FirstOrDefault(d=> d.Id ==  disputaSelecionado.Id);
                if (disputaDb != null)
                {
                    foreach (var resultado in disputaDb.ResultadoList.ToList()) //tolist cópia segura
                    {
                        if (resultado is not null)
                        {
                            //tira a associação dos dados para essa disputa selecionado;
                            if (resultado.Disputa.Id == disputaSelecionado.Id)
                            {
                                resultado.Disputa = new();//remove associação   
                                db.Resultados.Update(resultado);
                                db.SaveChanges();
                            }
                        }
                    }
                    db.Disputas.Remove(disputaDb);
                    db.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Saves the specified <see cref="Disputa"/> instance to the database.
        /// </summary>
        /// <param name="disputa">The <see cref="Disputa"/> object to be saved. Cannot be null.</param>
        /// <returns><see langword="true"/> if the <see cref="Disputa"/> was successfully saved; otherwise, <see
        /// langword="false"/>.</returns>
        internal static bool Save(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                   
                    foreach(Resultado resultado in disputa.ResultadoList)
                    {
                        Animal animal = resultado.Animal;
                        //verifica se o animal existe no banco de dados
                        var local = db.Animals.Local.FirstOrDefault(a=> a.Id == animal.Id);
                        if (local != null)
                            resultado.Animal = local; //usa o rastreado;
                        else
                            db.Animals.Attach(animal); //anexa como existente
                    }
                    db.Disputas.Add(disputa);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;
        }

        internal static bool  UpdateDisputa(Disputa disputa, List<Animal> animaisSelecionadosUi)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)

                {
                    db.Disputas.Attach(disputa);
                    List<Resultado> resultadosUi = disputa.ResultadoList.ToList();
                    if(animaisSelecionadosUi is not null && animaisSelecionadosUi.Count > 0)
                    {
                        var ids = animaisSelecionadosUi.Select(a=> a.Id).ToList();
                        var animaisDb = db.Animals.Where(a=>ids.Contains(a.Id)).ToList();
                        foreach(var animal in animaisDb)
                        {
                            if(animal is not null)
                            {
                                //Acho que está errado pensar melhor 
                                //senãrio onde não existe uma disputa
                                bool temEssaDisputa = animal.Resultados.Any(ar => resultadosUi.Any(rui => rui.Id == ar.Id));
                                if (!temEssaDisputa)
                                {
                                    Resultado resultado = resultadosUi.Find(res => res.Disputa.Id == disputa.Id) ?? new Resultado();
                                    db.Resultados.Attach(resultado);
                                    db.Entry(resultado).State = EntityState.Modified;
                                    if (resultado.Disputa is null)
                                        resultado.Disputa = disputa;
                                    resultado.Animal = animal;
                                    
                                    animal.Resultados.Add(resultado);
                                    db.Update(animal);
                                    db.Update(resultado);

                                }
                                else
                                {
                                    //caso onde a disputa exista.
                                    //procuro onde essa disputa caso com o resultado ou seja
                                    //onde o resultado -> disputa
                                    Resultado resultadoToDisputa = resultadosUi.Find(res=> res.Disputa.Id==disputa.Id) ?? new Resultado();
                                    //caso onde o animal -> resultado;
                                    Resultado? resultadoAnimalToResultado = animal.Resultados.Find(ar => ar.Id == resultadoToDisputa.Id);
                                    //verificando se é o mesmo resultado
                                    if(resultadoAnimalToResultado == null && resultadoToDisputa is null)
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }
                    db.Update(disputa);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;
        }

        internal static bool UpdateDisputa(Disputa disputa, List<Animal> animaisSelecionadosUi, List<Animal> animaisRemovidos)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                    var disputaDb = db.Disputas
                        .Include(d => d.ResultadoList)
                        .FirstOrDefault(d => d.Id == disputa.Id);
                    if (disputa.Nome != disputaDb.Nome)
                        disputaDb.Nome = disputa.Nome;
                    if(!disputa.DataEHora.Equals(disputaDb.DataEHora))
                        disputaDb.DataEHora = disputa.DataEHora;
                    //trataremos dos animais removidos
                    if(animaisRemovidos.Count > 0)
                    {
                        // se for maior que zero então temos animais removidos
                        foreach(var animal in animaisRemovidos)
                        {
                            if(animal is not null)
                            {
                                db.Animals.Attach(animal);
                                var resultadoDoAnimal = animal.Resultados.Find(res => res.Disputa.Id == disputaDb.Id);
                                if(resultadoDoAnimal is not null)
                                {
                                    db.Resultados.Attach(resultadoDoAnimal);
                                    //encontramos a disputa devemos remover ela da relação
                                    // no caso o resultado
                                    resultadoDoAnimal.Disputa = new Disputa();
                                    db.Resultados.Update(resultadoDoAnimal);
                                    disputa.ResultadoList.Remove(resultadoDoAnimal);

                                }
                            }
                        }
                    }
                    if(animaisSelecionadosUi.Count > 0)
                    {
                        foreach (var animal in animaisSelecionadosUi)
                        {
                            var animalDb = db.Animals
                                .Include(a=> a.Resultados)
                                .ThenInclude(r=>r.Disputa)
                                .FirstOrDefault(a=> a.Id == animal.Id);
                            // Verifica se o animal já tem um resultado vinculado a essa disputa
                            bool jaParticipa = animalDb.Resultados.Any(res => res.Disputa.Id == disputaDb.Id);

                            if (!jaParticipa)
                            {
                                // Verifica se o contexto já está rastreando esse animal
                                /*var local = db.Animals.Local.FirstOrDefault(a => a.Id == animal.Id);
                                if (local == null)
                                {
                                    db.Animals.Attach(animal); // começa a rastrear
                                }*/

                                // Cria o novo resultado e vincula à disputa e ao animal
                                var resultado = new Resultado(animalDb)
                                {
                                    Disputa = disputaDb
                                };
                                // Adiciona o resultado ao contexto
                                db.Resultados.Add(resultado);

                                // Atualiza as relações
                                animalDb.Resultados.Add(resultado);
                                disputaDb.ResultadoList.Add(resultado);

                                db.Animals.Update(animalDb);
                            }
                        }

                        // Atualiza a disputa (se necessário)
                        db.Disputas.Update(disputaDb);
                        db.SaveChanges();
                    }
                }
                return true;
            }catch {  return false; }
        }
    }
}
