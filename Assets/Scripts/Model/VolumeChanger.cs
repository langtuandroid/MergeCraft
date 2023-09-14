using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;

    private const int LowerVolumeBorder = -80;
    private const int UpperVolumeBorder = 0;
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolume = "SoundVolume";

    public void ChangeMusicVolume(float volume) =>
        _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(LowerVolumeBorder, UpperVolumeBorder, volume));

    public void ChangeSoundVolume(float volume) =>
        _mixer.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(LowerVolumeBorder, UpperVolumeBorder, volume));
}
