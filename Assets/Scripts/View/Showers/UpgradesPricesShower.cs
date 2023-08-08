using TMPro;
using UnityEngine;

public class UpgradesPricesShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _blockMoneyMultiplierPriceText;
    [SerializeField] private TMP_Text _moneyMultiplierPriceText;
    [SerializeField] private TMP_Text _blockCreationSpeedPriceText;
    [SerializeField] private TMP_Text _createdBlockLevelPriceText;

    public void ShowblockMoneyMultiplierPrice(double blockMoneyMultiplier) =>
        _blockMoneyMultiplierPriceText.text = NumberFormater.FormatNumber(blockMoneyMultiplier);

    public void ShowBlockLevelPrice(double createdBlockLevel) =>
        _createdBlockLevelPriceText.text = NumberFormater.FormatNumber(createdBlockLevel);

    public void ShowCreationSpeedPrice(double blockCreationSpeed) =>
        _blockCreationSpeedPriceText.text = NumberFormater.FormatNumber(blockCreationSpeed);

    public void ShowMoneyMultiplierPrice(double moneyMultiplier) =>
        _moneyMultiplierPriceText.text = NumberFormater.FormatNumber(moneyMultiplier);

}
