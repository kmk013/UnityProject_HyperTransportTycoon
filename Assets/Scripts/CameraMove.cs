using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private void LateUpdate()
    {
        InputTouch();
    }

    void InputTouch()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") < 0)
                transform.Translate(1f, 0, 0);

            if (Input.GetAxis("Mouse X") > 0)
                transform.Translate(-1f, 0, 0);

            if (Input.GetAxis("Mouse Y") < 0)
                transform.Translate(0, 1f, 0);

            if (Input.GetAxis("Mouse Y") > 0)
                transform.Translate(0, -1f, 0);
        }
    }
}
