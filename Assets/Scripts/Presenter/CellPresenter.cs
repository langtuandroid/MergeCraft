using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Cell))]
public class CellPresenter : MonoBehaviour, IDropHandler
{
    private Cell _cell;

    public void OnDrop(PointerEventData eventData)
    {
        Transform blockTransform = eventData.pointerDrag.transform;
        _cell.Occupie(blockTransform);
        blockTransform.localScale = Vector3.one;
    }

    private void Awake() => _cell = GetComponent<Cell>();
}
