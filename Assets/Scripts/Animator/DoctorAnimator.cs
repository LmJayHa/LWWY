using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAnimator : MonoBehaviour
{
    Animator doctorAnimator;

    public enum SeeWhere { FRONT, BACK, LEFT, RIGHT }

    private void Awake()
    {
        doctorAnimator = GetComponent<Animator>();
    }

    public void See(SeeWhere seeWhere)
    {
        switch (seeWhere)
        {
            case SeeWhere.FRONT:
                doctorAnimator.SetFloat("lastY", -1f);
                doctorAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.BACK:
                doctorAnimator.SetFloat("lastY", 1f);
                doctorAnimator.SetFloat("lastX", 0f);
                break;
            case SeeWhere.LEFT:
                doctorAnimator.SetFloat("lastY", 0f);
                doctorAnimator.SetFloat("lastX", -1f);
                break;
            case SeeWhere.RIGHT:
                doctorAnimator.SetFloat("lastY", 0f);
                doctorAnimator.SetFloat("lastX", 1f);
                break;
            default:
                break;
        }
    }
}
