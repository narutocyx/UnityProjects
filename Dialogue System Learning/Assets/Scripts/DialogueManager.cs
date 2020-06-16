using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;
    public Text dialogueText;
    public Text nameText;
    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;
    [SerializeField] private bool isScrolling;
    public float textSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueBox.activeInHierarchy)
        {
            if (!isScrolling)
            {
                currentLine++;
                if (currentLine < dialogueLines.Length)
                {
                    CheckName();
                    //dialogueText.text = dialogueLines[currentLine];
                    StartCoroutine(ScrollingText());
                }
                else
                {
                    dialogueBox.SetActive(false);
                    FindObjectOfType<PlayerMovement>().canMove = true;
                }
            }     
        }
    }

    public void ShowDialogue(string[] _newLines, bool _hasName)
    {
        dialogueLines = _newLines;
        currentLine = 0;
        CheckName();

        nameText.gameObject.SetActive(_hasName);

        //dialogueText.text = dialogueLines[currentLine];
        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);

        FindObjectOfType<PlayerMovement>().canMove = false;
    }

    public void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    public IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
}
