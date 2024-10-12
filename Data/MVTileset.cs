using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVTileset
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("note")] public string Note;

    [JsonProperty("mode")] public int Mode;
    [JsonProperty("flags")] public List<int> Flags;
    [JsonProperty("tilesetNames")] public List<string> TilesetNames;
}
