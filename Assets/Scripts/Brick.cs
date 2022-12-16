using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Manager Manager;
    [SerializeField] private GameObject Explo;
    private void Awake()
    {
        Manager = FindObjectOfType<Manager>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ball"))
        {
            print("Brick shout be destroyed");
            Instantiate(Explo, transform.position, quaternion.identity);
            Manager.InvokeCheckLevelFinished();
            Destroy(this.gameObject);
        }
    }
}
