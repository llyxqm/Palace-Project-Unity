using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        // 1. 快速检查标签
        if (other.CompareTag("tree"))
        {
            // 2.
            tree targetTree = other.GetComponent<tree>();

            // 3. 安全检查
            if (targetTree != null)
            {
                // 4. 执行扣血并传递位置
                targetTree.GetDamage(damage, transform.position);

                //直接关掉斧子的 Collider，防止一刀多伤
                 GetComponent<Collider>().enabled = false; 
            }
        }
    }


}
