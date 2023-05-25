Shader "Unlit/NewUnlitShader"
{
	Properties{
		_MainTex("MainTex",2D) = "white"{}
		_Color("Color Tint",Color) = (1,1,1,1)
		_Magnitude("Magnitude",Float) = 0.1
		_Frequency("Frequency",Float) = 0.5
		_Speed("Speed", Float) = 0.01
	}
 
		SubShader{
		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True"}
 
		Pass{
		Tags{"LightMode" = "ForwardBase"}
 
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
 
		CGPROGRAM
 
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
        
		sampler2D _MainTex;
		float4 _MainTex_ST;
		fixed4 _Color;
		float _Magnitude;
		float _Frequency;
		float _Speed;
 
		struct vI {
			float4 vertex:POSITION;
			float2 texcoord:TEXCOORD0;
		};
 
		struct vO {
			float4 pos:SV_POSITION;
			float2 uv:TEXCOORD0;
		};
 
		vO vert(vI v) {
			vO o;
			float4 offset = float4(0, 0, 0, 0);
			offset.y = sin(_Frequency *_Time.y+ v.vertex.x+ v.vertex.y+ v.vertex.z)*_Magnitude;
			o.pos = UnityObjectToClipPos(v.vertex + offset);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uv += float2(0, _Time.y*_Speed);
			return o;
		}
 
		fixed4 frag(vO i) :SV_Target{
			fixed4 c = tex2D(_MainTex,i.uv);
			c.rgb *= _Color.rgb;
			return c;
		}
 
		ENDCG
		 }
		 }
	FallBack "Transparent/VertexLit"
}