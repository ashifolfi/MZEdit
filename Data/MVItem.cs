using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVItem
{
    public struct SEffect
    {
        [JsonProperty("code")] public int Code;
        [JsonProperty("dataId")] public int DataId;
        [JsonProperty("value1")] public float Value1;
        [JsonProperty("value2")] public float Value2;
    }

    public struct SDamage
    {
        [JsonProperty("critical")] public bool Critical;
        [JsonProperty("elementId")] public int ElementId;
        [JsonProperty("formula")] public string Formula;
        [JsonProperty("type")] public int Type;
        [JsonProperty("variance")] public int Variance;
    }

    [JsonProperty("id")] public int Id;

    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
    [JsonProperty("note")] public string Note;

    [JsonProperty("consumable")] public bool Consumable;
    [JsonProperty("iTypeId")] public int ITypeId;
    [JsonProperty("iconIndex")] public int IconIndex;
    [JsonProperty("occasion")] public int Occasion;
    [JsonProperty("price")] public int Price;
    [JsonProperty("repeats")] public int Repeats;
    [JsonProperty("scope")] public int Scope;
    [JsonProperty("speed")] public int Speed;
    [JsonProperty("successRate")] public int SuccessRate;
    [JsonProperty("tpGain")] public int TpGain;
    [JsonProperty("effects")] public List<SEffect> Effects;
}
