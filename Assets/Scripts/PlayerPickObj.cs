using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickObj : MonoBehaviour
{
    public static PlayerPickObj playerPickObj
    {
        get
        {
            if (esc_instance == null)
            {
                esc_instance = FindObjectOfType<PlayerPickObj>();
            }

            return esc_instance;
        }
    }//GameManager를 싱글턴으로 설정

    private static PlayerPickObj esc_instance; //싱글턴에 이용된 인스턴스


    public GameObject _object;
    public SpriteRenderer _image;

}
