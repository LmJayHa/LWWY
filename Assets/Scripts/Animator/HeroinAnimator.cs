using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroinAnimator : MonoBehaviour
{
    Animator heroinAnimator;
    Transform heroinTransform;

    public enum SeeWhere { FRONT, BACK, LEFT, RIGHT }

    public bool isMoving { get { return (isMoveX || isMoveY); } }

    public bool isMoveX;

    public bool isMoveY;

    private float toPositionX;
    private float toPositionY;

    private float rate;

    private void Awake()
    {
        heroinAnimator = GetComponent<Animator>();
        heroinTransform = GetComponent<Transform>();
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
                heroinAnimator.SetFloat("lastY", -1f);
                heroinAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.BACK:
                heroinAnimator.SetFloat("lastY", 1f);
                heroinAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.LEFT:
                heroinAnimator.SetFloat("lastY", 0f);
                heroinAnimator.SetFloat("lastX", -1f);
                break;
            case SeeWhere.RIGHT:
                heroinAnimator.SetFloat("lastY", 0f);
                heroinAnimator.SetFloat("lastX", 1f);
                break;
            default:
                break;
        }
    }


    public void moveToX(float X)
    {
        if (heroinTransform.position.x > X)
        {
            rate = -1.0f;
            See(SeeWhere.LEFT);
            heroinAnimator.SetBool("isWalking", true);
        }
        else if (heroinTransform.position.x <= X)
        {
            rate = 1.0f;
            See(SeeWhere.RIGHT);
            heroinAnimator.SetBool("isWalking", true);
        }
        isMoveX = true;
        toPositionX = X;
    }

    public void moveToY(float Y)
    {
        if (heroinTransform.position.y > Y)
        {
            rate = -1.0f;
            See(SeeWhere.FRONT);
            heroinAnimator.SetBool("isWalking", true);
        }
        else if (heroinTransform.position.y <= Y)
        {
            rate = 1.0f;
            See(SeeWhere.BACK);
            heroinAnimator.SetBool("isWalking", true);
        }
        isMoveY = true;
        toPositionY = Y;

    }

    private void WalkingMoveToX()
    {
        if ((rate < 0 && heroinTransform.position.x <= toPositionX) || (rate > 0 && heroinTransform.position.x >= toPositionX))
        {
            isMoveX = false;
            heroinAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            heroinTransform.Translate(rate * Time.deltaTime * 1.5f, 0f, 0f);
        }
    }

    private void WalkingMoveToY()
    {
        if ((rate < 0 && heroinTransform.position.y <= toPositionY) || (rate > 0 && heroinTransform.position.y >= toPositionY))
        {
            isMoveY = false;
            heroinAnimator.SetBool("isWalking", false);
            return;
        }
        else
        {
            heroinTransform.Translate(0f, rate * Time.deltaTime * 1.5f, 0f);
        }

    }



    public void Sit() { heroinAnimator.SetTrigger("Sit"); }
    public void Stand() { heroinAnimator.SetTrigger("Stand"); }
    public void SeeLeft() { heroinAnimator.SetTrigger("SeeLeft"); }
    public void SeeRight() { heroinAnimator.SetTrigger("SeeRight"); }
    public void SeeFront() { heroinAnimator.SetTrigger("SeeFront"); }
    public void Blink() { heroinAnimator.SetTrigger("Blink"); }

    public void Hug() { heroinAnimator.SetTrigger("Hug"); }
    public void UnHug() { heroinAnimator.SetTrigger("UnHug"); }
}
