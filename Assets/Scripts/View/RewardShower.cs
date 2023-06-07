using UnityEngine;
using TMPro;

public class RewardShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardShowerText;

    public void Show(float money)
    {
        _rewardShowerText.text = money.ToString();
    }
}
