//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    InputAction shoot;
    //public float damage = 10f; 
    public float range = 100f;
    public float impactForce = 150;
    public int fireRate = 10;
    

    public ParticleSystem muzzleFlush;
    public GameObject impactEffect;

    public Camera cam;

    public Animator animator;
    //private float mextFireTime = 0f;
 
    void Start(){

        shoot = new InputAction("Shoot", binding:"<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        bool isShooting = shoot.ReadValue<float>() == 1;

        animator.SetBool("isShooting", isShooting);
        
        //&& Time.time >= nextFireTime
        if(isShooting ){
            //nextFireTime = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        AudioManager.instance.Play("Shoot");
        
        RaycastHit hitInfo;

        muzzleFlush.Play();

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range)){
           //Debug.Log(hitInfo.transform.name);
           if(hitInfo.rigidbody != null) {
               hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
           }

           Quaternion impactRotation = Quaternion.LookRotation(hitInfo.normal);

           GameObject impact = Instantiate(impactEffect, hitInfo.point, impactRotation);
           impact.transform.parent = hitInfo.transform;
           Destroy(impact, 5);
        }
    }
}
