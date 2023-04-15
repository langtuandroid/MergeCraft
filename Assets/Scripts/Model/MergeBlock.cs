using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MergeBlock : MonoBehaviour
{
    public bool MergeActivated => _mergeActivated;

    [SerializeField] private GameObject _mergedBlock;
    [SerializeField] private int _blockLevel;

    private bool _mergeActivated;
    private BoxCollider2D _collider;
    private float _passedMergeTime = 0.05f;
    private readonly float _mergeDuration = 0.05f;

    public void DeactivateMerge() => SetMergeActive(false);

    public void ActivateMerge()
    {
        SetMergeActive(true);
        _passedMergeTime = 0;
    }

    public void LaunchMergeTimer()
    {
        _passedMergeTime += Time.deltaTime;

        if (_passedMergeTime >= _mergeDuration)
            DeactivateMerge();
    }

    public void CreateMergedBlock(GameObject touchedBlock)
    {
        GameObject mergedBlock = Instantiate(_mergedBlock, transform.position, Quaternion.identity);
        Cell cell = transform.parent.GetComponent<Cell>();
        cell.Occupie(mergedBlock.transform);
        mergedBlock.transform.localScale = Vector3.one;

        Destroy(touchedBlock);
        Destroy(gameObject);
    }

    private void SetMergeActive(bool mergeActive)
    {
        _mergeActivated = mergeActive;
        _collider.enabled = mergeActive;
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        DeactivateMerge();
    }
}
