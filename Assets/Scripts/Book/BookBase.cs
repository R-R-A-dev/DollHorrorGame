using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace book
{
    [CreateAssetMenu(fileName = "NewBook",menuName = "Books/BookData")]
    public class BookBase : ScriptableObject
    {
        [SerializeField] private string bookTitle;// �{�̃^�C�g��
        [TextArea(3, 10)]
        [SerializeField] private string[] pages;// �y�[�W���Ƃ̕���

        public string Title => bookTitle;
        public string[] Pages => pages;

        public string getPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < pages.Length)
            {
                return pages[pageIndex];
            }

            return "���̃y�[�W�͑��݂��܂���";
        }

        public int TotalPages => pages.Length;
    }
}

