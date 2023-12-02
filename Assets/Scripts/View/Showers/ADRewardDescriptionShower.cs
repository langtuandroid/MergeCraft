using TMPro;
using UnityEngine;

public class ADRewardDescriptionShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _buildRewardText;
    
    public void ShowDescription(Translate selectedTranslate) => 
        _buildRewardText.text = selectedTranslate.BuildRewardDescription;
}
