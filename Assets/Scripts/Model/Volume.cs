using UnityEngine;
using UnityEngine.Audio;
using YG;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;

    private float _volume;
    private const int MinVolumeValue = -80;
    private const int MaxVolumeValue = 0;
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MasterVolume = "MasterVolume";

    public void Mute() => _mixer.audioMixer.SetFloat(MasterVolume, MinVolumeValue);
    public void Unmute() => _mixer.audioMixer.SetFloat(MasterVolume, MaxVolumeValue);

    public void ChangeMusicVolume(float volume) =>
        _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(MinVolumeValue, MaxVolumeValue, volume));

    public void ChangeSoundVolume(float volume) =>
        _mixer.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(MinVolumeValue, MaxVolumeValue, volume));

    private void OnGetDataEvent()
    {
        //_volume = YandexGame.savesData.Volume;
        _mixer.audioMixer.SetFloat(MasterVolume, _volume);
    }

    //private void OnEnable() => YandexGame.GetDataEvent += OnGetDataEvent;
    //private void OnDisable() => YandexGame.GetDataEvent -= OnGetDataEvent;
}
