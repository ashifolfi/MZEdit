using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MZEdit.Data;

public class MVActor
{
    [JsonProperty("id")] public int Id;
    [JsonProperty("classId")] public int ClassId;

    [JsonProperty("name")] public string Name;
    [JsonProperty("nickname")] public string Nickname;
    [JsonProperty("profile")] public string Profile;
    [JsonProperty("note")] public string Note;

    #region Graphic Names
    [JsonProperty("battlerName")] public string BattlerName;
    [JsonProperty("characterName")] public string CharacterName;
    [JsonProperty("faceName")] public string FaceName;
    #endregion

    #region Graphic Indices
    [JsonProperty("battlerIndex")] public int BattlerIndex;
    [JsonProperty("characterIndex")] public int CharacterIndex;
    [JsonProperty("faceIndex")] public int FaceIndex;
    #endregion

    [JsonProperty("equips")] public List<int> Equips;
    [JsonProperty("initialLevel")] public int InitialLevel;
    [JsonProperty("maxLevel")] public int MaxLevel;
    [JsonProperty("traits")] public List<MVTrait> Traits;
}
