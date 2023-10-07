using TMPro;
using UnityEngine;

public class UpgradesDescriptionShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _blockLevelNameText;
    [SerializeField] private TMP_Text _creationSpeedNameText;
    [SerializeField] private TMP_Text _moneyUpgradeNameText;
    [SerializeField] private TMP_Text _blockMoneyNameText;
    [Space(10), SerializeField] private TMP_Text _blockLevelDescriptionText;
    [SerializeField] private TMP_Text _creationSpeedDescriptionText;
    [SerializeField] private TMP_Text _moneyUpgradeDescriptionText;
    [SerializeField] private TMP_Text _blockMoneyDescriptionText;

    public void Show(Translate selectedTranslate)
    {
        ShowNames(selectedTranslate);
        ShowDescriptions(selectedTranslate);
    }

    private void ShowNames(Translate selectedTranslate)
    {
        _blockLevelNameText.text = selectedTranslate.BlockLevelUpgradeName;
        _creationSpeedNameText.text = selectedTranslate.CreationSpeedUpgradeName;
        _moneyUpgradeNameText.text = selectedTranslate.MoneyUpgradeName;
        _blockMoneyNameText.text = selectedTranslate.BlockMoneyUpgradeName;
    }

    private void ShowDescriptions(Translate selectedTranslate)
    {
        _blockLevelDescriptionText.text = selectedTranslate.BlockLevelUpgradeDescription;
        _creationSpeedDescriptionText.text = selectedTranslate.CreationSpeedUpgradeDescription;
        _moneyUpgradeDescriptionText.text = selectedTranslate.MoneyUpgradeDescription;
        _blockMoneyDescriptionText.text = selectedTranslate.BlockMoneyUpgradeDescription;
    }
}
