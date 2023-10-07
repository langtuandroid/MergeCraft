 using UnityEngine;

public class MergeBlock : MonoBehaviour
{
    public bool CanCreateMergedBlock => _mergedBlock != null && _rewardChest.RewardApplied;
    public bool MergeActivated => _mergeActivated;
    public int BlockLevel => _blockLevel;
    public MergeBlockAnimator MergeBlockAnimator => _mergeBlockAnimator;
    public RewardAnimator RewardAnimator => _rewardAnimator;
    public RewardShower RewardShower => _rewardShower;
    public RewardChest RewardChest => _rewardChest;

    [SerializeField] private MergeBlockAnimator _mergeBlockAnimator;
    [SerializeField] private RewardAnimator _rewardAnimator;
    [SerializeField] private RewardShower _rewardShower;
    [SerializeField] private RewardChest _rewardChest;
    [SerializeField] private BoxCollider2D _triggerCollider;
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

    public void TryCreateMergedBlock(MergeBlock touchedBlock)
    {
        if (CanCreateMergedBlock && touchedBlock.CanCreateMergedBlock)
        {
            MergeBlock mergedBlock = Instantiate(_mergedBlock, transform.position, Quaternion.identity);

            Cell cell = transform.parent.GetComponent<Cell>();
            cell.Occupie(mergedBlock);

            mergedBlock.RewardChest.Initialize(_rewardChest.Wallet);
            mergedBlock.MergeBlockAnimator.LaunchMergeBlockAnimation();

            Destroy(touchedBlock.gameObject);
            Destroy(gameObject);
        }
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
