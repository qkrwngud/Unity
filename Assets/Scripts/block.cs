using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class block : MonoBehaviour
{

    public GameObject player;

    public Transform target;


    NavMeshAgent agent;

    private Rigidbody player_rigid;
    private Rigidbody block_rigid;
    private Transform block_trans;

    private Renderer color_object;

    private bool check_speed = false;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid = player.GetComponent<Rigidbody>();
        block_rigid = gameObject.GetComponent<Rigidbody>();
        color_object = gameObject.GetComponent<Renderer>();
        block_trans = gameObject.GetComponent<Transform>();


        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

        block_trans.transform.eulerAngles = new Vector3(0, 0, 0);

        target = GameObject.Find("player").transform;

        agent.destination = target.transform.position;

        float distance = GetDistance(player_rigid.transform.position.x, player_rigid.transform.position.z,
            block_rigid.transform.position.x, block_rigid.transform.position.z);


        if (check_speed) agent.speed = 0;

        if (distance < 6)
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

    private void OnCollisionStay(Collision collision)
    {
        agent.speed = 0;

        check_speed = true;

        block_rigid.constraints = RigidbodyConstraints.FreezeAll;
    }



}
