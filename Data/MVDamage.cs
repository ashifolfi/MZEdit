using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVDamage
{
    [JsonProperty("critical")] public bool Critical;
    [JsonProperty("elementId")] public int ElementId;
    [JsonProperty("formula")] public string Formula;
    [JsonProperty("type")] public int Type;
    [JsonProperty("variance")] public int Variance;
}
