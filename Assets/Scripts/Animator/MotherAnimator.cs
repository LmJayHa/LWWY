using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherAnimator : MonoBehaviour
{
    Animator motherAnimator;
    Transform motherTransform;

    public enum SeeWhere { FRONT, BACK, LEFT, RIGHT }

    public bool isMoving { get { return (isMoveX || isMoveY); } }

    public bool isMoveX;

    public bool isMoveY;

    private float toPositionX;
    private float toPositionY;

    private float rate;

    private void Awake()
    {
        motherAnimator = GetComponent<Animator>();
        motherTransform = GetComponent<Transform>();
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
                motherAnimator.SetFloat("lastY", -1f);
                motherAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.BACK:
                motherAnimator.SetFloat("lastY", 1f);
                motherAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.LEFT:
                motherAnimator.SetFloat("lastY", 0f);
                motherAnimator.SetFloat("lastX", -1f);
                break;
            case SeeWhere.RIGHT:
                motherAnimator.SetFloat("lastY", 0f);
                motherAnimator.SetFloat("lastX", 1f);
                break;
            default:
                break;
        }
    }


    public void moveToX(float X)
    {
        if (motherTransform.position.x > X)
        {
            rate = -1.0f;
            See(SeeWhere.LEFT);
            motherAnimator.SetBool("isWalking", true);
        }
        else if (motherTransform.position.x <= X)
        {
            rate = 1.0f;
            See(SeeWhere.RIGHT);
            motherAnimator.SetBool("isWalking", true);
        }
        isMoveX = true;
        toPositionX = X;
    }

    public void moveToY(float Y)
    {
        if (motherTransform.position.y > Y)
        {
            rate = -1.0f;
            See(SeeWhere.FRONT);
            motherAnimator.SetBool("isWalking", true);
        }
        else if (motherTransform.position.y <= Y)
        {
            rate = 1.0f;
            See(SeeWhere.BACK);
            motherAnimator.SetBool("isWalking", true);
        }
        isMoveY = true;
        toPositionY = Y;

    }

    private void WalkingMoveToX()
    {
        if ((rate < 0 && motherTransform.position.x <= toPositionX) || (rate > 0 && motherTransform.position.x >= toPositionX))
        {
            isMoveX = false;
            motherAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            motherTransform.Translate(rate * Time.deltaTime * 1.5f, 0f, 0f);
        }
    }

    private void WalkingMoveToY()
    {
        if ((rate < 0 && motherTransform.position.y <= toPositionY) || (rate > 0 && motherTransform.position.y >= toPositionY))
        {
            isMoveY = false;
            motherAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            motherTransform.Translate(0f, rate * Time.deltaTime * 1.5f, 0f);
        }

    }
}
