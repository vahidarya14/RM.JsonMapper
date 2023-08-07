using System.Collections;
using System.Runtime.CompilerServices;
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


    public List<TDestination> MapList<TSource, TDestination>(List<TSource> from)
      where TDestination : class, new()
      where TSource : class
    {   
        var res = new List<TDestination>();
        if (from.GetType().GetGenericTypeDefinition() == typeof(List<>))
        {
            var lst = from as IList;
        
            foreach (var item in lst)
            {
                var r = Map<TDestination>(JsonSerializer.Serialize(item));
                res.Add(r);
            }
        }
        else
        {
            var r = Map<TDestination>(JsonSerializer.Serialize(from));
            res.Add(r);
        }

        return res;
    }

    public TDestination Map<TSource, TDestination>(TSource from)
        where TDestination : class, new()
        where TSource : class
    {
        return Map<TDestination>(JsonSerializer.Serialize(from));
    }

    public TDestination Map<TDestination>( string fromJson)
              where TDestination : class, new()
                                            => Map(fromJson, _mappingConfig).ToObject<TDestination>();
}
