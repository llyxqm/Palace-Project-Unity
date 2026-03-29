using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class RegisterHandler : MonoBehaviour
{
    [Header("注册输入框引用")]
    public TMP_InputField accountIF;
    public TMP_InputField passwordIF;
    public TMP_InputField confirmPasswordIF;

    // 这个方法专门绑定在“注册”按钮上
    public void ExecuteRegister()
    {
        string account = accountIF.text;
        string pw = passwordIF.text;
        string pwConfirm = confirmPasswordIF.text;

        // 逻辑判断开始
        if (!IsPureNumber(account, 6, 12))
        {
            Debug.LogError("<color=orange>账号格式错误：必须是6-12位纯数字</color>");
            return;
        }

        if (!IsPureNumber(pw, 6, 12))
        {
            Debug.LogError("<color=orange>密码格式错误：必须是6-12位纯数字</color>");
            return;
        }

        if (pw != pwConfirm)
        {
            Debug.LogError("<color=red>注册失败：两次密码输入不一致</color>");
            return;
        }

        // 全部通过后，打印成功并准备对接 API
        Debug.Log($"<color=green>验证通过！账号：{account}</color>");

        // TODO: 这里写调用数据库 API 的代码
    }

    // 一个通用的纯数字验证小工具
    private bool IsPureNumber(string input, int min, int max)
    {
        return Regex.IsMatch(input, $@"^\d{{{min},{max}}}$");
    }
}