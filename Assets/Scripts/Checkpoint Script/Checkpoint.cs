using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Health health;
    public Transform respawnpoint;
    Collider2D coll;

    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health.UpdateCheckpoint(respawnpoint.position);
            coll.enabled = false;
        }
    }
}
