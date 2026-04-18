using UnityEngine;

public class C_c: MonoBehaviour
{
    public float moveSpeed = 5f;        
    public float rotationSpeed = 10f;    // 转向平滑速度
    private CharacterController cc;      
    public Transform cameraTrans;        
    public Animator animator;  

    void Start()
    {
        // 初始化时获取当前物体上的 CharacterController 组件
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");  

        // 2. 如果没有任何输入，就直接跳过后面的逻辑（节省性能）
        if (Mathf.Abs(h) < 0.01f && Mathf.Abs(v) < 0.01f || animator.GetBool("build_room"))//修房子不能动
        {
            animator.SetFloat("speed", 0);
            ApplyGravity();
            return;
        }

        // 3. 【核心计算】确定参考方向
        // 计算从玩家指向相机的水平向量（忽略 Y 轴高度差）
        Vector3 CamToPlayer = transform.position - cameraTrans.position;
        CamToPlayer.y = 0;

        // 4. 定义“前”和“右”
        Vector3 camForward = CamToPlayer.normalized;
        // 利用向量叉乘：向上向量 叉乘 向前向量 = 向右向量
        Vector3 camRight = Vector3.Cross(Vector3.up, camForward).normalized;

        // 5. 组合最终的移动方向
        // 最终方向 = (向前向量 * 纵向输入) + (向右向量 * 横向输入)
        Vector3 moveDir = (camForward * v + camRight * h).normalized;
        //
        animator.SetFloat("speed", moveSpeed);
        // 6. 执行位移：使用 CharacterController 的 Move 方法
        cc.Move(moveDir * moveSpeed * Time.deltaTime);

        // 7. 处理旋转：让角色面朝移动的方向
        if (moveDir != Vector3.zero)
        {
            // 将方向向量转换为四元数旋转角度
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            // 使用 Slerp 球面插值，实现平滑转头，而不是瞬间闪现
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 8. 应用重力
        ApplyGravity();
    }

    // 提取出来的重力方法
    void ApplyGravity()
    {
        // 如果 CC 检测到没踩在地板上，就每帧往下推
        if (!cc.isGrounded)
        {
            cc.Move(Vector3.down * 9.8f * Time.deltaTime);
        }
    }
}