using Godot;
using System;

namespace MZEdit.UI.Components;

public partial class ImagePreview : VBoxContainer
{
    public float Zoom
    {
        get => (float)ZoomSlider.Value;
        set => ZoomSlider.Value = value;
    }

    public Texture2D Image
    {
        get => ImageRect.Texture;
        set
        {
            Zoom = 1.0f;
            ImageRect.Texture = value;
        }
    }

    [ExportCategory("SubControls (PRIVATE)")]
    [Export] private TextureRect ImageRect;
    [Export] private Slider ZoomSlider;
    [Export] private Camera2D ZoomCamera;

    public override void _Ready()
    {
        Zoom = 1.0f;
    }

    private void OnZIButton()
    {
        Zoom += 0.1f;
    }

    private void OnZOButton()
    {
        Zoom -= 0.1f;
    }

    private void OnSliderValueChanged(float value)
    {
        ZoomCamera.Zoom = new(value, value);
        ZoomCamera.Offset = ZoomCamera.GetViewportRect().Size / 2;
    }
}
