using UnityEngine;

public class FactoriesPrefabs : MonoBehaviour
{
    public Hud HudPrefab => _hudPrefab;

    [SerializeField] private Hud _hudPrefab;
}
