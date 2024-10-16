﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVItem
{
    [JsonProperty("id")] public int Id;

    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
    [JsonProperty("note")] public string Note;

    [JsonProperty("consumable")] public bool Consumable;
    [JsonProperty("iTypeId")] public int ITypeId;
    [JsonProperty("iconIndex")] public int IconIndex;
    [JsonProperty("occasion")] public int Occasion;
    [JsonProperty("price")] public int Price;
    [JsonProperty("repeats")] public int Repeats;
    [JsonProperty("scope")] public int Scope;
    [JsonProperty("speed")] public int Speed;
    [JsonProperty("successRate")] public int SuccessRate;
    [JsonProperty("tpGain")] public int TpGain;
    [JsonProperty("effects")] public List<MVEffect> Effects;
    [JsonProperty("damage")] public MVDamage Damage;
}
