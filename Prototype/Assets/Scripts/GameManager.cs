using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Burrow;
    public GameObject GameCamera;
    public GameObject FoxHappyUI;
    public GameObject FoxTiredUI;
    public GameObject FoxHidingUI;

    public GameObject BurrowLight;

    public GameObject blueGem;
    public GameObject GreenGem;
    public GameObject PinkGem;
    public GameObject PurpleGem;
    public GameObject YellowGem;
    public GameObject OrangeGem;

    public GameObject AL;

    private void Start()
    {
        blueGem.SetActive(false);
        GreenGem.SetActive(false);
        PinkGem.SetActive(false);
        PurpleGem.SetActive(false);
        YellowGem.SetActive(false);
        OrangeGem.SetActive(false);
    }
    public void ExitBurrow()
    {
        GameCamera.SetActive(true);
        Burrow.SetActive(false);
        AL.SetActive(false);
        FoxHidingUI.SetActive(false);
        FoxHappyUI.SetActive(true);
        BurrowLight.SetActive(true);
    }
}
