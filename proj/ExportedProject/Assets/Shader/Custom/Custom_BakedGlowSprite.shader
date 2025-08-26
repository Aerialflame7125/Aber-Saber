Shader "Custom/BakedGlowSprite" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_GlowOffset ("Glow Offset", Float) = 0
		[Space] [Toggle(_ENABLE_FOG)] _EnableFog ("Enable Fog", Float) = 1
		_FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
		[Space] [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactor ("Blend Src Factor", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactor ("Blend Dst Factor", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactorA ("Blend Src Factor A", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactorA ("Blend Dst Factor A", Float) = 10
		[Space] [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend Zero Zero, Zero Zero
			ZWrite Off
			Cull Off
			GpuProgramID 45620
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 color : COLOR0;
				float2 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float _GlowOffset;
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
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.color = v.color;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = 1.0 - _GlowOffset;
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0.y = saturate(tmp1.w * inp.color.w + -_GlowOffset);
                tmp0.x = tmp0.y / tmp0.x;
                tmp0.yzw = -tmp1.xyz * inp.color.xyz + float3(1.0, 1.0, 1.0);
                tmp1 = tmp1 * inp.color;
                o.sv_target.xyz = tmp0.xxx * tmp0.yzw + tmp1.xyz;
                o.sv_target.w = tmp1.w;
                return o;
			}
			ENDCG
		}
	}
}