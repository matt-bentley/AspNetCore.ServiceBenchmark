using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;
using Utf8Json;

namespace AspNetCore.ServiceBenchmark.Rest._3._1.Formatters
{
    internal sealed class Utf8JsonInputFormatter : IInputFormatter
    {
        private readonly IJsonFormatterResolver _resolver;

        public Utf8JsonInputFormatter() : this(null) { }
        public Utf8JsonInputFormatter(IJsonFormatterResolver resolver)
        {
            _resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        public bool CanRead(InputFormatterContext context) => context.HttpContext.Request.ContentType.StartsWith("application/json");

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Body.CanSeek && request.Body.Length == 0)
                return await InputFormatterResult.NoValueAsync();

            var result = await JsonSerializer.NonGeneric.DeserializeAsync(context.ModelType, request.Body, _resolver);
            return await InputFormatterResult.SuccessAsync(result);
        }
    }
}
