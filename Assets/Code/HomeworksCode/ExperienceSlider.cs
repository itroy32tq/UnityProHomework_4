using Lessons.Architecture.PM;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Assets.Code.HomeworksCode
{
    public sealed class ExperienceSlider : MonoBehaviour
    {
        [SerializeField] private Slider _experienceSlider;
        private IHeroPresenter _heroPresenter;

        public void SetAndSubscribeValue(IHeroPresenter heroPresenter)
        {
            _heroPresenter = heroPresenter;

            _experienceSlider.value = (float)_heroPresenter.CurrentExperience.Value / _heroPresenter.RequiredExperience;
            _heroPresenter.CurrentExperience.Subscribe(OnExperienceChanged);
        }

        private void OnExperienceChanged(int value)
        {
            _experienceSlider.value = (float)_heroPresenter.CurrentExperience.Value / _heroPresenter.RequiredExperience;
        }
    }
}
