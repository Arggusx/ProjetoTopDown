using UnityEngine;
using TMPro;

public class lifeBarUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI lifeText; // Controla os textos desse componente

    private void Update()
    {
        float porcentagem = ((float)player.currentHealthPlayer / player.totalHealthPlayer) * 100f;
        lifeText.text = $"Vida: ({porcentagem:F0}%)";
        player.healthBarPlayer.fillAmount = player.currentHealthPlayer / player.totalHealthPlayer; // Barra de bida 
    }
}