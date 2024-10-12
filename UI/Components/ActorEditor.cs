using Godot;
using MZEdit.Data;
using System;

namespace MZEdit.UI.Components;

public partial class ActorEditor : Control
{
    public int ActorID
    {
        get => m_Actor.Id;
        set
        {
            m_Actor = EditorMain.Instance.Actors.Find((e) => e != null && e.Id == value);
            RefreshInterface();
        }
    }
    private MVActor m_Actor;

    public Action ActorEdited;

    [ExportCategory("SubControls (PRIVATE)")]
    [Export] private TextureRect FieldFace;
    [Export] private TextureRect FieldCharacter;
    [Export] private TextureRect FieldBattler;
    [Export] private LineEdit FieldName;
    [Export] private LineEdit FieldNickName;
    [Export] private OptionButton FieldClass;
    [Export] private SpinBox FieldInitialLevel;
    [Export] private SpinBox FieldMaxLevel;
    [Export] private TextEdit FieldProfile;
    [Export] private TextEdit FieldNote;
    [Export] private Tree TraitsTree;
    [Export] private Tree EquipmentTree;

    public override void _Ready()
    {
        TraitsTree.HideRoot = true;
        TraitsTree.CreateItem();
        TraitsTree.SetColumnTitle(0, Tr("Type"));
        TraitsTree.SetColumnTitle(1, Tr("Content"));

        EquipmentTree.HideRoot = true;
        EquipmentTree.CreateItem();
        EquipmentTree.SetColumnTitle(0, Tr("Type"));
        EquipmentTree.SetColumnTitle(1, Tr("Equipment Item"));

        {
            var itemNames = new string[]
            {
                Tr("Weapon"),
                Tr("Shield"),
                Tr("Head"),
                Tr("Body"),
                Tr("Accessory")
            };
            foreach (var itemName in itemNames)
            {
                var item = EquipmentTree.CreateItem();
                item.SetText(0, itemName);
                item.SetText(1, "None");
            }
        }
    }

    public void RefreshInterface()
    {
        FieldName.Text = m_Actor.Name;
        FieldNickName.Text = m_Actor.Nickname;

        FieldClass.Clear();
        foreach (var classInf in EditorMain.Instance.Classes)
        {
            if (classInf != null)
            {
                FieldClass.AddItem(classInf.Name, classInf.Id);
            }
        }
        FieldClass.Select(m_Actor.ClassId - 1);

        FieldInitialLevel.Value = m_Actor.InitialLevel;
        FieldMaxLevel.Value = m_Actor.MaxLevel;
        FieldProfile.Text = m_Actor.Profile;
        FieldNote.Text = m_Actor.Note;
    }

    private void OnInitialLevelChanged(float value)
    {
        m_Actor.InitialLevel = (int)value;
        ActorEdited?.Invoke();
    }

    private void OnMaxLevelChanged(float value)
    {
        m_Actor.MaxLevel = (int)value;
        ActorEdited?.Invoke();
    }

    private void OnClassChanged(int index)
    {
        m_Actor.ClassId = FieldClass.GetItemId(index);
        ActorEdited?.Invoke();
    }

    private void OnNameChanged(string newText)
    {
        m_Actor.Name = newText;
        ActorEdited?.Invoke();
    }

    private void OnNicknameChanged(string newText)
    {
        m_Actor.Nickname = newText;
        ActorEdited?.Invoke();
    }

    private void OnProfileChanged()
    {
        m_Actor.Profile = FieldProfile.Text;
        ActorEdited?.Invoke();
    }

    private void OnNoteChanged()
    {
        m_Actor.Note = FieldNote.Text;
        ActorEdited?.Invoke();
    }
}
