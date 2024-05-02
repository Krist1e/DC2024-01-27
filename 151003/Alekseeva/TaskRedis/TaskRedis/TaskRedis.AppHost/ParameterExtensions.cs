using Aspire.Hosting.Publishing;
using Microsoft.Extensions.Configuration;
namespace TaskRedis.AppHost;

internal static class ParameterExtensions
{
    public static IResourceBuilder<ParameterResource> CreateStringParameter(this IDistributedApplicationBuilder builder, string value, string name)
    {
        ParameterDefault generatedPassword = new SimpleParameterDefault(value);
        
        var parameterResource = new ParameterResource(name, parameterDefault => GetParameterValue(builder.Configuration, name, parameterDefault), true)
        {
            Default = generatedPassword
        };

        return ResourceBuilder.Create(parameterResource, builder);
    }

    private static string GetParameterValue(IConfiguration configuration, string name, ParameterDefault? parameterDefault)
    {
        var configurationKey = $"Parameters:{name}";
        return configuration[configurationKey]
            ?? parameterDefault?.GetDefaultValue()
            ?? throw new DistributedApplicationException($"Parameter resource could not be used because configuration key '{configurationKey}' is missing and the Parameter has no default value.");
        ;
    }

    private class SimpleParameterDefault(string value) : ParameterDefault
    {
        public override void WriteToManifest(ManifestPublishingContext context)
        {
            context.Writer.WriteStartObject("simple");
            context.Writer.WriteString("value", value);
            context.Writer.WriteEndObject();
        }

        public override string GetDefaultValue() => value;
    }

    class ResourceBuilder
    {
        public static IResourceBuilder<T> Create<T>(T resource, IDistributedApplicationBuilder distributedApplicationBuilder) where T : IResource
        {
            return new ResourceBuilder<T>(resource, distributedApplicationBuilder);
        }
    }

    class ResourceBuilder<T>(T resource, IDistributedApplicationBuilder distributedApplicationBuilder) : IResourceBuilder<T> where T : IResource
    {
        public IDistributedApplicationBuilder ApplicationBuilder { get; } = distributedApplicationBuilder;

        public T Resource { get; } = resource;

        public IResourceBuilder<T> WithAnnotation<TAnnotation>(TAnnotation annotation, ResourceAnnotationMutationBehavior behavior = ResourceAnnotationMutationBehavior.Append) where TAnnotation : IResourceAnnotation => 
            throw new NotSupportedException();
    }
}
