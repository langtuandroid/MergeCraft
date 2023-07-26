using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Cell))]
public class CellPresenter : MonoBehaviour, IDropHandler
{
    [SerializeField] private Cell _cell;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.TryGetComponent<MergeBlock>(out MergeBlock mergeBlock);

        if (_cell.BlockInCell != null)
        {
            MergeBlockAnimator mergeBlockAnimator = _cell.BlockInCell.GetComponent<MergeBlockAnimator>();

            if (mergeBlock.BlockLevel == _cell.BlockInCell.BlockLevel && mergeBlockAnimator.AnimationPlaying == false)
                Occupie(mergeBlock);
        }
        else
        {
            Occupie(mergeBlock);
        }
    }

    private void Occupie(MergeBlock mergeBlock)
    {
        _cell.Occupie(mergeBlock);
        mergeBlock.transform.localScale = Vector3.one;
        mergeBlock.ActivateMerge();
    }
}
