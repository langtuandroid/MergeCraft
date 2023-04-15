using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MergeBlockAnimator : MonoBehaviour
{
    private Sequence _createBlockSequence;
    private Image _mergeBlockImage;

    public void LaunchCreateBlockAnimation(float duration)
    {
        _mergeBlockImage.raycastTarget = false;

        _createBlockSequence = DOTween.Sequence();
        _createBlockSequence.Append(transform.DOScale(Vector3.one, duration - 0.25f));
        _createBlockSequence.Append(transform.DOScale(new Vector3(1.1f, 1.1f ,1.1f), 0.1f));
        _createBlockSequence.Append(transform.DOScale(Vector3.one, 0.15f));
        _createBlockSequence.AppendCallback(() => _mergeBlockImage.raycastTarget = true);
    }

    private void Awake()
    {
        _mergeBlockImage = GetComponent<Image>();
        transform.localScale = Vector3.zero;
    }
}
