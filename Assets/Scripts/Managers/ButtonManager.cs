using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : SingleTon<ButtonManager> {

	public void EditModeCancelButton()
    {
        UIManager.instance.editMode.SetActive(false);
        StartCoroutine(CameraManager.instance.CameraZoomOut());
    }
}
