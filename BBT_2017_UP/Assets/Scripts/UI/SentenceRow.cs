using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceRow : MonoBehaviour
{
    public WordText wordPrefab;

    internal RectTransform rt;

    private HorizontalLayoutGroup layoutGroup;

    private Stack<WordText> textStack = new Stack<WordText>();

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        wordPrefab.gameObject.SetActive(false);
    }

    public WordText AddText(string word)
    {
        WordText wordClone = Instantiate(wordPrefab, wordPrefab.transform.parent);
        wordClone.gameObject.SetActive(true);
        wordClone.text.text = word;
        textStack.Push(wordClone);
        return wordClone;
    }

    public void PopText()
    {
        Destroy(textStack.Pop().gameObject);
    }
}
