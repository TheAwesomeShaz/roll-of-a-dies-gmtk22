using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject brokenMesh;

    // For Debug testing if the function actually works or not
    //private void Update()
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        BreakDeath();
    //    }
    //}

    public void BreakDeath()
    {
        Instantiate(brokenMesh,transform.position,Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
