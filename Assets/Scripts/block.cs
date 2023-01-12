using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    public GameObject player;

    private Rigidbody player_rigid;
    private Rigidbody block_rigid;

    private Renderer color_object;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid = player.GetComponent<Rigidbody>();
        block_rigid = gameObject.GetComponent<Rigidbody>();
        color_object = gameObject.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        float distance = GetDistance(player_rigid.transform.position.x, player_rigid.transform.position.z,
            block_rigid.transform.position.x, block_rigid.transform.position.z);


        if(distance < 6)
        {
            color_object.material.color = Color.black;
        }
        else
        {
            color_object.material.color = Color.white;
        }

    }

    float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = Mathf.Abs(x2 - x1);
        float height = Mathf.Abs(y2 - y1);

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

}
