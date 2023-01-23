using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody player_rigid;
    public float speed = 10f; // 속도 

    public float rotSpeed = 3f;
    public float damage;
    public GameObject enemy1;

    private Vector3 dir = Vector3.zero;

    private Rigidbody enemy1_rigid;

    public LayerMask Layer;

    private bool move = true;

    void Start()
    {
        player_rigid = GetComponent<Rigidbody>();

        enemy1_rigid = enemy1.GetComponent<Rigidbody>();
    }

    void Update()
    {

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize(); // 대각선에서만 빨리지는 경우를 없에줌


        // 공격
        float enemy1_distance = GetDistance(player_rigid.transform.position.x, player_rigid.transform.position.z,
            enemy1_rigid.transform.position.x, enemy1_rigid.transform.position.z);

        if(Input.GetKeyDown(KeyCode.K))
        {
            if (global::enemy1.enemy1_HP > 0 && enemy1_distance < 3.5)
            {
                move = false;
                global::enemy1.enemy1_HP -= damage;
                Debug.Log(damage + "의 피해를 입힘 | 체력: " + global::enemy1.enemy1_HP);

                Invoke("change_move", 0.2f); // 딜레이 이후에 change_move함수 호출

            }
        }

    }

    private void FixedUpdate() // 물리적인 이동이나 회전을 구현할때 사용
    {
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
            player_rigid.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
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
