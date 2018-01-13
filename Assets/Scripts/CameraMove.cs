using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [System.Serializable]
    public struct ClampVector
    {
        public float minClampX;
        public float maxClampX;
        [Space(10)]
        public float minClampZ;
        public float maxClampZ;
    }

    public ClampVector clampVector = new ClampVector();

    private void Update()
    {
        CameraClamp();
        InputTouch();
        Camera.main.transform.position = transform.position;
    }

    //마우스 입력 받아 좌표 이동 함수
    private void InputTouch()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") < 0)
                transform.Translate(transform.right * GameManager.instance.cameraSensivity);
            else if (Input.GetAxis("Mouse X") > 0)
                transform.Translate(-transform.right * GameManager.instance.cameraSensivity);

            if (Input.GetAxis("Mouse Y") < 0)
                transform.Translate(transform.forward * GameManager.instance.cameraSensivity);
            else if (Input.GetAxis("Mouse Y") > 0)
                transform.Translate(-transform.forward * GameManager.instance.cameraSensivity);
        }
    }
    //카메라 이동 제한 함수
    private void CameraClamp()
    {
        if (transform.position.x > clampVector.maxClampX)
            transform.position = new Vector3(clampVector.maxClampX, transform.position.y, transform.position.z);
        else if (transform.position.x < clampVector.minClampX)
            transform.position = new Vector3(clampVector.minClampX, transform.position.y, transform.position.z);

        if (transform.position.z > clampVector.maxClampZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, clampVector.maxClampZ);
        else if(transform.position.z < clampVector.minClampZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, clampVector.minClampZ);
    }
}
