using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _title;

        [SerializeField]
        private TMP_Text _description;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private BuyButton _buyButton;
        
        [SerializeField] private Button _closeButton;

        private IProductPresenter _productPresenter;
        private readonly CompositeDisposable _disposable = new ();

        public void Show(IPresenter args)
        {
            if (args is not IProductPresenter productPresenter)
            {
                throw new ArgumentException("Invalid argument type");
            }

            _productPresenter = productPresenter;
            
            _title.text = _productPresenter.Title;
            _description.text = _productPresenter.Description;
            _icon.sprite = _productPresenter.Icon;
            
            _buyButton.SetPrice(_productPresenter.Price);
            _productPresenter.BuyCommand.BindTo(_buyButton.Button).AddTo(_disposable);
            //_buyButton.AddListener(OnBuyButtonClicked);
            _closeButton.onClick.AddListener(Hide);
            UpdateButtonState();
            gameObject.SetActive(true); 
        }

        private void UpdateButtonState()
        {
            ButtonState buttonState = _productPresenter.CanBuy.Value
                ? ButtonState.Available
                : ButtonState.Locked;
            _buyButton.SetState(buttonState);
        }

        private void OnBuyButtonClicked()
        {
            if (!_productPresenter.Buy())
            {
                // TODO: show error message
            }

            UpdateButtonState();
        }

        private void Hide()
        {
            gameObject.SetActive(false); 
            _buyButton.RemoveListener(OnBuyButtonClicked);
            _closeButton.onClick.RemoveListener(Hide);
            _disposable.Clear();
        }
    }
}
