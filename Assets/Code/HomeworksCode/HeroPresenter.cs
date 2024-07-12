using Lessons.Architecture.PM;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    public sealed class HeroPresenter : IHeroPresenter
    {
        private readonly HeroInfo _heroInfo;
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public int RequiredExperience => 100 * (CurrentLevel.Value + 1);
        public bool CanMakeLevelUp => _currentExperience.Value == RequiredExperience;

        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;
        private readonly ReactiveProperty<bool> _canLevelUp; 

        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;

        private readonly CompositeDisposable _disposable = new();

        public ReactiveCommand CanLevelUpCommand { get; }

        public HeroPresenter(HeroInfo heroInfo)
        {
            _heroInfo = heroInfo;

            Name = heroInfo.Name;
            Description = heroInfo.Description;
            Icon = heroInfo.Icon;

            _currentLevel = new ReactiveProperty<int> (heroInfo.CurrentLevel);
            _currentExperience = new ReactiveProperty<int> (heroInfo.CurrentExperience);
            _canLevelUp = new ReactiveProperty<bool>(CanMakeLevelUp);

            CanLevelUpCommand = new ReactiveCommand(CanLevelUp);
            CanLevelUpCommand.Subscribe(OnLevelUpCommand).AddTo(_disposable);
        }

        

        private void OnLevelUpCommand(Unit obj)
        {
            LevelUp();
        }

        public void AddExperience(int range)
        {
            int xp = Math.Min(CurrentExperience.Value + range, RequiredExperience);
            _currentExperience.Value = xp;
        }

        public void LevelUp()
        {
            _currentExperience.Value = 0;
            _currentLevel.Value++;
        }

        public void AddStat(CharacterStat stat)
        {
            throw new NotImplementedException();
        }

        public CharacterStat GetStat(string name)
        {
            throw new NotImplementedException();
        }

        public CharacterStat[] GetStats()
        {
            throw new NotImplementedException();
        }

        public void RemoveStat(CharacterStat stat)
        {
            throw new NotImplementedException();
        }
    }
}
