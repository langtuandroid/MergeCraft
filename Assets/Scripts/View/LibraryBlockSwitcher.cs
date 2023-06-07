using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryBlockSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text _blockNameText;
    [SerializeField] private TMP_Text _blockNumberText;
    [Space(10), SerializeField] private Image _blockImage;
    [SerializeField] private Button _nextBlockButton;
    [SerializeField] private Button _previouslyBlockButton;
    [Space(10), SerializeField] private MergeBlock[] _mergeBlocks;

    private int _displayedBlockNumber;

    private bool CanDeactivateNextBlockButton => _displayedBlockNumber + 1 > _mergeBlocks.Length - 1;
    private bool CanDeactivatePreviouslyBlockButton => _displayedBlockNumber - 1 < 0;

    public void SwitchToNextBlock() => SwitchTo(_displayedBlockNumber + 1);
    public void SwitchToPreviouslyBlock() => SwitchTo(_displayedBlockNumber - 1);
    private void TryDeactivateNextBlockButton() => SetButtonActive(CanDeactivateNextBlockButton, _nextBlockButton);
    private void TryDeactivatePreviouslyBlockButton() => SetButtonActive(CanDeactivatePreviouslyBlockButton, _previouslyBlockButton);

    private void SwitchTo(int switchNumber)
    {
        _displayedBlockNumber = switchNumber;
        _blockNumberText.text = switchNumber.ToString();
        _blockImage.sprite = _mergeBlocks[switchNumber].GetComponent<Image>().sprite;

        TryDeactivatePreviouslyBlockButton();
        TryDeactivateNextBlockButton();
    }

    private void SetButtonActive(bool deactivateButton, Button button)
    {
        if (deactivateButton)
            button.interactable = false;
        else
            button.interactable = true;
    }

    private void OnEnable()
    {
        if (_mergeBlocks.Length >= 1)
            SwitchTo(0);
    }
}
