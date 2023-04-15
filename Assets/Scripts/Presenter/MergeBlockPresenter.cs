using UnityEngine;

[RequireComponent(typeof(MergeBlock))]
public class MergeBlockPresenter : MonoBehaviour
{
    private MergeBlock _mergeBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MergeBlock>())
            if (_mergeBlock.MergeActivated)
                _mergeBlock.CreateMergedBlock(collision.gameObject);
    }

    private void Update()
    {
        if (_mergeBlock.MergeActivated)
            _mergeBlock.LaunchMergeTimer();
    }

    private void Awake() => _mergeBlock = GetComponent<MergeBlock>();
}
