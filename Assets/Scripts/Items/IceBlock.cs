using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public float timeShocked = 2f;
    private Player player;

    void Update()
    {
        timeShocked -= Time.deltaTime;
        if (timeShocked <= 0)
        {
            player.StartMoving();
            Destroy(this.gameObject);
        }
    }

    public void Init(Player player)
    {
        this.player = player;
    }
}
