using UnityEngine;

public class ui_manager : MonoBehaviour
{
    [Header("面板引用")]
    public GameObject loginPanel;
    public GameObject registerPanel;

    // Unity 自动调用：游戏启动的第一帧执行
    private void Start()
    {
        // 确保一开始状态是正确的
        GoToLogin();
    }

    // 这个方法可以绑定在“去注册”按钮上
    public void GoToRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    // 这个方法可以绑定在“返回登录”按钮上
    public void GoToLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
}