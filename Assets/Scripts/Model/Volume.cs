using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private float _soundVolume = 0;
    private float _musicVolume = 0;
    private const int MinVolumeValue = -80;
    private const int MaxVolumeValue = 0;
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MasterVolume = "MasterVolume";

    public void Mute() => _mixer.audioMixer.SetFloat(MasterVolume, MinVolumeValue);
    public void Unmute() => _mixer.audioMixer.SetFloat(MasterVolume, MaxVolumeValue);

    public void ChangeMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(MinVolumeValue, MaxVolumeValue, volume));

        YandexGame.savesData.MusicVolume = volume;
        YandexGame.SaveProgress();
    }

    public void ChangeSoundVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(MinVolumeValue, MaxVolumeValue, volume));

        YandexGame.savesData.SoundVolume = volume;
        YandexGame.SaveProgress();
    }

    private void OnGetDataEvent()
    {
        SavesYG savesData = YandexGame.savesData;

        _soundVolume = savesData.SoundVolume;
        _musicVolume = savesData.MusicVolume;

        _soundSlider.value = Mathf.Lerp(MinVolumeValue, MaxVolumeValue, _soundVolume);
        _musicSlider.value = Mathf.Lerp(MinVolumeValue, MaxVolumeValue, _musicVolume);
    }

    private void OnEnable() => YandexGame.GetDataEvent += OnGetDataEvent;
    private void OnDisable() => YandexGame.GetDataEvent -= OnGetDataEvent;
}
