using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomSystem : MonoBehaviour {

    //드래그 이동과 중첩될 때 처리 변수
    private Vector3 touchStart;
    private Vector3 touchEnd;

	private void Update () {
        TouchFloor();
	}

    //바닥을 클릭했을 때 실행 함수
    private void TouchFloor()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchEnd = Input.mousePosition;
            if (!GameManager.instance.isEditMode && Vector3.Distance(touchStart, touchEnd) < 0.5f)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.CompareTag("Floor"))
                {
                    GameManager.instance.isEditMode = true;
                    UIManager.instance.editMode.SetActive(true);
                    StartCoroutine(MoveToHit(hit));
                    StartCoroutine(CameraManager.instance.CameraZoomIn(hit));
                }
            }
        }
    }

    //Ray를 통해 충돌된 오브젝트(바닥)으로 이동 함수
    private IEnumerator MoveToHit(RaycastHit hit)
    {
        while(Vector3.Distance(transform.position, hit.transform.position) >= 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z), GameManager.instance.cameraSensivity);
            yield return null;
        }
    }
}
