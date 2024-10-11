using Godot;
using System;

namespace MZEdit.UI.Components;

public partial class AudioPreview : VBoxContainer
{
    public AudioStream Audio {
        get => AudioPlayer.Stream;
        set
        {
            AudioPlayer.Stream = value;
            Stop();

            AudioStreamOggVorbis oggStream = (AudioStreamOggVorbis)AudioPlayer.Stream;
            AudioInfo.Text = "[b]File Info:[/b]\n" +
                $"[b]Length:[/b] {value.GetLength()} seconds";

            SeekBar.MaxValue = value.GetLength();
        }
    }

    [ExportGroup("SubControls (PRIVATE)")]
    [Export] private RichTextLabel AudioInfo;
    [Export] private Slider SeekBar;
    [Export] private Label EndTimeLabel;
    [Export] private Button PausePlayButton;
    [Export] private AudioStreamPlayer AudioPlayer;

    private bool IsDragging;

    public void Stop()
    {
        OnStopPressed();
    }

    public override void _Ready()
    {
        AudioInfo.Clear();
        SeekBar.Value = 0;
        SeekBar.MaxValue = 0;
        EndTimeLabel.Text = "0:00";
        PausePlayButton.Icon = GD.Load<Texture2D>("res://Resources/Icons/48x48/Media-playback-start.svg");

        // make sure it's stopped
        AudioPlayer.Stop();
        IsDragging = false;
    }

    public override void _Process(double delta)
    {
        if (AudioPlayer.Playing && !IsDragging)
        {
            SeekBar.Value = AudioPlayer.GetPlaybackPosition();
        }
    }

    private void OnPlayPressed()
    {
        if (AudioPlayer.Playing)
        {
            AudioPlayer.Stop();
            PausePlayButton.Icon = GD.Load<Texture2D>("res://Resources/Icons/48x48/Media-playback-start.svg");
        }
        else
        {
            AudioPlayer.Play((float)SeekBar.Value);
            PausePlayButton.Icon = GD.Load<Texture2D>("res://Resources/Icons/48x48/Media-playback-pause.svg");
        }
    }

    private void OnStopPressed()
    {
        AudioPlayer.Stop();
        SeekBar.Value = 0;
        PausePlayButton.Icon = GD.Load<Texture2D>("res://Resources/Icons/48x48/Media-playback-start.svg");
        PausePlayButton.ButtonPressed = false;
    }

    private void OnSeekRightPressed()
    {
        SeekBar.Value += 2;
        AudioPlayer.Seek((float)SeekBar.Value);
    }

    private void OnSeekLeftPressed()
    {
        SeekBar.Value -= 2;
        AudioPlayer.Seek((float)SeekBar.Value);
    }

    private void OnSeekBarDragStart()
    {
        IsDragging = true;
    }

    // we do this in drag end because we change the seekbar value to match the song progress
    private void OnSeekBarDragEnd(bool didChange)
    {
        AudioPlayer.Seek((float)SeekBar.Value);
        IsDragging = false;
    }
}
