Shader "ULTRAKILL/Master" {
	Properties {
		[MainProp] _Color ("Color", Vector) = (1,1,1,1)
		[MainProp] _MainTex ("Base (RGB)", 2D) = "white" {}
		[MainProp] _VertexWarpScale ("Vertex Warping Scalar", Range(0, 10)) = 1
		[MainProp] [Toggle] _Outline ("Assist Outline", Float) = 0
		[MainProp] [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull", Float) = 2
		[MainProp] [Toggle] _ZWrite ("ZWrite", Float) = 1
		[MainProp] [KeywordEnum(On, Off, Transparent)] _Fog ("Fog Mode", Float) = 0
		_Opacity ("Opacity", Range(0, 1)) = 1
		[Enum(Opaque,0,Cutout,1,Transparent,2,Advanced,3)] _BlendMode ("Blend Mode", Float) = 0
		[Toggle] _VertexLighting ("Vertex Lighting", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Source Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dest Blend", Float) = 0
		[Keyword(CAUSTICS)] [NoScaleOffset] _Cells ("Cells", 2D) = "white" {}
		[Keyword(CAUSTICS)] [NoScaleOffset] _Perlin ("Perlin", 2D) = "white" {}
		[Keyword(CAUSTICS)] _CausticColor ("Caustics Color", Vector) = (1,1,1,1)
		[Keyword(CAUSTICS)] _Scale ("World Scale", Float) = 1
		[Keyword(CAUSTICS)] _Falloff ("Mask Falloff", Float) = 1
		[Keyword(CAUSTICS)] _Speed ("Speed", Float) = 0.5
		[Keyword(CUSTOM_COLORS, REFLECTION)] _CubeTex ("Cube Texture", Cube) = "_Skybox" {}
		[Keyword(CUSTOM_COLORS, REFLECTION)] _ReflectionStrength ("Reflection Strength", Float) = 1
		[Keyword(CUSTOM_COLORS)] _IDTex ("ID Texture", 2D) = "white" {}
		[Keyword(CUSTOM_COLORS)] _CustomColor1 ("Custom Color 1", Vector) = (1,1,1,1)
		[Keyword(CUSTOM_COLORS)] _CustomColor2 ("Custom Color 2", Vector) = (1,1,1,1)
		[Keyword(CUSTOM_COLORS)] _CustomColor3 ("Custom Color 3", Vector) = (1,1,1,1)
		[Keyword(CUSTOM_COLORS)] _ReflectionFalloff ("Reflection Falloff", Float) = 1
		[Keyword(CUSTOM_LIGHTMAP)] _LightMapTex ("Light Map Texture", 2D) = "white" {}
		[Keyword(BLOOD_ABSORBER] [NoScaleOffset] _BloodTex ("BloodTex", 2D) = "black" {}
		[Keyword(BLOOD_FILLER)] _BloodNoiseTex ("Blood Noise Texture", 2D) = "white" {}
		[Keyword(CYBER_GRIND, EMISSIVE)] _EmissiveColor ("Emissive Color", Vector) = (1,1,1,1)
		[Keyword(CYBER_GRIND, EMISSIVE)] _EmissiveTex ("Emissive Texture", 2D) = "white" {}
		[Keyword(CYBER_GRIND, EMISSIVE)] _EmissiveIntensity ("Emissive Strength", Float) = 1
		[Keyword(CYBER_GRIND, EMISSIVE)] _EmissiveSaturation ("Emissive Saturation", Float) = 1
		[Keyword(CYBER_GRIND, EMISSIVE)] [Toggle] _UseAlbedoAsEmissive ("Use Albedo As Emissive", Float) = 1
		[Keyword(CYBER_GRIND, EMISSIVE)] [Toggle] _EmissiveReplaces ("Emissive Replaces Instead of Adding to Underlying Color", Float) = 0
		[Keyword(CYBER_GRIND)] _GradientFalloff ("Gradient Falloff", Float) = 1
		[Keyword(CYBER_GRIND)] _GradientScale ("Gradient Scale", Float) = 1
		[Keyword(CYBER_GRIND)] _GradientSpeed ("Gradient Speed", Float) = 1
		[Keyword(CYBER_GRIND)] [Toggle] _PCGamerMode ("PC Gamer Mode", Float) = 0
		[Keyword(CYBER_GRIND)] _RainbowDensity ("Rainbow Density", Float) = 1
		[Keyword(FRESNEL)] _FresnelColor ("Fresnel Color", Vector) = (1,1,1,1)
		[Keyword(FRESNEL)] _FresnelStrength ("Fresnel Strength", Float) = 1
		[Keyword(RAIN)] _RainColor ("Rain Color", Vector) = (0.2,0.2,0.3,1)
		[Keyword(RAIN)] _RainDrops ("Rain Drops", 2D) = "white" {}
		[Keyword(RAIN)] _RainTrails ("Rain Trails", 2D) = "white" {}
		[Keyword(RAIN)] _RainSpeed ("Rain Speed", Float) = 2
		[Keyword(RADIANCE)] _InflateStrength ("Inflate Strength", Float) = 1
		[Keyword(SCROLLING)] _ScrollSpeed ("Scroll Speed", Vector) = (0,0,0,0)
		[Keyword(VERTEX_DISPLACEMENT)] _VertexNoiseTex ("Vertex Noise Texture Lookup", 2D) = "black" {}
		[Keyword(VERTEX_DISPLACEMENT)] _VertexNoiseScale ("Vertex Distortion Density", Range(0, 10)) = 1
		[Keyword(VERTEX_DISPLACEMENT)] _VertexNoiseSpeed ("Vertex Distortion Speed", Range(0, 10)) = 1
		[Keyword(VERTEX_DISPLACEMENT)] _VertexNoiseAmplitude ("Vertex Distortion Amplitude", Range(0, 50)) = 1
		[Keyword(VERTEX_DISPLACEMENT)] _VertexScale ("Vertex Inflation Scale", Range(0, 1)) = 0
		[Keyword(VERTEX_DISPLACEMENT)] _FlowDirection ("Vertex Distortion Flow Direction (Normalized XYZ)", Vector) = (0,1,0,1)
		[Keyword(VERTEX_DISPLACEMENT)] [Toggle] _ReverseFlow ("Reverse Flow", Float) = 0
		[Keyword(VERTEX_DISPLACEMENT)] [Toggle] _LocalOffset ("Use Local Space Offset", Float) = 0
		[Keyword(VERTEX_DISPLACEMENT)] [Toggle] _RecalculateNormals ("Recalculate Normals", Float) = 0
		[Keyword(VERTEX_DISPLACEMENT)] _NormalOffsetScale ("Normal Offset Scale", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "ULTRAShaderEditor"
}