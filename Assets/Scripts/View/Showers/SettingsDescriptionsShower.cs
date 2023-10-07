using TMPro;
using UnityEngine;

public class SettingsDescriptionsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _musicNameText;
    [SerializeField] private TMP_Text _soundNameText;

    public void Show(Translate selectedTranslate)
    {
        _musicNameText.text = selectedTranslate.MusicName;
        _soundNameText.text = selectedTranslate.SoundName;
    }
}
