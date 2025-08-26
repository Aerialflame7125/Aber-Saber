Shader "Custom/UIChromaKey" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		_KeyColor ("Keying Color", Vector) = (0,1,1,0)
		_Threshold ("Threshold", Float) = 0.1
		_Smoothness ("Smoothness", Float) = 0.1
		[HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil ("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] [HideInInspector] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Name "DEFAULT"
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, Zero OneMinusSrcAlpha
			ColorMask 0 -1
			ZWrite Off
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 45645
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 color : COLOR0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _Color;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _MainTex_TexelSize;
			float4 _TextureSampleAdd;
			float4 _ClipRect;
			float4 _KeyColor;
			float _Threshold;
			float _Smoothness;
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
                o.color = v.color * _Color;
                o.texcoord.xy = v.texcoord.xy;
                o.texcoord1 = v.vertex;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                tmp0.xyz = -_MainTex_TexelSize.xyx;
                tmp0.xy = tmp0.xy + inp.texcoord.xy;
                tmp1 = tex2D(_MainTex, tmp0.xy);
                tmp0.x = dot(tmp1.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.xy = tmp1.zx - tmp0.xx;
                tmp1.x = dot(_KeyColor.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.xy = _KeyColor.zx - tmp1.xx;
                tmp1.xy = tmp1.xy * float2(0.5647, 0.7132);
                tmp0.xy = tmp0.xy * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.x = dot(tmp0.xy, tmp0.xy);
                tmp2 = tex2D(_MainTex, inp.texcoord.xy);
                tmp2 = tmp2 + _TextureSampleAdd;
                tmp0.y = dot(tmp2.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.zw = tmp2.zx - tmp0.yy;
                tmp1.zw = tmp1.zw * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp1.xy, tmp1.xy);
                tmp0.xy = sqrt(tmp0.xy);
                tmp0.x = tmp0.x + tmp0.y;
                tmp3 = _MainTex_TexelSize * float4(-1.0, 1.0, 1.0, -1.0) + inp.texcoord.xyxy;
                tmp4 = tex2D(_MainTex, tmp3.xy);
                tmp3 = tex2D(_MainTex, tmp3.zw);
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.zw = tmp4.zx - tmp0.yy;
                tmp1.zw = tmp1.zw * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp1.xy, tmp1.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.zw = tmp3.zx - tmp0.yy;
                tmp1.zw = tmp1.zw * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp1.xy, tmp1.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp1.zw = inp.texcoord.xy + _MainTex_TexelSize.xy;
                tmp3 = tex2D(_MainTex, tmp1.zw);
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp1.zw = tmp3.zx - tmp0.yy;
                tmp1.zw = tmp1.zw * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp1.xy, tmp1.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp0.w = 0.0;
                tmp0.yz = tmp0.zw + inp.texcoord.xy;
                tmp3 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp3.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp3.x = _MainTex_TexelSize.x;
                tmp3.yz = float2(0.0, 0.0);
                tmp0.yz = tmp3.xy + inp.texcoord.xy;
                tmp4 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp4.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp4.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp3.w = -_MainTex_TexelSize.y;
                tmp0.yz = tmp3.zw + inp.texcoord.xy;
                tmp3 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp3.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp3.x = 0.0;
                tmp3.y = _MainTex_TexelSize.y;
                tmp0.yz = tmp3.xy + inp.texcoord.xy;
                tmp3 = tex2D(_MainTex, tmp0.yz);
                tmp0.y = dot(tmp3.xyz, float3(0.2989, 0.5866, 0.1145));
                tmp0.yz = tmp3.zx - tmp0.yy;
                tmp0.yz = tmp0.yz * float2(0.5647, 0.7132) + -tmp1.xy;
                tmp0.y = dot(tmp0.xy, tmp0.xy);
                tmp0.y = sqrt(tmp0.y);
                tmp0.x = tmp0.y + tmp0.x;
                tmp0.x = tmp0.x * 0.1111111 + -_Threshold;
                tmp0.y = 1.0 / _Smoothness;
                tmp0.x = saturate(tmp0.y * tmp0.x);
                tmp0.y = tmp0.x * -2.0 + 3.0;
                tmp0.x = tmp0.x * tmp0.x;
                tmp0.x = tmp0.x * tmp0.y;
                tmp0.yz = inp.texcoord1.xy >= _ClipRect.xy;
                tmp0.yz = tmp0.yz ? 1.0 : 0.0;
                tmp1.xy = _ClipRect.zw >= inp.texcoord1.xy;
                tmp1.xy = tmp1.xy ? 1.0 : 0.0;
                tmp0.yz = tmp0.yz * tmp1.xy;
                tmp0.y = tmp0.z * tmp0.y;
                tmp0.y = tmp0.y * tmp2.w;
                tmp2.w = tmp0.x * tmp0.y;
                o.sv_target = tmp2 * inp.color;
                return o;
			}
			ENDCG
		}
	}
}