using UnityEngine;
using DG.Tweening;

public class BuildBlock : MonoBehaviour
{
    private void OnDestroy() => DOTween.Kill(transform);
}
