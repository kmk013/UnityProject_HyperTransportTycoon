using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {

    public GameObject ground, water;
    [Space(10)]
    public GameObject farm;
    public GameObject factory;
    public GameObject city;
    public GameObject oilwell;

    private GameObject floorParent;
    private const int X = 32, Y = 32;
    private int[,] arr = new int[X, Y];

    private List<GameObject> groundList = new List<GameObject>();
    private List<GameObject> waterList = new List<GameObject>();
    private int groundNum = 0;
    private int waterNum = 0;

    //========================================================================================================================

    private void Start()
    {
        floorParent = GameObject.Find("Floor");
        CreateWould();
        CreateBuilding();
    }

    //========================================================================================================================

    private void CreateWould()
    {
        Map_reset();

        for (int i = 0; i < 6; i++)
        {
            Cellular_Automata();
        }//6회 실행

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
                    waterList.Add(Instantiate(water, loc, Quaternion.identity));
                    waterList[waterNum].transform.parent = floorParent.transform;
                    waterList[waterNum].name = "바다: " + i.ToString() + "," + j.ToString();
                    waterNum++;
                }
                else if (arr[i, j] == 0)
                {
                    groundList.Add(Instantiate(ground, loc, Quaternion.identity));
                    groundList[groundNum].transform.parent = floorParent.transform;
                    groundList[groundNum].name = "육지: " + i.ToString() + "," + j.ToString();
                    groundNum++;
                }
            }
        }
    }

    //========================================================================================================================

    private void Map_reset()
    {
        int WATER_SIZE = 4;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                if (i < WATER_SIZE || j < WATER_SIZE || i > X - WATER_SIZE-1 || j > Y - WATER_SIZE-1)
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

    private void Cellular_Automata()
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

    private void CreateBuilding()
    {
        BuildingSetting(farm, 6, "Ground");
        BuildingSetting(factory, 6, "Ground");
        BuildingSetting(city, 8, "Ground");
        BuildingSetting(oilwell, 4, "Water");
    }

    private void BuildingSetting(GameObject a, int buildCount, string tag)
    {
        for(int cnt = 0; cnt < buildCount; cnt++)
        {
            GameObject building = Instantiate(a);
            if(tag.Contains("Ground"))
            {
                int num = Random.Range(0, groundList.Count);
                building.transform.parent = groundList[num].transform;
                groundList.RemoveAt(num);
            } else
            {
                int num = Random.Range(0, waterList.Count);
                building.transform.parent = waterList[num].transform;
                waterList.RemoveAt(num);
            }
            building.transform.localPosition = Vector3.zero;
        }
    }
    

}
