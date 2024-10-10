using Godot;
using log4net.Appender;
using log4net.Core;


namespace MZEdit;

/// <summary>
/// Log4Net appender for Godot
/// </summary>
internal class GodotLoggingProvider : AppenderSkeleton
{
    protected override void Append(LoggingEvent loggingEvent)
    {
        string fgColor = "WHITE";

        if (loggingEvent.Level == Level.Debug)
            fgColor = "WHITE";
        else if (loggingEvent.Level == Level.Info)
            fgColor = "DARK_GRAY";
        else if (loggingEvent.Level == Level.Warn)
            fgColor = "YELLOW";
        else if (loggingEvent.Level == Level.Error
            || loggingEvent.Level == Level.Alert
            || loggingEvent.Level == Level.Fatal)
            fgColor = "RED";
        else if (loggingEvent.Level == Level.Notice)
            fgColor = "BLUE";

        GD.PrintRich($"[color={fgColor}]{RenderLoggingEvent(loggingEvent)}[/color]");
    }
}
