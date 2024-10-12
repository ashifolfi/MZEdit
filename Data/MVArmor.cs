using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVArmor
{
    [JsonProperty("id")] public int Id;

    [JsonProperty("iconIndex")] public int IconIndex;
    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
    [JsonProperty("note")] public string Note;

    [JsonProperty("atypeId")] public int ATypeId;
    [JsonProperty("etypeId")] public int ETypeId;
    [JsonProperty("traits")] public List<MVTrait> Traits;
    [JsonProperty("params")] public List<int> Params;
    [JsonProperty("price")] public int Price;
}
