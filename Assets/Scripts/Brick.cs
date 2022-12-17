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
        Normal =0, Bomb =1, Recking =2, SpeedBall =3, SpeedPlayer =4, GiantPlayer =5,
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
            case BrickType.Normal: break;
            case BrickType.Bomb: Explosion(); break;
            case BrickType.Recking: Manager.TriggerReckingBallPower();  break;
            case BrickType.SpeedBall: Manager.TriggerSpeedBallPower(); break;
            case BrickType.SpeedPlayer: Manager.TriggerSpeedPlayerPower(); break;
            case BrickType.GiantPlayer: Manager.TriggerGiantPlayerPower(); break;
            default: print("this Brick is not defined"); break;
        }
        Instantiate(Explo, transform.position, quaternion.identity);
        Manager.InvokeCheckLevelFinished();
        Destroy(gameObject);
    }

    private void Explosion()
    {
        Instantiate(bigExplosion, transform.position, quaternion.identity);
        Collider2D[] hitBricks = Physics2D.OverlapCircleAll(transform.position, 2f);

        for (int i = 0; i < hitBricks.Length; i++)
        {
            if (hitBricks[i].gameObject.CompareTag("Brick") && hitBricks[i].gameObject != gameObject)
            {
                hitBricks[i].gameObject.GetComponent<Brick>().BrickIsHit();
            }
        }
    }
}
