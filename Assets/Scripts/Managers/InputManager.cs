using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : SingleTon<InputManager> {

    private Transform cameraMovePoint;
    private Vector3 mouseMovePoint;

    private Collider mouseDownCollider;
    private Collider mouseUpCollider;
    private Vector3 mouseDownPosition;
    private Vector3 mouseUpPosition;

    private void Start()
    {
        cameraMovePoint = GameObject.Find("CameraManager").transform;
    }

    private void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if(!GameManager.instance.isEditMode && !CameraManager.instance.isZooming)
            {
                mouseDownPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.CompareTag("Floor"))
                {
                    mouseDownCollider = hit.collider;
                    
                }
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if (!GameManager.instance.isEditMode)
            {
                mouseMovePoint = (new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * -1) * GameManager.instance.cameraSensivity * Time.deltaTime;
                cameraMovePoint.position += cameraMovePoint.rotation * mouseMovePoint;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (!GameManager.instance.isEditMode && !CameraManager.instance.isZooming)
            {
                mouseUpPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.CompareTag("Floor"))
                    mouseUpCollider = hit.collider;
                
                if (mouseDownCollider == mouseUpCollider && Vector3.Distance(mouseDownPosition, mouseUpPosition) < 10f)
                {
                    StartCoroutine(CameraManager.instance.MoveToHit(hit));
                    StartCoroutine(CameraManager.instance.CameraZoomIn());
                    GameManager.instance.isEditMode = true;
                }
            }
        }
	}
}
