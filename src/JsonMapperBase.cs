using System.Text.Json;
using System.Text.Json.Nodes;

namespace RM.JsonMapper;
public class JsonMapperBase
{

    public static TDestination Map<TSource, TDestination>(TSource fromJson, string mappingConfig)
         where TDestination : class, new()
         where TSource : class
    {
        var source = JsonSerializer.Serialize(fromJson);
        return Map(source, mappingConfig).ToObject<TDestination>();
    }

    public static TDestination Map<TDestination>(string fromJson, string mappingConfig) where TDestination : class, new()
                                                => Map(fromJson, mappingConfig).ToObject<TDestination>();




    public static JsonNode Map(string fromJson, string mappingConfig)
    {
        //Dictionary<string, (JsonTokenType Type, string Val)> _dic = new();
        //JsonToDic(_dic, fromJson, "");
        var sourceObj = JsonNode.Parse(fromJson);
        var jResult = JsonNode.Parse("{}");
        var mappingConfigs1 = mappingConfig.Replace("\r\n", "").Split(new[] { '}' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
        var mappingConfigs = mappingConfig.Replace("\r\n", "").Split(new[] { ',', ':' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

        JsonNode val = null;

        for (int i = 0; i < mappingConfigs.Count; i += 2)
        {
            var destProp = mappingConfigs[i];
            var sourceProp = mappingConfigs[i + 1];

            if (destProp.StartsWith("//"))
                continue;
            if (sourceProp.Contains("{"))
            {
                var innerMapping = "";
                for (int j = i + 1; j < mappingConfigs.Count; j += 2)
                {
                    innerMapping += mappingConfigs[j] + ":" + mappingConfigs[j + 1] + ",";
                    if (mappingConfigs[j + 1].Contains("}"))
                    {
                        i = j;
                        break;
                    }
                }

                var fromProp = innerMapping.Split('{')[0];
                innerMapping = innerMapping.Split('{')[1].Split('}')[0];

                var _arr = new JsonArray();
                JsonArray _fromObj = (JsonArray)JsonNode.Parse(sourceObj[fromProp].ToJsonString());

                foreach (var item in _fromObj)
                    _arr.Add(Map(item.ToJsonString(), innerMapping));

                val = _arr;
                sourceProp = fromProp;
            }
            else
            {
                var ps = sourceProp.Split('.');
                foreach (var p in ps)
                {
                    var bs = p.Split('[').Select(x => x.Replace("]", "")).ToList();
                    if (p == ps[0])
                        val = (bs.Count == 1) ? sourceObj[bs[0]] :
                                                JsonNode.Parse(sourceObj[bs[0]].ToJsonString())[int.Parse(bs[1])];
                    else
                        val = bs.Count == 1 ? val[bs[0]] :
                                              JsonNode.Parse(val[bs[0]].ToJsonString())[int.Parse(bs[1])];
                }
            }


            if (val == null)
            {
                jResult.Add(destProp, val);
                continue;
            }

            if (destProp.Contains("."))
            {
                var ps2 = destProp.Split('.');
                var jObj22 = jResult[ps2[0]];
                if (jObj22 != null)
                    AddPropToExsistingObjectProp(val, ps2, jObj22);

                else
                    CreateAndAddObjectProp(jResult, val, ps2);
            }
            else
            {
                jResult.Add(destProp, JsonNode.Parse(val.ToJsonString()));
            }


        }

        return jResult;
    }

    static JsonNode AddPropToExsistingObjectProp(JsonNode? val, string[] p, JsonNode? jObj22)
    {
        for (int i2 = 1; i2 < p.Length - 1; i2++)
            jObj22 = jObj22[p[i2]];

        ((JsonObject)jObj22).Add(p[p.Length - 1], JsonNode.Parse(val.ToJsonString()));
        return jObj22;
    }

    static void CreateAndAddObjectProp(JsonNode jRes, JsonNode? val, string[] p)
    {
        var jObj = JsonNode.Parse("{}");
        jObj.Add(p[p.Length - 1], JsonNode.Parse(val.ToJsonString()));

        for (int i2 = p.Length - 2; i2 > 0; i2--)
        {
            var _jObj = JsonNode.Parse("{}");
            _jObj.Add(p[i2], jObj);
            jObj = _jObj;
        }

        jRes.Add(p[0], jObj);
    }




    //static void JsonToDic(Dictionary<string, (JsonTokenType Type, string Val)> _dic, string jsonObj, string parent)
    //{
    //    var dictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement?>>(jsonObj);

    //    foreach (var jProp in dictionary)
    //    {
    //        var k = jProp.Key;
    //        var v = jProp.Value;
    //        var isObject = v.Value.ValueKind == JsonValueKind.Object;
    //        var isArray = v.Value.ValueKind == JsonValueKind.Array;
    //        if (!isObject && !isArray)
    //            _dic.Add(parent + k, (JsonTokenType.String, v.ToString()));
    //        else
    //        {
    //            if (isArray)
    //            {
    //                //var arr = JsonArray.Create(v.Value);
    //                //for (int i = 0; i < arr.Count; i++)
    //                //{
    //                //    _dic.Add(parent + k + "[" + i + "]", (JsonTokenType.StartObject, JsonArray.Create(v.Value)[i].ToString()));
    //                //}
    //                _dic.Add(parent + k, (JsonTokenType.StartArray, v.ToString()));
    //            }
    //            else
    //                JsonToDic(_dic, v.ToString(), parent + k + ".");
    //        }
    //    }
    //}


}
