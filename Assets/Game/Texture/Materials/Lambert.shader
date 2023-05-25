

Shader "Unlit/WaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed4 _Diffuse;

            struct vI
            {
                float4 vertex : POSITION;
                float4 normal:NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct vO
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal:TEXCOORD1;
            };
            vO vert (vI v)
            {
               vO o;
               o.vertex = UnityObjectToClipPos(v.vertex);      
               o.normal=UnityObjectToWorldNormal(v.normal);    
               o.uv=TRANSFORM_TEX(v.uv,_MainTex);                      
               return o;
            }
            fixed4 frag (vO i) : SV_Target
            {

                fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT;  
                fixed3 tex_color=tex2D(_MainTex,i.uv);
                fixed3 worldLight=normalize(_WorldSpaceLightPos0.xyz); 
                float intensity=max(0,dot(worldLight,i.normal))*1+0.5;
                fixed3 diffuse=tex_color*intensity;
                return fixed4(diffuse+ambient,1);
            }
            ENDCG
        }
    }



}
	

