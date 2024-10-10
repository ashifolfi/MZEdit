using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZEdit.Data;

/// <summary>
/// Sound Resource Entry Data
/// 
/// Contains a reference to a sound file and playback options
/// </summary>
public struct MVSound
{
    public string name;
    public int pan;
    public int pitch;
    public int volume;
}
