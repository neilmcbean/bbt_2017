﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceRowContainer : MonoBehaviour
{
    public int nRows;
    public SentenceRow sentenceRowPrefab;

    internal RectTransform rt;
    private VerticalLayoutGroup layoutGroup;

    private List<SentenceRow> rows = new List<SentenceRow>();
    private List<WordText> texts = new List<WordText>();
    private int rowIndex;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        sentenceRowPrefab.gameObject.SetActive(false);
        CreateRow();

    }

    public void AddText(WordGroupObject wordGroup)
    {
        
        string[] words = wordGroup.text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            SentenceRow currentRow = rows[rowIndex];
            WordText newText = currentRow.AddText(word);

            //To enforce the layout to rebuild, which makes the horizontal layoutgroup resize
            LayoutRebuilder.ForceRebuildLayoutImmediate(rt);

            float myWidth = rt.rect.width - layoutGroup.padding.horizontal;
            //We add a text, check if the width is fitting. 

            if (currentRow.rt.rect.width + newText.rt.rect.width >= myWidth)
            {
                CreateRow();
                currentRow.PopText();
                rowIndex++;
                //Safety check in case of really long words
                if (newText.rt.rect.width < myWidth)
                {
                    //go a word back
                    i -= 1;
                }
                else
                {
                    Debug.LogWarningFormat("The word {0} does not fit in the SentenceRowContainer! It will be skipped.", word);
                }

            }
            else
            {
                //We set the wordGroup of each textfield so we can highlight them alltogether

                newText.wordGroup = wordGroup;
                texts.Add(newText);
            }

        }
    }

    void CreateRow()
    {
        SentenceRow sentenceRowClone = Instantiate(sentenceRowPrefab, sentenceRowPrefab.transform.parent);
        sentenceRowClone.gameObject.SetActive(true);
        rows.Add(sentenceRowClone);
    }

    public void HighlightWordGroup(WordGroupObject wordGroup)
    {
        foreach (WordText text in texts)
        {
            if (text.wordGroup == wordGroup)
            {
                text.text.color = Color.blue;
            }
            else
            {
                text.text.color = Color.white;
            }
        }
    }

    public void OnTextButton(WordText clickedText)
    {
        foreach (WordText text in texts)
        {
            if (text.wordGroup == clickedText.wordGroup)
            {
                text.text.color = Color.red;
            }
            else
            {
                text.text.color = Color.white;
            }
        }
    }
}
