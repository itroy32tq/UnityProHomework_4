using Assets.Code.HomeworksCode;
using Code;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class HeroPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;

        [SerializeField] private LevelUpButton _levelUpButton;
        [SerializeField] private ExperienceSlider _experienceSlider;
        [SerializeField] private StatsPanelGroup _statsPanelGroup;

        [SerializeField] private Button _closeButton;

        private IHeroPresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        public void Show(IPresenter args)
        {
            if (args is not IHeroPresenter heroPresenter)
            {
                throw new ArgumentException("Invalid argument type");
            }

            _heroPresenter = heroPresenter;

            _name.text = _heroPresenter.Name;
            _description.text = _heroPresenter.Description;
            _icon.sprite = _heroPresenter.Icon;

            _experienceSlider.SetAndSubscribeValue(_heroPresenter);
            _statsPanelGroup.SetAndSubscribeValue(_heroPresenter);

            _heroPresenter.CurrentLevel.Subscribe(OnLevelChanged);
            
            _heroPresenter.CanLevelUpCommand.BindTo(_levelUpButton.Button).AddTo(_disposable);
            _heroPresenter.CanLevelUp.Subscribe(UpdateButtonState).AddTo(_disposable);
            UpdateButtonState(_heroPresenter.CanLevelUp.Value);

            gameObject.SetActive(true);
        }

        private void OnLevelChanged(int levelValue)
        {
            _level.text = levelValue.ToString();
        }

        private void UpdateButtonState(bool canUpdate)
        {
            ButtonState buttonState = canUpdate
                ? ButtonState.Available
                : ButtonState.Locked;
            _levelUpButton.SetState(buttonState);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
            _disposable.Clear();
        }
    }
}
