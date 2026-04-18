using UnityEngine;

public class Cut_tree : MonoBehaviour
{
    public Animator animator;
    public Transform handPoint;

    private BoxCollider currentAxeCollider;

    public void StartSwing()
    {
        // 动态找一下手里斧子的碰撞体
        if (handPoint.childCount > 0)
        {
            currentAxeCollider = handPoint.GetComponentInChildren<BoxCollider>();
            if (currentAxeCollider != null) currentAxeCollider.enabled = true;
        }
    }

    public void EndSwing()
    {
        if (currentAxeCollider != null) currentAxeCollider.enabled = false;
    }

    void Update()
    {
        // 逻辑：按下左键 且 手里拿着斧子
        if (Input.GetMouseButton(0) && IsHoldingAxe())
        {
            animator.SetBool("cut_tree", true);
        }
        else
        {
            animator.SetBool("cut_tree", false);
        }
    }

    // 关键：判断手里是否有斧子的函数
    bool IsHoldingAxe()
    {
        // 1. 先看手里有没有东西
        if (handPoint.childCount > 0)
        {
            // 2. 拿到手里第一个东西的名字
            string heldItemName = handPoint.GetChild(0).name;

            // 3. 判断名字里是否包含“Axe”或“斧子”
            // Unity生成的物体通常会带 (Clone) 后缀，用 Contains 
            if (heldItemName.Contains("Axe") || heldItemName.Contains("斧子"))
            {
                return true;
            }
        }

        return false; // 没东西或者不是斧子
    }
}