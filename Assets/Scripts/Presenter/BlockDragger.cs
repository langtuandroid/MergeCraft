using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image), typeof(MergeBlock))]
public class BlockDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private Image _blockImage;
    private MergeBlock _mergeBlock;

    public void OnDrag(PointerEventData eventData) => _rectTransform.anchoredPosition += eventData.delta / _rectTransform.parent.transform.localScale.x;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.parent.SetAsLastSibling();
        _blockImage.raycastTarget = false;
        _mergeBlock.DeactivateMerge();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _blockImage.raycastTarget = true;
        _mergeBlock.ActivateMerge();
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _blockImage = GetComponent<Image>();
        _mergeBlock = GetComponent<MergeBlock>();
    }
}
