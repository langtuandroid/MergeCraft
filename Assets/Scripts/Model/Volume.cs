using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private bool _saveDataLoaded;
    private float _soundVolume = 0.75f;
    private float _musicVolume = 0.75f;
    private const float MinSliderValue = 0;
    private const float MaxSliderValue = 1;
    private const float MinMixerVolumeValue = -80;
    private const float MaxMixerVolumeValue = 0;
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MasterVolume = "MasterVolume";

    public void Mute() => _mixer.audioMixer.SetFloat(MasterVolume, MinMixerVolumeValue);
    public void Unmute() => _mixer.audioMixer.SetFloat(MasterVolume, MaxMixerVolumeValue);

    public void TryChangeMusicVolume(float volume)
    {
        if (_saveDataLoaded)
        {
            _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(MinMixerVolumeValue, MaxMixerVolumeValue, volume));

            YandexGame.savesData.MusicVolume = volume;
            //YandexGame.SaveProgress();
        }
    }

    public void TryChangeSoundVolume(float volume)
    {
        if (_saveDataLoaded)
        {
            _mixer.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(MinMixerVolumeValue, MaxMixerVolumeValue, volume));

            YandexGame.savesData.SoundVolume = volume;
            //YandexGame.SaveProgress();
        }
    }

    private void OnGetDataEvent()
    {
        SavesYG savesData = YandexGame.savesData;

        _soundVolume = savesData.SoundVolume;
        _musicVolume = savesData.MusicVolume;

        _mixer.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(MinMixerVolumeValue, MaxMixerVolumeValue, _soundVolume));
        _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(MinMixerVolumeValue, MaxMixerVolumeValue, _musicVolume));

        _soundSlider.value = Mathf.Lerp(MinSliderValue, MaxSliderValue, _soundVolume);
        _musicSlider.value = Mathf.Lerp(MinSliderValue, MaxSliderValue, _musicVolume);

        _saveDataLoaded = true;
    }

    private void OnEnable() => YandexGame.GetDataEvent += OnGetDataEvent;
    private void OnDisable() => YandexGame.GetDataEvent -= OnGetDataEvent;
}
