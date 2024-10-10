using Godot;
using log4net;
using System;

namespace MZEdit;

public partial class MainWindow : Control
{
	private static readonly ILog Log = LogManager.GetLogger("MainWindow");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Log.Info("MainWindow Ready!");
	}
}
