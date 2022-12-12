using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;


public class BrickSpawner : MonoBehaviour
{
    private float maxRangeY = 14.5f, minRangeX = -10f,maxRangeX = 10f;

    [SerializeField] private GameObject Brick;
    private Color[] Colors = new Color[20];
    private float yexstence = 0.25f, xexstance = 0.5f;

    private GameObject[,] Bricks;
    // Start is called before the first frame update
   

    public void SpawnBricks(int RowSice , int CollumeNumber)
    {
        if (RowSice<1 || CollumeNumber < 1) {return; }
        if (RowSice > 15) { RowSice = 15; }
        if (CollumeNumber > 20) { CollumeNumber = 20; }

        for (int i = 0; i < CollumeNumber; i++)
        {
            Colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        
       
        Bricks = new GameObject[RowSice, CollumeNumber];
        float XSpaceBetweenBRicks = ((maxRangeX - minRangeX) - RowSice)/(RowSice+1); 
        for (int i = 0; i < CollumeNumber; i++)
        {
            float yForNextBrick = maxRangeY - (i + 1) * (2* yexstence *1.5f);
            for (int j = 0; j < RowSice; j++)
            {
                float xForNextBrick = xexstance + XSpaceBetweenBRicks+ minRangeX +  j * (xexstance*2 + XSpaceBetweenBRicks);
                Bricks[j,i] = Instantiate(Brick, new Vector3(xForNextBrick, yForNextBrick, 0), quaternion.identity);
                Bricks[j,i].gameObject.GetComponent<SpriteRenderer>().color = Colors[i];
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        int RowSice = 10 , CollumeNumber = 13;
        if (RowSice<1 || CollumeNumber < 1) {return; }
        if (RowSice > 11) { RowSice = 11; }
        if (CollumeNumber > 20) { CollumeNumber = 20; }

        for (int i = 0; i < CollumeNumber; i++)
        {
            Colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        
       
        Bricks = new GameObject[RowSice, CollumeNumber];
        float XSpaceBetweenBRicks = ((maxRangeX - minRangeX) - RowSice)/(RowSice+1); 
        for (int i = 0; i < CollumeNumber; i++)
        {
            float yForNextBrick = maxRangeY - (i + 1) * ( yexstence *1.5f);
            for (int j = 0; j < RowSice; j++)
            {
                float xForNextBrick = xexstance + XSpaceBetweenBRicks+ minRangeX +  j * (1f + XSpaceBetweenBRicks);
               Gizmos.DrawCube(  new Vector3(xForNextBrick, yForNextBrick, 0), new Vector3(xexstance*2,yexstence*2,1f));
                
            }
        }
    }*/
}
