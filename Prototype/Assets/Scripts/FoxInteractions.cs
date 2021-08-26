using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxInteractions : MonoBehaviour
{

    public GameObject Crystal1;
    public GameObject Crystal1UI;
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
        
       
    }
   
    
   

    IEnumerator DestroyCrystal()
    {
        yield return new WaitForSeconds(3);
        Crystal1.SetActive(false);
        my_Animator.SetBool("isCrystal", false);
    }

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
