using Godot;
using System;

namespace MZEdit.UI.Components;

[GlobalClass]
[Tool] // we need this to allow drawing the text in editor
public partial class GroupBoxContainer : Container
{

    [Export] public string GroupName
    {
        get => m_GroupName;
        set
        {
            m_GroupName = value;
            if (IsNodeReady())
            {
                QueueRedraw();
            }
        }
    }
    private string m_GroupName = "GroupBox";
    private Vector2 m_FirstChildMinSize = Vector2.Zero;

    public override void _Notification(int what)
    {
        switch (what)
        {
            case (int)NotificationSortChildren:
                m_FirstChildMinSize = Vector2.Zero;
                foreach (var child in GetChildren())
                {
                    if (child is not Control)
                    {
                        continue;
                    }

                    var styleBox = GetThemeStylebox("normal", "GroupBoxContainer");
                    if (m_FirstChildMinSize == Vector2.Zero)
                    {
                        m_FirstChildMinSize = ((Control)child).GetCombinedMinimumSize();
                    }
                    FitChildInRect(
                        (Control)child,
                        new(
                            styleBox.GetMargin(Side.Left),
                            24 + styleBox.GetMargin(Side.Top),
                            Size.X - (styleBox.GetMargin(Side.Right) + styleBox.GetMargin(Side.Left)),
                            Size.Y - (24 + styleBox.GetMargin(Side.Bottom) + styleBox.GetMargin(Side.Top))
                        )
                    );
                }

                break;
        }
    }

    public override Vector2 _GetMinimumSize()
    {
        var styleBox = GetThemeStylebox("normal", "GroupBoxContainer");

        return m_FirstChildMinSize
            + new Vector2(0, 24)
            + new Vector2(
                styleBox.GetMargin(Side.Left) + styleBox.GetMargin(Side.Right),
                styleBox.GetMargin(Side.Top) + styleBox.GetMargin(Side.Bottom)
            );
    }

    public override void _Draw()
    {
        DrawString(
            GetThemeDefaultFont(),
            new Vector2(4, 16),
            GroupName
        );

        DrawStyleBox(GetThemeStylebox("normal", "GroupBoxContainer"), new(new(0, 24), new(Size.X, Size.Y - 24)));
    }
}
