using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl
{
    private AudioMixer _mixer;

    public VolumeControl(AudioMixer mixer)
    {
        _mixer = mixer;
    }

    public void SetVolume(string parameterName, float volume)
    {
        _mixer.SetFloat(parameterName, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat(parameterName, volume);
    }
}
