using UnityEngine;
using UnityEngine.UI;
using YG;

public class BlockProgressNotificationPresenter : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private LibraryBlockSwitcher _libraryBlockSwitcher;
    [Space(10), SerializeField] private GameObject _notificationPanel;
    [SerializeField] private GameObject _notificationTouchBlocker;
    [SerializeField] private Button _closeNotificationButton;
    [SerializeField] private Notifications _notifications;
    [Space(10), SerializeField] private Cell[] _cells;

    private PanelAnimator _panelAnimator = new PanelAnimator();
    private bool _blockLevelRecovered = false;
    private int _achievedBlockLevel = 1;

    private void TryRecoverBlockLevel()
    {
        if (_blockLevelRecovered == false && YandexGame.savesData != null)
        {
            SavesYG savesData = YandexGame.savesData;

            if (savesData.AchievedBlockLevel > _achievedBlockLevel)
                _achievedBlockLevel = savesData.AchievedBlockLevel;

            _blockLevelRecovered = true;
        }
    }

    private void OnCellOccupied(MergeBlock mergeBlock)
    {
        TryRecoverBlockLevel();
        _soundPlayer.PlayOccupieSound();

        if (mergeBlock.BlockLevel > _achievedBlockLevel)
        {
            string notification = _translatesContainer.SelectedTranslate.NewBlockNotification;
            Sprite blockSprite = _libraryBlockSwitcher.GetBlockSprite(mergeBlock.BlockLevel - 1);
            string blockName = _translatesContainer.SelectedTranslate.GetBlockName(mergeBlock.BlockLevel - 1);

            _notifications.ShowBlockProgressNotification(notification, blockSprite, blockName);
            ActivateNotificationPanel(mergeBlock.BlockLevel);
        }
    }

    private void ActivateNotificationPanel(int blockLevel)
    {
        _notificationTouchBlocker.SetActive(true);
        _panelAnimator.LaunchIncreaseAnimation(_notificationPanel);

        _achievedBlockLevel = blockLevel;
        YandexGame.savesData.AchievedBlockLevel = blockLevel;
        YandexGame.SaveProgress();
    }

    private void OnEnable()
    {
        foreach (var cell in _cells)
            cell.CellOccupied += OnCellOccupied;

        _closeNotificationButton.onClick.AddListener(() => _notificationTouchBlocker.SetActive(false));
        _closeNotificationButton.onClick.AddListener(() => YandexGame.FullscreenShow());
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
            cell.CellOccupied -= OnCellOccupied;

        _closeNotificationButton.onClick.RemoveAllListeners();
    }
}
