using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Manager Manager;

    private void Awake()
    {
        Manager = FindObjectOfType<Manager>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ball"))
        {
            print("Brick shout be destroyed");
            Destroy(this.gameObject);
            Manager.InvokeCheckLevelFinished();
        }
    }
}
