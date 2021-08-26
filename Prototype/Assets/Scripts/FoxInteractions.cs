using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxInteractions : MonoBehaviour
{

    public GameObject Crystal1;
    public GameObject Crystal1UI;

    public GameObject Crystal2;
    public GameObject Crystal1U2;

    public GameObject Crystal3;
    public GameObject Crystal1U3;

    public GameObject Crystal4;
    public GameObject Crystal1U4;

    public GameObject Crystal5;
    public GameObject Crystal1U5;

    public GameObject Crystal6;
    public GameObject Crystal1U6;

    public Animator my_Animator;

    void Start()
    {

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag ("Crystal"))
        {
            Debug.Log("Crystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal());
            //Crystal1UI.SetActive(true);
            
        }

        if (other.gameObject.CompareTag("GreenCrystal"))
        {
            Debug.Log("GreenCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal2());
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("PinkCrystal"))
        {
            Debug.Log("PinkCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal3());
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("PurpleCrystal"))
        {
            Debug.Log("PurpleCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal4());
            //Crystal1UI.SetActive(true);

        }


    }
   
    
   

    IEnumerator DestroyCrystal()
    {
        yield return new WaitForSeconds(3);
        Crystal1.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1UI.SetActive(true);
    }
    IEnumerator DestroyCrystal2()
    {
        yield return new WaitForSeconds(3);
        Crystal2.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1U2.SetActive(true);
    }
    IEnumerator DestroyCrystal3()
    {
        yield return new WaitForSeconds(3);
        Crystal3.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1U3.SetActive(true);
    }
    IEnumerator DestroyCrystal4()
    {
        yield return new WaitForSeconds(3);
        Crystal4.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1U4.SetActive(true);
    }

}
