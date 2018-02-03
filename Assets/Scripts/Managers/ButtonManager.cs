using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : SingleTon<ButtonManager> {

	public void EditModeCancelButton()
    {
        StartCoroutine(CameraManager.instance.CameraZoomOut());
        GameManager.instance.isEditMode = false;
    }
}
