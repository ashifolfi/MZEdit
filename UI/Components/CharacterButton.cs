using Godot;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MZEdit.UI.Components;

[GlobalClass]
public partial class CharacterButton : Button
{
    public int SpriteIndex { get; private set; }
    public string SpriteName { get; private set; }

    public override void _Ready()
    {
        IconAlignment = HorizontalAlignment.Center;
        CustomMinimumSize = new Vector2(128, 128);
    }

    public void SetCharacter(string imgName, int imgIndex)
    {
        if (imgIndex > 7 || imgIndex < 0)
        {
            throw new ArgumentException("imgIndex must be between 0 and 7! (sheets hold a maximum of 8 characters)");
        }

        string path = Path.Combine(EditorMain.Instance.ProjectPath, "img", "characters", imgName + ".png");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Couldn't find character image {imgName}!");
        }

        SpriteName = imgName;
        SpriteIndex = imgIndex;

        // parse out flags from filename
        bool singleChar = imgName[0] == '$' || imgName[1] == '$';
        bool noShift = imgName[0] == '!' || imgName[1] == '!';

        Image charImg = Image.LoadFromFile(path);

        if (singleChar)
        {
            Icon = ImageTexture.CreateFromImage(
                charImg.GetRegion(new Rect2I(
                    0, 0,
                    charImg.GetWidth() / 3, charImg.GetHeight() / 4
                ))
            );
        }
        else
        {
            var subImageRect = new Rect2I(
                imgIndex > 3
                ? new Vector2I((imgIndex - 4) * (charImg.GetWidth() / 4), charImg.GetHeight() / 2)
                : new Vector2I(imgIndex * (charImg.GetWidth() / 4), 0),
                new Vector2I(charImg.GetWidth() / 4, charImg.GetHeight() / 2)
            );

            var subImage = charImg.GetRegion(subImageRect);
            Icon = ImageTexture.CreateFromImage(
                subImage.GetRegion(new Rect2I(
                    0, 0,
                    subImage.GetWidth() / 3, subImage.GetHeight() / 4
                ))
            );
        }
    }
}
