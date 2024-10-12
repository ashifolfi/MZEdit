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
    public List<MVMapInfo?> MapInfos;
    public List<MVActor?> Actors;
    public List<MVClass?> Classes;
    public List<MVEvent?> CommonEvents;
    public List<MVItem?> Items;
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

        string systemSerialized = JsonConvert.SerializeObject(SystemData);
        string actorsSerialized = JsonConvert.SerializeObject(Actors);
        string classesSerialized = JsonConvert.SerializeObject(Classes);
        string commonEventsSerialized = JsonConvert.SerializeObject(CommonEvents);
        string itemsSerialized = JsonConvert.SerializeObject(Items);
        string mapinfosSerialized = JsonConvert.SerializeObject(MapInfos);
        string tilesetsSerialized = JsonConvert.SerializeObject(Tilesets);

        File.WriteAllText(Path.Combine(dataDir, "System.json"), systemSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Actors.json"), actorsSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Classes.json"), classesSerialized);
        File.WriteAllText(Path.Combine(dataDir, "CommonEvents.json"), commonEventsSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Items.json"), itemsSerialized);
        File.WriteAllText(Path.Combine(dataDir, "MapInfos.json"), mapinfosSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Tilesets.json"), tilesetsSerialized);

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
        Classes = JsonConvert.DeserializeObject<List<MVClass?>>(File.ReadAllText(Path.Combine(dataDir, "Classes.json")));
        CommonEvents = JsonConvert.DeserializeObject<List<MVEvent?>>(File.ReadAllText(Path.Combine(dataDir, "CommonEvents.json")));
        Items = JsonConvert.DeserializeObject<List<MVItem?>>(File.ReadAllText(Path.Combine(dataDir, "Items.json")));
        MapInfos = JsonConvert.DeserializeObject<List<MVMapInfo?>>(File.ReadAllText(Path.Combine(dataDir, "MapInfos.json")));
        Tilesets = JsonConvert.DeserializeObject<List<MVTileset?>>(File.ReadAllText(Path.Combine(dataDir, "Tilesets.json")));

        Log.Info($"Loaded project {SystemData.GameTitle}");
        OnProjectLoaded?.Invoke();
    }
}
