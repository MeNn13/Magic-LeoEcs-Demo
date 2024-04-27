﻿using Assets.Code.Scripts.View;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Geo.Defending
{
    internal class DefendingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<DefendingComponent, HealthComponent> _filter;
        private readonly EffectConfig _effectConfig;

        private DefendEffectData _effect => _effectConfig.DefendingData as DefendEffectData;

        private Color _healthFillColor;
        private Color _healthBackColor;
        private Color _defendColor;

        public void Init()
        {
            _healthFillColor = SetColorHex("#84353C");
            _healthBackColor = SetColorHex("#808080");
            _defendColor = SetColorHex("#64492E");
        }

        private Color SetColorHex(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out Color color);
            return color;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref DefendingComponent component = ref _filter.Get1(i);
                ref HealthComponent health = ref _filter.Get2(i);

                if (component.duration >= 0)
                {
                    component.duration -= Time.deltaTime;

                    ChangeHealthSlide(ref health, _effect.DefensePoints, component.defensePoints,
                        _healthBackColor, _defendColor);
                }
                else
                {
                    MeshRenderer meshRenderer = component.objTransform.GetComponent<MeshRenderer>();
                    meshRenderer.materials = component.previousMaterials;

                    ChangeHealthSlide(ref health, health.maxHealth, health.health,
                        _healthBackColor, _healthFillColor);

                    entity.Del<DefendingComponent>();
                }
            }
        }

        private void ChangeHealthSlide(ref HealthComponent health, float maxPoint, float point,
            Color background, Color fill)
        {
            health.ui.SetBackgroundColor(background);
            health.ui.SetFillColor(fill);
            health.ui.UpdateHealth(point / maxPoint);
        }
    }
}
