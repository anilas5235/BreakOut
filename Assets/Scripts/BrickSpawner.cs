using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;


public class BrickSpawner : MonoBehaviour
{
    public static BrickSpawner Instance;
    private float maxRangeY = 14.5f -1f, minRangeX = -10f,maxRangeX = 10f;

    [SerializeField] private GameObject[] Bricktypes;
    private Color[] Colors = new Color[20];
    private float yexstence = 0.25f, xexstance = 0.75f;
    [SerializeField] public Level[] _levels;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBricks(int level)
    {
        Brick[] lingeringBricks = FindObjectsOfType<Brick>();

        for (int i = 0; i < lingeringBricks.Length; i++)
        { Destroy(lingeringBricks[i].gameObject); }
        
        if (level > _levels.Length ) {print("Spawn of Bricks failed- Level does not exist");  return; }
        SpawnRow(_levels[level].Row1,maxRangeY - 1 * (3 * yexstence ));
        SpawnRow(_levels[level].Row2,maxRangeY - 2 * (3 * yexstence ));
        SpawnRow(_levels[level].Row3,maxRangeY - 3 * (3 * yexstence ));
        SpawnRow(_levels[level].Row4,maxRangeY - 4 * (3 * yexstence ));
        SpawnRow(_levels[level].Row5,maxRangeY - 5 * (3 * yexstence ));
        SpawnRow(_levels[level].Row6,maxRangeY - 6 * (3 * yexstence ));
        SpawnRow(_levels[level].Row7,maxRangeY - 7 * (3 * yexstence ));
        SpawnRow(_levels[level].Row8,maxRangeY - 8 * (3 * yexstence ));
        SpawnRow(_levels[level].Row9,maxRangeY - 9 * (3 * yexstence ));
        SpawnRow(_levels[level].Row10,maxRangeY - 10 * (3 * yexstence ));
    }

    private void SpawnRow(int[] rowdata, float yValue)
    {
        if(rowdata.Length < 1){return; }
        int Rowleangth = rowdata.Length;
        float XSpaceBetweenBricks = ((maxRangeX - minRangeX) - (Rowleangth * xexstance*2) )/(Rowleangth+1); 

        for (int i = 0; i < rowdata.Length; i++)
        {
            float xForNextBrick = xexstance + XSpaceBetweenBricks+ minRangeX +  i * (xexstance*2 + XSpaceBetweenBricks);
            switch (rowdata[i])
            {
                case 0:Instantiate(Bricktypes[0], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                case 1:Instantiate(Bricktypes[1], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                case 2:Instantiate(Bricktypes[2], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                case 3:Instantiate(Bricktypes[3], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                case 4:Instantiate(Bricktypes[4], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                case 5:Instantiate(Bricktypes[5], new Vector3(xForNextBrick, yValue, 0), quaternion.identity);  break;
                
            }
        }
    }
    
    public void UpdateStarsInData(int level, int stars)
    {
        _levels[level].numberOfAchievedStars = stars;
    }
    
    public void RestAllStarsData() { for (int i = 0; i < _levels.Length; i++) { _levels[i].numberOfAchievedStars = 0; } }
}
