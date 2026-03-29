using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_p : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 1.0f;
    private float currentAngle = 0f; // 记录当前的总弧度
    private float radius;            // 固定的圆周半径
    private float heightOffset; // 相机相对于玩家的高度偏移

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("请在 Inspector 中将 Player 对象拖到 camera_p 脚本的 player 变量上！");
            enabled = false;
            return;
        }
        Vector3 offset = transform.position - player.position;
        Vector3 horizontalOffset = new Vector3(offset.x, 0, offset.z);// 水平偏移
        radius = horizontalOffset.magnitude; // 水平半径
        heightOffset = offset.y; // 高度偏移
        currentAngle = Mathf.Atan2(horizontalOffset.x, horizontalOffset.z); // 初始角度
    }

    void LateUpdate()
    {
        if(player == null) return;
        // 2. 获取鼠标位移作为“弧长增量”
        float deltaS = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // 3. 反向算出弧度变化量：Δθ = Δs / r
        float deltaTheta = deltaS / radius;

        // 4. 累加弧度
        currentAngle += deltaTheta;

        // 5. 根据新角度算出新的相对坐标
        float newX = Mathf.Sin(currentAngle) * radius;
        float newZ = Mathf.Cos(currentAngle) * radius;

        transform.position = player.position + new Vector3(newX, heightOffset, newZ);

        // 7. 始终注视角色
        transform.LookAt(player.position + Vector3.up * 2f);
    }
}
