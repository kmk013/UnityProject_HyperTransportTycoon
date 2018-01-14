using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    //카메라 이동 제한 값
    [System.Serializable]
    public struct ClampVector
    {
        public float minClampValue;
        public float maxClampValue;
    }
    public ClampVector clampVector = new ClampVector();

    private void Update()
    {
        InputTouch();
        CameraClamp();
        Camera.main.transform.position = transform.position;
    }

    //마우스 입력 받아 좌표 이동 함수
    private void InputTouch()
    {
        if (!GameManager.instance.isEditMode)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                    transform.position += transform.right * GameManager.instance.cameraSensivity;
                else if (Input.GetAxis("Mouse X") > 0)
                    transform.position -= transform.right * GameManager.instance.cameraSensivity;

                if (Input.GetAxis("Mouse Y") < 0)
                    transform.position += transform.forward * GameManager.instance.cameraSensivity;
                else if (Input.GetAxis("Mouse Y") > 0)
                    transform.position -= transform.forward * GameManager.instance.cameraSensivity;
            }
        }
    }

    //카메라 이동 제한 함수
    private void CameraClamp()
    {
        if (transform.position.x >= clampVector.maxClampValue)
            transform.position = Vector3.Lerp(transform.position,new Vector3(clampVector.maxClampValue, transform.position.y, transform.position.z), GameManager.instance.cameraSensivity);
        else if (transform.position.x <= clampVector.minClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(clampVector.minClampValue, transform.position.y, transform.position.z), GameManager.instance.cameraSensivity);
    
        if (transform.position.z >= clampVector.maxClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, clampVector.maxClampValue), GameManager.instance.cameraSensivity);
        else if(transform.position.z <= clampVector.minClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, clampVector.minClampValue), GameManager.instance.cameraSensivity);
    }
}
