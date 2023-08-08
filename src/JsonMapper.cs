using System.Collections;
using System.Text.Json;
using System.Linq;

namespace RM.JsonMapper;

public class JsonMapper : JsonMapperBase
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


    public List<TToType> MapList< TToType>(IEnumerable fromList)
        where TToType : class, new()
    {
        var res = new List<TToType>();
        if (fromList.GetType().GetGenericTypeDefinition() == typeof(List<>))
        {
            res.AddRange(fromList.OfType<object>().Select(item => Map<TToType>(JsonSerializer.Serialize(item))));
        }
        else
            res.Add(Map<TToType>(JsonSerializer.Serialize(fromList)));

        return res;
    }


    public TToType Map<TToType>(object fromObject)
        where TToType : class, new()
        => Map<TToType>(JsonSerializer.Serialize(fromObject));

    public TToType Map<TToType>(string fromJson)
        where TToType : class, new()
        => Map(fromJson, _mappingConfig).ToObject<TToType>();

    public string Map(string fromJson)
        => Map(fromJson, _mappingConfig).ToJsonString();


    [Obsolete]
    public List<TToType> MapList<TSource, TToType>(List<TSource> fromList)
     where TToType : class, new()
     where TSource : class
    {
        var res = new List<TToType>();
        if (fromList.GetType().GetGenericTypeDefinition() == typeof(List<>))
        {
            res = (from item in fromList
                   select Map<TToType>(JsonSerializer.Serialize(item))).ToList();
        }
        else
            res.Add(Map<TToType>(JsonSerializer.Serialize(fromList)));

        return res;
    }

    [Obsolete]
    public TToType Map<TSource, TToType>(TSource from)
        where TToType : class, new()
        where TSource : class
        => Map<TToType>(JsonSerializer.Serialize(from));
}
