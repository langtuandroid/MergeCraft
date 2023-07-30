using UnityEngine;
using UnityEngine.Events;

public class BuildingCreator : MonoBehaviour
{
    public event UnityAction<Building> BuildingCreated;
    public event UnityAction BuildingDestroyed;

    [SerializeField] private Canvas _buildCanvas;
    [Space(10), SerializeField] private Building[] _buildings;

    private int _currentBuildingNumber;
    private Building _createdBuilding;

    public void TryCreateBuilding()
    {
        if (_currentBuildingNumber < _buildings.Length)
        {
            _createdBuilding = Instantiate(_buildings[_currentBuildingNumber], _buildCanvas.transform);
            BuildingCreated?.Invoke(_createdBuilding);
        }
    }

    public void TryDestroyCreatedBuilding()
    {
        if (_createdBuilding != null)
        {
            Destroy(_createdBuilding.gameObject);
            _currentBuildingNumber++;
            BuildingDestroyed?.Invoke();
        }
    }

    private void Start() => TryCreateBuilding();
}
