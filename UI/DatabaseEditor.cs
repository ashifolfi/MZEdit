using Godot;
using System;
using log4net;

namespace MZEdit.UI;

public partial class DatabaseEditor : Window
{
    private static readonly ILog Log = LogManager.GetLogger("DatabaseEditor");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Hide();
	}

	private void OnClose()
	{
		Hide();
	}
}
