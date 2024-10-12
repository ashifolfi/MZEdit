using Godot;
using System;
using log4net;
using log4net.Appender;
using log4net.Layout;
using MZEdit.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MZEdit;

/// <summary>
/// Singleton used to startup logging, hold state, etc.
/// </summary>
public partial class EditorMain : Node
{
    public static EditorMain Instance { get; private set; }
    private static readonly ILog Log = LogManager.GetLogger("Editor");

    public string ProjectPath;

    public MVSystem SystemData;
    public List<MVActor?> Actors;
    public List<MVArmor?> Armors;
    public List<MVClass?> Classes;
    public List<MVEvent?> CommonEvents;
    public List<MVItem?> Items;
    public List<MVMapInfo?> MapInfos;
    public List<MVTileset?> Tilesets;

    public Action OnProjectLoaded;

    /// <summary>
    /// Indicates if any unsaved changes are present or not
    /// </summary>
    public bool DirtyProject { get; private set; }

    public override void _EnterTree()
    {
        var gdAppender = new GodotLoggingProvider
        {
            Layout = new PatternLayout("[%date][%level][%logger] %message")
        };
        gdAppender.ActivateOptions();

        log4net.Config.BasicConfigurator.Configure(new IAppender[] { gdAppender });
        Log.Info($"MZEdit v{ProjectSettings.GetSetting("application/config/version")}");
        Log.Info("Configured Logger");

        Instance = this;
        ProjectPath = "";
        SystemData = null;
        MapInfos = null;
        Actors = null;
        Classes = null;
        CommonEvents = null;
        DirtyProject = false;
    }

    public void LaunchPlaytest()
    {
#if GODOT_WINDOWS
        string nwPath = "";
        // check if we are an editor build via checking if this exists (it's not packed)
        if (Godot.FileAccess.FileExists("res://unpacked/nwjs/win-sdk/nw.exe"))
        {
            nwPath = ProjectSettings.GlobalizePath("res://unpacked/nwjs/win-sdk/nw.exe");
        }
        else
        {
            // exported build, it'll be next to the executable
            nwPath = Path.Combine(OS.GetExecutablePath(), "nwjs", "win-sdk", "nw.exe");
        }

        OS.CreateProcess(nwPath, new[] { ProjectPath });
#endif
    }

    public void SaveProject()
    {
        string dataDir = Path.Combine(ProjectPath, "data");

        File.WriteAllText(Path.Combine(dataDir, "System.json"), JsonConvert.SerializeObject(SystemData));
        File.WriteAllText(Path.Combine(dataDir, "Actors.json"), JsonConvert.SerializeObject(Actors));
        File.WriteAllText(Path.Combine(dataDir, "Armors.json"), JsonConvert.SerializeObject(Armors));
        File.WriteAllText(Path.Combine(dataDir, "Classes.json"), JsonConvert.SerializeObject(Classes));
        File.WriteAllText(Path.Combine(dataDir, "CommonEvents.json"), JsonConvert.SerializeObject(CommonEvents));
        File.WriteAllText(Path.Combine(dataDir, "Items.json"), JsonConvert.SerializeObject(Items));
        File.WriteAllText(Path.Combine(dataDir, "MapInfos.json"), JsonConvert.SerializeObject(MapInfos));
        File.WriteAllText(Path.Combine(dataDir, "Tilesets.json"), JsonConvert.SerializeObject(Tilesets));

        Log.Info($"Saved project {SystemData.GameTitle}");
        DirtyProject = false;
    }

    public void LoadProject(string projectPath)
    {
        if (!File.Exists(projectPath))
        {
            Log.Error($"No project file exists at {projectPath}");
            return;
        }

        ProjectPath = Path.GetDirectoryName(projectPath);

        string dataDir = Path.Combine(ProjectPath, "data");
        SystemData = JsonConvert.DeserializeObject<MVSystem>(File.ReadAllText(Path.Combine(dataDir, "System.json")));
        Actors = JsonConvert.DeserializeObject<List<MVActor?>>(File.ReadAllText(Path.Combine(dataDir, "Actors.json")));
        Armors = JsonConvert.DeserializeObject<List<MVArmor?>>(File.ReadAllText(Path.Combine(dataDir, "Armors.json")));
        Classes = JsonConvert.DeserializeObject<List<MVClass?>>(File.ReadAllText(Path.Combine(dataDir, "Classes.json")));
        CommonEvents = JsonConvert.DeserializeObject<List<MVEvent?>>(File.ReadAllText(Path.Combine(dataDir, "CommonEvents.json")));
        Items = JsonConvert.DeserializeObject<List<MVItem?>>(File.ReadAllText(Path.Combine(dataDir, "Items.json")));
        MapInfos = JsonConvert.DeserializeObject<List<MVMapInfo?>>(File.ReadAllText(Path.Combine(dataDir, "MapInfos.json")));
        Tilesets = JsonConvert.DeserializeObject<List<MVTileset?>>(File.ReadAllText(Path.Combine(dataDir, "Tilesets.json")));

        Log.Info($"Loaded project {SystemData.GameTitle}");
        OnProjectLoaded?.Invoke();
    }
}
