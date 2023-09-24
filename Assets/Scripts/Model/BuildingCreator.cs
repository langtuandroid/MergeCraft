using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingCreator : MonoBehaviour, IActivatable
{
    public event UnityAction<Building, int> BuildingCreated;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private int _firstBuildingReward;
    [SerializeField] private Canvas _buildCanvas;
    [Space(10), SerializeField] private Building[] _buildings;

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
            _createdBuilding = Instantiate(GetBuildingPrefab(GetBuildingNumber()), _buildCanvas.transform);
            _createdBuilding.Intialize(_wallet, _firstBuildingReward * _createdBuildingNumber);

            BuildingCreated?.Invoke(_createdBuilding, _createdBuildingNumber - 1);
            _createdBuildingNumber++;
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
