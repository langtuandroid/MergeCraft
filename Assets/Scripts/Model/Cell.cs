using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public event UnityAction<MergeBlock> CellOccupied;
    public MergeBlock BlockInCell => _blockInCell;

    private Image _cellImage;
    private MergeBlock _blockInCell;

    public void Occupie(MergeBlock block)
    {
        if (block.transform.parent != null)
            if (block.transform.parent.TryGetComponent<Cell>(out Cell previouslyCell))
                previouslyCell.ResetCell();

        block.transform.parent = _cellImage.transform;
        block.transform.localPosition = Vector3.zero;
        block.transform.localScale = Vector3.one;

        _blockInCell = block;
        CellOccupied?.Invoke(_blockInCell);
    }

    private void ResetCell() => _blockInCell = null;
    private void Awake() => _cellImage = GetComponent<Image>();
}
