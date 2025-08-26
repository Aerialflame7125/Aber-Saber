"vs_4_0
					
#version 330
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_explicit_uniform_location : require
					
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
layout(std140) uniform VGlobals {
	vec4 unused_0_0[5];
	float _CapUVSize;
};
layout(std140) uniform UnityPerCamera {
	vec4 unused_1_0[4];
	vec3 _WorldSpaceCameraPos;
	vec4 unused_1_2[4];
};
layout(std140) uniform UnityPerDraw {
	mat4x4 unity_ObjectToWorld;
	mat4x4 unity_WorldToObject;
	vec4 unused_2_2[2];
};
layout(std140) uniform UnityPerFrame {
	vec4 unused_3_0[17];
	mat4x4 unity_MatrixVP;
	vec4 unused_3_2[2];
};
layout(std140) uniform Props {
	vec4 unused_4_0;
	vec4 _SizeParams;
};
in  vec4 in_POSITION0;
in  vec2 in_TEXCOORD0;
out vec2 vs_TEXCOORD1;
out vec3 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
vec2 u_xlat1;
vec4 u_xlat2;
float u_xlat3;
int u_xlati3;
bool u_xlatb4;
float u_xlat6;
float u_xlat7;
float u_xlat9;
void main()
{
	u_xlat0.xy = _WorldSpaceCameraPos.yy * unity_WorldToObject[1].xz;
	u_xlat0.xy = unity_WorldToObject[0].xz * _WorldSpaceCameraPos.xx + u_xlat0.xy;
	u_xlat0.xy = unity_WorldToObject[2].xz * _WorldSpaceCameraPos.zz + u_xlat0.xy;
	u_xlat0.xy = u_xlat0.xy + unity_WorldToObject[3].xz;
	u_xlat6 = dot(u_xlat0.xy, u_xlat0.xy);
	u_xlat6 = sqrt(u_xlat6);
	u_xlat0.xy = u_xlat0.xy / vec2(u_xlat6);
	u_xlat0.z = (-u_xlat0.y);
	u_xlat1.x = in_POSITION0.x * _SizeParams.x;
	u_xlat1.y = in_POSITION0.z;
	u_xlat6 = dot(u_xlat0.xz, u_xlat1.xy);
	u_xlat0.x = dot((-u_xlat0.yx), u_xlat1.xy);
	u_xlat3 = in_POSITION0.y + (-_SizeParams.z);
	u_xlat9 = in_POSITION0.y + -0.5;
	u_xlat1.x = in_TEXCOORD0.y + -0.5;
	u_xlatb4 = abs(u_xlat1.x)>=0.49000001;
	u_xlat7 = u_xlatb4 ? 1.0 : float(0.0);
	u_xlat9 = u_xlat9 * u_xlat7;
	u_xlat9 = u_xlat9 * _SizeParams.w;
	u_xlat3 = u_xlat3 * _SizeParams.y + u_xlat9;
	u_xlat2 = vec4(u_xlat3) * unity_ObjectToWorld[1];
	u_xlat2 = unity_ObjectToWorld[0] * u_xlat0.xxxx + u_xlat2;
	u_xlat0 = unity_ObjectToWorld[2] * vec4(u_xlat6) + u_xlat2;
	u_xlat2 = u_xlat0 + unity_ObjectToWorld[3];
	vs_TEXCOORD2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
	u_xlat0 = u_xlat2.yyyy * unity_MatrixVP[1];
	u_xlat0 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat0;
	u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat0;
	gl_Position = unity_MatrixVP[3] * u_xlat2.wwww + u_xlat0;
	u_xlati0 = int((0.0<u_xlat1.x) ? 0xFFFFFFFFu : uint(0));
	u_xlati3 = int((u_xlat1.x<0.0) ? 0xFFFFFFFFu : uint(0));
	u_xlati0 = (-u_xlati0) + u_xlati3;
	u_xlat0.x = float(u_xlati0);
	u_xlat3 = (-_CapUVSize) + 0.25;
	u_xlat0.x = u_xlat3 * u_xlat0.x;
	u_xlat0.x = (u_xlatb4) ? 0.0 : u_xlat0.x;
	vs_TEXCOORD1.y = u_xlat0.x + in_TEXCOORD0.y;
	vs_TEXCOORD1.x = in_TEXCOORD0.x;
	return;