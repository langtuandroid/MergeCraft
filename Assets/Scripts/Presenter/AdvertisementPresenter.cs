using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AdvertisementPresenter : MonoBehaviour
{
    [SerializeField] private Timer _timer; 
    [SerializeField] private Volume _volume;
    [SerializeField] private TMP_Text _moneyRewardText;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private RemainingTimeShower _remainingTimeShower;
    [SerializeField] private Button _moneyRewardMultiplierButton;
    [SerializeField] private Button _buildingRewardButton;

    private const int MultiplierRewardIndex = 0;
    private const int BuildingRewardIndex = 1;
    private Building _createdBuilding;
    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;
    private void OnOpenVideoEvent() => _volume.Mute();
    private void OnCloseVideoEvent() => _volume.Unmute();
    private void OnBuildingRewardButtonClicked() => YandexGame.RewVideoShow(BuildingRewardIndex);
    private void OnMultiplierButtonClicked() => YandexGame.RewVideoShow(MultiplierRewardIndex);
    private void OnTimerUpdated(float remainingTime) => _remainingTimeShower.Show(remainingTime);
    private void OnBuildingCreated(Building createdBuilding) => _createdBuilding = createdBuilding;

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
        else if (rewardIndex == BuildingRewardIndex)
        {
            _buildingRewardButton.gameObject.SetActive(false);
            _createdBuilding.BuildAllBlocks();
        }
    }

    private void OnEnable()
    {
        _timer.TimerUpdated += OnTimerUpdated;
        _timer.TimerFinished += OnTimerFinished;
        _buildingCreator.BuildingCreated += OnBuildingCreated;

        _moneyRewardMultiplierButton.onClick.AddListener(() => OnMultiplierButtonClicked());
        _buildingRewardButton.onClick.AddListener(() => OnBuildingRewardButtonClicked());

        YandexGame.RewardVideoEvent += OnRewardVideoEvent;
        YandexGame.OpenVideoEvent += OnOpenVideoEvent;
        YandexGame.CloseVideoEvent += OnCloseVideoEvent;
    }

    private void OnDisable()
    {
        _timer.TimerUpdated -= OnTimerUpdated;
        _timer.TimerFinished -= OnTimerFinished;
        _buildingCreator.BuildingCreated -= OnBuildingCreated;

        _moneyRewardMultiplierButton.onClick.RemoveAllListeners();
        _buildingRewardButton.onClick.RemoveAllListeners();

        YandexGame.RewardVideoEvent -= OnRewardVideoEvent;
        YandexGame.OpenVideoEvent -= OnOpenVideoEvent;
        YandexGame.CloseVideoEvent -= OnCloseVideoEvent;
    }

    private void Start() => YandexGame.FullscreenShow();
}
