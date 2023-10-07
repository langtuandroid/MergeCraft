using UnityEngine;
using DG.Tweening;

public class PanelAnimator
{
    private const float ScaleDuration = 0.25f;

    public void LaunchIncreaseAnimation(GameObject panel)
    {
        float targetScale = panel.transform.localScale.x;

        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(targetScale, ScaleDuration);
    }
}
