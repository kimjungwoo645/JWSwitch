using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwitchMaster : MonoBehaviour
{
    public JwSwitch bgmSwitch;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgmSwitch.switchIsOnAction = (Bgm) =>
        {
            if (Bgm == true)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        };
    }

    void Update()
    {
        
    }
}
