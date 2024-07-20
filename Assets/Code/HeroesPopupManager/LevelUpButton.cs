using Code;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.HomeworksCode
{
    public sealed class LevelUpButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space]
        [SerializeField] private Image _buttonBackground;

        [SerializeField] private Sprite _availableButtonSprite;

        [SerializeField] private Sprite _lockedButtonSprite;

        public Button Button => _button;

   
        public void SetState(ButtonState state)
        {

            switch (state)
            {
                case ButtonState.Available:
                    Button.interactable = true;
                    _buttonBackground.sprite = _availableButtonSprite;
                    break;
                case ButtonState.Locked:
                    Button.interactable = false;
                    _buttonBackground.sprite = _lockedButtonSprite;
                    break;
                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
