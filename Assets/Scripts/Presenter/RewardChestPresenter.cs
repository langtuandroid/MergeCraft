using UnityEngine;
using UnityEngine.UI;

public class RewardChestPresenter : MonoBehaviour
{
    [SerializeField] private RewardChest _chest;
    [SerializeField] private RewardChestAnimator _rewardChestAnimator;
    [SerializeField] private RewardAnimator _rewardAnimator;
    [SerializeField] private RewardShower _rewardShower;
    [SerializeField] private Button _chestOpeningButton;

    private void OnChestOpened()
    {
        if (_chest.RewardApplied == false)
        {
            _rewardShower.ShowBuildBlockCount(_chest.BuildBlockReward);
            _rewardChestAnimator.LaunchOpenChestAnimation();
            _rewardAnimator.LaunchGettingBlockRewardAnimation();
            _chest.TryApplyReward();
        }
    }

    private void OnEnable() => _chestOpeningButton.onClick.AddListener(() => OnChestOpened());
    private void OnDisable() => _chestOpeningButton.onClick.RemoveAllListeners();
}

