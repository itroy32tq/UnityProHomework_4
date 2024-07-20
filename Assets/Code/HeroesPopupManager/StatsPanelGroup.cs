using Lessons.Architecture.PM;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    public sealed class StatsPanelGroup : MonoBehaviour
    {
        [SerializeField] private List<CharacterStat> _stats;

        private IHeroPresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        internal void SetAndSubscribeValue(IHeroPresenter heroPresenter)
        {
            _heroPresenter = heroPresenter;

            _heroPresenter.Stats.ObserveAdd().Subscribe(OnStatAdd).AddTo(_disposable);
            _heroPresenter.Stats.ObserveRemove().Subscribe(OnStatRemove).AddTo(_disposable);

            SetStatus(_heroPresenter.Stats);
        }

       
        private void OnStatValueChanged(CharacterStat stat)
        {
            for (int i = 0; i < _stats.Count; i++)
            {
                CharacterStat item = _stats[i];
                if (item.Name == stat.Name)
                {
                    item.ValueLabel.text = stat.Value.ToString();
                }
            }
        }

        private void OnStatRemove(CollectionRemoveEvent<CharacterStat> removeEvent)
        {
            CharacterStat removeItem = removeEvent.Value;
            for (int i = 0; i < _stats.Count; i++)
            {
                CharacterStat item = _stats[i];
                if (item.Name == removeItem.Name)
                {
                    item.Label.gameObject.SetActive(false);
                    item.Point.gameObject.SetActive(false);
                    item.ValueLabel.gameObject.SetActive(false);
                    removeItem.OnValueChanged -= OnStatValueChanged;
                }
            }
        }

        private void OnStatAdd(CollectionAddEvent<CharacterStat> addEvent)
        {
            CharacterStat addItem = addEvent.Value;
            for (int i = 0; i < _stats.Count; i++)
            {
                CharacterStat item = _stats[i];
                if (item.Name == addItem.Name)
                {
                    item.Label.gameObject.SetActive(true);
                    item.Point.gameObject.SetActive(true);
                    item.ValueLabel.gameObject.SetActive(true);
                    item.ValueLabel.text = addItem.Value.ToString();

                    addItem.OnValueChanged += OnStatValueChanged;
                }
            }
            
        }

        private void SetStatus(IReadOnlyReactiveCollection<CharacterStat> list)
        {
            List<CharacterStat> activeStats = list.Join(_stats,
                x => x.Name,
                y => y.Name,
                (x, y) =>
                {
                    y.Label.gameObject.SetActive(true);
                    y.Point.gameObject.SetActive(true);
                    y.ValueLabel.gameObject.SetActive(true);
                    y.ValueLabel.text = x.Value.ToString();

                    x.OnValueChanged += OnStatValueChanged;
                    return y;

                }).ToList();
        }
    }
}
