using Godot;
using System;

namespace MZEdit.UI.Components;

public partial class ActorEditor : Control
{
    [Export] public int ActorID { get; set; }

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
}
