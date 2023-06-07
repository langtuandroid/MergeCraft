using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardChestAnimator : MonoBehaviour
{
    [SerializeField] private Image _chestImage;

    private readonly int _openingMoveStep = 75;
    private readonly float _openingAnimationDuration = 0.5f;
    private readonly Color _openingChestColor = new Color(1, 1, 1, 0);

    public void LaunchOpenChestAnimation()
    {
        _chestImage.transform.DOLocalMoveY(_chestImage.transform.position.y + _openingMoveStep, _openingAnimationDuration);
        _chestImage.DOColor(_openingChestColor, _openingAnimationDuration).OnComplete(() => _chestImage.gameObject.SetActive(false));
    }

    private void OnDestroy() => DOTween.Kill(_chestImage);
}
