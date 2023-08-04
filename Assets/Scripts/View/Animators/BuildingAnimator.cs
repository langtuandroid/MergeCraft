using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BuildingAnimator
{
    public event UnityAction BuildingDecreased;
    public bool DecreaseAnimationLaunched => _decreaseBuildingSequence != null;

    private Sequence _decreaseBuildingSequence;
    private readonly float _pushDelay = 0.25f;
    private readonly float _decreaseDelay = 1f;
    private readonly float _increasePushScale = 1.25f;
    private readonly float _decreasePushScale = 1f;
    private readonly float _decreaseScale = 0;

    public void LaunchDecreaseBuildingAnimation(Building building, ParticleSystem confetti)
    {
        _decreaseBuildingSequence = DOTween.Sequence();
        //_decreaseBuildingSequence.InsertCallback(0, () => confetti.Play());
        _decreaseBuildingSequence.Append(building.transform.DOScale(_increasePushScale, _pushDelay));
        _decreaseBuildingSequence.Append(building.transform.DOScale(_decreasePushScale, _pushDelay));
        _decreaseBuildingSequence.Append(building.transform.DOScale(_decreaseScale, _decreaseDelay));
        _decreaseBuildingSequence.AppendCallback(() => BuildingDecreased?.Invoke());
        _decreaseBuildingSequence.AppendCallback(() => _decreaseBuildingSequence = null);
    }
}
