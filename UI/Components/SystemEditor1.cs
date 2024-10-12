using Godot;
using System;

namespace MZEdit.UI.Components;

public partial class SystemEditor1 : Control
{
    [ExportCategory("SubControls (PRIVATE)")]
    [Export] private Tree MusicTree;
    [Export] private Tree SoundTree;
    [Export] private Tree PartyTree;
    [Export] private LineEdit Currency;
    [Export] private ColorPickerButton WindowColor;
    [Export] private CharacterButton BoatButton;
    [Export] private CharacterButton ShipButton;
    [Export] private CharacterButton AirshipButton;
    [ExportGroup("Options")]
    [Export] private CheckButton optAutosave;
    [Export] private CheckButton optDisplayTp;
    [Export] private CheckButton optExtraExp;
    [Export] private CheckButton optFloorDeath;
    [Export] private CheckButton optFollowers;
    [Export] private CheckButton optKeyItemsNumber;
    [Export] private CheckButton optSlipDeath;
    [Export] private CheckButton optTransparent;
    [ExportGroup("Battle")]
    [Export] private OptionButton battleSystem;
    [Export] private OptionButton battleScreen;
    [ExportGroup("Title")]
    [Export] private LineEdit gameTitle;
    [Export] private CheckButton optDrawTitle;

    public override void _Ready()
    {
        EditorMain.Instance.OnProjectLoaded += OnProjectLoad;

        MusicTree.HideRoot = true;
        MusicTree.CreateItem();
        MusicTree.SetColumnTitle(0, Tr("Type"));
        MusicTree.SetColumnTitle(1, Tr("Filename"));
        
        SoundTree.HideRoot = true;
        SoundTree.CreateItem();
        SoundTree.SetColumnTitle(0, Tr("Type"));
        SoundTree.SetColumnTitle(1, Tr("Filename"));

        PartyTree.HideRoot = true;
        PartyTree.CreateItem();
        PartyTree.SetColumnTitle(0, Tr("Actor"));

        // Populate sound tree
        {
            var nameList = new string[]
            {
                Tr("Cursor"),
                Tr("OK"),
                Tr("Cancel"),
                Tr("Buzzer"),
                Tr("Equip"),
                Tr("Save"),
                Tr("Load"),
                Tr("Battle Start"),
                Tr("Escape"),
                Tr("Enemy Attack"),
                Tr("Enemy Damage"),
                Tr("Enemy Collapse"),
                Tr("Boss Collapse 1"),
                Tr("Boss Collapse 2"),
                Tr("Ally Damage"),
                Tr("Ally Death"),
                Tr("Recovery"),
                Tr("Miss"),
                Tr("Evasion"),
                Tr("Magic Evasion"),
                Tr("Magic Reflection"),
                Tr("Shop"),
                Tr("Use Item"),
                Tr("Use Skill")
            };
            foreach (var name in nameList)
            {
                var item = SoundTree.CreateItem();
                item.SetText(0, name);
                item.SetText(1, "None");
            }
        }

        // Populate music tree
        {
            var nameList = new string[]
            {
                Tr("Title"),
                Tr("Battle"),
                Tr("Victory"),
                Tr("Defeat"),
                Tr("Game Over"),
                Tr("Boat"),
                Tr("Ship"),
                Tr("Airship")
            };
            foreach (var name in nameList)
            {
                var item = MusicTree.CreateItem();
                item.SetText(0, name);
                item.SetText(1, "None");
            }
        }

    }

