using TMPro;
using UnityEngine;

public class BuildProgressShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private TMP_Text _buildingNumberText;

    public void ShowProgress(int buildedBlocks, int allBlocks) => 
        _progressText.text = (buildedBlocks + " / " + allBlocks).ToString();

    public void ShowBuildingNumber(int createdBuildingNumber) => 
        _buildingNumberText.text = ("LVL " + (createdBuildingNumber + 1)).ToString();
}
