using Godot;
using MZEdit.Data;
using System.Collections.Generic;
using System.Linq;

namespace MZEdit.UI.Components;

public partial class MapList : Tree
{
	private TreeItem m_Root;
	private Dictionary<int, TreeItem> m_Items;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EditorMain.Instance.OnProjectLoaded += RecreateMapTree;
		m_Items = new Dictionary<int, TreeItem>();
	}

	public void RecreateMapTree()
	{
		Clear();
		m_Items.Clear();

        m_Root = CreateItem();
		m_Root.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/brick.png"));
		m_Root.SetText(0, EditorMain.Instance.SystemData.GameTitle);

		foreach (var mapinfo in EditorMain.Instance.MapInfos.Where((e) => e != null && e.ParentId == 0).ToList())
		{
            CreateItemAndChildren(mapinfo);
        }
    }

    private void CreateItemAndChildren(MVMapInfo mapinfo)
    {
        TreeItem item;
        if (mapinfo.ParentId > 0)
        {
            item = CreateItem(m_Items[mapinfo.ParentId]);
        }
        else
        {
            item = CreateItem(m_Root);
        }

        item.SetText(0, mapinfo.Name);
        m_Items.Add(mapinfo.Id, item);
        item.Collapsed = !mapinfo.Expanded;

        var children = EditorMain.Instance.MapInfos
            .Where((e) => e != null && e.ParentId == mapinfo.Id)
            .ToList();
        children.Sort((a, b) => a.Order.CompareTo(b.Order));

        if (children.Count > 0)
        {
            item.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/images.png"));
        }
        else
        {
            item.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/image.png"));
        }

        foreach (var child in children)
        {
            CreateItemAndChildren(child);
        }
    }
}
