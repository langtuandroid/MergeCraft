using UnityEngine;
using UnityEngine.UI;

public class BuildingPresenter : MonoBehaviour
{
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private Button _buildBlockButton;
    [SerializeField] private Button _openBuildMenuButton;
    [SerializeField] private Canvas _buildCanvas;

    private Building _createdBuilding;

    private void OnBlockBuilded()
    {
        if (_createdBuilding.BuildedBlocksCount ==_createdBuilding.BlocksCount)
        {
            _buildBlockButton.onClick.RemoveAllListeners();
            _createdBuilding.BlockBuilded -= OnBlockBuilded;
            _buildingCreator.TryDestroyCreatedBuilding();
        }
    }

    private void OnBuildingCreated(Building building)
    {
        _createdBuilding = building;
        _createdBuilding.BlockBuilded += OnBlockBuilded;
        _buildBlockButton.onClick.AddListener(() => _createdBuilding.TryBuildBlock());
    }

    private void BuildingDestroyed()
    {
        _buildingCreator.TryCreateBuilding();
    }

    private void OnEnable()
    {
        _buildingCreator.BuildingCreated += OnBuildingCreated;
        _buildingCreator.BuildingDestroyed += BuildingDestroyed;
        _openBuildMenuButton.onClick.AddListener(() => _buildCanvas.gameObject.SetActive(true));
    }

    private void OnDisable()
    {
        _buildingCreator.BuildingCreated -= OnBuildingCreated;
        _buildingCreator.BuildingDestroyed -= BuildingDestroyed;
        _openBuildMenuButton.onClick.RemoveAllListeners();
    }
}
