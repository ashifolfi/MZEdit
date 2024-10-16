using Godot;
using System;

namespace MZEdit.UI;

public partial class AboutView : Window
{
	[Export] private VBoxContainer NamesContainer;
	[Export] private RichTextLabel LicenseText;
	[Export] private Label HeaderVerLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HeaderVerLabel.Text = $"MZEdit v{ProjectSettings.GetSetting("application/config/version")}";

		string authorsText = FileAccess.GetFileAsString("res://AUTHORS.txt");
		foreach (var line in authorsText.Split("\n"))
		{
			var label = new Label();
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.Text = line;

			NamesContainer.AddChild(label);
		}

		LicenseText.Clear();
		LicenseText.PushMono();
		LicenseText.AppendText(FileAccess.GetFileAsString("res://LICENSE.txt"));
		LicenseText.Pop();
	}

	private void OnClose()
	{
		Hide();
	}
}
