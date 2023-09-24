using UnityEngine;
using UnityEngine.UI;

public class BuildingPresenter : MonoBehaviour
{
    [SerializeField] private Notifications _notifications;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private BuildProgressShower _buildProgressShower;
    [SerializeField] private ParticleSystem _confettiParticle;
    [SerializeField] private Image _buildingRewardImage;
    [SerializeField] private Button _buildBlockButton;
    [SerializeField] private Button _openBuildMenuButton;
    [SerializeField] private Button _closeBuildMenuButton;
    [SerializeField] private Canvas _buildCanvas;
   
    private Building _createdBuilding;
    private BuildingAnimator _buildingAnimator = new BuildingAnimator();
    private BuildBlockAnimator _buildBlockAnimator = new BuildBlockAnimator();
    private BuildingRewardAnimator _buildingRewardAnimator = new BuildingRewardAnimator();
    private bool _buildingBuilded;

    private bool AllBlocksBuilded => _createdBuilding.BuildedBlocksCount == _createdBuilding.BlocksCount;

    private void OnBlocksCountChanged(int buildedBlocks, int allBlocks) => _buildProgressShower.ShowProgress(buildedBlocks, allBlocks);
    private void OnBlockActivated(BuildBlock block) => _buildBlockAnimator.LaunchBuildAnimation(block);
    private void OnBuildingDestroyed() => _buildingCreator.TryCreateBuilding();

    private void OnBuildingDecreased()
    {
        _createdBuilding.ApplyBuildingReward();
        _buildingCreator.TryDestroyCreatedBuilding();
    }

    private void OnBlockBuilded()
    {
        if (AllBlocksBuilded && _buildingAnimator.DecreaseAnimationLaunched == false)
        {
            _buildingBuilded = true;
            _buildBlockButton.interactable = false;
            _buildBlockButton.onClick.RemoveAllListeners();

            _createdBuilding.BlockActivated -= OnBlockActivated;
            _createdBuilding.BlocksCountChanged -= OnBlocksCountChanged;

            _buildingAnimator.LaunchDecreaseBuildingAnimation(_createdBuilding, _confettiParticle);
            _buildingRewardAnimator.LaunchIncreaseRewardAnimation(_buildingRewardImage);
        }
    }

    private void OnBuildingCreated(Building building, int createdBuildingNumber)
    {
        _createdBuilding = building;
        _buildProgressShower.ShowBuildingNumber(createdBuildingNumber);
        _buildProgressShower.ShowBuildPrice(_createdBuilding.BuildBlockPrice);

        _createdBuilding.BlockActivated += OnBlockActivated;
        _createdBuilding.BlocksCountChanged += OnBlocksCountChanged;

        _buildBlockButton.onClick.AddListener(() => _createdBuilding.TryBuildBlock());
        _createdBuilding.CalculateBlocksCount();
        _buildingBuilded = false;
    }

    private void TryActivateBuildButton()
    {
        if (_buildingBuilded == false)
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

        _openBuildMenuButton.onClick.AddListener(() => _buildCanvas.gameObject.SetActive(true));
        _closeBuildMenuButton.onClick.AddListener(() => _buildCanvas.gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _buildBlockAnimator.BlockBuilded -= OnBlockBuilded;
        _buildingCreator.BuildingCreated -= OnBuildingCreated;
        _buildingCreator.BuildingDestroyed -= OnBuildingDestroyed;
        _buildingAnimator.BuildingDecreased -= OnBuildingDecreased;

        _openBuildMenuButton.onClick.RemoveAllListeners();
        _closeBuildMenuButton.onClick.RemoveAllListeners();
    }

    private void Update() => TryActivateBuildButton();
}
