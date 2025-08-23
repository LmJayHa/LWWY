using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettableObj : InterectableObj
{
    public int itemCode; // 이 아이템의 코드

    public override void interection()
    {
        base.interection();

        if (PlayerData.playerData.addItem(itemCode)) {
            //+)아이템을 얻었다 내용 추가
            Destroy(gameObject);
        }
    }

}
