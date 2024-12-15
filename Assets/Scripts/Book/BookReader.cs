using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace book
{
    public class BookReader : MonoBehaviour
    {
        [SerializeField] private Text bookTitleText; // 本のタイトルを表示するテキスト
        [SerializeField] private Text bookContentText; // ページの内容を表示するテキスト
        [SerializeField] private BookBase currentBook; // 現在読んでいる本
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

