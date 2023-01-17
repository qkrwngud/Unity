using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rigidbody;
    public float speed = 10f; // 속도 
    public float jump = 3f; // 점프 높이 
    public float dash = 5f; // 대시 
    public float rotSpeed = 3f;

    private Vector3 dir = Vector3.zero;

    private bool Ground = false;
    public LayerMask Layer;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize(); // 대각선에서만 빨리지는 경우를 없에줌

        CheckGround();

        if(Input.GetButtonDown("Jump") && Ground)
        {
            rigidbody.drag = 0;

            Vector3 jumpPower = Vector3.up * jump; // 위로 올라갈 힘 
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if(Input.GetButtonDown("Dash"))
        {
            rigidbody.drag = 20;

            Vector3 dashPower = transform.forward * -Mathf.Log(1 / rigidbody.drag) * dash;
            rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }

    }

    private void FixedUpdate() // 물리적인 이동이나 회전을 구현할때 사용
    {
        /*
            dir은 처음에 전부 0인데
            입력을 받으면 Vector3.zero와 달라진다
        */
        if (dir != Vector3.zero)
        {
            // 지금 바라보는 방향의 부호 != 갈 방 부호
            // Mathf.Sigh은 부호를 판별해줌 0이면 0, 양수면 1, 음수면 -1
            if(Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }


            // Lerp는 처음 위치에서 dir까지 서서히 움직임(Time.deltaTime만 넣으면 너무 느려서 3f를 곱해줌)
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed *Time.deltaTime ); 
        }
        
        // 현재 위치 + 갈 방향 * 속도 * Time.deltaTime
        rigidbody.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);

    }

    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, Layer))
        {
            Ground = true;
        }
        else
        {
            Ground = false;
        }
        Debug.Log(Ground);
    }

}
