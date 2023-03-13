using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCursor : MonoBehaviour
{
    private GameObject crosshair;
    private Vector2 chPos;
    public float angle;
    public float transformFactor;
    private GameObject player;
    private bool facingLeft; 
    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        player = GameObject.FindGameObjectWithTag("player");
        facingLeft = false; 
    }

    // Update is called once per frame
    void Update()
    {

       /* if (player.transform.localScale.x < 0)
        {
            transformFactor = 180;
        }
        else
        {
            transformFactor = 0;
        }
       */
        chPos = crosshair.transform.position;
        angle = Mathf.Atan2((( chPos.y-transform.position.y)), ((chPos.x - transform.position.x)));
        transform.rotation = Quaternion.Euler(0, 0, (angle * Mathf.Rad2Deg + transformFactor));

        if((int)(angle * Mathf.Rad2Deg) > 90 && !facingLeft )
        {
            flip(player, true); 
        }
        if ((int)(angle * Mathf.Rad2Deg) < 90 && !facingLeft)
        {
            flip(player, false);
        }
    }
    void flip(GameObject player, bool flip)
    {
        player.GetComponent<SpriteRenderer>().flipX = flip; 
       
    }
}
