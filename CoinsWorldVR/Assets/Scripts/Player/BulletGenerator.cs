using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    // public GameObject bullet;
    public static BulletGenerator sharedInstance;
    public GameObject bulletPrefab;
    private string bulletResource = "Bullet";

    private float bulletSpeed = 2500;

    void Start()
    {
        sharedInstance = this;
    }

    public void GenerateBullet()
    {
        //Shoot, the position and the rotation is of the gunbarret
        GameObject tempBullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce((tempRigidBodyBullet.transform.forward * -1) * bulletSpeed);
        Destroy(tempBullet, 5f);
    }

}
