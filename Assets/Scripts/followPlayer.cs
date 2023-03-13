using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player"); 
    }

    // Update is called once per frame
    void Update()
    {
       gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5) ; 
        
    }
}
