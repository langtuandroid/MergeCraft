using TMPro;
using UnityEngine;

public class UpgradesInfoShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _blockMoneyMultiplierPriceText;
    [SerializeField] private TMP_Text _blockMoneyMultiplierLevelText;
    [SerializeField] private TMP_Text _moneyMultiplierPriceText;
    [SerializeField] private TMP_Text _moneyMultiplierLevelText;
    [SerializeField] private TMP_Text _blockCreationSpeedPriceText;
    [SerializeField] private TMP_Text _blockCreationSpeedLevelText;
    [SerializeField] private TMP_Text _createdBlockLevelPriceText;
    [SerializeField] private TMP_Text _createdBlockLevelText;

    public void ShowBlockMoneyMultiplierInfo(double price, int level) =>
        Show(_blockMoneyMultiplierPriceText, _blockMoneyMultiplierLevelText, price, level);

    public void ShowBlockLevelInfo(double price, int level) =>
        Show(_createdBlockLevelPriceText, _createdBlockLevelText, price, level);

    public void ShowCreationSpeedInfo(double price, int level) =>
        Show(_blockCreationSpeedPriceText, _blockCreationSpeedLevelText, price, level);

    public void ShowMoneyMultiplierInfo(double price, int level) =>
        Show(_moneyMultiplierPriceText, _moneyMultiplierLevelText, price, level);

    private void Show(TMP_Text priceText, TMP_Text levelText, double price, int level)
    {
        priceText.text = NumberFormater.FormatNumber(price);
        levelText.text = level.ToString();
    }
}
