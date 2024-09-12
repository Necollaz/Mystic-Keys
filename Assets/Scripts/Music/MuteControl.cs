public class MuteControl
{
    public bool IsAudioOn { get; private set; } = true;

    public void Toggle(bool isOn, VolumeControl volumeControl, float sliderValue)
    {
        IsAudioOn = isOn;
        volumeControl.SetVolume("AllMusicVolume", IsAudioOn ? sliderValue : 0);
    }
}