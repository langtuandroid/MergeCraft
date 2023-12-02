using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    [SerializeField] private GameObject _buildNotification;
    [SerializeField] private GameObject _shopNotification;
    [Space(10), SerializeField] private Image _achievedBlockImage;
    [SerializeField] private TMP_Text _achievedBlockNameText;
    [SerializeField] private TMP_Text _newBlockText;

    public void ActivateBuildNotification() => _buildNotification.gameObject.SetActive(true);
    public void ActivateShopNotification() => _shopNotification.gameObject.SetActive(true);
    public void DeactivateBuildNotification() => _buildNotification.gameObject.SetActive(false);
    public void DeactivateShopNotification() => _shopNotification.gameObject.SetActive(false);

    public void ShowBlockProgressNotification(string notification, Sprite blockSprite, string blockName)
    {
        _newBlockText.text = notification;
        _achievedBlockNameText.text = blockName;
        _achievedBlockImage.sprite = blockSprite;
    }
}
