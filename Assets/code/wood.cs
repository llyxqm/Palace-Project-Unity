using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class wood : MonoBehaviour
{
    //防止一次性捡取多个木头
    private static bool isAnyWoodBeingPicked = false;
    // Start is called before the first frame update
    public GameObject interactionUI_bt;
    private bool isPlayerInRange = false;
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
