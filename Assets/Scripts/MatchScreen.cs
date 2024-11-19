using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MatchScreen : MonoBehaviour
{
    public RectTransform contentPanel;
    public float transitionDuration = 0.5f;
    float currentX = 0;
    public TMP_Text PausePlayText;
    public bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        Time.timeScale = isPause?0:1;
    }

    public void PlayPause()
    {
        isPause = !isPause;
        if (isPause)
        {
            PausePlayText.text = "PLAY";
        }
        else
        {
            PausePlayText.text = "PAUSE";
        }
    }

    public void MoveLeft()
    {
        currentX += 893f;
        currentX = Mathf.Clamp(currentX, -2679f, 0);
        Vector3 currentPosition = contentPanel.anchoredPosition;
        LeanTween.value(gameObject, currentPosition.x, currentX, transitionDuration).setOnUpdate((float x) =>
        {
            currentPosition.x = x;
            contentPanel.anchoredPosition = currentPosition;
        }).setIgnoreTimeScale(true)
        .setUseEstimatedTime(true);
    }

    public void MoveRight()
    {
        currentX += -893f;
        currentX = Mathf.Clamp(currentX,-2679f,0);
        Vector3 currentPosition = contentPanel.anchoredPosition;
        LeanTween.value(gameObject, currentPosition.x, currentX, transitionDuration).setOnUpdate((float x) =>
        {
            currentPosition.x = x;
            contentPanel.anchoredPosition = currentPosition;
        })
            .setIgnoreTimeScale(true)
            .setUseEstimatedTime(true);
    }
}
