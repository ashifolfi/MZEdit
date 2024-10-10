using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVClass
{
    public struct SLearning
    {
        [JsonProperty("level")] public int Level;
        [JsonProperty("note")] public string Note;
        [JsonProperty("skillId")] public int SkillId;
    }

    [JsonProperty("id")] public int Id;

    [JsonProperty("name")] public string Name;
    [JsonProperty("note")] public string Note;

    [JsonProperty("traits")] public List<MVTrait> Traits;
    [JsonProperty("learnings")] public List<SLearning> Learnings;

    [JsonProperty("expParams")] public List<int> ExpParams;
    [JsonProperty("params")] public List<List<int>> Params;
}
