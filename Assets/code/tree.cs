using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTree: MonoBehaviour
{
    private Vector3 lasthintpoint;
    public int HP = 100;
    public GameObject stump; 
    public GameObject downtree;

    public void GetDamage(int damage, Vector3 hintpoint)
    {
        lasthintpoint = hintpoint;
        HP -= damage;
        if(HP <= 0)
        {
         Die();
        }
    }
    void Die()
    {
        // 1. 计算方向向量 (从撞击点指向树中心)
        Vector3 fallDirection = transform.position - lasthintpoint;

        // 2. 抹平高度差，只保留水平方向的推力
        fallDirection.y = 0;
        fallDirection.Normalize(); // 变成长度为1的单位向量

        // 3. 销毁本体
        Destroy(gameObject);

        // 4. 生成树桩（树桩保持原位，不需要旋转）
        Instantiate(stump, transform.position, Quaternion.identity);

        // 5. 生成倒下的树干
        GameObject treeBody = Instantiate(downtree, transform.position, Quaternion.identity);

        Rigidbody rb = treeBody.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 往反方向给一个瞬间的冲力
            rb.AddForce(fallDirection * 30f, ForceMode.Impulse);
        }

    }
}
