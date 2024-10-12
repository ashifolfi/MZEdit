using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace MZEdit.Data;

/// <summary>
/// System Database Structure
/// </summary>
public class MVSystem
{
    #region Substructures
    /// <summary>
    /// Terms Substructure
    /// </summary>
    public struct STerms
    {
        [JsonProperty("basic")] public List<string> Basic;
        [JsonProperty("params")] public List<string> Params;
        [JsonProperty("commands")] public List<string?> Commands;
        [JsonProperty("messages")] public Dictionary<string, string> Messages;
    }

    /// <summary>
    /// Advanced Substructure
    /// 
    /// Contains screen size and font options
    /// </summary>
    public struct SAdvanced
    {
        [JsonProperty("gameId")] public int GameId;
        
        [JsonProperty("screenWidth")] public int ScreenWidth;
        [JsonProperty("screenHeight")] public int ScreenHeight;
        [JsonProperty("uiAreaWidth")] public int UiAreaWidth;
        [JsonProperty("uiAreaHeight")] public int UiAreaHeight;

        [JsonProperty("mainFontFilename")] public string MainFontFilename;
        [JsonProperty("numberFontFilename")] public string NumberFontFilename;
        [JsonProperty("fallbackFonts")] public string FallbackFonts;
        [JsonProperty("fontSize")] public int FontSize;

        [JsonProperty("screenScale")] public int ScreenScale;
        [JsonProperty("windowOpacity")] public int WindowOpacity;
    }

    /// <summary>
    /// Attack Motion Substructure
    /// </summary>
    public struct SAttackMotion
    {
        [JsonProperty("type")] public int Type;
        [JsonProperty("weaponImageId")] public int WeaponImageId;
    }

    /// <summary>
    /// Test Battler Substructure
    /// 
    /// For test battle mode party members
    /// </summary>
    public struct STestBattler
    {
        [JsonProperty("actorId")] public int ActorId;
        [JsonProperty("level")] public int Level;
        [JsonProperty("equips")] public List<int> Equips;
    }
    
    /// <summary>
    /// Title Command Window Substructure
    /// 
    /// Contains the offset and background setting for the title menu
    /// </summary>
    public struct STitleCmdWin
    {
        [JsonProperty("background")] public int Background;
        [JsonProperty("offsetX")] public int OffsetX;
        [JsonProperty("offsetY")] public int OffsetY;
    }

    /// <summary>
    /// Vehicle Substructure
    /// 
    /// Used to define the vehicles ship, boat, and airship
    /// Contains a unique start position, character info, and BGM
    /// </summary>
    public struct SVehicle
    {
        [JsonProperty("bgm")] public MVSound Bgm;
        [JsonProperty("characterIndex")] public int CharacterIndex;
        [JsonProperty("characterName")] public string CharacterName;
        [JsonProperty("startMapId")] public int StartMapId;
        [JsonProperty("startX")] public int StartX;
        [JsonProperty("startY")] public int StartY;
    }
    #endregion

    [JsonProperty("versionId")] public int VersionId;
    [JsonProperty("editMapId")] public int EditMapId;

    #region Options
    [JsonProperty("windowTone")] public float[] WindowTone;
    [JsonProperty("menuCommands")] public List<bool> MenuCommands;
    [JsonProperty("itemCategories")] public List<bool> ItemCategories;
    [JsonProperty("optSplashScreen")] public bool OptSplashScreen;
    [JsonProperty("optMessageSkip")] public bool OptMessageSkip;
    [JsonProperty("optAutosave")] public bool OptAutosave;
    [JsonProperty("optDisplayTp")] public bool OptDisplayTp;
    [JsonProperty("optDrawTitle")] public bool OptDrawTitle;
    [JsonProperty("optExtraExp")] public bool OptExtraExp;
    [JsonProperty("optFloorDeath")] public bool OptFloorDeath;
    [JsonProperty("optFollowers")] public bool OptFollowers;
    [JsonProperty("optKeyItemsNumber")] public bool OptKeyItemsNumber;
    [JsonProperty("optSideView")] public bool OptSideView;
    [JsonProperty("optSlipDeath")] public bool OptSlipDeath;
    [JsonProperty("optTransparent")] public bool OptTransparent;
    [JsonProperty("partyMembers")] public List<int> PartyMembers;
    [JsonProperty("magicSkills")] public List<int> MagicSkills;
    [JsonProperty("attackMotions")] public List<SAttackMotion> AttackMotions;

    [JsonProperty("tileSize")] public int TileSize;
    [JsonProperty("locale")] public string Locale;
    [JsonProperty("advanced")] public SAdvanced Advanced;
    #endregion

    #region Title
    [JsonProperty("gameTitle")] public string GameTitle;
    [JsonProperty("title1Name")] public string Title1Name;
    [JsonProperty("title2Name")] public string Title2Name;
    [JsonProperty("titleBgm")] public MVSound TitleBgm;
    [JsonProperty("titleCommandWindow")] public STitleCmdWin TitleCommandWindow;
    #endregion

    #region Start Point
    [JsonProperty("startMapId")] public int StartMapId;
    [JsonProperty("startX")] public int StartX;
    [JsonProperty("startY")] public int StartY;
    #endregion

    #region Vehicles
    [JsonProperty("ship")] public SVehicle Ship;
    [JsonProperty("boat")] public SVehicle Boat;
    [JsonProperty("airship")] public SVehicle Airship;
    #endregion

    #region Battle Data
    [JsonProperty("battleback1Name")] public string Battleback1Name;
    [JsonProperty("battleback2Name")] public string Battleback2Name;
    [JsonProperty("battlerHue")] public int BattlerHue;
    [JsonProperty("battlerName")] public string BattlerName;
    [JsonProperty("battleSystem")] public int BattleSystem;
    [JsonProperty("testTroopId")] public int TestTroopId;
    [JsonProperty("testBattlers")] public List<STestBattler> TestBattlers;
    #endregion

    #region Sounds & Music
    [JsonProperty("sounds")] public List<MVSound> Sounds;
    [JsonProperty("victoryMe")] public MVSound VictoryMe;
    [JsonProperty("defeatMe")] public MVSound DefeatMe;
    [JsonProperty("gameoverMe")] public MVSound GameoverMe;
    [JsonProperty("battleBgm")] public MVSound BattleBgm;
    #endregion

    #region Words
    [JsonProperty("variables")] public List<string> Variables;
    [JsonProperty("switches")] public List<string> Switches;
    [JsonProperty("skillTypes")] public List<string> SkillTypes;
    [JsonProperty("weaponTypes")] public List<string> WeaponTypes;
    [JsonProperty("armorTypes")] public List<string> ArmorTypes;
    [JsonProperty("elements")] public List<string> Elements;
    [JsonProperty("equipTypes")] public List<string> EquipTypes;
    [JsonProperty("currencyUnit")] public string CurrencyUnit;
    [JsonProperty("terms")] public STerms Terms;
    #endregion
}
