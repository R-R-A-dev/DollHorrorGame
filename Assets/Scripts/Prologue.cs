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


    private int currentIndex = 0; // ���݂̃y�[�W
    private bool canProceed = false; // �N���b�N�Ŏ��ɐi�߂邩
    [SerializeField] private float displayTime = 20f; // �\������

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
            // �e�L�X�g���X�V���ĕ\��
            prologueText.text = currentBook.Pages[currentIndex];
            nextText.SetActive(false);
            // �N���b�N���ꎞ�I�ɖ�����
            canProceed = false;
            yield return new WaitForSeconds(displayTime); // ��莞�ԑ҂�
            nextText.SetActive(false);
            nextText.SetActive(true);

            // �N���b�N�Ŏ��ɐi�߂�悤�ɂ���
            canProceed = true;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1)); // �N���b�N�҂�

            currentIndex++;
        }
        if(currentBook.Pages.Length == currentIndex)
        {
            prologueTextObj.SetActive(false);
            nextText.SetActive(false);
            yield return new WaitForSeconds(20);
            //�t�F�[�h�A�E�g
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
/*�e�L�X�g�𑗂�I�������ɑ���\�ɂȂ�܂ł̊Ԃ��ł���
*/