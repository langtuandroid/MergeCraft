using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public bool Occupied => _cellImage.transform.childCount > 0;
    public bool Blocked => _blocked;

    [SerializeField] private bool _blocked;
    [SerializeField] private Sprite _blockedSprite;
    [SerializeField] private Sprite _unblockedSprite;

    private Image _cellImage;

    public void Block() => SetBlockActive(true, _blockedSprite);
    public void Unblock() => SetBlockActive(false, _unblockedSprite);

    public void Occupie(Transform block)
    {
        block.parent = _cellImage.transform;
        block.localPosition = Vector3.zero;
    }

    private void SetBlockActive(bool blockActive, Sprite cellSprite)
    {
        _cellImage.sprite = cellSprite;
        _blocked = blockActive;
    }

    private void Awake() => _cellImage = GetComponent<Image>();
}
