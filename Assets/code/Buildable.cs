using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable: MonoBehaviour
{
    public GameObject interactionUI_bt;
    private bool isPlayerInRange = false;
    public Animator animator;
    public float buildTime = 3f; // 建造所需时间
    public float height;
    private float currentheight;
    private float addHeightPerSecond;
    private Renderer rend;
    private Material roomat;
    // 当有物体进入触发区域


    private void OnTriggerEnter(Collider other)
    {

        // 检查进入的是不是玩家
        if (other.CompareTag("Player"))
        {
            interactionUI_bt.SetActive(true);
            isPlayerInRange = true;
        }
    }

    // 当物体离开触发区域
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
        rend = GetComponent<Renderer>();
        roomat = rend.material;
        height = rend.bounds.size.y / transform.localScale.y;//原始高度
        addHeightPerSecond = height / buildTime;
        if (interactionUI_bt == null)
        {
            interactionUI_bt = GameObject.Find("build_Button");
        }
    }

    // 在Update中检查玩家是否按下交互键
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (animator.GetBool("build_room"))
            {
                animator.SetBool("build_room", false);
            }
            else
            {
                animator.SetBool("build_room", true);
                
            }
        }
        if(animator.GetBool("build_room") && currentheight < height)
        {
            
                currentheight += addHeightPerSecond * Time.deltaTime;
                roomat.SetFloat("_BuildHeight", currentheight);

        }
    }
}
