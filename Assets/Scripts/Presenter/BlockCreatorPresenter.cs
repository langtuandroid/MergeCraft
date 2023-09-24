using UnityEngine;

public class BlockCreatorPresenter : MonoBehaviour
{
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private CreationProgressShower _creationProgressShower;

    private void OnCreationProgressChanged(float progress) => _creationProgressShower.Show(progress);
    private void OnDisable() => _blockCreator.CreationProgressChanged -= OnCreationProgressChanged;
    private void OnEnable() => _blockCreator.CreationProgressChanged += OnCreationProgressChanged;

    private void Update()
    {
        if (_blockCreator.CreatorActivated == true)
            _blockCreator.TryCreateBlock();
    }
}
