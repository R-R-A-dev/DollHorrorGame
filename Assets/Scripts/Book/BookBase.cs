using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace book
{
    [CreateAssetMenu(fileName = "NewBook",menuName = "Books/BookData")]
    public class BookBase : ScriptableObject
    {
        [SerializeField] private string bookTitle;// 本のタイトル
        [TextArea(3, 10)]
        [SerializeField] private string[] pages;// ページごとの文章

        public string Title => bookTitle;
        public string[] Pages => pages;

        public string getPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < pages.Length)
            {
                return pages[pageIndex];
            }

            return "このページは存在しません";
        }

        public int TotalPages => pages.Length;
    }
}

