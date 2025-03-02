using book;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Prologue : MonoBehaviour
{
    [SerializeField] private BookBase currentBook;
    [SerializeField] GameObject prologueTextObj;
    [SerializeField] GameObject prologueScreen;
    [SerializeField] GameObject prologueScreenFade;
    [SerializeField] private Text prologueText;
    [SerializeField] private GameObject nextText;
    public bool isPrologue = true;


    private int currentIndex = 0; // 現在のページ
    private bool canProceed = false; // クリックで次に進めるか
    [SerializeField] private float displayTime = 20f; // 表示時間

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartPrologue()
    {
        prologueScreen.SetActive(true);
        yield return new WaitForSeconds(10);
        prologueTextObj.SetActive(true);
        while (currentIndex < currentBook.Pages.Length)
        {
            // テキストを更新して表示
            prologueText.text = currentBook.Pages[currentIndex];
            nextText.SetActive(false);
            // クリックを一時的に無効化
            canProceed = false;
            yield return new WaitForSeconds(displayTime); // 一定時間待つ
            nextText.SetActive(false);
            nextText.SetActive(true);

            // クリックで次に進めるようにする
            canProceed = true;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1)); // クリック待ち

            currentIndex++;
        }
        if(currentBook.Pages.Length == currentIndex)
        {
            prologueTextObj.SetActive(false);
            nextText.SetActive(false);
            yield return new WaitForSeconds(20);
            //フェードアウト
            yield return new WaitForSeconds(5f);

            FirstPersonController.freezeTrg = false;
            yield return new WaitForSeconds(1f);
            prologueScreen.SetActive(false);
            prologueScreenFade.SetActive(true);

            yield return new WaitForSeconds(1);
            prologueScreenFade.SetActive(false);



        }

    }
}
/*テキストを送り終わった後に操作可能になるまでの間ができる
*/