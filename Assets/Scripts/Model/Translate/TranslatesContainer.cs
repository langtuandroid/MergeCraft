using UnityEngine;
using UnityEngine.Events;
using YG;

public class TranslatesContainer : MonoBehaviour
{
    public event UnityAction TranslateSelected;
    public Translate SelectedTranslate => _selectedTranslate;

    [SerializeField] private RussianTranslate _russianTranslate;
    [SerializeField] private EnglishTranslate _englishTranslate;

    private const string RussianLanguageShortcut = "ru";
    private const string EnglishLanguageShortcut = "en";
    private Translate _selectedTranslate;

    private void OnSwitchLangEvent(string languageShortcut)
    {
        if (languageShortcut == RussianLanguageShortcut)
            _selectedTranslate = _russianTranslate;
        else if (languageShortcut == EnglishLanguageShortcut)
            _selectedTranslate = _englishTranslate;

        TranslateSelected?.Invoke();
    }

    private void OnEnable() => YandexGame.SwitchLangEvent += OnSwitchLangEvent;
    private void OnDisable() => YandexGame.SwitchLangEvent -= OnSwitchLangEvent;
}
