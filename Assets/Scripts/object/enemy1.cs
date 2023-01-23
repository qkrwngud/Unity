using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy1 : MonoBehaviour
{

    public GameObject player;

    public Transform target;

    public static float enemy1_HP = 100.0f;

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

        target = GameObject.Find("player").transform;
    }

    void Update()
    {
        float distance = GetDistance(player_rigid.transform.position.x, player_rigid.transform.position.z,
            block_rigid.transform.position.x, block_rigid.transform.position.z);

        if (AIon)
        {
            agent.destination = target.transform.position;

            if (enemy1_HP <= 0)
            {
                AIon = false;

                color_object.material.color = Color.white;

                block_rigid.constraints = RigidbodyConstraints.FreezeAll;

                agent.isStopped = true;
            }
        }

        if (!AIon)
        {
            if (distance <= 5)
            {
                AIon = true;
                color_object.material.color = Color.black;
            }
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
