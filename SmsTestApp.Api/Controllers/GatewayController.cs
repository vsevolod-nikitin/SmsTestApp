using Microsoft.AspNetCore.Mvc;
using SmsTestApp.Api.Services;
using SmsTestApp.Contracts.Menu;

namespace SmsTestApp.Api.Controllers
{
    /// <summary>
    /// REST реализация тестового API.
    /// </summary>
    /// <param name="menuService">Сервис для работы с меню.</param>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public class GatewayController(IMenuService menuService) : ControllerBase
    {
        /// <summary>
        /// Получить набор доступных блюд.
        /// </summary>
        /// <param name="request">Запрос на получение.</param>
        /// <returns>Набор блюд.</returns>
        [HttpGet("menus", Name = "GetMenu")]
        [ProducesResponseType<GetMenuResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenu(GetMenuRequest request)
        {
            var items = await menuService.GetMenuAsync(request.CommandParameters.WithPrice);

            var response = new GetMenuResponse
            {
                Success = true,
            };
            response.Data.MenuItems.AddRange(items);

            return Ok(response);
        }
    }
}
