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
    [Space(10), SerializeField] private Sprite[] _mergeBlockSprites;
    [Space(10), SerializeField] private string[] _russianMergeBlockNames;
    [SerializeField] private string[] _englishMergeBlockNames;

    private int _displayedBlockNumber;
    private readonly int _firstBlockNumber = 0;

    private bool CanDeactivateNextBlockButton => _displayedBlockNumber + 1 > _mergeBlockSprites.Length - 1;
    private bool CanDeactivatePreviouslyBlockButton => _displayedBlockNumber - 1 < 0;

    public void SwitchToNextBlock() => SwitchTo(_displayedBlockNumber + 1);
    public void SwitchToPreviouslyBlock() => SwitchTo(_displayedBlockNumber - 1);
    public string GetBlockName(int nameNumber) => _englishMergeBlockNames[nameNumber];
    public Sprite GetBlockSprite(int spriteNumber) => _mergeBlockSprites[spriteNumber];

    private void TryDeactivateNextBlockButton() => SetButtonActive(CanDeactivateNextBlockButton, _nextBlockButton);
    private void TryDeactivatePreviouslyBlockButton() => SetButtonActive(CanDeactivatePreviouslyBlockButton, _previouslyBlockButton);

    private void SwitchTo(int switchNumber)
    {
        _displayedBlockNumber = switchNumber;
        _blockNumberText.text = (switchNumber + 1).ToString();
        _blockImage.sprite = _mergeBlockSprites[switchNumber];
        _blockNameText.text = _englishMergeBlockNames[switchNumber];

        TryDeactivatePreviouslyBlockButton();
        TryDeactivateNextBlockButton();
    }

    private void SetButtonActive(bool canDeactivateButton, Button button)
    {
        if (canDeactivateButton)
            button.interactable = false;
        else
            button.interactable = true;
    }

    private void OnEnable()
    {
        if (_mergeBlockSprites.Length >= 1)
            SwitchTo(_firstBlockNumber);
    }
}
