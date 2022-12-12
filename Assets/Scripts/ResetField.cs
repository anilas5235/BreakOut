using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetField : MonoBehaviour
{
    private Manager Manager;

    private void Start()
    {
        Manager = GameObject.FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            col.gameObject.GetComponent<Ball>().BallReset();
            Manager.FailHappend();
        }
    }
}
