using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoxPrefabScript : MonoBehaviour
{
    public TMP_Dropdown DropdownBox;


    public TextMeshProUGUI textBox;

    public int xCor = TutorialBoxText.CurrentCoords.x;
    public int yCor = TutorialBoxText.CurrentCoords.y;

    // Start is called before the first frame update
    void Start()
    {
        DropdownBox.ClearOptions();

        //Add dropdown options
        foreach ((string ChapterTitle, string[] Pages) x in TutorialBoxText.Chapters)
        {
            DropdownBox.AddOptions(new List<string> { x.ChapterTitle });
        }

        (string Chapter, string Page) CurrentPage = TutorialBoxText.GoToChapter(0);

        textBox.text = CurrentPage.Page;

    }

    private void Update()
    {
        xCor = TutorialBoxText.CurrentCoords.x;
        yCor = TutorialBoxText.CurrentCoords.y;
    }

    public void GoToNextPage()
    {
        (string Chapter, string Page) CurrentPage = TutorialBoxText.GetText(1);
        //Calls HandleChapterSelects below
        DropdownBox.value = TutorialBoxText.CurrentCoords.x;

        textBox.text = CurrentPage.Page;
    }

    public void GoToPrevPage()
    {
        (string Chapter, string Page) CurrentPage = TutorialBoxText.GetText(-1);
        //Calls HandleChapterSelects below
        DropdownBox.value = TutorialBoxText.CurrentCoords.x;

        textBox.text = CurrentPage.Page;
    }

    public void HandleChapterSelect(int i)
    {
        //Required to work with DropdownBox.value
        //Only go to chapter is the user clicked on the dropdown value
        //And not when user uses buttons to go to another chapter
        if (TutorialBoxText.CurrentCoords.x != i)
        {
            (string Chapter, string Page) CurrentPage = TutorialBoxText.GoToChapter(i);
            textBox.text = CurrentPage.Page;

        }
    }
}