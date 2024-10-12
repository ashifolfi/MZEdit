using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVTroop
{
    public struct STroopMember
    {
        [JsonProperty("enemyId")] public int EnemyId;
        [JsonProperty("x")] public int X;
        [JsonProperty("y")] public int Y;
        [JsonProperty("hidden")] public bool Hidden;
    }

    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("members")] public List<STroopMember> Members;
    // todo: Pages element
}
