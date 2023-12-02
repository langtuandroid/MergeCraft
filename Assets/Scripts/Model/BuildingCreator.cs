using UnityEngine;
using UnityEngine.Events;
using YG;

public class BuildingCreator : MonoBehaviour
{
    public event UnityAction<int> RecoveredBuildingLimitReached;
    public event UnityAction<int> BuildingNumberChanged;
    public event UnityAction<Building> BuildingCreated;
    public event UnityAction BuildingLimitReached;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private Canvas _buildCanvas;
    [SerializeField] private BlockCreator _blockCreator;
    [Space(10), SerializeField] private Building[] _buildings;
    [SerializeField] private double[] _buildingCreationRewards;
    [SerializeField] private double[] _buildingBlockPrices;

    private const int RecoveredNumberOffset = 2;
    private int _createdBuildingNumber = 1;
    private int _destroyedBuildingsCount = 0;
    private Building _createdBuilding = null;
    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryRecoverBuilding()
    {
        SavesYG savesData = YandexGame.savesData;

        if (savesData.CreatedBuildingNumber > 0)
            RecoverBuilding(savesData);
        else
            TryCreateBuilding(); 
    }

    public void TryCreateBuilding()
    {
        if (_buildings.Length > 0 && _createdBuilding == null)
        {
            if (_destroyedBuildingsCount < _blockCreator.CreationBlockLevel + 1)
            {
                int buildingPrefabNumber = GetBuildingPrefabNumber();
                CreateBuilding(buildingPrefabNumber);

                BuildingNumberChanged?.Invoke(_createdBuildingNumber - 1);
                _createdBuildingNumber++;

                YandexGame.savesData.CreatedBuildingNumber = _createdBuildingNumber;
                YandexGame.savesData.CreatedBuildingPrefabNumber = buildingPrefabNumber;
                YandexGame.SaveProgress();
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
            _createdBuilding = null;
            _destroyedBuildingsCount++;

            YandexGame.savesData.BuildingBlocksActivity = null;
            YandexGame.savesData.DestroyedBuildingsCount = _destroyedBuildingsCount;
            YandexGame.SaveProgress();

            BuildingDestroyed?.Invoke();
        }
    }

    private int GetBuildingPrefabNumber()
    {
        int buildingNumber;

        if (_destroyedBuildingsCount < _buildings.Length)
            buildingNumber = _destroyedBuildingsCount;
        else
            buildingNumber = Random.Range(0, _buildings.Length);

        return buildingNumber;
    }

    private void RecoverBuilding(SavesYG savesData)
    {
        _createdBuildingNumber = savesData.CreatedBuildingNumber;
        _destroyedBuildingsCount = savesData.DestroyedBuildingsCount;
        BuildingNumberChanged?.Invoke(_createdBuildingNumber - RecoveredNumberOffset);

        Debug.Log("_destroyedBuildingsCount " + _destroyedBuildingsCount);
        Debug.Log("_blockCreator.CreationBlockLevel " + (_blockCreator.CreationBlockLevel + 1));

        if (_destroyedBuildingsCount < _blockCreator.CreationBlockLevel + 1)
        {
            CreateBuilding(savesData.CreatedBuildingPrefabNumber);
            Debug.Log("Пересоздал");
        }    
        else
        {
            RecoveredBuildingLimitReached?.Invoke(_buildings[savesData.CreatedBuildingPrefabNumber].BlocksCount);
            Debug.Log("Не Пересоздал из-за лимита");
        }
    }

    private void CreateBuilding(int buildingPrefabNumber)
    {
        _createdBuilding = Instantiate(_buildings[buildingPrefabNumber], _buildCanvas.transform);
        _createdBuilding.Intialize(_wallet, _buildingCreationRewards[_destroyedBuildingsCount],
            (int)(_buildingBlockPrices[_destroyedBuildingsCount] + _wallet.BuildBlocksMoney) / _createdBuilding.BlocksCount);

        BuildingCreated?.Invoke(_createdBuilding);
    }
}
