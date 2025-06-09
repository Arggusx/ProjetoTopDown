using UnityEngine;
using TMPro;

public class XPUI : MonoBehaviour
{
    [SerializeField] private PlayerItems playerItems;
    [SerializeField] private TextMeshProUGUI xpText;

    private void Update()
    {
        float porcentagem = ((float)playerItems.currentXP / playerItems.xpLimit) * 100f;
        xpText.text = $"Level: {playerItems.currentLevel} | XP: ({porcentagem:F0}%)";
    }
}
