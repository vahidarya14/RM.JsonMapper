﻿using System.Text.Json;
using System.Text.Json.Nodes;

namespace RM.JsonMapper;

public static class JsonNodeExt
{
    public static TDestination ToObject<TDestination>(this JsonNode node)
    {
        try
        {
            return JsonSerializer.Deserialize<TDestination>(node);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public static JsonNode? Add(this JsonNode? node, string prop, JsonNode val)
    {
        try
        {
            node[prop] = val;
        }
        catch (Exception)
        {
            node[prop] = JsonValue.Create(val.ToString());// "--error";
        }

        return node;
    }
}
