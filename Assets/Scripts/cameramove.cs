using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    public GameObject player;

    private Vector3 vec = new Vector3(0, 13.16f, -8.27f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = player.transform.position + vec;
    }
}
