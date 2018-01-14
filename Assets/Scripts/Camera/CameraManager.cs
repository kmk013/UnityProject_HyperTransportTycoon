using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager> {

    //Camera Zoom In/Out
    public IEnumerator CameraZoomIn(RaycastHit hit)
    {
        while (Camera.main.fieldOfView >= 50.0f)
        {
            Camera.main.fieldOfView -= Time.deltaTime * 10f;
            yield return null;
        }
    }
    public IEnumerator CameraZoomOut()
    {
        while (Camera.main.fieldOfView <= 60.0f)
        {
            Camera.main.fieldOfView += Time.deltaTime * 10f;
            yield return null;
        }
    }
}
