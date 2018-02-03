using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager> {

    public GameObject editMode;

    private void Update()
    {
        if (GameManager.instance.isEditMode)
            editMode.SetActive(true);
        else
            editMode.SetActive(false);
    }
}
