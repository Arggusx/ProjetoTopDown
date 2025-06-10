using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerUIBar : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (player.currentHunger > 0)
        {
            player.hungerBarPlayer.fillAmount = player.currentHunger / player.maxHunger;
        }
        else
        {
            player.hungerBarPlayer.fillAmount = 0;
        }
    }
}
