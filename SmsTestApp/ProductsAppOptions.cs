namespace SmsTestApp
{
    /// <summary>
    /// Настройки приложения для работы с продуктами.
    /// </summary>
    public sealed class ProductsAppOptions
    {
        /// <summary>
        /// Точка доступа к REST API.
        /// </summary>
        public string? HttpEndpoint { get; set; }

        /// <summary>
        /// Точка доступа к gRPC API.
        /// </summary>
        public string? GrpcEndpoint { get; set; }
    }
}
