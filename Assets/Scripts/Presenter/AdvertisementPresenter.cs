using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AdvertisementPresenter : MonoBehaviour
{
    [SerializeField] private Timer _timer; 
    [SerializeField] private Volume _volume;
    [SerializeField] private TMP_Text _moneyRewardText;
    [SerializeField] private RemainingTimeShower _remainingTimeShower;
    [SerializeField] private Button _moneyRewardMultiplierButton;
    [SerializeField] private Button _blockMoneyRewardButton;

    private const int MultiplierRewardIndex = 0;
    private const int BlockRewardCount = 200;
    private const int BlockRewardIndex = 1;
    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;
    private void OnOpenVideoEvent() => _volume.Mute();
    private void OnCloseVideoEvent() => _volume.Unmute();
    private void OnBlockRewardButtonClicked() => YandexGame.RewVideoShow(BlockRewardIndex);
    private void OnMultiplierButtonClicked() => YandexGame.RewVideoShow(MultiplierRewardIndex);
    private void OnTimerUpdated(float remainingTime) => _remainingTimeShower.Show(remainingTime);

    private void OnTimerFinished()
    {
        _wallet.RevertMoneyMultiplier();
        _remainingTimeShower.Deactivate();
        _moneyRewardText.gameObject.SetActive(true);
    }

    private void OnRewardVideoEvent(int rewardIndex)
    {
        if (rewardIndex == MultiplierRewardIndex)
        {
            _wallet.ActivateAdMoneyMultiplier();
            _moneyRewardText.gameObject.SetActive(false);
            _remainingTimeShower.Activate();
            _timer.ActivateTimer();
        }
        else if (rewardIndex == BlockRewardIndex)
        {
            _wallet.TryAddBuildBlocksMoney(BlockRewardCount);
        }
    }

    private void OnEnable()
    {
        _timer.TimerUpdated += OnTimerUpdated;
        _timer.TimerFinished += OnTimerFinished;

        _moneyRewardMultiplierButton.onClick.AddListener(() => OnMultiplierButtonClicked());
        _blockMoneyRewardButton.onClick.AddListener(() => OnBlockRewardButtonClicked());

        YandexGame.RewardVideoEvent += OnRewardVideoEvent;
        YandexGame.OpenVideoEvent += OnOpenVideoEvent;
        YandexGame.CloseVideoEvent += OnCloseVideoEvent;
    }

    private void OnDisable()
    {
        _timer.TimerUpdated -= OnTimerUpdated;
        _timer.TimerFinished -= OnTimerFinished;

        _moneyRewardMultiplierButton.onClick.RemoveAllListeners();
        _blockMoneyRewardButton.onClick.RemoveAllListeners();

        YandexGame.RewardVideoEvent -= OnRewardVideoEvent;
        YandexGame.OpenVideoEvent -= OnOpenVideoEvent;
        YandexGame.CloseVideoEvent -= OnCloseVideoEvent;
    }
}
