using UnityEngine;

public class AssetProvider : MonoBehaviour
{
    public Hud HudPrefab => _hudPrefab;

    [SerializeField] private Hud _hudPrefab;
}
