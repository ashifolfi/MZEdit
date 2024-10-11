using Godot;
using System;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace MZEdit.UI;

public partial class DatabaseEditor : Window
{
    private static readonly ILog Log = LogManager.GetLogger("DatabaseEditor");

	[ExportGroup("SubControls (PRIVATE)")]
	[Export] private ItemList ActorList;
	[Export] private Components.ActorEditor ActorEditor;

    [Export] private ItemList ClassList;
    [Export] private ItemList ItemList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		EditorMain.Instance.OnProjectLoaded += OnProjectLoad;
		Hide();
	}

	private void OnActorChanged()
	{
		var actor = EditorMain.Instance.Actors.Find(e => e != null && e.Id == ActorEditor.ActorID);
		ActorList.SetItemText(actor.Id - 1, $"{actor.Id}: {actor.Name}");
	}

	private void OnProjectLoad()
	{
		Log.Info("Refreshing Database contents");
		ActorList.Clear();
		ClassList.Clear();
		ItemList.Clear();

		{ // Actors
			var sortedList = EditorMain.Instance.Actors
				.Where((e) => e != null)
				.ToList();
			sortedList.Sort((a, b) => a.Id.CompareTo(b.Id));

			foreach (var item in sortedList)
			{
				ActorList.AddItem($"{item.Id}: {item.Name}");
			}
		}

        { // Classes
            var sortedList = EditorMain.Instance.Classes
                .Where((e) => e != null)
                .ToList();
            sortedList.Sort((a, b) => a.Id.CompareTo(b.Id));

            foreach (var item in sortedList)
            {
                ClassList.AddItem($"{item.Id}: {item.Name}");
            }
        }

        { // Items
            var sortedList = EditorMain.Instance.Items
                .Where((e) => e != null)
                .ToList();
            sortedList.Sort((a, b) => a.Id.CompareTo(b.Id));

            foreach (var item in sortedList)
            {
                ItemList.AddItem($"{item.Id}: {item.Name}");
            }
        }
    }

	private void OnActorSelected(int item)
	{
		ActorEditor.ActorID = item + 1;
    }

	private void OnClose()
	{
		Hide();
	}
}
