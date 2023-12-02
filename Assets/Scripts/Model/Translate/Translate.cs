using UnityEngine;

public abstract class Translate
{
    public string MusicName => _musicName;
    public string SoundName => _soundName;
    public string ContinueWord => _continueWord;
    public string BuildRewardDescription => _buildRewardDescription;
    public string BuildLimitDescription => _buildLimitDescription;
    public string NewBlockNotification => _newBlockNotification;
    public string BlockLevelUpgradeName => _blockLevelUpgradeName;
    public string CreationSpeedUpgradeName => _creationSpeedUpgradeName;
    public string MoneyUpgradeName => _moneyUpgradeName;
    public string BlockMoneyUpgradeName => _blockMoneyUpgradeName;
    public string MainTutorialDescription => _mainTutorialDescription;
    public string BuildTutorialDescription => _buildTutorialDescription;
    public string BlockLevelUpgradeDescription => _blockLevelUpgradeDescription;
    public string CreationSpeedUpgradeDescription => _creationSpeedUpgradeDescription;
    public string MoneyUpgradeDescription => _moneyUpgradeDescription;
    public string BlockMoneyUpgradeDescription => _blockMoneyUpgradeDescription;

    [SerializeField] private string _soundName;
    [Space(5), SerializeField] private string _musicName;
    [Space(5), SerializeField] private string _continueWord;
    [Space(5), SerializeField] private string _buildRewardDescription;
    [Space(5), SerializeField] private string _buildLimitDescription;
    [Space(5), SerializeField] private string _newBlockNotification;
    [Space(5), SerializeField] private string _blockLevelUpgradeName;
    [Space(5), SerializeField] private string _creationSpeedUpgradeName;
    [Space(5), SerializeField] private string _moneyUpgradeName;
    [Space(5), SerializeField] private string _blockMoneyUpgradeName;
    [Space(10), TextArea(1, 150), SerializeField] private string _blockLevelUpgradeDescription;
    [SerializeField, TextArea(1, 150)] private string _creationSpeedUpgradeDescription;
    [SerializeField, TextArea(1, 150)] private string _moneyUpgradeDescription;
    [SerializeField, TextArea(1, 150)] private string _blockMoneyUpgradeDescription;
    [Space(10), SerializeField, TextArea(1, 150)] private string _mainTutorialDescription;
    [SerializeField, TextArea(1, 150)] private string _buildTutorialDescription;
    [Space(10), SerializeField] private string[] _blockNames;

    public string GetBlockName(int blockNumber) => _blockNames[blockNumber];
}
