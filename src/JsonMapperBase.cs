using System.Text.Json;
using System.Text.Json.Nodes;

namespace RM.JsonMapper;
public class JsonMapperBase
{
    public static JsonNode Map(string fromJson, string mappingConfig)
    {
        Dictionary<string, (JsonTokenType Type, string Val)> _dic = new ();

        JsonToDic(_dic, fromJson, "");

        var jResult = JsonNode.Parse("{}");
        var mappings = mappingConfig.Replace("\r\n", "").Split(new[] { ',', ':' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
        var i = -1;
        var comment = false;
        foreach (var mapping in mappings)
        {
            i++;
            if (mapping.StartsWith("//"))
            {
                comment = true;
                continue;
            }
            if (comment == true)
            {
                comment = false;
                continue;
            }
            if (i < mappings.Count - 1 && mappings[i + 1].Contains("["))
            {
                var p = mappings[i + 1].Split('[');
                //var index = mappings[i + 1].Substring(mappings[i + 1].IndexOf('[')).Substring(mappings[i + 1].IndexOf(']'));
                var arr = JsonArray.Parse(_dic[p[0]].Val);
            }

            if (!_dic.ContainsKey(mapping))
            {

                if (mappings[i + 1].Contains("]."))
                {
                    var p = mappings[i + 1].Split('.');
                    var val1 = JsonNode.Parse(_dic[p[0]].Val.Replace("\r\n", "").Replace("\\\"", "\""));

                    var jObj22 = JsonNode.Parse(_dic[p[0]].Val);
                    for (int i2 = 1; i2 < p.Length - 1; i2++)
                        jObj22 = (JsonObject)jObj22[p[i2]];
                    
                    jResult.Add(mapping, jObj22[p[p.Length - 1]]);
                }
                continue;
            }
            if (i % 2 == 0) continue;
            var val = _dic[mapping].Val.Replace("\r\n", "").Replace("\\\"", "\"");
            var typ = _dic[mapping].Type;
            var prop = mappings[i - 1];
            if (val == null)
            {
                jResult.Add(prop, val);

                continue;
            }

            if (prop.Contains("."))
            {
                var p = prop.Split('.');
                var jObj22 = jResult[p[0]];
                if (jObj22 != null)
                {
                    AddPropToExsistingObjectProp(val, p, jObj22);
                }
                else
                {
                    AddObjectProp(jResult, val, p);
                }
            }
            else
            {
                if (typ == JsonTokenType.String)
                    jResult.Add(prop, val);
                else if (typ == JsonTokenType.StartArray)
                    jResult.Add(prop, JsonNode.Parse(val));
                else
                    jResult.Add(prop, JsonNode.Parse(val));
            }


        }

        return jResult;
    }


    public static TDestination Map<TSource,TDestination>(TSource fromJson, string mappingConfig)
         where TDestination : class, new()
         where TSource : class
    {
        var source= JsonSerializer.Serialize(fromJson);
        return Map(source, mappingConfig).ToObject<TDestination>();
    }

    public static TDestination Map< TDestination>(string fromJson, string mappingConfig)
           where TDestination : class, new()
                                                => Map(fromJson, mappingConfig).ToObject<TDestination>();
    


    static JsonNode AddPropToExsistingObjectProp(string? val, string[] p, JsonNode? jObj22)
    {
        for (int i2 = 1; i2 < p.Length - 1; i2++)
            jObj22 = jObj22[p[i2]];

        ((JsonObject)jObj22).Add(p[p.Length - 1], val);
        return jObj22;
    }

    static void AddObjectProp(JsonNode jRes, string? val, string[] p)
    {
        var jObj = JsonNode.Parse("{}");
        jObj.Add(p[p.Length - 1], val);

        for (int i2 = p.Length - 2; i2 > 0; i2--)
        {
            var _jObj = JsonNode.Parse("{}");
            _jObj.Add(p[i2], jObj);
            jObj = _jObj;
        }

        jRes.Add(p[0], jObj);
    }

    static void JsonToDic(Dictionary<string, (JsonTokenType Type, string Val)> _dic,string jsonObj, string parent)
    {
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement?>>(jsonObj);

        foreach (var jProp in dictionary)
        {
            var k = jProp.Key;
            var v = jProp.Value;
            var isObject= v.Value.ValueKind == JsonValueKind.Object;
            var isArray = v.Value.ValueKind == JsonValueKind.Array;
            if (!isObject && !isArray)
                _dic.Add(parent + k, (JsonTokenType.String, v.ToString()));
            else
            {
                if (isArray)
                {
                    for (int i = 0; i < k.Length; i++)
                    {
                        _dic.Add(parent + k + "[" + i + "]", (JsonTokenType.StartObject, JsonArray.Create(v.Value)[i].ToString()));
                    }
                    _dic.Add(parent + k, (JsonTokenType.StartArray, v.ToString()));
                }
                else
                    JsonToDic(_dic, v.ToString(), parent + k + ".");
            }
        }
    }


}
