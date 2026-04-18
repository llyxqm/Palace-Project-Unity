using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bag : MonoBehaviour
{
    public int max = 30;
    private string current_name;
    private int curent_count;
    Image img;
    Sprite sp;//背包里的显示物品图片组件
    TextMeshProUGUI text;//背包里的显示物品名字和数量组件
    private void OnEnable()
    {
        ClearBagDisplay();

        int cur_slot = 0;
        Transform cur_Child;
        using (SqliteConnection conn = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/palace.db"))
        {
            conn.Open();
            using (SqliteCommand cmd = new SqliteCommand(conn))
            {
                cmd.CommandText = "SELECT inv.item_id, inv.count, item.name FROM Inventory inv JOIN Item item ON inv.item_id = item.id";
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        curent_count = reader.GetInt32(1);
                        current_name = reader.GetString(2);
                        cur_Child = transform.GetChild(cur_slot);
                        img = cur_Child.GetComponent<Image>();
                        sp = Resources.Load<Sprite>("image/" + current_name);
                        img.sprite = sp;
                        
                        text = cur_Child.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = current_name + " x" + curent_count;

                        cur_slot++;
                    }
                }
            }
        }
    }

    void ClearBagDisplay()
    {
        foreach (Transform child in transform)
        {
            // 比如把图片设为透明，文字设为空
            Image img = child.GetComponent<Image>();
            if (img != null) img.sprite = null;

            TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null) txt.text = "";
        }
    }
}
