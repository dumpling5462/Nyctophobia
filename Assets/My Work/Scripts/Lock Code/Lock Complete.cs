using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockComplete : MonoBehaviour
{
    [SerializeField] GameObject[] locks;
    [SerializeField] AudioSource winsound;
    [SerializeField] float soundlength;
    int lockCount;
    int currentnumberoflocks;

    private void Awake()
    {
        lockCount = locks.Length;
    }
    void Update()
    {
        foreach (GameObject lockobject in locks)
        {
            if (lockobject.GetComponent<LockScript>().GemPlaced)
            {
                currentnumberoflocks += 1;
            }
        }
        if (currentnumberoflocks == lockCount)
        {
            wingame();
        }
        else
        {
            currentnumberoflocks = 0;
        }
    }

    void wingame()
    {
        winsound.Play();
        Invoke("loadwinscreen",soundlength);
    }
    private void loadwinscreen()
    {
        SceneManager.LoadScene("WinScene");
    }
}
