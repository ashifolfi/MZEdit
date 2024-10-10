using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public struct MVTrait
{
    [JsonProperty("code")] public int Code;
    [JsonProperty("dataId")] public int DataId;
    [JsonProperty("value")] public float Value;
}
