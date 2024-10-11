using Godot;
using log4net;
using System;

namespace MZEdit.UI;

public partial class MainWindow : Control
{
	private static readonly ILog Log = LogManager.GetLogger("MainWindow");

	[Export] private FileDialog OpenProjDialog;
	[Export] private DatabaseEditor DatabaseEditor;
	[Export] private Window AboutWindow;
	[Export] private ResourceManager ResourceViewer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Log.Info("MainWindow Ready!");
		AboutWindow.Hide();
	}

	public void OnOpenDatabase()
	{
		DatabaseEditor.PopupCentered();
	}

	public void OnOpenPressed()
	{
		OpenProjDialog.PopupCentered(new Vector2I(640, 480));
	}

	public void OnSavePressed()
	{
		EditorMain.Instance.SaveProject();
	}

	public void OnOpenFilePicked(string path)
	{
		EditorMain.Instance.LoadProject(path);
		GetViewport().GetWindow().Title = $"MZEdit - {EditorMain.Instance.SystemData.GameTitle}";
	}

	public void OnHelpMenuIdPressed(int id)
	{
		switch(id)
		{
			case 0: // MZEdit Documentation
				OS.ShellOpen("https://github.com/ashifolfi/MZEdit/wiki");
				break;
			case 2: // RPG Maker MZ Documentation
				OS.ShellOpen("https://rpgmakerofficial.com/product/MZ_help-en/#t=01.html");
				break;
			case 1: // About MZEdit
				AboutWindow.PopupCentered();
				break;
		}
	}

	public void OnFileMenuPressed(int id)
	{
		switch(id)
		{
			case 0: // New Project
				break;
			case 1: // Open Project
				OnOpenPressed();
				break;
			case 2: // Save Project
				OnSavePressed();
				break;
			case 3: // Close Project
				break;
			case 4: // Quit
				GetTree().Quit();
				break;
		}
	}

    public void OnToolsMenuPressed(int id)
    {
		switch(id)
		{
			case 0: // Database
				OnOpenDatabase();
				break;
			case 1: // Plugins
				break;
			case 2: // Sound test
				break;
			case 3: // Resource Manager
				ResourceViewer.PopupCentered();
				break;
        }
    }
}
