using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoxPrefabScript : MonoBehaviour
{
    public TMP_Dropdown DropdownBox;


    public TextMeshProUGUI textBox;

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

    public void GoToNextPage()
    {
        (string Chapter, string Page) CurrentPage = TutorialBoxText.GetText(1);
        DropdownBox.value = TutorialBoxText.CurrentCoords.x;

        textBox.text = CurrentPage.Page;
    }

    public void GoToPrevPage()
    {
        (string Chapter, string Page) CurrentPage = TutorialBoxText.GetText(-1);
        DropdownBox.value = TutorialBoxText.CurrentCoords.x;

        textBox.text = CurrentPage.Page;
    }

    public void HandleChapterSelect(int i)
    {
        (string Chapter, string Page) CurrentPage = TutorialBoxText.GoToChapter(i);
        DropdownBox.value = TutorialBoxText.CurrentCoords.x;

        textBox.text = CurrentPage.Page;

    }
}