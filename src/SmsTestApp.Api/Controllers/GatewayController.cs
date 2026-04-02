using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmsTestApp.Api.Services;
using SmsTestApp.Contracts;
using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;
using System.Text.Json;

namespace SmsTestApp.Api.Controllers
{
    /// <summary>
    /// REST реализация тестового API.
    /// </summary>
    /// <param name="menuService">Сервис для работы с меню.</param>
    /// <param name="orderService">Сервис для работы с заказами.</param>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public class GatewayController(
        IMenuService menuService,
        IOrderService orderService) : ControllerBase
    {
        /// <summary>
        /// Выполнение произвольного запроса.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Набор данных.</returns>
        [HttpPost("execute-request", Name = "ExecuteRequest")]
        [ProducesResponseType<Response>(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecuteRequest(Request request)
        {
            try
            {
                var result = await ExecuteRequestAsync(request.Command, request.CommandParameters);
                return Ok(new Response
                {
                    Command = request.Command,
                    Success = true,
                    Data = result,
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response
                {
                    Command = request.Command,
                    Success = false,
                    ErrorMessage = ex.Message,
                });
            }
        }


        /// <summary>
        /// Выполнить запрос на основе команды и параметров.
        /// </summary>
        /// <param name="command">Команда на выполнение.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Результат работы.</returns>
        /// <exception cref="InvalidOperationException">Команда недоступна.</exception>
        private async Task<object?> ExecuteRequestAsync(string command, object parameters)
        {
            // В реальной реализации здесь будет логика обработки различных типов команд.
            // Сейчас зашиты только команды для получения меню и отправки заказа.
            return command switch
            {
                "GetMenu" => await GetMenuAsync(GetRequestParameters<GetMenuRequest>(parameters)),
                "SendOrder" => await SendOrderAsync(GetRequestParameters<SendOrderCommand>(parameters)),
                _ => throw new InvalidOperationException($"Unsupported command: {command}")
            };
        }

        private async Task<GetMenuResponse> GetMenuAsync(GetMenuRequest request)
        {
            var response = new GetMenuResponse
            {
                Items = [.. await menuService.GetMenuAsync(request.WithPrice)]
            };

            return response;
        }

        private async Task<object?> SendOrderAsync(SendOrderCommand command)
        {
            await orderService.SendOrderAsync(command.OrderId, command.Items);

            return null;
        }

        /// <summary>
        /// Произвести разбор параметров запроса в указанный тип.
        /// </summary>
        /// <typeparam name="T">Тип параметров.</typeparam>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Набор параметров.</returns>
        /// <exception cref="InvalidOperationException">Разбор параметров произвести не удалось.</exception>
        private static T GetRequestParameters<T>(object parameters)
        {
            // В реальной реализации может потребоваться валидация. Сейчас она отсутствует.
            return JsonSerializer.Deserialize<T>(parameters.ToString() ?? string.Empty)
                    ?? throw new InvalidOperationException("Failed to deserialize command parameters.");
        }
    }
}
