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
        /// Имя пользователя для аутентификации при взаимодействии с REST API. Используется только при указании <see cref="HttpEndpoint"/>.
        /// </summary>
        public string? HttpUsername { get; set; }

        /// <summary>
        /// Пароль пользователя для аутентификации при взаимодействии с REST API. Используется только при указании <see cref="HttpEndpoint"/>.
        /// </summary>
        public string? HttpPassword { get; set; }

        /// <summary>
        /// Точка доступа к gRPC API.
        /// Является приоритетной, если указано вместе с <see cref="HttpEndpoint"/>. В случае указания обеих точек доступа, будет использоваться gRPC API.
        /// </summary>
        public string? GrpcEndpoint { get; set; }
    }
}
