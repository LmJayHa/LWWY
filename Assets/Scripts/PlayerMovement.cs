using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/*
    ==========================================================
     playerMovement : 플레이어의 움직임을 관리하는 스크립트

    (---필수 컴포넌트---)
    1. PlayerInput
    2. Rigidbody2D

    (---수정사항---)

    21.07.03 : 임재하 : 기본적인 틀 제작
    21.07.15 : 임재하 : 이동시 velocity를 이용하도록 수정, canMove변수 추가
    ==========================================================
     */





public class PlayerMovement : MonoBehaviour
{
    //받아올 컴포넌트
    private PlayerInput playerInput;


    public float movingSpeed = 2f;//이동속도

    public bool canMove; //움직일 수 있는 상태인지

    public bool canRun = true;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        canMove = true;
    }//컴포넌트를 이 오브젝트에서 받아옴

    void FixedUpdate()
    {
        if (GameManager.canInput == false) { return; }
            Move();
    }

    private void Move()
    {
        if (GameManager.canInput == false) { return; }

        if (!canMove) return;




        if (playerInput.isMoving)
        {

            Vector2 moveVector = new Vector2(playerInput.moveRateWidth, playerInput.moveRateHeight);

            SoundManager.soundManager.PlayFootStepSounds();

            if (moveVector.x != 0)
            {
                if (playerInput.run && canRun) 
                    transform.Translate(playerInput.moveRateWidth * Time.deltaTime * movingSpeed*3f, 0f, 0f);
                else
                    transform.Translate(playerInput.moveRateWidth * Time.deltaTime * movingSpeed, 0f,0f);
            }
            else if (moveVector.y != 0) {
                if (playerInput.run && canRun)
                    transform.Translate(0f, playerInput.moveRateHeight * Time.deltaTime * movingSpeed*3f, 0f);
                else
                    transform.Translate(0f, playerInput.moveRateHeight * Time.deltaTime * movingSpeed,0f);
            }
        }

    }//플레이어를 움직이는 함수
}
