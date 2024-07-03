using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductView : MonoBehaviour
    {
        [SerializeField] private BuyButton _button;
    
        [SerializeField] private TMP_Text _titleText;

        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private Image _iconImage;
        private IProductPresenter _productPresenter;
        private readonly CompositeDisposable _disposable = new ();

        public void Initialized(IProductPresenter productPresenter)
        {
            _productPresenter = productPresenter;
            _titleText.text = productPresenter.Title;
            _descriptionText.text = productPresenter.Description;
            _iconImage.sprite = productPresenter.Icon;
            _button.SetPrice(productPresenter.Price);
    
            _disposable.Clear();
            _productPresenter.BuyCommand.BindTo(_button.Button).AddTo(_disposable);
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            BuyButtonState buttonState = _productPresenter.CanBuy.Value
                ? BuyButtonState.Available
                : BuyButtonState.Locked;
            _button.SetState(buttonState);
        }
    }
}
