namespace DesafioBemol.Services
{
    using DesafioBemol.Repositories;
    using Microsoft.Azure.ServiceBus;
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace MeuProjeto.Services
    {
        public class FilaConsumer
        {
            private readonly IQueueClient _queueClient;
            private readonly IObjetoRepository _objetoRepository;

            public FilaConsumer(IQueueClient queueClient, IObjetoRepository objetoRepository)
            {
                _queueClient = queueClient;
                _objetoRepository = objetoRepository;
            }

            public async Task ConsumeFila()
            {
                _queueClient.RegisterMessageHandler(async (message, cancellationToken) =>
                {
                    try
                    {
                        string objetoJson = Encoding.UTF8.GetString(message.Body);

                        await _objetoRepository.ProcessarObjeto(objeto);

                        await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                    catch (Exception ex)
                    {
                        await _queueClient.AbandonAsync(message.SystemProperties.LockToken);
                    }
                }, new MessageHandlerOptions(ExceptionReceivedHandler) { AutoComplete = false });

                Console.ReadLine();

                await _queueClient.CloseAsync();
            }

            Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
            {
                return Task.CompletedTask;
            }
        }
    }
}
