using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TypeWriterText : MonoBehaviour
{
    public float delayBetweenCharacters = 0.1f;
    private float lastCharTime;
    private int charIndex;
    private TMP_Text textComponent;
    private string fullText;
    private bool isTyping = false;
    
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fullText = textComponent.text;
        textComponent.text = "";
    }
    
    public async UniTask StartTyping()
    {
        if (!isTyping)
        {
            isTyping = true;
            charIndex = 0;
            lastCharTime = Time.time;
            await TypeAsync();
        }
    }

    private async UniTask TypeAsync()
    {
        while (isTyping)
        {
            if (charIndex < fullText.Length)
            {
                if (Time.time - lastCharTime > delayBetweenCharacters)
                {
                    textComponent.text += fullText[charIndex];
                    charIndex++;
                    lastCharTime = Time.time;
                }

                await UniTask.DelayFrame(1);
            }
            else
            {
                isTyping = false;
            }
        }
       
    }
}
