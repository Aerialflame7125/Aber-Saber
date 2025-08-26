Shader "Custom/SteamVR_SphericalProjection" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_N ("N (normal of plane)", Vector) = (0,0,0,0)
		_Phi0 ("Phi0", Float) = 0
		_Phi1 ("Phi1", Float) = 1
		_Theta0 ("Theta0", Float) = 0
		_Theta1 ("Theta1", Float) = 1
		_UAxis ("uAxis", Vector) = (0,0,0,0)
		_VAxis ("vAxis", Vector) = (0,0,0,0)
		_UOrigin ("uOrigin", Vector) = (0,0,0,0)
		_VOrigin ("vOrigin", Vector) = (0,0,0,0)
		_UScale ("uScale", Float) = 1
		_VScale ("vScale", Float) = 1
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 29809
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _Phi0;
			float _Phi1;
			float _Theta0;
			float _Theta1;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _N;
			float4 _UAxis;
			float4 _VAxis;
			float4 _UOrigin;
			float4 _VOrigin;
			float _UScale;
			float _VScale;
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
                tmp0.xy = -float2(_Phi0.x, _Theta0.x) + float2(_Phi1.x, _Theta1.x);
                o.texcoord.xy = v.texcoord.xy * tmp0.xy + float2(_Phi0.x, _Theta0.x);
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0.xy = sin(inp.texcoord.yx);
                tmp1.xy = cos(inp.texcoord.yx);
                tmp2.x = tmp0.y * tmp0.x;
                tmp2.z = tmp0.x * tmp1.y;
                tmp2.y = tmp1.x;
                tmp0.x = dot(tmp2.xyz, _N.xyz);
                tmp0.xyz = tmp2.xyz / tmp0.xxx;
                tmp1.xyz = tmp0.xyz - _UOrigin.xyz;
                tmp0.xyz = tmp0.xyz - _VOrigin.xyz;
                tmp0.x = dot(tmp0.xyz, _VAxis.xyz);
                tmp0.y = tmp0.x * _VScale;
                tmp0.z = dot(tmp1.xyz, _UAxis.xyz);
                tmp0.x = tmp0.z * _UScale;
                o.sv_target = tex2D(_MainTex, tmp0.xy);
                return o;
			}
			ENDCG
		}
	}
}