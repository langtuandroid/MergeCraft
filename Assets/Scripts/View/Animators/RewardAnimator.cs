using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class RewardAnimator : MonoBehaviour
{
    [SerializeField] private Image _moneyImage;
    [SerializeField] private Image _buildBlockImage;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _buildBlockText;
    [Space(10), SerializeField] private float _scaleDuration;
    [SerializeField] private float _fadeDuration;
    [Space(10), SerializeField] private Vector3 _targetScaleSize;

    private Sequence _gettingMoneyRewardSequence;
    private Sequence _gettingBlockRewardSequence;

    public void LaunchGettingMoneyRewardAnimation() => 
        _gettingMoneyRewardSequence = LaunchGettingRewardAnimation(_gettingMoneyRewardSequence, _moneyImage, _moneyText);

    public void LaunchGettingBlockRewardAnimation() =>
        _gettingBlockRewardSequence = LaunchGettingRewardAnimation(_gettingBlockRewardSequence, _buildBlockImage, _buildBlockText);

    private Sequence LaunchGettingRewardAnimation(Sequence sequence, Image rewardImage, TMP_Text rewardText)
    {
        rewardImage.color = Color.white;
        rewardText.color = Color.white;

        rewardImage.transform.localScale = Vector3.zero;
        rewardImage.gameObject.SetActive(true);

        sequence = DOTween.Sequence();
        sequence.Append(rewardImage.transform.DOScale(_targetScaleSize, _scaleDuration / 2));
        sequence.Append(rewardImage.transform.DOScale(Vector3.one, _scaleDuration / 2));
        sequence.Append(rewardImage.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        sequence.Insert(_scaleDuration, rewardText.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        sequence.AppendCallback(() => rewardImage.gameObject.SetActive(false));
        sequence.AppendCallback(() => sequence.Kill(true));

        return sequence;
    }

    private void OnDestroy()
    {
        _gettingMoneyRewardSequence.Kill();
        _gettingBlockRewardSequence.Kill();
    }
}
