using Godot;
using log4net;
using System;

namespace MZEdit.UI;

public partial class MainWindow : Control
{
	private static readonly ILog Log = LogManager.GetLogger("MainWindow");

	[Export] private FileDialog OpenProjDialog;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Log.Info("MainWindow Ready!");
	}

	public void OnOpenPressed()
	{
		OpenProjDialog.PopupCentered(new Vector2I(640, 480));
	}

	public void OnOpenFilePicked(string path)
	{
		EditorMain.Instance.LoadProject(path);
		GetViewport().GetWindow().Title = $"MZEdit - {EditorMain.Instance.SystemData.GameTitle}";
	}
}
