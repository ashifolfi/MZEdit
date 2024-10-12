using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVEventCommand
{
    [JsonProperty("code")] public int Code;
    [JsonProperty("indent")] public int? Indent;
    [JsonProperty("parameters")] public List<object> Parameters;
}

public class MVEvent
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("switchId")] public int SwitchId;
    [JsonProperty("trigger")] public int Trigger;
    [JsonProperty("list")] public List<MVEventCommand> List;
}
