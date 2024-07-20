using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public TMP_Text Label { get; private set; }
        [field: SerializeField] public TMP_Text ValueLabel { get; private set; }
        [field: SerializeField] public Image Point { get; private set; }

        public Action<CharacterStat> OnValueChanged;

        public void SetValue(int value)
        { 
            Value = value;
            OnValueChanged?.Invoke(this);
        }
    }
}