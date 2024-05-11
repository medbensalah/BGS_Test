using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenuScreen : MonoBehaviour
{
    [SerializeField] private List<Button> filterButtons = new List<Button>();
    [SerializeField] private ItemsList itemsList;
    [SerializeField] private EquipMenuDetails equipMenuDetails;
    private List<Item> items = new List<Item>();

    private ItemType _selectedFilter;

    public ItemType SelectedFilter
    {
        get => _selectedFilter;
        private set
        {
            _selectedFilter = value;
            FilterBtn.OnFilterBtnClicked?.Invoke(filterButtons.FirstOrDefault(btn => btn.name == SelectedFilter.ToString()));

            if (_selectedFilter == ItemType.All)
            {
                items.Clear();
                foreach (var keyValuePair in PlayerData.Instance.Inventory)
                {
                    for (int i = 0; i < keyValuePair.Value; i++)
                    {
                        items.Add(keyValuePair.Key);
                    }
                }
            }
            else
            {
                items.Clear();
                foreach (var keyValuePair in PlayerData.Instance.Inventory.Where(item => item.Key.ItemType == value))
                {
                    for (int i = 0; i < keyValuePair.Value; i++)
                    {
                        items.Add(keyValuePair.Key);
                    }
                }
            }
        }
    }

    private Item _selectedItem;

    public Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            equipMenuDetails.gameObject.SetActive(true);
            equipMenuDetails.Init(_selectedItem);
        }
    }

    private CanvasGroup _canvasGroup;
    private float _fadeDuration = 0.2f;

    public static Action<Item> OnSelectItem;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init()
    {
        SelectedFilter = ItemType.All;
        StartCoroutine(Show());
        itemsList.Init(items, false);
        OnSelectItem += OnSelectItemHandler;
    }

    private IEnumerator Show()
    {
        GameManager.Instance.DisbleInput();
        float elapsedTime = 0.0f;
        while (elapsedTime < _fadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / _fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (elapsedTime >= _fadeDuration)
        {
            _canvasGroup.alpha = 1;
        }
    }

    private IEnumerator Hide()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _fadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / _fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (elapsedTime >= _fadeDuration)
        {
            _canvasGroup.alpha = 0;
            GameManager.Instance.EnableInput();
            Destroy(gameObject);
        }
    }

    public void OnSelectFilter(int filterIndex)
    {
        SelectedFilter = (ItemType)filterIndex;
        itemsList.Init(items, false);
    }

    private void OnSelectItemHandler(Item item)
    {
        SelectedItem = item;
    }

    public void OnClose()
    {
        OnSelectItem -= OnSelectItemHandler;
        StartCoroutine(Hide());
    }
}