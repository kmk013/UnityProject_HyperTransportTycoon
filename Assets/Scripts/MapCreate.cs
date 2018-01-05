using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {

    public GameObject island, sea;


    private void Awake()
    {
        StartCoroutine(CreateWould());
    }

    IEnumerator CreateWould()
    {
        int x = 32, y = 32; //맵 타일 x,y 값
        float width = 1f, height = 1f; //블럭의 가로 세로


        Vector3 loc = new Vector3(0, 0, 0);
        //처음 블럭 위치를 조정

        for(int i = 0; i < x; i++)
        {
           
            loc.x += width;

            loc.z = 0;
            for (int j = 0; j < y; j++)
            {
                loc.z += height;
                Instantiate(island, loc, Quaternion.identity);

                yield return new WaitForSeconds(0.01f);
            }
        }

    }
}
