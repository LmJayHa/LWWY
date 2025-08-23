using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    ==========================================================
    CaptureMode: 캡쳐창의 이동에 대한 스크립트

    (---수정사항---)

    21.07.22 : 임재하 : 기본적인 틀을 구성->움직임 구성         

    ==========================================================
     */









public class CaptureMode : MonoBehaviour
{
    public Camera mainCamera;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition;
    }

    private void OnEnable()
    {
        rectTransform.position = Input.mousePosition;
    }
}
