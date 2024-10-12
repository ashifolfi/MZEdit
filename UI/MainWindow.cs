using Godot;
using log4net;
using MZEdit.UI.Components;
using System;
using System.Runtime.CompilerServices;

namespace MZEdit.UI;

public partial class MainWindow : Control
{
	private static readonly ILog Log = LogManager.GetLogger("MainWindow");

	[ExportCategory("SubControls (PRIVATE)")]
	[Export] private MenuBar MainMenubar;
	[Export] private HBoxContainer MainToolbar;
	[Export] private MapList MapList;
	[ExportGroup("Dialogs")]
	[Export] private FileDialog OpenProjDialog;
	[Export] private DatabaseEditor DatabaseEditor;
	[Export] private Window AboutWindow;
	[Export] private ResourceManager ResourceViewer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Log.Info("MainWindow Ready!");
		AboutWindow.Hide();
		NoProjectUI();

		EditorMain.Instance.OnProjectLoaded += OnProjectLoad;
	}


	/// <summary>
	/// Changes the UI state into no project mode
	/// </summary>
	private void NoProjectUI()
	{
		DatabaseEditor.Hide();
		ResourceViewer.Hide();

        // disable certain menu items
        {
            var menu = MainMenubar.GetNode<PopupMenu>("File");
			menu.SetItemDisabled(menu.GetItemIndex(2), true);
            menu.SetItemDisabled(menu.GetItemIndex(3), true);
        }

		{
            var menu = MainMenubar.GetNode<PopupMenu>("Tools");
            menu.SetItemDisabled(menu.GetItemIndex(0), true);
            menu.SetItemDisabled(menu.GetItemIndex(1), true);
            menu.SetItemDisabled(menu.GetItemIndex(2), true);
            menu.SetItemDisabled(menu.GetItemIndex(3), true);
        }

		MainToolbar.GetNode<Button>("btnSave").Disabled = true;
        MainToolbar.GetNode<Button>("btnUndo").Disabled = true;
        MainToolbar.GetNode<Button>("btnCut").Disabled = true;
        MainToolbar.GetNode<Button>("btnCopy").Disabled = true;
        MainToolbar.GetNode<Button>("btnPaste").Disabled = true;
        MainToolbar.GetNode<Button>("btnModeTiles").Disabled = true;
        MainToolbar.GetNode<Button>("btnModeEvents").Disabled = true;
        MainToolbar.GetNode<Button>("btnZoomIn").Disabled = true;
        MainToolbar.GetNode<Button>("btnZoomOut").Disabled = true;
        MainToolbar.GetNode<Button>("btnZoom11").Disabled = true;
        MainToolbar.GetNode<Button>("btnDatabase").Disabled = true;
        MainToolbar.GetNode<Button>("btnPlugins").Disabled = true;
        MainToolbar.GetNode<Button>("btnSound").Disabled = true;
        MainToolbar.GetNode<Button>("btnSearch").Disabled = true;
        MainToolbar.GetNode<Button>("btnTestPlay").Disabled = true;
    }

    private void OnProjectLoad()
    {
        DatabaseEditor.Hide();
        ResourceViewer.Hide();

        // disable certain menu items
        {
            var menu = MainMenubar.GetNode<PopupMenu>("File");
            menu.SetItemDisabled(menu.GetItemIndex(2), false);
            menu.SetItemDisabled(menu.GetItemIndex(3), false);
        }

        {
            var menu = MainMenubar.GetNode<PopupMenu>("Tools");
            menu.SetItemDisabled(menu.GetItemIndex(0), false);
            menu.SetItemDisabled(menu.GetItemIndex(1), false);
            menu.SetItemDisabled(menu.GetItemIndex(2), false);
            menu.SetItemDisabled(menu.GetItemIndex(3), false);
        }

        MainToolbar.GetNode<Button>("btnSave").Disabled = false;
        MainToolbar.GetNode<Button>("btnUndo").Disabled = false;
        MainToolbar.GetNode<Button>("btnCut").Disabled = false;
        MainToolbar.GetNode<Button>("btnCopy").Disabled = false;
        MainToolbar.GetNode<Button>("btnPaste").Disabled = false;
        MainToolbar.GetNode<Button>("btnModeTiles").Disabled = false;
        MainToolbar.GetNode<Button>("btnModeEvents").Disabled = false;
        MainToolbar.GetNode<Button>("btnZoomIn").Disabled = false;
        MainToolbar.GetNode<Button>("btnZoomOut").Disabled = false;
        MainToolbar.GetNode<Button>("btnZoom11").Disabled = false;
        MainToolbar.GetNode<Button>("btnDatabase").Disabled = false;
        MainToolbar.GetNode<Button>("btnPlugins").Disabled = false;
        MainToolbar.GetNode<Button>("btnSound").Disabled = false;
        MainToolbar.GetNode<Button>("btnSearch").Disabled = false;
        MainToolbar.GetNode<Button>("btnTestPlay").Disabled = false;
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

	public void OnPlaytestPressed()
	{
        EditorMain.Instance.SaveProject();
        EditorMain.Instance.LaunchPlaytest();
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
				NoProjectUI();
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
