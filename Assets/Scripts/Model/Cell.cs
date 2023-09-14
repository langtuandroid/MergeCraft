using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public event UnityAction<MergeBlock> CellOccupied;
    public MergeBlock BlockInCell => _blockInCell;
    public bool Blocked => _blocked;

    [SerializeField] private bool _blocked;
    [SerializeField] private Sprite _blockedSprite;
    [SerializeField] private Sprite _unblockedSprite;

    private Image _cellImage;
    private MergeBlock _blockInCell;

    public void Unblock() => SetBlockActive(false, _unblockedSprite);

    public void Occupie(MergeBlock block)
    {
        if (block.transform.parent != null)
            if (block.transform.parent.TryGetComponent<Cell>(out Cell previouslyCell))
                previouslyCell.ResetCell();

        block.transform.parent = _cellImage.transform;
        block.transform.localPosition = Vector3.zero;

        _blockInCell = block;
        CellOccupied?.Invoke(_blockInCell);
    }

    private void SetBlockActive(bool blockActive, Sprite cellSprite)
    {
        _cellImage.sprite = cellSprite;
        _blocked = blockActive;
    }

    private void OnValidate()
    {
        if (_blocked)
            SetBlockActive(true, _blockedSprite);
    }

    private void ResetCell() => _blockInCell = null;
    private void Awake() => _cellImage = GetComponent<Image>();
}
