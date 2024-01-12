using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector2 offset; // Use the same offset as your camera

    void Update()
    {
        if (player)
        {
            // Follow the player with an offset
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }
}