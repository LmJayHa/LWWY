using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamiliaAnimator : MonoBehaviour
{
    Animator nurseAnimator;
    Transform nurseTransform;

    public enum SeeWhere { FRONT, BACK, LEFT, RIGHT }

    public bool isMoving { get { return (isMoveX || isMoveY); } }

    public bool isMoveX;

    public bool isMoveY;

    private float toPositionX;
    private float toPositionY;

    private float rate;

    private void Awake()
    {
        nurseAnimator = GetComponent<Animator>();
        nurseTransform = GetComponent<Transform>();
        isMoveX = false;
        isMoveY = false;
    }

    private void Update()
    {
        if (isMoveX)
        {
            WalkingMoveToX();
        }
        else if (isMoveY)
        {
            WalkingMoveToY();
        }
    }




    public void See(SeeWhere seeWhere)
    {
        switch (seeWhere)
        {
            case SeeWhere.FRONT:
                nurseAnimator.SetFloat("lastY", -1f);
                nurseAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.BACK:
                nurseAnimator.SetFloat("lastY", 1f);
                nurseAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.LEFT:
                nurseAnimator.SetFloat("lastY", 0f);
                nurseAnimator.SetFloat("lastX", -1f);
                break;
            case SeeWhere.RIGHT:
                nurseAnimator.SetFloat("lastY", 0f);
                nurseAnimator.SetFloat("lastX", 1f);
                break;
            default:
                break;
        }
    }


    public void moveToX(float X)
    {
        if (nurseTransform.position.x > X)
        {
            rate = -1.0f;
            See(SeeWhere.LEFT);
            nurseAnimator.SetBool("isWalking", true);
        }
        else if (nurseTransform.position.x <= X)
        {
            rate = 1.0f;
            See(SeeWhere.RIGHT);
            nurseAnimator.SetBool("isWalking", true);
        }
        isMoveX = true;
        toPositionX = X;
    }

    public void moveToY(float Y)
    {
        if (nurseTransform.position.y > Y)
        {
            rate = -1.0f;
            See(SeeWhere.FRONT);
            nurseAnimator.SetBool("isWalking", true);
        }
        else if (nurseTransform.position.y <= Y)
        {
            rate = 1.0f;
            See(SeeWhere.BACK);
            nurseAnimator.SetBool("isWalking", true);
        }
        isMoveY = true;
        toPositionY = Y;

    }

    private void WalkingMoveToX()
    {
        if ((rate < 0 && nurseTransform.position.x <= toPositionX) || (rate > 0 && nurseTransform.position.x >= toPositionX))
        {
            isMoveX = false;
            nurseAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            nurseTransform.Translate(rate * Time.deltaTime * 1.5f, 0f, 0f);
        }
    }

    private void WalkingMoveToY()
    {
        if ((rate < 0 && nurseTransform.position.y <= toPositionY) || (rate > 0 && nurseTransform.position.y >= toPositionY))
        {
            isMoveY = false;
            nurseAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            nurseTransform.Translate(0f, rate * Time.deltaTime * 1.5f, 0f);
        }

    }
}