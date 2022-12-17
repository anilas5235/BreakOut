using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
   public float moveSpeed = 13;
   private Rigidbody2D rb;

   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
   }

   void Update()
    {
        if(Manager.Instance.currentGameState != Manager.GameState.Playing){return;}
        rb.velocity = new Vector2(0f, 0f);
        transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f);
    }
}
