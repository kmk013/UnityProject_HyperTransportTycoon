using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager> {

    public bool isZooming = false;

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
        CameraClamp();
        Camera.main.transform.position = transform.position;
    }

    //카메라 이동 제한 함수
    private void CameraClamp()
    {
        if (transform.position.x > clampVector.maxClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(clampVector.maxClampValue, transform.position.y, transform.position.z), GameManager.instance.cameraSensivity * Time.deltaTime);
        else if (transform.position.x < clampVector.minClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(clampVector.minClampValue, transform.position.y, transform.position.z), GameManager.instance.cameraSensivity * Time.deltaTime);

        if (transform.position.z > clampVector.maxClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, clampVector.maxClampValue), GameManager.instance.cameraSensivity * Time.deltaTime);
        else if (transform.position.z < clampVector.minClampValue)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, clampVector.minClampValue), GameManager.instance.cameraSensivity * Time.deltaTime);
    }

    //Camera Zoom In/Out
    public IEnumerator CameraZoomIn()
    {
        isZooming = true;
        while (Camera.main.fieldOfView >= 50.0f)
        {
            Camera.main.fieldOfView -= Time.deltaTime * 10f;
            yield return null;
        }
        isZooming = false;
    }
    public IEnumerator CameraZoomOut()
    {
        isZooming = true;
        while (Camera.main.fieldOfView <= 60.0f)
        {
            Camera.main.fieldOfView += Time.deltaTime * 50f;
            yield return null;
        }
        isZooming = false;
    }

    //Ray를 통해 충돌된 오브젝트(바닥)으로 이동 함수
    public IEnumerator MoveToHit(RaycastHit hit)
    {
        while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x - 1.75f, transform.position.y, hit.transform.position.z - 2)) >= 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x - 1.75f, transform.position.y, hit.transform.position.z - 2f), GameManager.instance.cameraSensivity * Time.deltaTime);
            yield return null;
        }
    }
}
