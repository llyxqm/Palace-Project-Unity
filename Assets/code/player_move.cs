using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 5f;

    void Start()
    {
        // 1. 获取挂在同一个物体上的 Animator 组件
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 2. 获取玩家的输入（横向和纵向）
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 3. 计算移动向量
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        // 4. 计算移动的总强度 (Magnitude)
        // 如果玩家没按键，curSpeed 就是 0；按了键，它就会大于 0
        float curSpeed = movement.magnitude;

        // 5. 【关键】把这个数值传给 Animator 里的 "speed" 变量
        // 名字必须和你图中 Animator 面板里写的一模一样（区分大小写）
        anim.SetFloat("speed", curSpeed);

        // 控制角色位移的代码...
        if (curSpeed > 0.1f)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
            // 顺便让角色转向移动方向
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}