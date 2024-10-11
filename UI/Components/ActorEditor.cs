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

    [Export] public TextureRect FieldFace;
    [Export] public TextureRect FieldCharacter;
    [Export] public TextureRect FieldBattler;
    [Export] public LineEdit FieldName;
    [Export] public LineEdit FieldNickName;
    [Export] public OptionButton FieldClass;
    [Export] public SpinBox FieldInitialLevel;
    [Export] public SpinBox FieldMaxLevel;
    [Export] public TextEdit FieldProfile;
    [Export] public TextEdit FieldNote;
    
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
