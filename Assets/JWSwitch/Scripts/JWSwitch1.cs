using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JWSwitch1 : MonoBehaviour
{
    public bool isOn; // 스위치 true = On, false = Off
    [Range(0, 3)] public float moveDuration = 3.0f;  //스위치 이동 시간
    int switchLimit = 0;

    const float totalHandleMoveLength = 72.0f;
    float halfMoveLength = totalHandleMoveLength / 2;

    //Color
    public Color handleColor;
    public Color offBackgroundColor;
    public Color onBackgroundColor;

    Image handleImage;                  //스위치 핸들 이미지
    Image backgroundImage;              //스위치 배경 이미지
    RectTransform handleRectTransform;  //스위치 핸들

    //코루틴
    Coroutine moveHandleCoroutine;             //핸들 이동 코루틴
    Coroutine changeBackGroundColorCoroutine;  //배경색 변경 코루틴

    void Start()
    {
        //핸들 위치 초기화
        GameObject handleObject = transform.Find("Handle").gameObject;
        handleRectTransform = handleObject.GetComponent<RectTransform>();
        //핸들 이미지
        handleImage = handleObject.GetComponent<Image>();
        handleImage.color = handleColor;  
        //백그라운드 이미지
        backgroundImage = GetComponent<Image>();
        backgroundImage.color = onBackgroundColor;

        if(isOn)
        {
            handleRectTransform.anchoredPosition = new Vector2(halfMoveLength, 0); //Off 위치
        }
        else
        {
            handleRectTransform.anchoredPosition = new Vector2(-halfMoveLength, 0); //On 위치
        }
    }
    public void OnClickSwitch()
    {
        Vector2 fromPosition = handleRectTransform.anchoredPosition;  //시작위치
        Vector2 toPosition = (isOn) ? new Vector2(-halfMoveLength, 0) : new Vector2(halfMoveLength, 0);  //도착위치
        Vector2 distance = toPosition - fromPosition;  //이동거리

        float ratio = Mathf.Abs(distance.x) / totalHandleMoveLength;
        float duration = moveDuration * ratio;

        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition,toPosition,duration));
        //백그라운드 컬러 체인지 코루틴

        Color fromColor = backgroundImage.color;
        Color toColor = (isOn) ? onBackgroundColor : offBackgroundColor;

        if (changeBackGroundColorCoroutine != null)
        {
            StopCoroutine(changeBackGroundColorCoroutine);
            changeBackGroundColorCoroutine = null;
        }
        changeBackGroundColorCoroutine = StartCoroutine(changeBackGroundColor(fromColor,toColor,duration));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="formPosition">핸들의 시작 위치</param>
    /// <param name="toPosition">핸들의 목적지 위치</param>
    /// <param name="duration">핸들이 이동하는 시간</param>
    /// <returns>없음</returns>
    IEnumerator moveHandle(Vector2 fromPosition,Vector2 toPosition,float duration)
    {
        float currentTime = 0f;
        if (switchLimit == 0)
        {
            switchLimit += 1;
            while (currentTime < duration)
            {

                float t = currentTime / duration;
                Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
                handleRectTransform.anchoredPosition = newPosition;

                currentTime += Time.deltaTime;
                yield return null;
            }
            switchLimit -= 1;
            isOn = !isOn;
        }
    }
    //터치시 스위치의 배경 색상을 바꿔주는 동작
    /// <summary>
    /// 스위치 배경 함수
    /// </summary>
    /// <param name="fromColor">초기 색상</param>
    /// <param name="toColor">변경할 색상</param>
    /// <param name="duration">변경 시간</param>
    /// <returns></returns>
    IEnumerator changeBackGroundColor(Color fromColor,Color toColor,float duration)
    {
        float currentTime = 0f;
        while(currentTime < duration)
        {
            float t = currentTime / duration;
            Color newColor = Color.Lerp(fromColor, toColor, t);

            backgroundImage.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
