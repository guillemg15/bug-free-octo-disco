using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f; //rango de disparo
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShoot = true;
    
    private void OnEnable() 
    {
        canShoot = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot == true)
        {
           StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0){
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit this thing" + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
}
