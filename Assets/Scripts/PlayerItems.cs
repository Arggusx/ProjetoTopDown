using System.Collections; // Biblioteca para uso de coleções (como corrotinas)
using System.Collections.Generic; // Biblioteca para listas e dicionários genéricos
using UnityEngine; // Biblioteca principal da Unity

public class PlayerItems : MonoBehaviour
{

    [Header("Amounts")]
    // Armazenam as quantidades de cada recuso
    public int totalWood; // Quantidade total de madeira
    public int carrots; // Quantidade de cenouras
    public float currentWater; // Quantidade atual de água


    [Header("Limits")]
    // Limitadores de recursos encapsulados
    public float waterLimit = 50;
    public float woodLimit = 5;
    public float carrotLimit = 10;

    public void WaterLimit(float water) // Método para limitar a quantidade de água ao recarregar
    {
        if (currentWater <= waterLimit) // Verifica se ainda pode adicionar água
        {
            currentWater += water; // Adiciona água ao total atual
        }
    }
}
