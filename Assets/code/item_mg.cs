using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/*
public class item_mg : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        using (SqliteConnection conn = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/palace.db"))
        {
            conn.Open();
            LoadWoodDataToClass(conn);
            LoadAxeDataToClass(conn);
            LoadHammerDataToClass(conn);
        }
    }

    void LoadWoodDataToClass(SqliteConnection conn)
    {
        //从数据库获取木头信息并赋值给Wood类的静态变量
                using (SqliteCommand cmd = new SqliteCommand(conn))
                {
                    cmd.CommandText = "SELECT id, max_stack FROM Item WHERE name = '木头'";
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Wood.id = reader.GetInt32(0);
                            Debug.Log("木头ID: " + Wood.id);
                            Wood.maxStack = reader.GetInt32(1);
                            Debug.Log("木头最大堆叠数量: " + Wood.maxStack);
                        }
                    }
                }

        Debug.Log("木头数据已加载");
    }


    void LoadAxeDataToClass(SqliteConnection conn)
    {
        //从数据库获取斧子信息并赋值给Axe类的静态变量
                using (SqliteCommand cmd = new SqliteCommand(conn))
                {
                    cmd.CommandText = "SELECT id, max_stack FROM Item WHERE name = '斧子'";
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Axe.id = reader.GetInt32(0);
                            Debug.Log("斧子ID: " + Axe.id);
                            Axe.maxStack = reader.GetInt32(1);
                            Debug.Log("斧子最大堆叠数量: " + Axe.maxStack);
                        }
                    }
                }
        Debug.Log("斧子数据已加载");
    }

    void LoadHammerDataToClass(SqliteConnection conn)
    {
        //从数据库获取锤子信息并赋值给hammer类的静态变量
                using (SqliteCommand cmd = new SqliteCommand(conn))
                {
                    cmd.CommandText = "SELECT id, max_stack FROM Item WHERE name = '锤子'";
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Hammer.id = reader.GetInt32(0);
                            Debug.Log("锤子ID: " + Hammer.id);
                            Hammer.maxStack = reader.GetInt32(1);
                            Debug.Log("锤子最大堆叠数量: " + Hammer.maxStack);
                }
                    }
                }
        Debug.Log("锤子数据已加载");
    }
}
*/


//更好的自动方法
public class item_mg : MonoBehaviour
{
    void Start()
    {
        string dbPath = "URI=file:" + Application.persistentDataPath + "/palace.db";
        using (SqliteConnection conn = new SqliteConnection(dbPath))
        {
            conn.Open();

            // 只需要传入类名，代码会自动拿类名去数据库查 english_name
            AutoLoad(conn, typeof(Wood));//类放进tarfetClasc
            AutoLoad(conn, typeof(Axe));
            AutoLoad(conn, typeof(Hammer));
        }
    }
    //Type元类
    void AutoLoad(SqliteConnection conn, Type targetClass)
    {
        using (SqliteCommand cmd = new SqliteCommand(conn))
        {
            // 直接用类名 (targetClass.Name) 去查 english_name
            cmd.CommandText = "SELECT id, max_stack FROM Item WHERE english_name = @className";
            cmd.Parameters.AddWithValue("@className", targetClass.Name);//targetClass.Name为类名字符串

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    // 赋值给类里的静态变量
                    AssignStaticField(targetClass, "id", reader.GetInt32(0));
                    AssignStaticField(targetClass, "maxStack", reader.GetInt32(1));
                    
                    Debug.Log($"[自动加载] 识别类 {targetClass.Name} -> 匹配数据库成功");
                }
                else
                {
                    Debug.LogError($"[错误] 数据库中没有 english_name 为 '{targetClass.Name}' 的数据！");
                }
            }
        }
    }
    void AssignStaticField(Type type, string fieldName, object value)
    {
        // 寻找类中的静态变量
        //public为0001，static为1000,|后就是1010,相当于组合起来同时满足public和static的条件
        FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);//在public和static的变量找到fieldName变量,返回一个FieldInfo对象;
        if (field != null)
        {
            field.SetValue(null, value);
        }
        else
        {
            // 如果报错找不到字段，请检查 Wood/Axe 类里的变量名是否叫 id, maxStack, chinaName
            Debug.LogWarning($"在类 {type.Name} 中找不到静态变量: {fieldName}");
        }
    }
}