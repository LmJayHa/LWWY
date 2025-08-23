using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndLock : MonoBehaviour
{
    public bool isForest;
    public bool isFlowermap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isForest)
            {
                ChatManager.chatManager.OpenChat(246, null);
            }
            else if (!isForest && !isFlowermap)
            {
                ChatManager.chatManager.OpenChat(247, null);
            }
            else if (!isForest && isFlowermap)
            {
                ChatManager.chatManager.OpenChat(253, null);
            }
           
            
        }
        
    }
}
