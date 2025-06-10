using UnityEngine;
using TMPro;

public class XPUI : MonoBehaviour
{
    [SerializeField] private PlayerItems playerItems;
    [SerializeField] private TextMeshProUGUI xpText;

    private void Update()
    {
        float porcentagem = ((float)playerItems.currentXP / playerItems.xpLimit) * 100f;

        if (playerItems.currentXP > 0)
        {
            xpText.text = $"Level: {playerItems.currentLevel} | XP: ({porcentagem:F0}%)";
        }
        else if (playerItems.currentXP <= 0)
        {
            xpText.text = $"Level: {playerItems.currentLevel} | XP: (0%)";
        }

    }
}
