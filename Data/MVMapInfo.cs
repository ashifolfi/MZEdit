using System;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVMapInfo
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("expanded")] public bool Expanded;
    [JsonProperty("order")] public int Order;
    [JsonProperty("parentId")] public int ParentId;
    [JsonProperty("scrollX")] public float ScrollX;
    [JsonProperty("scrollY")] public float ScrollY;
}
