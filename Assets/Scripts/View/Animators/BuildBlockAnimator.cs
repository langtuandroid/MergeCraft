using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BuildBlockAnimator
{
    public event UnityAction BlockBuilded;

    private readonly int _buildHeight = 250;
    private readonly float _buildDuration = 0.35f;

    public void LaunchBuildAnimation(BuildBlock buildBlock)
    {
        float targetPosition = buildBlock.transform.localPosition.y;
        float startPosition = targetPosition + _buildHeight;

        buildBlock.transform.localPosition = new Vector3(
            buildBlock.transform.localPosition.x, startPosition, buildBlock.transform.localPosition.z);

        buildBlock.transform.DOLocalMoveY(targetPosition, _buildDuration).OnComplete(() => BlockBuilded?.Invoke());
    }
}
