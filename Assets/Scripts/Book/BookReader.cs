using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace book
{
    public class BookReader : MonoBehaviour
    {
        [SerializeField] private Text bookTitleText; // �{�̃^�C�g����\������e�L�X�g
        [SerializeField] private Text bookContentText; // �y�[�W�̓��e��\������e�L�X�g
        [SerializeField] private BookBase currentBook; // ���ݓǂ�ł���{
        private int currentPage = 0;


        public void LoadBook(BookBase book)
        {
            currentBook = book;
            currentPage = 0;
            DisplayPage();
        }

        public void NextPage()
        {
            if (currentPage < currentBook.TotalPages - 1)
            {
                currentPage++;
                DisplayPage();
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 0)
            {
                currentPage--;
                DisplayPage();
            }
        }

        public void StopRead()
        {

        }

        private void DisplayPage()
        {
            bookTitleText.text = currentBook.Title;
            bookContentText.text = currentBook.Pages[currentPage];
        }
    }
}

