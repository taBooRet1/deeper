using UnityEngine;

public class Coal : Ore
{
    public override void OnMined()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.currentFuel += 10;
    }
}
