using Stride.Engine;
using Stride.Particles.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWater.Water
{
    /// <summary>
    /// Manages the ripples, very simple atm.
    /// Add this component to the entity that has the particle system
    /// Parent entity should be the "source" of the ripples, ie player entity
    /// </summary>
    public class WaterRipplesComponent : SyncScript
    {
        private ParticleSystemComponent _particleSystemComponent;
        public TransformComponent WaterPlane { get; set; }

        public override void Start()
        {
            base.Start();

            _particleSystemComponent = Entity.Get<ParticleSystemComponent>();
        }

        public override void Update()
        {
            if (_particleSystemComponent == null || WaterPlane == null)
                return;

            var waterHeight = WaterPlane.WorldMatrix.TranslationVector.Y;
            var parentPositionY = Entity.GetParent().Transform.WorldMatrix.TranslationVector.Y;
            var distanceToWater = (parentPositionY - waterHeight);

            // Move particle system to water surface
            Entity.Transform.Position.Y = waterHeight - parentPositionY;

            // Disable spawners unless entity at or below water surface
            if (distanceToWater <= 0.0f)
            {
                foreach (var emitter in _particleSystemComponent.ParticleSystem.Emitters)
                {
                    foreach (var spawner in emitter.Spawners)
                    {
                        spawner.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (var emitter in _particleSystemComponent.ParticleSystem.Emitters)
                {
                    foreach (var spawner in emitter.Spawners)
                    {
                        spawner.Enabled = false;
                    }
                }
            }
        }
    }
}
