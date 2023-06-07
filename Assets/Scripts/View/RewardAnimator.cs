using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class RewardAnimator : MonoBehaviour
{
    [SerializeField] private Image _moneyImage;
    [SerializeField] private TMP_Text _moneyText;
    [Space(10), SerializeField] private float _scaleDuration;
    [SerializeField] private float _fadeDuration;
    [Space(10), SerializeField] private Vector3 _targetScaleSize;

    private Sequence _gettingRewardSequence;

    public void LaunchGettingRewardAnimation()
    {
        _moneyImage.color = Color.white;
        _moneyText.color = Color.white;

        _moneyImage.transform.localScale = Vector3.zero;
        _moneyImage.gameObject.SetActive(true);

        _gettingRewardSequence = DOTween.Sequence();
        _gettingRewardSequence.Append(_moneyImage.transform.DOScale(_targetScaleSize, _scaleDuration / 2));
        _gettingRewardSequence.Append(_moneyImage.transform.DOScale(Vector3.one, _scaleDuration / 2));
        _gettingRewardSequence.Append(_moneyImage.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        _gettingRewardSequence.Insert(_scaleDuration, _moneyText.DOColor(new Color(1, 1, 1, 0), _fadeDuration));
        _gettingRewardSequence.AppendCallback(() => _moneyImage.gameObject.SetActive(false));
    }

    private void OnDestroy() => _gettingRewardSequence.Kill(true);
}
