using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void TryInitialize()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            _canvas.worldCamera = Camera.main;
    }

    private void OnEnable() => TryInitialize();
}
