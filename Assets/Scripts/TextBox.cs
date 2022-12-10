using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float delayChar;
    [SerializeField] private float delayMessage;
    [TextArea]
    [SerializeField] private string[] message = new String[]{};

    private Coroutine coroutine;
    
    private int activeIndex;
    private bool messageEnd;
    private bool skipped;
    
    private void Start()
    {
        ActivateTextBox();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (skipped) 
                NextText();
            else
                SkipText();
        }
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < message[activeIndex].Length; i++)
        {
            textComponent.text = message[activeIndex].Substring(0, i);
            yield return new WaitForSeconds(delayChar);
        }

        yield return new WaitForSeconds(delayMessage);

        NextText();
    }

    private void SkipText()
    {
        StopCoroutine(coroutine);
        skipped = true;
        textComponent.text = message[activeIndex];
    }

    private void NextText()
    {
        if (activeIndex + 1 != message.Length)
        {
            activeIndex++;
            skipped = false;
            coroutine = StartCoroutine(TypeText());
        }
        else
        {
            CloseTextBox();
        }
    }

    private void CloseTextBox()
    {
        gameObject.SetActive(false);
    }

    public void ActivateTextBox()
    {
        gameObject.SetActive(true);
        coroutine = StartCoroutine(TypeText());
    }
}
