using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Animator anim;
    public GameObject pickupFX;
    public GameObject idleFX;
    public GameObject knifeDestroyVFX;
    public Transform target;
    public bool isThrowingKnife; //if false then it is pickup
    public float throwSpeed =10f;
    public bool isSpinSet;
    Vector3 desiredTOPos;
    Vector3 desiredParticlePos;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        idleFX.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isThrowingKnife && other.gameObject.CompareTag("Player"))
        {
            GameController.instance.numberOfKnives++;
            Vector3 vfxPos = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
            Instantiate(pickupFX,vfxPos,Quaternion.identity);
            Destroy(gameObject);
            Destroy(this.gameObject);

        }

        if (isThrowingKnife && other.CompareTag("Enemy"))
        {
            desiredParticlePos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            GameObject newRedFX = Instantiate(knifeDestroyVFX,desiredParticlePos,Quaternion.identity);
            Destroy(newRedFX, 5f);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject,10f);   
        }

    }

    public void SetTarget(Transform newTarget)
    {
        isThrowingKnife = true;
        target = newTarget;
        
    }

    private void Update()
    {
        if (isThrowingKnife)
        {
            if (!isSpinSet) 
            {
                idleFX.SetActive(false);
                anim.SetBool("Spin",true);
                isSpinSet = true;
            }
            desiredTOPos = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
            

            transform.position = Vector3.MoveTowards(transform.position, desiredTOPos, throwSpeed * Time.deltaTime);

        }
    }

}
