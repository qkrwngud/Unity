using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rigidbody;
    public float speed = 10f; // 속도 

    public float rotSpeed = 3f;
    public float damage;
    public GameObject rigidblock;


    private Vector3 dir = Vector3.zero;

    private Rigidbody block_rigid;

    public LayerMask Layer;

    private bool move = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        block_rigid = rigidblock.GetComponent<Rigidbody>();
    }

    void Update()
    {

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize(); // 대각선에서만 빨리지는 경우를 없에줌


        // 공격
        float distance = GetDistance(rigidbody.transform.position.x, rigidbody.transform.position.z,
            block_rigid.transform.position.x, block_rigid.transform.position.z);


        if (block.block_HP > 0)
        {
            if (distance < 2)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    move = false;
                    block.block_HP -= damage;
                    Debug.Log(damage + "의 피해를 입힘");

                    Invoke("change_move", 0.2f); // 딜레이 이후에 change_move함수 호출

                }
            }
        }



    }

    private void FixedUpdate() // 물리적인 이동이나 회전을 구현할때 사용
    {
        /*
            dir은 처음에 전부 0인데
            입력을 받으면 Vector3.zero와 달라진다
        */

        if (move)
        {
            if (dir != Vector3.zero)
            {
                // 지금 바라보는 방향의 부호 != 갈 방 부호
                // Mathf.Sigh은 부호를 판별해줌 0이면 0, 양수면 1, 음수면 -1
                if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
                {
                    transform.Rotate(0, 1, 0);
                }


                // Lerp는 처음 위치에서 dir까지 서서히 움직임(Time.deltaTime만 넣으면 너무 느려서 3f를 곱해줌)
                transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            }

            // 현재 위치 + 갈 방향 * 속도 * Time.deltaTime
            rigidbody.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
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

    void change_move()
    {
        move = true;
    }

}
