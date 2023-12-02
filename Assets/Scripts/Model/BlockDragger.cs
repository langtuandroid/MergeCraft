using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform), typeof(Image), typeof(MergeBlock))]
public class BlockDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _positionResetDuration;

    private RectTransform _rectTransform;
    private MergeBlock _mergeBlock;
    private Camera _mainCamera;
    private Image _blockImage;
    private Vector3 _offset;

    public void ActivateDrag() => _blockImage.raycastTarget = true;
    public void DeactivateDrag() => _blockImage.raycastTarget = false;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(eventData.position);
        _rectTransform.position = newPosition + _offset;
    } 

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = _rectTransform.position - _mainCamera.ScreenToWorldPoint(eventData.position);
        _rectTransform.parent.SetAsLastSibling();
        _blockImage.raycastTarget = false;
        _mergeBlock.DeactivateMerge();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.DOLocalMove(Vector3.zero, _positionResetDuration);
        _blockImage.raycastTarget = true;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _blockImage = GetComponent<Image>();
        _mergeBlock = GetComponent<MergeBlock>();
        _mainCamera = Camera.main;
    }

    private void OnDestroy() => DOTween.Kill(_rectTransform);
}
