using Godot;
using System;
using log4net;
using log4net.Appender;
using log4net.Layout;

namespace MZEdit;

/// <summary>
/// Singleton used to startup logging, hold state, etc.
/// </summary>
public partial class EditorMain : Node
{
    public static EditorMain Instance { get; private set; }
    private static readonly ILog Log = LogManager.GetLogger("Editor");

    public override void _EnterTree()
    {
        var gdAppender = new GodotLoggingProvider
        {
            Layout = new PatternLayout($"[%date][%level][%logger] %message%newline")
        };

        log4net.Config.BasicConfigurator.Configure(new IAppender[] {  });
        Log.Info($"MZEdit v{ProjectSettings.GetSetting("application/config/version")}");
        Log.Info("Configured Logger");

        Instance = this;
    }
}
