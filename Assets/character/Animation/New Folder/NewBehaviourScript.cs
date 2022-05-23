using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform centralAxis;
    public Transform cam;
    public float camSpeed;
    float mouseX;
    float mouseY;
    float wheel;

    Vector3 movement;
    public Transform playerAxis; //플레이어의 중심축
    public Transform player; //플레이어       

    void CamMove()
    {
        //마우스 왼쪽이나 오른쪽 클릭일때 카메라 회전시키기
        if (Input.GetMouseButton(1) | Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            //카메라 상하 움직임 제한
            if (mouseY > 20)
                mouseY = 20;
            if (mouseY < -3)
                mouseY = -3;

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY,
                    centralAxis.rotation.y + mouseX, 0) * camSpeed);


        }
    }
    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -3)
            wheel = -3;
        if (wheel <= -7)
            wheel = -7;
        cam.localPosition = new Vector3(0, 0, wheel);
    }
    void Update()
    {
        CamMove();
        Zoom();

        //플레이어 이동,회전 파트
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector3(moveX, 0, moveY);

        if (movement != Vector3.zero)
        {
            playerAxis.Translate(movement * Time.deltaTime);
            player.localRotation = Quaternion.Slerp(player.transform.localRotation,
                Quaternion.LookRotation(movement), 5 * Time.deltaTime);

            //플레이어 이동 애니메이션
            player.GetComponent<Animator>().SetBool("Walk", true);
        }

        //플레이어 대기 애니메이션
        if (movement == Vector3.zero)
            player.GetComponent<Animator>().SetBool("Walk", false);
    }
    private void LateUpdate()
    {
        // 카메라 중심축이 플레이어 따라다니기
        centralAxis.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
    }
}
