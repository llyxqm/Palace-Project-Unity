using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSlot_button : MonoBehaviour
{
    public GameObject drop;
    public GameObject equip;

    public void Slotshow()
    {
        bool isActive = drop.activeSelf;
        drop.SetActive(!isActive);
        equip.SetActive(!isActive);
    }
}
