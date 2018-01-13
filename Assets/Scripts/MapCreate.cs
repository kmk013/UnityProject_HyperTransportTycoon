using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {

    public GameObject island, sea;
    [Space(10)]
    public GameObject floorParent;

    const int X = 32, Y = 32;
    int[,] arr = new int[X, Y];

    //========================================================================================================================

    private void Awake()
    {
        StartCoroutine(CreateWould());
    }

    //========================================================================================================================

    IEnumerator CreateWould()
    {
        Map_reset();


        for (int i = 0; i < 5; i++)
        {
            Cellular_Automata();
        }//5회 실행

        float width = 1f, height = 1f; //블럭의 가로 세로
        Vector3 loc = new Vector3(0, 0, 0); //처음 좌표 초기화

        for(int i = 0; i < X; i++)
        {
            loc.x += width;

            loc.z = 0f;
            for(int j = 0; j < Y; j++)
            {
                loc.z += height;

                if(arr[i,j] == 1)
                {
                    GameObject newTile = Instantiate(sea, loc, Quaternion.identity);
                    newTile.transform.parent = floorParent.transform;
                }
                else if (arr[i, j] == 0)
                {
                    GameObject newTile = Instantiate(island, loc, Quaternion.identity);
                    newTile.transform.parent = floorParent.transform;
                }

                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    //========================================================================================================================

    void Map_reset()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                if (i == 0 || j == 0 || i == X - 1 || j == Y - 1)
                {
                    arr[i, j] = 1; continue;
                }

                int rand = (int)Random.Range(1, 101);

                if (rand <= 45) { arr[i, j] = 0; }
                else { arr[i, j] = 1; }
            }
        }//첫 생성 초기화 반복문
    }
    
    //========================================================================================================================

    void Cellular_Automata()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                int count = 0;

                for (int k = i - 1; k <= i + 1; k++)
                {
                    for (int l = j - 1; l <= j + 1; l++)
                    {
                        if (k < 0 || l < 0 || k >= X || l >= Y) continue;
                        if (arr[k, l] == 0) count++;
                    }
                }
                //주변 블럭 갯수 체크

                if (count >= 5) arr[i, j] = 0;
            }
        }//샐룰러 오토마타 1회 실행
    }

    //========================================================================================================================
}
