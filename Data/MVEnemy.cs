using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MZEdit.Data;

public class MVEnemy
{
    public struct SDropItem
    {
        [JsonProperty("dataId")] public int DataId;
        [JsonProperty("denominator")] public int Denominator;
        [JsonProperty("kind")] public int Kind;
    }

    public struct SAction
    {
        [JsonProperty("conditionParam1")] public float ConditionParam1;
        [JsonProperty("conditionParam2")] public float ConditionParam2;
        [JsonProperty("conditionType")] public int ConditionType;
        [JsonProperty("rating")] public int Rating;
        [JsonProperty("skillId")] public int SkillId;
    }

    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("note")] public string Note;

    [JsonProperty("actions")] public List<SAction> Actions;
    [JsonProperty("dropItems")] public List<SDropItem> DropItems;
    [JsonProperty("traits")] public List<MVTrait> Traits;
    [JsonProperty("params")] public List<int> Params;

    [JsonProperty("gold")] public int Gold;
    [JsonProperty("exp")] public int Exp;

    [JsonProperty("battlerHue")] public int BattlerHue;
    [JsonProperty("battlerName")] public string BattlerName;

}
