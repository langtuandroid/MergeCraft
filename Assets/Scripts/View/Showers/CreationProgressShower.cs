using UnityEngine;
using UnityEngine.UI;

public class CreationProgressShower : MonoBehaviour
{
    [SerializeField] private Image _creationProgressImage;

    public void Show(float progress) => _creationProgressImage.fillAmount = progress;
}
