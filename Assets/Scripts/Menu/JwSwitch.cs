using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JwSwitch : MonoBehaviour
{
    bool Bgm; //true = On , false = Off
    int OnOffLimit = 0;
    public float Speed;
    public float time;

    public Action<bool> switchIsOnAction;
    public RectTransform handleRectTransform;
    public Image image;

    private void Start()
    {
        Bgm = true;
        handleRectTransform.anchoredPosition = new Vector2(-25, 0);
        image.color = Color.blue;
    }

    //스위치 On Off 동작

    public void OnClickBgmSwitch()
    {
        if(Bgm == true) //off함수
        {
            StartCoroutine(OffSwitch());
        }
        else if(Bgm == false) //on함수
        {
            StartCoroutine(OnSwitch()); 
        }
        switchIsOnAction(Bgm);

        IEnumerator OnSwitch()
        {
            if (OnOffLimit == 1)
            {
                OnOffLimit -= 1;
                float move = 25.0f;
                while (move > -25)
                {
                    move -= Speed;
                    handleRectTransform.anchoredPosition = new Vector2(move, 0);
                    yield return new WaitForSeconds(time);
                }
                image.color = Color.blue;
                Bgm = true;
            }
        }
        IEnumerator OffSwitch()
        {
            if (OnOffLimit == 0)
            {
                OnOffLimit += 1;
                float move = -25.0f;
                while (move < 25)
                {
                    move += Speed;
                    handleRectTransform.anchoredPosition = new Vector2(move, 0);
                    yield return new WaitForSeconds(time);
                }
                image.color = Color.black;
                Bgm = false;
            }
        }
    }
}
