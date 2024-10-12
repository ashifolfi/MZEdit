using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVWeapon
{
    [JsonProperty("id")] public int Id;

    [JsonProperty("iconIndex")] public int IconIndex;
    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
    [JsonProperty("note")] public string Note;

    [JsonProperty("animationId")] public int AnimationId;
    [JsonProperty("wtypeId")] public int WTypeId;
    [JsonProperty("etypeId")] public int ETypeId;
    [JsonProperty("traits")] public List<MVTrait> Traits;
    [JsonProperty("params")] public List<int> Params;
    [JsonProperty("price")] public int Price;
}
