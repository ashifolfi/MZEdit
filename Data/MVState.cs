using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVState
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("iconIndex")] public int IconIndex;
    [JsonProperty("name")] public string Name;
    [JsonProperty("note")] public string Note;

    [JsonProperty("message1")] public string Message1;
    [JsonProperty("message2")] public string Message2;
    [JsonProperty("message3")] public string Message3;
    [JsonProperty("message4")] public string Message4;

    [JsonProperty("traits")] public List<MVTrait> Traits;

    [JsonProperty("releaseByDamage")] public bool ReleaseByDamage;
    [JsonProperty("removeAtBattleEnd")] public bool RemoveAtBattleEnd;
    [JsonProperty("removeByDamage")] public bool RemoveByDamage;
    [JsonProperty("removeByRestriction")] public bool RemoveByRestriction;
    [JsonProperty("removeByWalking")] public bool RemoveByWalking;

    [JsonProperty("minTurns")] public int MinTurns;
    [JsonProperty("maxTurns")] public int MaxTurns;
    [JsonProperty("restriction")] public int Restriction;
    [JsonProperty("stepsToRemove")] public int StepsToRemove;
    [JsonProperty("priority")] public int Priority;
    [JsonProperty("overlay")] public int Overlay;
    [JsonProperty("motion")] public int Motion;
    [JsonProperty("autoRemovalTiming")] public int AutoRemovalTiming;
    [JsonProperty("chanceByDamage")] public int ChanceByDamage;
    [JsonProperty("messageType")] public int MessageType;
}
