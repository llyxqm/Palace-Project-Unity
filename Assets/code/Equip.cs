using UnityEngine;
using TMPro;

public class Equip : MonoBehaviour
{
    public Transform handPoint;

    public void OnEquipClick()
    {
        TMP_Text slotText = transform.parent.GetComponentInChildren<TMP_Text>();

        if (slotText != null)
        {
            // 1. 提取名字
            string fullText = slotText.text;
            string itemName = fullText.Split(' ')[0];

            // 2. 加载预制体
            GameObject toolPrefab = Resources.Load<GameObject>("tool/" + itemName);

            if (toolPrefab != null)
            {
                //清理手部 ---
                // 遍历 handPoint 下所有的子物体并删掉
                foreach (Transform child in handPoint)
                {
                    Destroy(child.gameObject);
                }
                // -----------------------

                // 3. 生成并挂载
                // Instantiate 的第二个参数传 handPoint，直接让它生成为 handPoint 的子物体
                GameObject newTool = Instantiate(toolPrefab, handPoint);

                // 4. 重置位置和旋转
                newTool.transform.localPosition = Vector3.zero;
                newTool.transform.localRotation = Quaternion.identity;

                Debug.Log("成功装备物品: " + itemName);
            }
            else
            {
                Debug.LogError("加载失败！路径错误：" + itemName);
            }
        }
    }
}
