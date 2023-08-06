using System.Text.Json;

namespace RM.JsonMapper;

public class JsonMapper: JsonMapperBase
{
    string _mappingConfig;
    public JsonMapper(string mappingConfig)
    {
        if (string.IsNullOrWhiteSpace(mappingConfig))
            throw new Exception("first set [mapping Config] using  [WithConfig]");

        _mappingConfig = mappingConfig;
    }

    public JsonMapper WithConfig(string mappingConfig)
    {
        if (string.IsNullOrWhiteSpace(mappingConfig))
            throw new Exception("[mapping Config] cannot be null or empty");

        _mappingConfig = mappingConfig;
        return this;
    }


    public TDestination Map<TSource, TDestination>(TSource from)
        where TDestination : class,new()
        where TSource : class
                                            => Map<TDestination>(JsonSerializer.Serialize(from));

    public TDestination Map<TDestination>( string fromJson)
              where TDestination : class, new()
                                            => Map(fromJson, _mappingConfig).ToObject<TDestination>();
}
