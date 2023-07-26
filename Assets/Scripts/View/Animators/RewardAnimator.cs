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

    private Sequence _gettingRewardSequence;

    public void LaunchGettingMoneyRewardAnimation() => LaunchGettingRewardAnimation(_moneyImage, _moneyText);
    public void LaunchGettingBlockRewardAnimation() => LaunchGettingRewardAnimation(_buildBlockImage, _buildBlockText);

    private void LaunchGettingRewardAnimation(Image rewardImage, TMP_Text rewardText)
    {
        rewardImage.color = Color.white;
        rewardText.color = Color.white;

        rewardImage.transform.localScale = Vector3.zero;
        rewardImage.gameObject.SetActive(true);

        _gettingRewardSequence = DOTween.Sequence();
        _gettingRewardSequence.Append(rewardImage.transform.DOScale(_targetScaleSize, _scaleDuration / 2));
        _gettingRewardSequence.Append(rewardImage.transform.DOScale(Vector3.one, _scaleDuration / 2));
        _gettingRewardSequence.Append(rewardImage.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        _gettingRewardSequence.Insert(_scaleDuration, rewardText.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        _gettingRewardSequence.AppendCallback(() => rewardImage.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        _gettingRewardSequence.Kill(true);

        DOTween.Kill(_moneyText);
        DOTween.Kill(_moneyImage);
        DOTween.Kill(_buildBlockText);
        DOTween.Kill(_buildBlockImage);
    }
}
