using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class block : MonoBehaviour
{

    public GameObject player;

    public Transform target;

    public static float block_HP = 100.0f;

    NavMeshAgent agent;

    private Rigidbody player_rigid;
    private Rigidbody block_rigid;

    private Renderer color_object;

    private bool AIon = false;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid = player.GetComponent<Rigidbody>();
        block_rigid = gameObject.GetComponent<Rigidbody>();
        color_object = gameObject.GetComponent<Renderer>();


        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

        if(AIon)
        {
            target = GameObject.Find("player").transform;
            agent.destination = target.transform.position;
        }


        float distance = GetDistance(player_rigid.transform.position.x, player_rigid.transform.position.z,
            block_rigid.transform.position.x, block_rigid.transform.position.z);

        if (!AIon)
        {
            if (distance < 5)
            {
                color_object.material.color = Color.black;
                AIon = true;
            }
            else
            {
                color_object.material.color = Color.white;
            }
        }

        if (block_HP <= 0)
        {
            AIon = false;
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
