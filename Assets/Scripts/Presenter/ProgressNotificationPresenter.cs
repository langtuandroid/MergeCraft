using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressNotificationPresenter : MonoBehaviour
{
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private LibraryBlockSwitcher _libraryBlockSwitcher;
    [SerializeField] private SoundPlayer _soundPlayer;
    [Space(10), SerializeField] private GameObject _notificationPanel;
    [SerializeField] private GameObject _notificationTouchBlocker;
    [SerializeField] private Image _achievedBlockImage;
    [SerializeField] private TMP_Text _achievedBlockNameText;
    [SerializeField] private TMP_Text _newBlockText;
    [SerializeField] private Button _closeNotificationButton;
    [Space(10), SerializeField] private Cell[] _cells;

    private PanelAnimator _panelAnimator = new PanelAnimator();
    private int _achievedBlockLevel = 1;

    private void OnCellOccupied(MergeBlock mergeBlock)
    {
        _soundPlayer.PlayOccupieSound();

        if (mergeBlock.BlockLevel > _achievedBlockLevel)
            ShowNotification(mergeBlock.BlockLevel);
    }

    private void ShowNotification(int blockLevel)
    {
        _newBlockText.text = _translatesContainer.SelectedTranslate.NewBlockNotification;

        _achievedBlockImage.sprite = _libraryBlockSwitcher.GetBlockSprite(blockLevel - 1);
        _achievedBlockNameText.text = _translatesContainer.SelectedTranslate.GetBlockName(blockLevel - 1);

        _notificationTouchBlocker.SetActive(true);
        _panelAnimator.LaunchIncreaseAnimation(_notificationPanel);
        _achievedBlockLevel = blockLevel;
    }

    private void OnEnable()
    {
        foreach (var cell in _cells)
            cell.CellOccupied += OnCellOccupied;

        _closeNotificationButton.onClick.AddListener(() => _notificationTouchBlocker.SetActive(false));
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
            cell.CellOccupied -= OnCellOccupied;

        _closeNotificationButton.onClick.RemoveAllListeners();
    }
}
