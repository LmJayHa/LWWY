using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTopDown : MonoBehaviour
{
    float y;
    float yUp;

    bool toGo = true;

    void Start()
    {
        y = transform.position.y;
        yUp = y + 1;
    }
    void Update()
    {
        if (toGo)
        {
            transform.Translate(0, Time.deltaTime * 1f, 0);
            if (transform.position.y>=yUp)
            {
                transform.position = new Vector2(transform.position.x, yUp);
                toGo = !toGo;
            }
        }
        else
        {
            transform.Translate(0, -Time.deltaTime * 1f, 0);
            if (transform.position.y <= y)
            {
                transform.position = new Vector2(transform.position.x, y);
                toGo = !toGo;
            }
        }
    }
}
