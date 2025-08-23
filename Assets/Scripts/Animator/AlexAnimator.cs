using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexAnimator : MonoBehaviour
{
    Animator alexAnimator;

    public enum SeeWhere { FRONT, BACK, LEFT, RIGHT }

    private void Awake()
    {
        alexAnimator = GetComponent<Animator>();
    }

    public void See(SeeWhere seeWhere)
    {
        switch (seeWhere)
        {
            case SeeWhere.FRONT:
                alexAnimator.SetFloat("lastY", -1f);
                alexAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.BACK:
                alexAnimator.SetFloat("lastY", 1f);
                alexAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.LEFT:
                alexAnimator.SetFloat("lastY", 0f);
                alexAnimator.SetFloat("lastX", -1f);
                break;
            case SeeWhere.RIGHT:
                alexAnimator.SetFloat("lastY", 0f);
                alexAnimator.SetFloat("lastX", 1f);
                break;
            default:
                break;
        }
    }
}
