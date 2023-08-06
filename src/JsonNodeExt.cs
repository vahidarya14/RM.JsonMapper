﻿using System.Text.Json;
using System.Text.Json.Nodes;

namespace RM.JsonMapper;

public static class JsonNodeExt
{
    public static TDestination ToObject<TDestination>(this JsonNode node)
    {
        return JsonSerializer.Deserialize<TDestination>(node);
    }


    public static JsonNode? Add(this JsonNode? node, string prop, JsonNode val)
    {
        node[prop] = val;
        return node;
    }
}