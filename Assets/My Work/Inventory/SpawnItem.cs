using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector2 PlayerPos = new Vector2(player.position.x, player.position.y-1f);
        Instantiate(item, PlayerPos,Quaternion.identity);
    }
}
