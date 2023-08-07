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


    public List<TDestination> MapList<TSource, TDestination>(List<TSource> fromList)
      where TDestination : class, new()
      where TSource : class
    {   
        var res = new List<TDestination>();
        if (fromList.GetType().GetGenericTypeDefinition() == typeof(List<>))
        {
            res=(from item in fromList
                         select Map<TDestination>(JsonSerializer.Serialize(item))).ToList();
        }
        else
            res.Add(Map<TDestination>(JsonSerializer.Serialize(fromList)));
        
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
