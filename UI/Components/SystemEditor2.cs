using Godot;
using System;
using System.Security.Cryptography;

namespace MZEdit.UI.Components;

public partial class SystemEditor2 : Control
{
    [ExportCategory("SubControls (PRIVATE)")]
    [Export] private Tree AdvancedOptionsTree;
    [Export] private Tree AttackMotionTree;
    [Export] private Tree MagicSkillsTree;
    [Export] private CheckBox cmd1;
    [Export] private CheckBox cmd2;
    [Export] private CheckBox cmd3;
    [Export] private CheckBox cmd4;
    [Export] private CheckBox cmd5;
    [Export] private CheckBox cmd6;
    [Export] private CheckBox item1;
    [Export] private CheckBox item2;
    [Export] private CheckBox item3;
    [Export] private CheckBox item4;

    public override void _Ready()
    {
        EditorMain.Instance.OnProjectLoaded += OnProjectLoad;

        AdvancedOptionsTree.HideRoot = true;
        AdvancedOptionsTree.CreateItem();
        AdvancedOptionsTree.SetColumnTitle(0, Tr("Name"));
        AdvancedOptionsTree.SetColumnTitle(1, Tr("Value"));

        MagicSkillsTree.HideRoot = true;
        MagicSkillsTree.CreateItem();
        MagicSkillsTree.SetColumnTitle(0, Tr("Skill Type"));

        AttackMotionTree.HideRoot = true;
        AttackMotionTree.CreateItem();
        AttackMotionTree.SetColumnTitle(0, Tr("Type"));
        AttackMotionTree.SetColumnTitle(1, Tr("Motion"));
        AttackMotionTree.SetColumnTitle(2, Tr("Image"));

        {
            var itemNames = new string[]
            {
                Tr("Game ID"),
                Tr("Screen Width"),
                Tr("Screen Height"),
                Tr("UI Area Width"),
                Tr("UI Area Height"),
                Tr("Main Font Filename"),
                Tr("Number Font Filename"),
                Tr("Fallback Fonts"),
                Tr("Font Size"),
            };
            foreach (var name in itemNames)
            {
                var item = AdvancedOptionsTree.CreateItem();
                item.SetText(0, name);
                item.SetEditable(1, true);
            }
        }
    }

    private void OnProjectLoad()
    {
        {
            var root = AdvancedOptionsTree.GetRoot();
            var adv = EditorMain.Instance.SystemData.Advanced;

            root.GetChild(0).SetText(1, Convert.ToString(adv.GameId));
            root.GetChild(1).SetText(1, Convert.ToString(adv.ScreenWidth));
            root.GetChild(2).SetText(1, Convert.ToString(adv.ScreenHeight));
            root.GetChild(3).SetText(1, Convert.ToString(adv.UiAreaWidth));
            root.GetChild(4).SetText(1, Convert.ToString(adv.UiAreaHeight));
            root.GetChild(5).SetText(1, adv.MainFontFilename);
            root.GetChild(6).SetText(1, adv.NumberFontFilename);
            root.GetChild(7).SetText(1, adv.FallbackFonts);
            root.GetChild(8).SetText(1, Convert.ToString(adv.FontSize));
        }

        cmd1.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[0];
        cmd2.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[1];
        cmd3.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[2];
        cmd4.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[3];
        cmd5.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[4];
        cmd6.ButtonPressed = EditorMain.Instance.SystemData.MenuCommands[5];

        item1.ButtonPressed = EditorMain.Instance.SystemData.ItemCategories[0];
        item2.ButtonPressed = EditorMain.Instance.SystemData.ItemCategories[1];
        item3.ButtonPressed = EditorMain.Instance.SystemData.ItemCategories[2];
        item4.ButtonPressed = EditorMain.Instance.SystemData.ItemCategories[3];
    }

    private void OnAdvTreeEdit()
    {
        var item = AdvancedOptionsTree.GetSelected();

        switch (item.GetIndex())
        {
            case 0: // Game ID
                EditorMain.Instance.SystemData.Advanced.GameId = Convert.ToInt32(item.GetText(1));
                break;
            case 1: // Screen Width
                EditorMain.Instance.SystemData.Advanced.ScreenWidth = Convert.ToInt32(item.GetText(1));
                break;
            case 2: // Screen Height
                EditorMain.Instance.SystemData.Advanced.ScreenHeight = Convert.ToInt32(item.GetText(1));
                break;
            case 3: // UI Area Width
                EditorMain.Instance.SystemData.Advanced.UiAreaWidth = Convert.ToInt32(item.GetText(1));
                break;
            case 4: // UI Area Height
                EditorMain.Instance.SystemData.Advanced.UiAreaHeight = Convert.ToInt32(item.GetText(1));
                break;
            case 5: // Main Font Filename
                EditorMain.Instance.SystemData.Advanced.MainFontFilename = item.GetText(1);
                break;
            case 6: // Number Font Filename
                EditorMain.Instance.SystemData.Advanced.NumberFontFilename = item.GetText(1);
                break;
            case 7: // Fallback Fonts
                EditorMain.Instance.SystemData.Advanced.FallbackFonts = item.GetText(1);
                break;
            case 8: // Font Size
                EditorMain.Instance.SystemData.Advanced.FontSize = Convert.ToInt32(item.GetText(1));
                break;
        }
    }

    private void OnCmd1Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[0] = toggled;
    private void OnCmd2Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[1] = toggled;
    private void OnCmd3Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[2] = toggled;
    private void OnCmd4Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[3] = toggled;
    private void OnCmd5Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[4] = toggled;
    private void OnCmd6Changed(bool toggled) => EditorMain.Instance.SystemData.MenuCommands[5] = toggled;

    private void OnItem1Changed(bool toggled) => EditorMain.Instance.SystemData.ItemCategories[0] = toggled;
    private void OnItem2Changed(bool toggled) => EditorMain.Instance.SystemData.ItemCategories[1] = toggled;
    private void OnItem3Changed(bool toggled) => EditorMain.Instance.SystemData.ItemCategories[2] = toggled;
    private void OnItem4Changed(bool toggled) => EditorMain.Instance.SystemData.ItemCategories[3] = toggled;
}
