using UnityEngine;
using TMPro;

public class lifeBarUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI lifeText; // Controla os textos desse componente

    private void Update()
    {
        lifeText.text = $"Vida: {player.currentHealthPlayer}/{player.totalHealthPlayer}";
    }
}