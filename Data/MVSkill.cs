using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVSkill
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
    [JsonProperty("note")] public string Note;

    [JsonProperty("animationId")] public int AnimationId;
    [JsonProperty("iconIndex")] public int IconIndex;

    [JsonProperty("effects")] public List<MVEffect> Effects;
    [JsonProperty("damage")] public MVDamage Damage;
    [JsonProperty("hitType")] public int HitType;

    [JsonProperty("mpCost")] public int MpCost;
    [JsonProperty("tpCost")] public int TpCost;
    [JsonProperty("tpGain")] public int TpGain;

    [JsonProperty("occasion")] public int Occasion;
    [JsonProperty("repeats")] public int Repeats;
    [JsonProperty("scope")] public int Scope;
    [JsonProperty("speed")] public int Speed;
    [JsonProperty("successRate")] public int SuccessRate;
    [JsonProperty("stypeId")] public int STypeId;
    [JsonProperty("requiredWtypeId1")] public int RequiredWTypeId1;
    [JsonProperty("requiredWtypeId2")] public int RequiredWTypeId2;

    [JsonProperty("message1")] public string Message1;
    [JsonProperty("message2")] public string Message2;
    [JsonProperty("messageType")] public int MessageType;
}
