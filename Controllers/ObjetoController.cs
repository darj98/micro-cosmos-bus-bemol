using DesafioBemol.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
public class ObjetoController : ControllerBase
{
    private readonly IObjetoRepository _objetoRepository;
    private readonly IQueueSender _queueSender;

    public ObjetoController(IObjetoRepository objetoRepository, IQueueSender queueSender)
    {
        _objetoRepository = objetoRepository;
        _queueSender = queueSender;
    }

    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ExceptionFilter))] 
    [ServiceFilter(typeof(ActionFilter))] 
    public async Task<IActionResult> CreateObjeto(ObjetoDTO objetoDTO)
    {
        try
        {
            var stopwatch = Stopwatch.StartNew();

            await _objetoRepository.Create(objetoDTO);

            await _queueSender.SendMessage(objetoDTO);

            stopwatch.Stop();
            var executionTime = stopwatch.Elapsed;

            Console.WriteLine($"Duração da execução: {executionTime}");

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Ocorreu um erro ao processar a requisição." });
        }
    }
}