using UnityEngine;
using UnityEngine.UI;
using YG;

public class BuildingPresenter : MonoBehaviour
{
    [SerializeField] private Notifications _notifications;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private BuildProgressShower _buildProgressShower;
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private GameObject _buildingLimitPanel;
    [SerializeField] private Image _buildingRewardImage;
    [SerializeField] private Button _buildBlockButton;
    [SerializeField] private Button _openBuildMenuButton;
    [SerializeField] private Button _closeBuildMenuButton;
    [SerializeField] private Button _adBuildingRewardButton;
    [SerializeField] private Canvas _buildCanvas;
   
    private Building _createdBuilding;
    private BuildingAnimator _buildingAnimator = new BuildingAnimator();
    private BuildBlockAnimator _buildBlockAnimator = new BuildBlockAnimator();
    private BuildingRewardAnimator _buildingRewardAnimator = new BuildingRewardAnimator();
    private bool _buildingBuilded;

    private bool AllBlocksBuilded => _createdBuilding.BuildedBlocksCount == _createdBuilding.BlocksCount;

    private void OnBuildingDestroyed() => _buildingCreator.TryCreateBuilding();
    private void OnBuildingLimitReached() => _buildingLimitPanel.gameObject.SetActive(true);
    private void OnBlockActivated(BuildBlock block) => _buildBlockAnimator.LaunchBuildAnimation(block);
    private void OnBuildingNumberChanged(int createdBuildingNumber) => _buildProgressShower.ShowBuildingNumber(createdBuildingNumber);

    private void OnBlocksCountChanged(int buildedBlocks, int allBlocks) => 
        _buildProgressShower.ShowProgress(buildedBlocks, allBlocks);

    private void OnBuildingDecreased()
    {
        _createdBuilding.ApplyBuildingReward();
        _buildingCreator.TryDestroyCreatedBuilding();
    }

    private void OnRecoveredBuildingLimitReached(int previouslyBuildingBlocksCount)
    {
        _buildProgressShower.ShowProgress(previouslyBuildingBlocksCount, previouslyBuildingBlocksCount);
        _adBuildingRewardButton.gameObject.SetActive(false);
        _buildingLimitPanel.gameObject.SetActive(true);
        _buildBlockButton.interactable = false;
    }

    private void OnBlockBuilded()
    {
        if (AllBlocksBuilded && _buildingAnimator.DecreaseAnimationLaunched == false)
        {
            _adBuildingRewardButton.gameObject.SetActive(false);

            _buildingBuilded = true;
            _buildBlockButton.interactable = false;
            _buildBlockButton.onClick.RemoveAllListeners();

            _createdBuilding.BlockActivated -= OnBlockActivated;
            _createdBuilding.BlocksCountChanged -= OnBlocksCountChanged;

            _buildingAnimator.LaunchDecreaseBuildingAnimation(_createdBuilding);
            _buildingRewardAnimator.LaunchIncreaseRewardAnimation(_buildingRewardImage);
        }
    }

    private void OnBuildingCreated(Building building)
    {
        _createdBuilding = building;
        _buildProgressShower.ShowBuildPrice(_createdBuilding.BuildBlockPrice);

        _createdBuilding.BlockActivated += OnBlockActivated;
        _createdBuilding.BlocksCountChanged += OnBlocksCountChanged;

        _buildBlockButton.onClick.AddListener(() => _createdBuilding.TryBuildBlock());
        _createdBuilding.TryRestoreBuildedBlocks();

        _buildingLimitPanel.gameObject.SetActive(false);
        _adBuildingRewardButton.gameObject.SetActive(true);

        _buildingBuilded = false;
    }

    private void TryActivateBuildButton()
    {
        if (_buildingBuilded == false && _createdBuilding != null)
        {
            if (_createdBuilding.CanBuyBlock && _createdBuilding.BlocksEnough)
            {
                _buildBlockButton.interactable = true;
                _notifications.ActivateBuildNotification();
            }
            else
            {
                _buildBlockButton.interactable = false;
                _notifications.DeactivateBuildNotification();
            }
        }
    }

    private void OnEnable()
    {
        _buildBlockAnimator.BlockBuilded += OnBlockBuilded;
        _buildingCreator.BuildingCreated += OnBuildingCreated;
        _buildingCreator.BuildingDestroyed += OnBuildingDestroyed;
        _buildingAnimator.BuildingDecreased += OnBuildingDecreased;
        _buildingCreator.BuildingLimitReached += OnBuildingLimitReached;
        _buildingCreator.BuildingNumberChanged += OnBuildingNumberChanged;
        _buildingCreator.RecoveredBuildingLimitReached += OnRecoveredBuildingLimitReached;

        _openBuildMenuButton.onClick.AddListener(() => _buildCanvas.gameObject.SetActive(true));
        _closeBuildMenuButton.onClick.AddListener(() => _buildCanvas.gameObject.SetActive(false));

        _openBuildMenuButton.onClick.AddListener(() => YandexGame.FullscreenShow());
        _closeBuildMenuButton.onClick.AddListener(() => YandexGame.FullscreenShow());
    }

    private void OnDisable()
    {
        _buildBlockAnimator.BlockBuilded -= OnBlockBuilded;
        _buildingCreator.BuildingCreated -= OnBuildingCreated;
        _buildingCreator.BuildingDestroyed -= OnBuildingDestroyed;
        _buildingAnimator.BuildingDecreased -= OnBuildingDecreased;
        _buildingCreator.BuildingLimitReached -= OnBuildingLimitReached;
        _buildingCreator.BuildingNumberChanged -= OnBuildingNumberChanged;
        _buildingCreator.RecoveredBuildingLimitReached -= OnRecoveredBuildingLimitReached;

        _openBuildMenuButton.onClick.RemoveAllListeners();
        _closeBuildMenuButton.onClick.RemoveAllListeners();
    }

    private void Update() => TryActivateBuildButton();
}
