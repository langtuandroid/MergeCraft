using UnityEngine;
using UnityEngine.Events;
using YG;

public class BuildingCreator : MonoBehaviour, IActivatable
{
    public event UnityAction<Building> BuildingCreated;
    public event UnityAction<int> BuildingNumberChanged;
    public event UnityAction BuildingLimitReached;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private Canvas _buildCanvas;
    [SerializeField] private BlockCreator _blockCreator;
    [Space(10), SerializeField] private Building[] _buildings;
    [SerializeField] private double[] _buildingCreationRewards;
    [SerializeField] private double[] _buildingBlockPrices;

    private int _createdBuildingNumber = 1;
    private Building _createdBuilding;
    private Wallet _wallet;

    public void Activate() => TryCreateBuilding();
    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryCreateBuilding()
    {
        if (_buildings.Length > 0 && _createdBuilding == null)
        {
            if (_createdBuildingNumber - 1 < _blockCreator.CreationBlockLevel + 1)
            {
                _createdBuilding = Instantiate(_buildings[GetBuildingNumber()], _buildCanvas.transform);
                _createdBuilding.Intialize(_wallet, _buildingCreationRewards[_createdBuildingNumber - 1],
                    (int)(_buildingBlockPrices[_createdBuildingNumber - 1] + _wallet.BuildBlocksMoney) / _createdBuilding.BlocksCount);

                BuildingCreated?.Invoke(_createdBuilding);
                BuildingNumberChanged?.Invoke(_createdBuildingNumber - 1);

                YandexGame.savesData.CreatedBuildingNumber = _createdBuildingNumber;
                YandexGame.savesData.CreatedBuilding = _createdBuilding;
                YandexGame.SaveProgress();

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
            _createdBuilding = null;
            BuildingDestroyed?.Invoke();
        }
    }

    private int GetBuildingNumber()
    {
        int buildingNumber;

        if (_createdBuildingNumber - 1 < _buildings.Length)
            buildingNumber = _createdBuildingNumber - 1;
        else
            buildingNumber = Random.Range(0, _buildings.Length);

        return buildingNumber;
    }

    private void OnGetDataEvent()
    {
        SavesYG savesData = YandexGame.savesData;
        _createdBuildingNumber = savesData.CreatedBuildingNumber;

        if (savesData.CreatedBuilding != null)
        {
            if (_createdBuildingNumber - 1 < _blockCreator.CreationBlockLevel + 1)
            {
                _createdBuilding = savesData.CreatedBuilding;
                BuildingCreated?.Invoke(_createdBuilding);
                BuildingNumberChanged?.Invoke(_createdBuildingNumber - 1);
            }
            else
            {
                BuildingLimitReached?.Invoke();
            }
        }
    }

    private void OnEnable() => YandexGame.GetDataEvent += OnGetDataEvent;
    private void OnDisable() => YandexGame.GetDataEvent -= OnGetDataEvent;
}
