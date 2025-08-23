using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ==========================================================
     playerInterection : 플레이어의 상호작용을 다루는 스크립트

    (---수정사항---)

    21.07.23 : 임재하 : 기본적인 틀 제작
    ==========================================================
     */

public class PlayerInterection : MonoBehaviour
{

    private PlayerInput playerInput; //플레이어의 입력

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.interection) {
            Interect();
        }
    }

    private void Interect() {
        if (GameManager.canInput == false) return;

        Collider2D[] overlapedObjects = Physics2D.OverlapCircleAll(transform.position,1f);

        for (int i = 0; i < overlapedObjects.Length; i++)
        {
            InterectableObj interectable = overlapedObjects[i].transform.GetComponent<InterectableObj>();
            if (interectable != null) {
                interectable.interection();
                return;
            }
        }
    }
}