    private void OnProjectLoad()
    {
        // set sound tree
        {
            int i = 0;
            foreach (var soundinf in EditorMain.Instance.SystemData.Sounds)
            {
                SoundTree.GetRoot().GetChild(i).SetText(1, soundinf.name);
                i++;
            }
        }

        // set music tree
        {
            var root = MusicTree.GetRoot();
            var sysdat = EditorMain.Instance.SystemData;

            root.GetChild(0).SetText(1, sysdat.TitleBgm.name);
            root.GetChild(1).SetText(1, sysdat.BattleBgm.name);
            root.GetChild(2).SetText(1, sysdat.VictoryMe.name);
            root.GetChild(3).SetText(1, sysdat.DefeatMe.name);
            root.GetChild(4).SetText(1, sysdat.GameoverMe.name);
            root.GetChild(5).SetText(1, sysdat.Boat.Bgm.name);
            root.GetChild(6).SetText(1, sysdat.Ship.Bgm.name);
            root.GetChild(7).SetText(1, sysdat.Airship.Bgm.name);
        }

        // set option toggle states
        optAutosave.ButtonPressed = EditorMain.Instance.SystemData.OptAutosave;
        optDisplayTp.ButtonPressed = EditorMain.Instance.SystemData.OptDisplayTp;
        optExtraExp.ButtonPressed = EditorMain.Instance.SystemData.OptExtraExp;
        optFloorDeath.ButtonPressed = EditorMain.Instance.SystemData.OptFloorDeath;
        optFollowers.ButtonPressed = EditorMain.Instance.SystemData.OptFollowers;
        optKeyItemsNumber.ButtonPressed = EditorMain.Instance.SystemData.OptKeyItemsNumber;
        optSlipDeath.ButtonPressed = EditorMain.Instance.SystemData.OptSlipDeath;
        optTransparent.ButtonPressed = EditorMain.Instance.SystemData.OptTransparent;
        optDrawTitle.ButtonPressed = EditorMain.Instance.SystemData.OptDrawTitle;

        battleScreen.Selected = EditorMain.Instance.SystemData.OptSideView ? 1 : 0;
        battleSystem.Selected = EditorMain.Instance.SystemData.BattleSystem;

        gameTitle.Text = EditorMain.Instance.SystemData.GameTitle;
        Currency.Text = EditorMain.Instance.SystemData.CurrencyUnit;

        // the range is -255 - 255 which is... what the fuck?
        WindowColor.Color = new Color(
            EditorMain.Instance.SystemData.WindowTone[0] / 255,
            EditorMain.Instance.SystemData.WindowTone[1] / 255,
            EditorMain.Instance.SystemData.WindowTone[2] / 255,
            EditorMain.Instance.SystemData.WindowTone[3] / 255
        );

        // set vehicle graphics
        BoatButton.SetCharacter(
            EditorMain.Instance.SystemData.Boat.CharacterName,
            EditorMain.Instance.SystemData.Boat.CharacterIndex
        );
        ShipButton.SetCharacter(
            EditorMain.Instance.SystemData.Ship.CharacterName,
            EditorMain.Instance.SystemData.Ship.CharacterIndex
        );
        AirshipButton.SetCharacter(
            EditorMain.Instance.SystemData.Airship.CharacterName,
            EditorMain.Instance.SystemData.Airship.CharacterIndex
        );
    }

    private void OnWindowToneChanged(Color color)
    {
        EditorMain.Instance.SystemData.WindowTone = new float[] { color.R * 255, color.G * 255, color.B * 255, color.A * 255 };
    }

    private void OnGameTitleChanged(string newText)
    {
        EditorMain.Instance.SystemData.GameTitle = newText;
    }

    private void OnCurrencyChanged(string newText)
    {
        EditorMain.Instance.SystemData.CurrencyUnit = newText;
    }

    private void OnOptAutosaveChanged(bool toggled) => EditorMain.Instance.SystemData.OptAutosave = toggled;
    private void OnOptDisplayTpChanged(bool toggled) => EditorMain.Instance.SystemData.OptDisplayTp = toggled;
    private void OnOptExtraExpChanged(bool toggled) => EditorMain.Instance.SystemData.OptExtraExp = toggled;
    private void OnOptFloorDeathChanged(bool toggled) => EditorMain.Instance.SystemData.OptFloorDeath = toggled;
    private void OnOptFollowersChanged(bool toggled) => EditorMain.Instance.SystemData.OptFollowers = toggled;
    private void OnOptKeyItemsNumberChanged(bool toggled) => EditorMain.Instance.SystemData.OptKeyItemsNumber = toggled;
    private void OnOptSlipDeathChanged(bool toggled) => EditorMain.Instance.SystemData.OptSlipDeath = toggled;
    private void OnOptTransparentChanged(bool toggled) => EditorMain.Instance.SystemData.OptTransparent = toggled;
    private void OnOptDrawTitleChanged(bool toggled) => EditorMain.Instance.SystemData.OptDrawTitle = toggled;

    private void OnBattleSystemChanged(int item)
    {
        EditorMain.Instance.SystemData.BattleSystem = item;
    }

    private void OnBattleScreenChanged(int item)
    {
        EditorMain.Instance.SystemData.OptSideView = item == 0;
    }
}
