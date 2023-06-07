using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class MergeBlockAnimator : MonoBehaviour
{
    public event UnityAction AnimationLaunched;
    public event UnityAction AnimationFinished;
    public bool AnimationPlaying => _animationPlaying;

    private Sequence _increaseScaleSequence;
    private Image _mergeBlockImage;
    private bool _animationPlaying;
    private readonly float _mergeAnimationDuration = 0.5f;

    public void LaunchCreateBlockAnimation(float duration) => LaunchIncreaseScaleAnimation(duration);
    public void LaunchMergeBlockAnimation() => LaunchIncreaseScaleAnimation(_mergeAnimationDuration);

    private void LaunchIncreaseScaleAnimation(float duration)
    {
        _animationPlaying = true;
        transform.localScale = Vector3.zero;
        _mergeBlockImage.color = Color.gray;
        AnimationLaunched?.Invoke();

        _increaseScaleSequence = DOTween.Sequence();
        _increaseScaleSequence.Append(transform.DOScale(Vector3.one, duration - 0.25f));
        _increaseScaleSequence.AppendCallback(() => _mergeBlockImage.color = Color.white);
        _increaseScaleSequence.Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f));
        _increaseScaleSequence.Append(transform.DOScale(Vector3.one, 0.15f));
        _increaseScaleSequence.AppendCallback(() => AnimationFinished?.Invoke());
        _increaseScaleSequence.AppendCallback(() => _animationPlaying = false);
    }

    private void Awake() => _mergeBlockImage = GetComponent<Image>();

    private void OnDestroy() => _increaseScaleSequence.Kill(true);
}
