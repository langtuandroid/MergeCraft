using UnityEngine;
using UnityEngine.UI;

public class RewardChestPresenter : MonoBehaviour
{
    [SerializeField] private RewardChest _chest;
    [SerializeField] private RewardChestAnimator _rewardChestAnimator;
    [SerializeField] private RewardAnimator _rewardAnimator;
    [SerializeField] private RewardShower _rewardShower;
    [SerializeField] private Button _chestOpeningButton;
    [SerializeField] private ParticleSystem _openingParticleEffect;

    private void OnChestOpened()
    {
        //_openingParticleEffect.Play();
        _rewardShower.Show(_chest.MoneyReward);
        _rewardChestAnimator.LaunchOpenChestAnimation();
        _rewardAnimator.LaunchGettingRewardAnimation();
        _chest.Open();
    }

    private void OnEnable() => _chestOpeningButton.onClick.AddListener(() => OnChestOpened());
    private void OnDisable() => _chestOpeningButton.onClick.RemoveAllListeners();
}

