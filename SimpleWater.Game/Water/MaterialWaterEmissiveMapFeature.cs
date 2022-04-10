using SimplerWater.Water.Effects;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering.Materials;
using Stride.Rendering.Materials.ComputeColors;
using Stride.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWater.Water
{
    /// <summary>
    /// Extending the emissive map allows to easily override the final shading color
    /// </summary>
    [DataContract(nameof(MaterialWaterEmissiveMapFeature))]
    [Display("Water Emissive Map")]
    public class MaterialWaterEmissiveMapFeature : MaterialFeature, IMaterialEmissiveFeature
    {
        [DataMember(10), Display(name: "Texture", category: "Caustics")] public Texture Caustics { get; set; }

        [DataMember(20), Display(name: "Speed", category: "Caustics")] public float CausticsSpeed { get; set; } = 0.1f;
        [DataMember(21), Display(name: "Scale", category: "Caustics")] public float CausticsScale { get; set; } = 8;
        [DataMember(22), Display(name: "Strength", category: "Caustics")] public float CausticsStrength { get; set; } = 1;
        [DataMember(23), Display(name: "Split", category: "Caustics")] public float CausticsSplit { get; set; } = 0.0015f;

        [DataMember(30), Display(name: "Density", category: "Fog")] public float FogDensity { get; set; } = 0.1f;
        [DataMember(31), Display(name: "Color", category: "Fog")] public Color3 FogColor { get; set; } = new Color3(0, 0, 0);

        [DataMember(40), Display(name: "Strength", category: "Refraction")] public float RefractionStrength { get; set; } = 25;
        [DataMember(41), Display(category: "Refraction")] public Color3 TintColor { get; set; } = new Color3(0.6f, 0.63f, 0.79f);

        [DataMember(50), Display(name: "Offset", category: "Fresnel")] public float FresenlOffset { get; set; } = 0.05f;
        [DataMember(51), Display(name: "Strength", category: "Fresnel")] public float FresnelStrength { get; set; } = 0.55f;
        [DataMember(52), Display(name: "Power", category: "Fresnel")] public float FresnelPower { get; set; } = 14;

        [DataMember(60), Display(name: "Render Target", category: "Ripples")] public Texture RipplesRenderTarget { get; set; }

        public override void GenerateShader(MaterialGeneratorContext context)
        {
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.CausticsTexture, Caustics);

            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.CausticsSpeed, CausticsSpeed);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.CausticsScale, CausticsScale);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.CausticsStrength, CausticsStrength);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.CausticsSplit, CausticsSplit);

            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.FogDensity, FogDensity);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.FogColor, FogColor);

            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.RefractionStrength, RefractionStrength);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.TintColor, TintColor);

            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.FresenlOffset, FresenlOffset);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.FresnelStrength, FresnelStrength);
            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.FresnelPower, FresnelPower);

            context.MaterialPass.Parameters.Set(MaterialWaterSurfaceEmissiveShadingKeys.RipplesTexture, RipplesRenderTarget);

            var shaderBuilder = context.AddShading(this);
            shaderBuilder.ShaderSources.Add(new ShaderClassSource("MaterialWaterSurfaceEmissiveShading"));
        }

        public bool Equals(IMaterialShadingModelFeature other)
            => other is MaterialEmissiveMapFeature;
    }
}
