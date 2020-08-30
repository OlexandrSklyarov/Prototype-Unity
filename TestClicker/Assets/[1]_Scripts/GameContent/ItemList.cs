using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SA.TestGame
{
    public class ItemList : MonoBehaviour
    {
        #region Var

        [SerializeField] RectTransform content;
        [SerializeField] Button addButton;
        [SerializeField] Button removeButton;
        [SerializeField] GameObject itemPrefab;

        float minContentSize;

        #endregion


        #region Init

        void Awake()
        {
            minContentSize = itemPrefab.GetComponent<RectTransform>().sizeDelta.y;

            InitButton();           
        }


        void InitButton()
        {
            addButton.onClick.AddListener(() => AddItem());
            removeButton.onClick.AddListener(() => RemoveLastItem());
        }

        #endregion


        #region Content


        void AddItem()
        {
            if(itemPrefab)
            {
                var item = Instantiate(itemPrefab, content);
                item.GetComponentInChildren<TextMeshProUGUI>().text = $"Item {content.childCount}";

                UpdateContentSize();
            }
        }


        void RemoveLastItem()
        {
            if (content.childCount > 0)
            {
                var last = content.GetChild(content.childCount-1);
                Destroy(last.gameObject);

                UpdateContentSize();
            }
        }


        void UpdateContentSize()
        {
            var size = content.sizeDelta;

            size.y = (content.childCount > 1) ?             
                size.y = (content.childCount-1) * minContentSize:
                size.y = minContentSize;

            content.sizeDelta = size;
        }

        #endregion
    }
}