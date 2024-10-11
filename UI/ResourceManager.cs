using Godot;
using Godot.Collections;
using log4net;
using System;
using System.IO;
using System.Security;

namespace MZEdit.UI;

public partial class ResourceManager : Window
{
    private static readonly ILog Log = LogManager.GetLogger("ResourceViewer");

    [ExportGroup("SubControls (PRIVATE)")]
    [Export] private Tree FileTree;
    [Export] private Components.AudioPreview AudioControl;
    [Export] private Components.ImagePreview ImageControl;

    private TreeItem m_Root;
    private Dictionary<ulong, string> m_Files;

    public override void _Ready()
    {
        FileTree.HideRoot = true;
        m_Files = new();

        AudioControl.Visible = false;
        ImageControl.Visible = false;

        EditorMain.Instance.OnProjectLoaded += OnProjectLoad;
    }

    private void OnProjectLoad()
    {
        Log.Info("Refreshing file list!");

        FileTree.Clear();
        m_Root = FileTree.CreateItem();

        m_Files.Clear();

        // create all of our img snd whatever folders with contents
        var imgDir = Path.Combine(EditorMain.Instance.ProjectPath, "img");
        var audioDir = Path.Combine(EditorMain.Instance.ProjectPath, "audio");

        foreach (var path in Directory.GetDirectories(imgDir))
        {
            var item = FileTree.CreateItem();
            item.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/folder.png"));
            item.SetText(0, Path.GetRelativePath(EditorMain.Instance.ProjectPath, path));
            item.Collapsed = true; // collapse from the start for ease

            foreach (var fpath in Directory.GetFiles(path))
            {
                var citem = FileTree.CreateItem(item);
                citem.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/image.png"));
                citem.SetText(0, Path.GetFileName(fpath));

                m_Files.Add(citem.GetInstanceId(), fpath);
            }
        }

        foreach (var path in Directory.GetDirectories(audioDir))
        {
            var item = FileTree.CreateItem();
            item.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/folder.png"));
            item.SetText(0, Path.GetRelativePath(EditorMain.Instance.ProjectPath, path));
            item.Collapsed = true; // collapse from the start for ease

            foreach (var fpath in Directory.GetFiles(path))
            {
                var citem = FileTree.CreateItem(item);
                citem.SetIcon(0, GD.Load<Texture2D>("res://Resources/Icons/16x16/music.png"));
                citem.SetText(0, Path.GetFileName(fpath));

                m_Files.Add(citem.GetInstanceId(), fpath);
            }
        }
    }

    private void OnItemSelected()
    {
        ulong fIndex = FileTree.GetSelected().GetInstanceId();

        if (m_Files.ContainsKey(fIndex))
        {
            if (Path.GetExtension(m_Files[fIndex]) == ".ogg")
            {
                ImageControl.Visible = false;
                
                AudioControl.Visible = true;
                AudioControl.Audio = AudioStreamOggVorbis.LoadFromFile(m_Files[fIndex]);
            }
            else if (Path.GetExtension(m_Files[fIndex]) == ".png")
            {
                AudioControl.Stop();
                AudioControl.Visible = false;

                ImageControl.Visible = true;
                ImageControl.Image = ImageTexture.CreateFromImage(Image.LoadFromFile(m_Files[fIndex]));
            }
            else
            {
                AudioControl.Visible = false;
                AudioControl.Stop();
                ImageControl.Visible = false;
                Log.Error($"File extension {Path.GetExtension(m_Files[fIndex])} not recognized! Only png images and ogg audio is supported by RPG Maker MZ!");
            }
        }
        else
        {
            Log.Debug("Item not inside file list, not a file");
        }
    }

    private void OnClose()
    {
        Hide();
    }
}
