using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store some game setting settings.
/// </summary>
public static class GameSettingParamStorage
{
    private static float musicVolume = 1;
    public static float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }

    private static float effectVolume = 1;
    public static float EffectVolume
    {
        get { return effectVolume; }
        set { effectVolume = value; }
    }

    private static float mouseSensitivity = 200;
    public static float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    private static bool musicStatus = true;
    public static bool MusicStatus
    {
        get { return musicStatus; }
        set { musicStatus = value; }
    }

    private static bool effectStatus = true;
    public static bool EffectStatus
    {
        get { return effectStatus; }
        set { effectStatus = value; }
    }
}
