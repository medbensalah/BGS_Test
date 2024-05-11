using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private List<Button> filterButtons = new List<Button>();
    [SerializeField] private ItemsList itemsList;
    [SerializeField] private ShopDetails shopDetails;
    private List<Item> items = new List<Item>();
    
    private Color _btnColor= new Color(0.604f, 0.604f, 0.604f, 0.4117647f);
    private Color _btnActiveColor= new Color(1.0f, 0.98f, 0.9f, 0.6f);
    
    private ItemType _selectedFilter;
    public ItemType SelectedFilter {
        get => _selectedFilter;
        private set
        {
            _selectedFilter = value;
            FilterBtn.OnFilterBtnClicked?.Invoke(filterButtons.FirstOrDefault(btn => btn.name == SelectedFilter.ToString()));
         
            if (_selectedFilter == ItemType.All)
                items = _shopItems.shopItems.ToList();
            else
                items = _shopItems.shopItems.Where(item => item.ItemType == value).ToList();
        }
    }
    
    private Item _selectedItem;
    public Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            shopDetails.gameObject.SetActive(true);
            shopDetails.Init(_selectedItem);
        }
    }

    private CanvasGroup _canvasGroup;
    private float _fadeDuration = 0.2f;
    
    private ShopItems_so _shopItems;

    public static Action<Item> OnSelectItem;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(ShopItems_so shopItems)
    {
        _shopItems = shopItems;
        moneyText.text = $"Money: {PlayerData.Instance.Money}";
        PlayerData.Instance.OnMoneyChanged += OnMoneyChangedHandler;
        //initialize filter buttons
        OnSelectFilter((int)ItemType.All);
        
        StartCoroutine(Show());
        itemsList.Init(items);
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
        itemsList.Init(items);
    }

    private void OnSelectItemHandler(Item item)
    {
        SelectedItem = item;
    }
    
    private void OnMoneyChangedHandler()
    {
        moneyText.text = $"Money: {PlayerData.Instance.Money}";
    }
    
    public void OnClose()
    {
        OnSelectItem -= OnSelectItemHandler;
        PlayerData.Instance.OnMoneyChanged -= OnMoneyChangedHandler;
        StartCoroutine(Hide());
    }
}
