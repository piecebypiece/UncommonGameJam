using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스템프 하나의 정보를 표시
public class UIStemp : MonoBehaviour
{
    [SerializeField] private StempInfo info;
    [SerializeField] private Text text;
    [SerializeField] private Image image;
    //[SerializeField] private Text showText;

    public void SetInfo(StempInfo info)
    {
        this.info = info;
        text.enabled = info.kind == StempInfo.Kind.Word;
        //image.enabled = info.kind == StempInfo.Kind.Emoji;

        switch (info.kind)
        {
            case StempInfo.Kind.Word:
                SetText();
                break;
            case StempInfo.Kind.Emoji: 
                SetEmoji(); 
                break;
        }
    }

    void SetText()
    {
        text.text = info.key;
        //showText.text = text.text;
    }

    void SetEmoji()
    {
        //image.sprite = ???
    }
}
