using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Manager Manager;
    [SerializeField] private GameObject Explo, bigExplosion;
    public enum BrickType
    {
        Normal =0,
        Bomb =1,
    }
    public BrickType thisBrickType;
    private void Awake()
    {
        Manager = Manager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {BrickIsHit(); }

    public void BrickIsHit()
    {
        switch (thisBrickType)
        {
            case BrickType.Normal:Instantiate(Explo, transform.position, quaternion.identity);
                break;
            case BrickType.Bomb:
                Instantiate(bigExplosion, transform.position, quaternion.identity);
                Collider2D[] hitBricks = Physics2D.OverlapCircleAll(transform.position, 3f);
                for (int i = 0; i < hitBricks.Length; i++)
                {
                    if (!hitBricks[i].CompareTag("Brick")) { continue; }
                    hitBricks[i].GetComponent<Brick>().BrickIsHit();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        
        
        Manager.InvokeCheckLevelFinished();
        Destroy(this.gameObject);
    }
}
