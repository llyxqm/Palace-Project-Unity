using Mono.Data.Sqlite; 
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
public class Wood : MonoBehaviour
{
    //木头和背包基本信息
    public int max = 30;//背包容量
    public  static int id; //木头的数据库id;
    public  static int maxStack; //木头的最大堆叠数量


    private static bool isAnyWoodBeingPicked = false;    //防止一次性捡取多个木头
    public GameObject interactionUI_bt; //交互提示UI
    private bool isPlayerInRange = false;//玩家是否在范围内
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionUI_bt.SetActive(true);
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionUI_bt.SetActive(false);
            isPlayerInRange = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    { 
        GameObject root = GameObject.Find("InteractCanvas");
        Transform btnTransform = root.transform.Find("get_Button");
        interactionUI_bt = btnTransform.gameObject;


    }
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isAnyWoodBeingPicked)
        {
            isAnyWoodBeingPicked = true;
            //背包和数据库
            using (SqliteConnection conn = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/palace.db"))//接口实现,using后自动调用实现的dispose,编译器自动换成try-finally
            {
                conn.Open();
                using (SqliteCommand cmd = new SqliteCommand(conn))
                {                      //先试着堆
                    cmd.CommandText = $"UPDATE inventory SET count = count + 1 WHERE item_id = {id} AND count < {maxStack}";
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        //堆不了
                        cmd.CommandText = "SELECT COUNT(*) FROM inventory";
                        int currentItems = System.Convert.ToInt32(cmd.ExecuteScalar());
                        //检查背包是否已满
                        if (currentItems >= max)
                        {
                            Debug.Log("格子已占满，无法增加新物品类型！");
                            isAnyWoodBeingPicked = false;
                            return; // 终止，不销毁物体
                        }
                        // 没有现有的木头堆，尝试插入新记录
                        cmd.CommandText = $"INSERT INTO inventory (item_id, count) VALUES ({id}, 1)";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            


            //销毁木头
            Destroy(gameObject);
                interactionUI_bt.SetActive(false);
                isPlayerInRange = false;
            }
        }
   
    private void LateUpdate()
    {
        //最后重置状态，允许下一次捡取
        isAnyWoodBeingPicked = false;
    }
}
