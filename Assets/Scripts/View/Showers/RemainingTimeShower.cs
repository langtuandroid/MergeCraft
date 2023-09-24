using UnityEngine;
using UnityEngine.UI;

public class RemainingTimeShower : MonoBehaviour
{
    [SerializeField] private Image _remainingTimeImage;

    public void Activate() => _remainingTimeImage.gameObject.SetActive(true);
    public void Deactivate() => _remainingTimeImage.gameObject.SetActive(false);
    public void Show(float remainingTime) => _remainingTimeImage.fillAmount = remainingTime;
}
