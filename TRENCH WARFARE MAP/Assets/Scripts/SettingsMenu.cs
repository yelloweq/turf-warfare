using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetFullscreen(bool IsFullScreen)
    {

        Screen.fullScreen = IsFullScreen;

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
}
