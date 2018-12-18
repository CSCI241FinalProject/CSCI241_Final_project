using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pausemenu;


    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) 
        {
            
            pausemenu.SetActive(!pausemenu.activeSelf);
        }
    }
}
