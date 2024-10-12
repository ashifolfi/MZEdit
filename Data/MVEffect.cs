using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVEffect
{
    [JsonProperty("code")] public int Code;
    [JsonProperty("dataId")] public int DataId;
    [JsonProperty("value1")] public float Value1;
    [JsonProperty("value2")] public float Value2;
}
