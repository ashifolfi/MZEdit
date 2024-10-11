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
    public List<MVItem?> Items;

    public Action OnProjectLoaded;

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
    }

    public void SaveProject()
    {
        string dataDir = Path.Combine(ProjectPath, "data");

        string systemSerialized = JsonConvert.SerializeObject(SystemData);
        string actorsSerialized = JsonConvert.SerializeObject(Actors);
        string classeSerialized = JsonConvert.SerializeObject(Classes);
        string itemsSerialized = JsonConvert.SerializeObject(Items);
        string mapinfosSerialized = JsonConvert.SerializeObject(MapInfos);

        // TEMP: rename to new while we make sure everything works
        File.WriteAllText(Path.Combine(dataDir, "System.json"), systemSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Actors.json"), actorsSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Classes.json"), classeSerialized);
        File.WriteAllText(Path.Combine(dataDir, "Items.json"), itemsSerialized);
        File.WriteAllText(Path.Combine(dataDir, "MapInfos.json"), mapinfosSerialized);

        Log.Info($"Saved project {SystemData.GameTitle}");
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
        Items = JsonConvert.DeserializeObject<List<MVItem?>>(File.ReadAllText(Path.Combine(dataDir, "Items.json")));
        MapInfos = JsonConvert.DeserializeObject<List<MVMapInfo?>>(File.ReadAllText(Path.Combine(dataDir, "MapInfos.json")));

        Log.Info($"Loaded project {SystemData.GameTitle}");
        OnProjectLoaded?.Invoke();
    }
}
