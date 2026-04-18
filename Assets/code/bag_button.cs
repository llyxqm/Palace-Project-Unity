using UnityEngine;

public class bag_button : MonoBehaviour
{
    public GameObject bag;

    private void Start()
    {
        bag.SetActive(false);
        // 确保游戏开始时时间是正常流逝的
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            switchbagcanvas();
        }
    }

    public void switchbagcanvas()
    {
        // 1. 切换背包显示状态
        bool isOpening = !bag.activeSelf;
        bag.SetActive(isOpening);

        // 2. 根据背包状态控制游戏暂停
        if (isOpening)
        {
            // 打开背包：时间缩放设为 0（游戏完全静止）
            Time.timeScale = 0f;
            Debug.Log("游戏暂停");
        }
        else
        {
            // 关闭背包：时间缩放恢复为 1（正常运行）
            Time.timeScale = 1f;
            Debug.Log("游戏恢复");
        }
    }
}