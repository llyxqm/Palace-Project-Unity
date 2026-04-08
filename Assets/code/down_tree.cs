using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_tree : MonoBehaviour
{
    //private Quaternion woodqtpy = Quaternion.Euler(-90, 0, 0);//木头和树的旋转差
    //倾倒方向
    private Vector3 fall_direction;
    //木头生成位置偏移
    private Vector3 wood_offset;
    public GameObject wood;
    public float barycenter_py = -0.2f;
    public Collider head;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, barycenter_py, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contactCount > 0)//防高速碰撞时丢失碰撞信息
        {
            if (collision.GetContact(0).thisCollider == head && collision.collider.CompareTag("ground"))
            {
                wood_offset = transform.rotation * Vector3.up * 1.2f;//木头生成位置偏移
                fall_direction = Vector3.Cross(collision.GetContact(0).normal, Vector3.up).normalized;
                Instantiate(wood, transform.position + wood_offset, transform.rotation);
                Instantiate(wood, transform.position, transform.rotation);
                Instantiate(wood, transform.position - wood_offset, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
