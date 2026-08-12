#region

using System;
using System.Collections.Generic;
using IbrahKit.UI.Generic;
using IbrahKit.UI.Modifier;
using IbrahKit.UI.Selectable;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Other
{
    public class UI_PageView : MonoBehaviour, IUIInit
    {
        [SerializeField] private UI_Selectable left;

        [SerializeField] private UI_Selectable right;

        [SerializeField] private UI_Modifier_Text_Localizer pageText;

        [SerializeField] private Transform pageContent;

        private int amountPerPage;

        private List<Transform> content = new();
        private int currentPageIndex;

        private int maxPageIndex;

        public Action<int> onPageChanged;

        public void OnMenuInitBottomUp()
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

        public void OnMenuInitTopDown()
        {
        }

        public void Config(List<Transform> content, int amountPerPage)
        {
            int i = 0;

            int maxPageIndex = 0;

            foreach (var item in this.content)
            {
                Destroy(gameObject);
            }

            content.Clear();

            this.content = content;

            while (i < content.Count)
            {
                i++;

                if (i == (amountPerPage * (maxPageIndex + 1))) maxPageIndex++;
            }

            currentPageIndex = 0;

            this.amountPerPage = amountPerPage;

            this.maxPageIndex = maxPageIndex;

            OnPageChanged();
        }

        private void GoLeft() => ChangePage(-1);

        private void GoRight() => ChangePage(1);

        private void ChangePage(int dir)
        {
            currentPageIndex += dir;

            currentPageIndex = Mathf.Clamp(currentPageIndex, 0, maxPageIndex);

            OnPageChanged();
        }

        private void OnPageChanged()
        {
            (int start, int end) = GetIndexRange();

            for (int i = 0; i < content.Count; i++)
            {
                bool inRange = i >= start && i < end;

                content[i].gameObject.SetActive(inRange);
            }

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

        private (int start, int end) GetIndexRange()
        {
            int startIndex = amountPerPage * currentPageIndex;

            int endIndex = Mathf.Clamp(startIndex + amountPerPage, startIndex, content.Count);

            return (startIndex, endIndex);
        }
    }
}