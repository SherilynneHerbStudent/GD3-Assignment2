using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public bool hasBlue = false;
    public bool hasGreen = false;
    public bool hasPink = false;
    public bool hasPurple = false;
    public bool hasOrange = false;
    public bool hasYellow= false;

    public GameObject PrototypeWin;
    public GameManager GM;

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
            hasBlue = true;
            GM.blueGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("GreenCrystal"))
        {
            Debug.Log("GreenCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal2());
            hasGreen = true;
            GM.GreenGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("PinkCrystal"))
        {
            Debug.Log("PinkCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal3());
            hasPink = true;
            GM.PinkGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("PurpleCrystal"))
        {
            Debug.Log("PurpleCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal4());
            hasPurple = true;
            GM.PurpleGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("OrangeCrystal"))
        {
            Debug.Log("OrangeCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal5());
            hasOrange = true;
            GM.OrangeGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("YellowCrystal"))
        {
            Debug.Log("YellowCrystal");
            my_Animator.SetBool("isCrystal", true);
            StartCoroutine(DestroyCrystal6());
            hasYellow = true;
            GM.YellowGem.SetActive(true);
            //Crystal1UI.SetActive(true);

        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("dead");
            SceneManager.LoadScene("LoseGame");
        }


    }

    private void Update()
    {
        if (hasBlue == true && hasGreen == true && hasOrange == true && hasYellow == true && hasPurple == true && hasPink == true)
        {
            StartCoroutine(PrototypeEnd());
            
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

    IEnumerator DestroyCrystal5()
    {
        yield return new WaitForSeconds(3);
        Crystal5.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1U5.SetActive(true);
    }

    IEnumerator DestroyCrystal6()
    {
        yield return new WaitForSeconds(3);
        Crystal6.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
        Crystal1U6.SetActive(true);
    }

    IEnumerator PrototypeEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");

    }
}
