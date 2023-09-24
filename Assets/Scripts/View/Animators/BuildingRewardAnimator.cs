using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRewardAnimator
{
    private Sequence _rewardSequence;
    private const float IntermediateScaleDuration = 0.25f;
    private const float DefaultScaleDuration = 1f;
    private readonly Vector3 _startScale = Vector3.zero;
    private readonly Vector3 _defaultScale = Vector3.one;
    private readonly Vector3 _intermediateScale = new Vector3(1.25f, 1.25f, 1.25f);

    public void LaunchIncreaseRewardAnimation(Image rewardImage)
    {
        rewardImage.transform.localScale = _startScale;
        rewardImage.gameObject.SetActive(true);

        _rewardSequence = DOTween.Sequence();
        _rewardSequence.Append(rewardImage.transform.DOScale(_defaultScale, DefaultScaleDuration));
        _rewardSequence.Append(rewardImage.transform.DOScale(_intermediateScale, IntermediateScaleDuration));
        _rewardSequence.Append(rewardImage.transform.DOScale(_startScale, DefaultScaleDuration));
        _rewardSequence.AppendCallback(() => rewardImage.gameObject.SetActive(false));
    }
}
