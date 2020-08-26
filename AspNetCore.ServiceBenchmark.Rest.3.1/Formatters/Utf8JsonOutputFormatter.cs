using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;
using Utf8Json;

namespace AspNetCore.ServiceBenchmark.Rest._3._1.Formatters
{
    internal sealed class Utf8JsonOutputFormatter : IOutputFormatter
    {
        private readonly IJsonFormatterResolver _resolver;

        public Utf8JsonOutputFormatter() : this(null) { }
        public Utf8JsonOutputFormatter(IJsonFormatterResolver resolver)
        {
            _resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context) => true;


        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (!context.ContentTypeIsServerDefined)
                context.HttpContext.Response.ContentType = "application/json";

            if (context.ObjectType == typeof(object))
            {
                await JsonSerializer.NonGeneric.SerializeAsync(context.HttpContext.Response.Body, context.Object, _resolver);
            }
            else
            {
                await JsonSerializer.NonGeneric.SerializeAsync(context.ObjectType, context.HttpContext.Response.Body, context.Object, _resolver);
            }
        }
    }
}
