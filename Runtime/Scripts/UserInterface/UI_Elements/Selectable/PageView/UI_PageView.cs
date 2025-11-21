using System;
using UnityEngine;

namespace IbrahKit
{
    public class UI_PageView : MonoBehaviour, IMenuUpdate
    {
        private int currentPageIndex;

        private int amountPerPage;

        private int maxPageIndex;

        [SerializeField] private UI_Selectable left;

        [SerializeField] private UI_Selectable right;

        [SerializeField] private UI_Interactive_Extension_Localization pageText;

        [SerializeField] private Transform pageContent;

        public Action<int> onPageChanged;

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInit()
        {
            throw new System.NotImplementedException();
        }

        public void OnMenuInit()
        {
            if (left != null)
            {
                onPageChanged += UpdateLeftButton;
                left.GetStateController().GetOnPressSuccess().AddListener(GoLeft);
            }

            if (right != null)
            {
                onPageChanged += UpdateRightButton;
                right.GetStateController().GetOnPressSuccess().AddListener(GoRight);
            }

            if (pageText != null)
            {
                onPageChanged += UpdatePageText;
            }
        }

        public void Config(int amount, int amountPerPage)
        {
            int i = 0;

            int maxPageIndex = 0;

            while (i < amount)
            {
                i++;

                if (i == (amountPerPage * (maxPageIndex + 1))) maxPageIndex++;
            }

            currentPageIndex = 0;

            this.amountPerPage = amountPerPage;

            this.maxPageIndex = maxPageIndex;

            OnPageChanged();
        }

        public void GoLeft() => ChangePage(-1);

        public void GoRight() => ChangePage(1);

        private void ChangePage(int dir)
        {
            foreach (Transform child in pageContent)
            {
                Destroy(child.gameObject);
            }

            currentPageIndex += dir;

            currentPageIndex = Mathf.Clamp(currentPageIndex, 0, maxPageIndex);

            OnPageChanged();
        }

        public void OnPageChanged()
        {
            onPageChanged?.Invoke(currentPageIndex);
        }

        private void UpdatePageText(int page)
        {
            pageText.SetParam((page + 1 + "/" + (maxPageIndex + 1)).ToString());
        }

        private void UpdateLeftButton(int page)
        {
            left.gameObject.SetActive(page != 0);
        }

        private void UpdateRightButton(int page)
        {
            right.gameObject.SetActive(page != maxPageIndex);
        }

        public Transform GetPageContent() => pageContent;

        public (int, int) GetIndexRange(int maxIndex)
        {
            int startIndex = amountPerPage * currentPageIndex;

            int endIndex = Mathf.Clamp(startIndex + amountPerPage, startIndex, maxIndex);

            return (startIndex, endIndex);
        }

        public int GetCurrentPage() => currentPageIndex;
    }
}