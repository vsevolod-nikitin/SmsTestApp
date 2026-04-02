using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace SmsTestApp.UnitTests
{
    public sealed class AppExtensionsTests
    {
        [Fact]
        public void AddProductsApp_ShouldThrowInvalidOperationException_WhenNoEndpointsConfigured()
        {
            // Arrange
            var services = Mock.Of<IServiceCollection>();
            var action = () => services.AddProductsApp((options) => { });

            // Assert
            var exception = action.ShouldThrow<InvalidOperationException>();
            exception.Message.ShouldBe("Не указано ни одной из конечных точек для взаимодействия с продуктами.");
        }
    }
}
