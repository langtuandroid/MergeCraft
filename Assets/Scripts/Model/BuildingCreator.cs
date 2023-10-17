using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingCreator : MonoBehaviour, IActivatable
{
    public event UnityAction<Building, int> BuildingCreated;
    public event UnityAction BuildingLimitReached;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private Canvas _buildCanvas;
    [SerializeField] private BlockCreator _blockCreator;
    [Space(10), SerializeField] private Building[] _buildings;
    [SerializeField] private double[] _buildingCreationRewards;
    [SerializeField] private double[] _buildingBlockPrices;

    private List<Building> _bigBuildings = new List<Building>();
    private const int BigBuildingBlockCount = 400;
    private int _createdBuildingNumber = 1;
    private Building _createdBuilding;
    private Wallet _wallet;

    public void Activate() => TryCreateBuilding();
    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryCreateBuilding()
    {
        if (_buildings.Length > 0)
        {
            if (_createdBuildingNumber - 1 < _blockCreator.CreationBlockLevel + 1)
            {
                _createdBuilding = Instantiate(GetBuildingPrefab(GetBuildingNumber()), _buildCanvas.transform);
                _createdBuilding.Intialize(_wallet, _buildingCreationRewards[_createdBuildingNumber - 1], 
                    (int)_buildingBlockPrices[_createdBuildingNumber - 1] / _createdBuilding.BlocksCount);

                BuildingCreated?.Invoke(_createdBuilding, _createdBuildingNumber - 1);
                _createdBuildingNumber++;
            }
            else
            {
                BuildingLimitReached?.Invoke();
            }
        }
    }

    public void TryDestroyCreatedBuilding()
    {
        if (_createdBuilding != null)
        {
            Destroy(_createdBuilding.gameObject);
            BuildingDestroyed?.Invoke();
        }
    }

    private int GetBuildingNumber()
    {
        int buildingNumber;

        if (_createdBuildingNumber - 1 < _buildings.Length)
            buildingNumber = _createdBuildingNumber - 1;
        else if (_bigBuildings.Count > 0)
            buildingNumber = Random.Range(0, _bigBuildings.Count);
        else
            buildingNumber = Random.Range(0, _buildings.Length);

        return buildingNumber;
    }

    private Building GetBuildingPrefab(int buildingNumber)
    {
        Building prefab;

        if (buildingNumber < _buildings.Length || _bigBuildings.Count < 0)
            prefab = _buildings[buildingNumber];
        else
            prefab = _bigBuildings[buildingNumber];

        return prefab;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _buildings.Length; i++)
        {
            if (_buildings[i].BlocksCount >= BigBuildingBlockCount)
                _bigBuildings.Add(_createdBuilding);
        }
    }
}
