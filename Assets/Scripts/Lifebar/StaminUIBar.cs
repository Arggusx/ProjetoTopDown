using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminUIBar : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (player.currentStamina > 0)
        {
            player.staminBarPlayer.fillAmount = player.currentStamina / player.maxStamina;
        }
        else
        {
            player.staminBarPlayer.fillAmount = 0;
        }
    }
}
