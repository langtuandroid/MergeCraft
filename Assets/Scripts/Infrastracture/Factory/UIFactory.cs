using UnityEngine;

public class UIFactory : IUIFactory
{
    public Hud CreatedHud => _createdHud;

    private Hud _hudPrefab;
    private Hud _createdHud;

    public void CreateHud() => _createdHud = Create(_hudPrefab);
    public UIFactory(Hud hudPrefab) => _hudPrefab = hudPrefab;
    private Hud Create(Hud hud) => Object.Instantiate(hud);
}
