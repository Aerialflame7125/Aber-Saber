Shader "Custom/CustomParticles" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		[Space] [Toggle(_ENABLE_SOFT_PARTICLES)] _EnableSoftParticles ("Soft Particles", Float) = 0
		_SoftFactor ("Soft Factor", Range(0, 5)) = 0
		[Space] [Toggle(_ENABLE_CLOSE_TO_CAMERA_DISAPPEAR)] _EnableCloseToCameraDisappear ("Close to Camera Dissapear", Float) = 0
		_CloseToCameraOffset ("Close to Camera Offset", Float) = 0.5
		_CloseToCameraFactor ("Close to Camera Factor", Float) = 0.5
		[Space] [Toggle(_ENABLE_NOISE_DITHERING)] _EnableNoiseDithering ("Noise Dithering", Float) = 0
		[Space] [Toggle(_ENABLE_VIEW_ALIGN_DISAPPEAR)] _EnableViewAlignDisappear ("View Align Dissapear", Float) = 0
		[Space] [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrc ("Blend Src", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDst ("Blend Dst", Float) = 0
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend Zero Zero, Zero Zero
			ZWrite Off
			Cull Off
			GpuProgramID 30883
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float4 color : COLOR0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.sv_position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.color = v.color;
                o.texcoord.xy = v.texcoord.xy;
                o.texcoord1 = v.normal;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0 = tmp0 * inp.color;
                o.sv_target.xyz = tmp0.www * tmp0.xyz;
                o.sv_target.w = tmp0.w;
                return o;
			}
			ENDCG
		}
	}
}