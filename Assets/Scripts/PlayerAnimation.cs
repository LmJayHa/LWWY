using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation playerAnimation
    {
        get
        {
            if (pM_instance == null)
            {
                pM_instance = FindObjectOfType<PlayerAnimation>();
            }

            return pM_instance;
        }
    }//SoundManager를 싱글턴으로 설정

    private static PlayerAnimation pM_instance; //싱글턴에 이용된 인스턴스

    public enum SeeWhere { FRONT, LEFT, RIGHT, BACK }


    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private Animator playerAnimator;


    private float lastX;
    private float lastY;


    public int state = 0;


    public bool isMoving { get { return (isMoveX || isMoveY); } }

    public bool isMoveX;

    public bool isMoveY;

    public bool isRunning { get { return (isRunX || isRunY); } }

    public bool isRunX;

    public bool isRunY;

    private float toPositionX;
    private float toPositionY;

    private float rate;



    public void changeState(int idx) {
        state = idx;

        if (idx == 0)
        {
            playerAnimator.SetBool("isKid", false);
        }
        else if (idx == 1) {
            playerAnimator.SetBool("isKid", true);
        }
        else if (idx == 2)
        {
            playerAnimator.SetBool("isEdel", true);
        }

    }









    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        isMoveX = false;
        isMoveY = false;
    }

    private void Update()
    {
        if (GameManager.canInput == false)
        {
            if (isMoving)
            {
                if (isMoveX)
                {
                    WalkingMoveToX();
                }
                else if (isMoveY)
                {
                    WalkingMoveToY();
                }
            } else if (isRunning) {
                if (isRunX)
                {
                    runningMoveToX();
                }
                else if (isRunY)
                {
                    runningMoveToY();
                }
            }
            else
            {
                playerAnimator.SetBool("isRun", false);
                playerAnimator.SetBool("isWalking", false);
            }

        }
        else {
            MoveAnimationParameter();
        }
    }

    public void MoveAnimationParameter() {

        playerAnimator.SetFloat("velocityX", playerInput.moveRateWidth);
        if (playerInput.moveRateWidth == 0f)
            playerAnimator.SetFloat("velocityY", playerInput.moveRateHeight);
        else
            playerAnimator.SetFloat("velocityY", 0f);

        if (playerInput.isMoving)
        {

            if (playerInput.moveRateWidth != 0) {
                lastX = playerInput.moveRateWidth;
                lastY = 0f;
            } else if (playerInput.moveRateHeight != 0) {
                lastX = 0f;
                lastY = playerInput.moveRateHeight;
            }

            playerAnimator.SetFloat("lastX", lastX);
            playerAnimator.SetFloat("lastY", lastY);
            playerAnimator.SetBool("isWalking", true);

            if(playerInput.run)
                playerAnimator.SetBool("isRun", true);
            else
                playerAnimator.SetBool("isRun", false);

        }
        else {
            playerAnimator.SetBool("isWalking", false);
            if (playerInput.run)
                playerAnimator.SetBool("isRun", true);
            else
                playerAnimator.SetBool("isRun", false);
        }

    }

    public void seeAnimationWhere(SeeWhere seeWhere) {
        switch (seeWhere)
        {
            case SeeWhere.FRONT:
                lastX = 0f;
                lastY = -1f;
                playerAnimator.SetFloat("velocityX", 0f);
                playerAnimator.SetFloat("velocityY", -1f);
                break;
            case SeeWhere.LEFT:
                lastX = -1f;
                lastY = 0f;
                playerAnimator.SetFloat("velocityX", -1f);
                playerAnimator.SetFloat("velocityY", 0f);
                break;
            case SeeWhere.RIGHT:
                lastX = 1f;
                lastY = 0f;
                playerAnimator.SetFloat("velocityX", 1f);
                playerAnimator.SetFloat("velocityY", 0f);
                break;
            case SeeWhere.BACK:
                lastX = 0f;
                lastY = 1f;
                playerAnimator.SetFloat("velocityX", 0f);
                playerAnimator.SetFloat("velocityY", 1f);
                break;
            default:
                break;
        }
        playerAnimator.SetFloat("lastX", lastX);
        playerAnimator.SetFloat("lastY", lastY);
        playerAnimator.SetBool("isWalking", false);
    }




    //자동 애니메이션 용
    public void moveToX(float X)
    {
        GameManager.canInput = false;
        if (transform.position.x > X)
        {
            rate = -1.0f;
            seeAnimationWhere(SeeWhere.LEFT);
            playerAnimator.SetBool("isWalking", true);
        }
        else if (transform.position.x <= X)
        {
            rate = 1.0f;
            seeAnimationWhere(SeeWhere.RIGHT);
            playerAnimator.SetBool("isWalking", true);
        }
        isMoveX = true;
        toPositionX = X;
    }

    public void moveToY(float Y)
    {
        GameManager.canInput = false;
        if (transform.position.y > Y)
        {
            rate = -1.0f;
            seeAnimationWhere(SeeWhere.FRONT);
            playerAnimator.SetBool("isWalking", true);
        }
        else if (transform.position.y <= Y)
        {
            rate = 1.0f;
            seeAnimationWhere(SeeWhere.BACK);
            playerAnimator.SetBool("isWalking", true);
        }
        isMoveY = true;
        toPositionY = Y;

    }

    private void WalkingMoveToX()
    {
        if ((rate < 0 && transform.position.x <= toPositionX) || (rate > 0 && transform.position.x >= toPositionX))
        {
            isMoveX = false;
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetFloat("velocityX", 0f);
            playerAnimator.SetFloat("velocityY", 0f);
            return;
        }
        else
        {
            transform.Translate(rate * Time.deltaTime * 1.5f, 0f, 0f);
        }
    }

    private void WalkingMoveToY()
    {
        if ((rate < 0 && transform.position.y <= toPositionY) || (rate > 0 && transform.position.y >= toPositionY))
        {
            isMoveY = false;
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetFloat("velocityX", 0f);
            playerAnimator.SetFloat("velocityY", 0f);
            return;
        }
        else
        {
            transform.Translate(0f, rate * Time.deltaTime * 1.5f, 0f);
        }

    }

    //달리는 애니메이션
    public void runToX(float X)
    {
        GameManager.canInput = false;
        if (transform.position.x > X)
        {
            rate = -1.0f;
            seeAnimationWhere(SeeWhere.LEFT);
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetBool("isRun", true);
        }
        else if (transform.position.x <= X)
        {
            rate = 1.0f;
            seeAnimationWhere(SeeWhere.RIGHT);
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetBool("isRun", true);
        }
        isRunX = true;
        toPositionX = X;
    }

    public void runToY(float Y)
    {
        GameManager.canInput = false;
        if (transform.position.y > Y)
        {
            rate = -1.0f;
            seeAnimationWhere(SeeWhere.FRONT);
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetBool("isRun", true);
        }
        else if (transform.position.y <= Y)
        {
            rate = 1.0f;
            seeAnimationWhere(SeeWhere.BACK);
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetBool("isRun", true);
        }
        isRunY = true;
        toPositionY = Y;

    }

    private void runningMoveToX()
    {
        if ((rate < 0 && transform.position.x <= toPositionX) || (rate > 0 && transform.position.x >= toPositionX))
        {
            isRunX = false;
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isRun", false);
            playerAnimator.SetFloat("velocityX", 0f);
            playerAnimator.SetFloat("velocityY", 0f);
            return;
        }
        else
        {
            transform.Translate(rate * Time.deltaTime * 1.5f*3f, 0f, 0f);
        }
    }

    private void runningMoveToY()
    {
        if ((rate < 0 && transform.position.y <= toPositionY) || (rate > 0 && transform.position.y >= toPositionY))
        {
            isRunY = false;
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isRun", false);
            playerAnimator.SetFloat("velocityX", 0f);
            playerAnimator.SetFloat("velocityY", 0f);
            return;
        }
        else
        {
            transform.Translate(0f, rate * Time.deltaTime * 1.5f * 3f, 0f);
        }

    }

    public void Hug() { playerAnimator.SetTrigger("Hug"); }
    public void UnHug() { playerAnimator.SetTrigger("UnHug"); }
    public void Sit() { playerAnimator.SetTrigger("Sit"); }
    public void Stand() { playerAnimator.SetTrigger("Stand"); }
    public void SeeLeft() { playerAnimator.SetTrigger("SeeLeft"); }
    public void SeeRight() { playerAnimator.SetTrigger("SeeRight"); }
    public void SeeFront() { playerAnimator.SetTrigger("SeeFront"); }
    public void Blink() { playerAnimator.SetTrigger("Blink"); }
}
