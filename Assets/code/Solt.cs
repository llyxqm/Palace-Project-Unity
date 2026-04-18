using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // 隐藏的身份信息
    public int itemID;
    public string itemName;

    // UI 组件引用
    public Image iconImage;
    public Text countText;

    // 提供一个公开的方法，让 Manager 调用
    public void SetupSlot(int id, string name, int count)
    {
        this.itemID = id;
        this.itemName = name;
        this.countText.text = count.ToString();

        // 去 Resources/Icons 文件夹找同名图片
        Sprite s = Resources.Load<Sprite>("Icons/" + name);
        if (s != null) iconImage.sprite = s;
    }
}