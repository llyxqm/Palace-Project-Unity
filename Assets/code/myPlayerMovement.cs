using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 playerVelocity; // 专门处理 Y 轴（跳跃和重力）

    public float moveSpeed = 5f;
    public float gravityValue = -9.81f; // 地球重力
    public float rotationSpeed = 10f;

    void Start()
    {
        // 1. 获取组件
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 2. 处理地面检测：如果落地了，重力加速度清零（给个小的向下力保证贴地）
        if (cc.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // 3. 获取输入并计算相机朝向（沿用你之前的逻辑）
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;

        // 4. 水平移动：使用 cc.Move
        if (moveDir.magnitude > 0.1f)
        {
            cc.Move(moveDir * moveSpeed * Time.deltaTime);

            // 转向逻辑
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 5. 垂直移动：计算重力
        playerVelocity.y += gravityValue * Time.deltaTime;
        // 这一步是把重力应用到 CC 上
        cc.Move(playerVelocity * Time.deltaTime);
    }
}