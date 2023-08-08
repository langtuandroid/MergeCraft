using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] private GameObject _buildNotification;
    [SerializeField] private GameObject _shopNotification;

    public void ActivateBuildNotification() => _buildNotification.gameObject.SetActive(true);
    public void ActivateShopNotification() => _shopNotification.gameObject.SetActive(true);
    public void DeactivateBuildNotification() => _buildNotification.gameObject.SetActive(false);
    public void DeactivateShopNotification() => _shopNotification.gameObject.SetActive(false);
}
