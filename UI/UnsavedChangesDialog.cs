using Godot;
using System;

namespace MZEdit.UI;

[GlobalClass]
public partial class UnsavedChangesDialog : AcceptDialog
{
    [Signal] public delegate void OnNoSavePressedEventHandler();

    public override void _Ready()
    {
        OkButtonText = Tr("Save");
        AddButton(Tr("Don't Save"), true, "nosave");
        AddCancelButton(Tr("Cancel"));

        CustomAction += UnsavedChangesDialog_CustomAction;
    }

    private void UnsavedChangesDialog_CustomAction(StringName action)
    {
        if (action == "nosave")
        {
            EmitSignal(SignalName.OnNoSavePressed);
        }
    }
}
