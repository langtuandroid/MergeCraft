using UnityEngine;
using UnityEngine.Events;

public class BuildingCreator : MonoBehaviour, IActivatable
{
    public event UnityAction<Building, int> BuildingCreated;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private Canvas _buildCanvas;
    [Space(10), SerializeField] private Building[] _buildings;

    private int _createdBuildingNumber;
    private Building _createdBuilding;
    private Wallet _wallet;

    public void Activate() => TryCreateBuilding();
    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryCreateBuilding()
    {
        if (_buildings.Length > 0)
        {
            int buildingNumber;

            if (_createdBuildingNumber < _buildings.Length)
                buildingNumber = _createdBuildingNumber;
            else
                buildingNumber = Random.Range(0, _buildings.Length);

            _createdBuilding = Instantiate(_buildings[buildingNumber], _buildCanvas.transform);
            _createdBuilding.Intialize(_wallet);

            BuildingCreated?.Invoke(_createdBuilding, _createdBuildingNumber);
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
}
