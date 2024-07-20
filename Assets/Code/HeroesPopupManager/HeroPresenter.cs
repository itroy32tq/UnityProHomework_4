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

        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;
        private readonly ReactiveProperty<bool> _canLevelUp;
        private readonly ReactiveCollection<CharacterStat> _stats;
        private readonly CompositeDisposable _disposable = new();

        public IReadOnlyReactiveCollection<CharacterStat> Stats => _stats;
        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;

        

        public ReactiveCommand CanLevelUpCommand { get; }

        public HeroPresenter(HeroInfo heroInfo)
        {
            _heroInfo = heroInfo;

            Name = _heroInfo.Name;
            Description = _heroInfo.Description;
            Icon = _heroInfo.Icon;

            _currentLevel = new ReactiveProperty<int> (_heroInfo.CurrentLevel);
            _currentExperience = new ReactiveProperty<int> (_heroInfo.CurrentExperience);
            _canLevelUp = new ReactiveProperty<bool>(_currentExperience.Value == RequiredExperience);
            _stats = new ReactiveCollection<CharacterStat> (_heroInfo.Stats);

            CanLevelUpCommand = new ReactiveCommand(_canLevelUp);
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
            _canLevelUp.Value = _currentExperience.Value == RequiredExperience;
        }

        public void LevelUp()
        {
            _currentExperience.Value = 0;
            _canLevelUp.Value = _currentExperience.Value == RequiredExperience;
            _currentLevel.Value++;
        }

        public void AddStat(CharacterStat stat)
        {
            CharacterStat target = GetStat(stat.Name);

            if (target == null)
            { 
                _stats.Add(stat);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (CharacterStat stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            return null;
        }

        public CharacterStat[] GetStats()
        {
            throw new NotImplementedException();
        }

        public void RemoveStat(CharacterStat stat)
        {
            CharacterStat target = GetStat(stat.Name);

            if (target != null)
            {
                _stats.Remove(target);
            }
        }
    }
}
