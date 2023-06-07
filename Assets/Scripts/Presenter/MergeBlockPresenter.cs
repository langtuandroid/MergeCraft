using UnityEngine;

[RequireComponent(typeof(MergeBlock))]
public class MergeBlockPresenter : MonoBehaviour
{
    [SerializeField] private MergeBlock _mergeBlock;
    [SerializeField] private BlockDragger _blockDragger;
    [SerializeField] private MergeListener _mergeListener;
    [SerializeField] private MergeBlockAnimator _mergeBlockAnimator;

    private void OnCreateAnimationLaunched()
    {
        _blockDragger.DeactivateDrag();
        _mergeListener.Deactivate();
    }

    private void OnCreateAnimationFinished()
    {
        _blockDragger.ActivateDrag();
        _mergeListener.Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MergeBlock>(out MergeBlock mergeBlock))
            if (_mergeBlock.BlockLevel == mergeBlock.BlockLevel)
                if (_mergeBlock.MergeActivated)
                    _mergeBlock.CreateMergedBlock(mergeBlock.gameObject);
    }

    private void Update()
    {
        if (_mergeBlock.MergeActivated)
            _mergeBlock.LaunchMergeTimer();
    }

    private void OnEnable()
    {
        _mergeBlockAnimator.AnimationLaunched += OnCreateAnimationLaunched;
        _mergeBlockAnimator.AnimationFinished += OnCreateAnimationFinished;
    }

    private void OnDisable()
    {
        _mergeBlockAnimator.AnimationLaunched -= OnCreateAnimationLaunched;
        _mergeBlockAnimator.AnimationFinished -= OnCreateAnimationFinished;
    }
}
