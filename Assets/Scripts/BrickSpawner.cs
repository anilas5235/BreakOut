using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;


public class BrickSpawner : MonoBehaviour
{
    private float maxRangeY = 14.5f -1f, minRangeX = -10f,maxRangeX = 10f;

    [SerializeField] private GameObject Brick;
    private Color[] Colors = new Color[20];
    private float yexstence = 0.25f, xexstance = 0.75f;
    [SerializeField] private Level[] _levels;

    // Start is called before the first frame update


    public void SpawnBricks(int level)
    {
        if (level > _levels.Length ) {print("Spawn of Bricks failed- Level does not exist");  return; }
        SpawnRow(_levels[level].Row1,maxRangeY - 1 * (3 * yexstence ));
        SpawnRow(_levels[level].Row2,maxRangeY - 2 * (3 * yexstence ));
        SpawnRow(_levels[level].Row3,maxRangeY - 3 * (3 * yexstence ));
        SpawnRow(_levels[level].Row4,maxRangeY - 4 * (3 * yexstence ));
        SpawnRow(_levels[level].Row5,maxRangeY - 5 * (3 * yexstence ));
        SpawnRow(_levels[level].Row7,maxRangeY - 6 * (3 * yexstence ));
        SpawnRow(_levels[level].Row8,maxRangeY - 7 * (3 * yexstence ));
        SpawnRow(_levels[level].Row9,maxRangeY - 8 * (3 * yexstence ));
        SpawnRow(_levels[level].Row10,maxRangeY - 9 * (3 * yexstence ));
    }

    private void SpawnRow(int[] Rowdata, float yValue)
    {
        if(Rowdata.Length < 1){return; }
        int Rowleangth = Rowdata.Length -1;
        float XSpaceBetweenBricks = ((maxRangeX - minRangeX) - (Rowleangth * xexstance*2) )/(Rowleangth+1); 

        for (int i = 0; i < Rowdata.Length-1; i++)
        {
            float xForNextBrick = xexstance + XSpaceBetweenBricks+ minRangeX +  i * (xexstance*2 + XSpaceBetweenBricks);
            switch (Rowdata[i])
            {
                case 0:Instantiate(Brick, new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
            }
        }
    }
}
