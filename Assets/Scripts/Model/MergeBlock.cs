 using UnityEngine;

public class MergeBlock : MonoBehaviour
{
    public int BlockLevel => _blockLevel;
    public bool MergeActivated => _mergeActivated;

    [SerializeField] private BoxCollider2D _triggerCollider;
    [SerializeField] private RewardChest _chest;
    [SerializeField] private MergeBlock _mergedBlock;
    [SerializeField] private int _blockLevel;

    private bool _mergeActivated;
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
        MergeBlock mergedBlock = Instantiate(_mergedBlock, transform.position, Quaternion.identity);

        Cell cell = transform.parent.GetComponent<Cell>();
        cell.Occupie(mergedBlock);

        mergedBlock.GetComponent<RewardChest>().Initialize(_chest.Wallet);
        mergedBlock.GetComponent<MergeBlockAnimator>().LaunchMergeBlockAnimation();

        Destroy(touchedBlock);
        Destroy(gameObject);
    }

    private void SetMergeActive(bool mergeActive)
    {
        _mergeActivated = mergeActive;
        _triggerCollider.enabled = mergeActive;
    }

    private void OnValidate()
    {
        if (_triggerCollider.isTrigger == false)
            _triggerCollider = null;
    }

    private void Awake() => DeactivateMerge();
}
