using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;
using DesafioBemol.Models;
using Microsoft.Azure.Cosmos;

namespace DesafioBemol.Repositories
{
    public class ObjetoRepository : IObjetoRepository
    {
        private readonly Container _container;

        public ObjetoRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public Task CreateObjeto(Objeto objeto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteObjeto(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Objeto>> GetAllObjetos()
        {
            throw new NotImplementedException();
        }

        public Task<Objeto> GetObjetoById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessarObjeto(Objeto objeto)
        {
            try
            {
                ItemResponse<Objeto> existingItemResponse = await _container.ReadItemAsync<Objeto>(objeto.Id.ToString(), new PartitionKey(objeto.Id.ToString()));

                if (existingItemResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Atualizar o objeto existente
                    objeto.DataAtualizacao = DateTime.UtcNow;

                    ItemResponse<Objeto> updateResponse = await _container.ReplaceItemAsync(objeto, objeto.Id.ToString(), new PartitionKey(objeto.Id.ToString()));
                    if (updateResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Objeto atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao atualizar o objeto!");
                    }
                }
                else if (existingItemResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Inserir um novo objeto no banco de dados
                    ItemResponse<Objeto> createResponse = await _container.CreateItemAsync(objeto, new PartitionKey(objeto.Id.ToString()));
                    if (createResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        Console.WriteLine("Objeto criado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao criar o objeto!");
                    }
                }
                else
                {
                    Console.WriteLine("Código de status de resposta desconhecido: " + existingItemResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o objeto: " + ex.Message);
            }
        }

        public Task UpdateObjeto(Objeto objeto)
        {
            throw new NotImplementedException();
        }
    }
}