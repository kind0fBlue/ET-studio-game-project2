Shader "Unlit/MetalButton"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Matallic("Matalic", Range(0,1)) = 0.5
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _baseColor("Base Color", Color) = (1,1,1)

        _nonMatrF0("NonMatal RF0", Color) = (0.3,0.3,0.3)
        _MatrF0("Matal RF0", Color) = (0.95,0.95,0.95)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }


        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM


        #pragma multi_compile_fwdbase	

        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "AutoLight.cginc"
        #include "mMath.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float4 uv : TEXCOORD0;
            float3 worldNormal : TEXCOORD1;
            float3 worldTangent : TEXCOORD2;
            float3 worldBinormal : TEXCOORD3;
            float3 worldPos : TEXCOORD4;

            SHADOW_COORDS(5)
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _Smoothness;
        float _Matallic;
        half3 _baseColor;
        half3  _nonMatrF0;
        half3  _MatrF0;
        v2f vert(appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);

            o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
            o.uv.zw = o.uv.xy;

            o.worldNormal = UnityObjectToWorldNormal(v.normal); // z
            o.worldTangent = UnityObjectToWorldDir(v.tangent.xyz);  // x
            o.worldBinormal = cross(o.worldNormal, o.worldTangent) * v.tangent.w;   // y

            o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

            TRANSFER_SHADOW(o);

            return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
            half3 bump = half3(0,0,1);
            half3 worldNormal = normalize(bump.x * i.worldTangent + bump.y * i.worldBinormal + bump.z * i.worldNormal);
            half3 tangent = normalize(i.worldTangent);
            half3 binormal = normalize(i.worldBinormal);

            half3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
            half3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
            half3 halfDir = normalize(worldLightDir + viewDir);

            float VoN = dot(worldNormal, viewDir);
            float NoL = dot(worldNormal, worldLightDir);
            float NoH = dot(worldNormal, halfDir);


            float roughness = 1 - _Smoothness;
            roughness *= (1.7 - 0.7 * roughness) * 0.98;
            roughness += 0.02;


            float mata = 0.98 * _Matallic;

            fixed4 col = tex2D(_MainTex, i.uv);
            col.rgb *= _baseColor.rgb;

            half3 rF0 = lerp(_nonMatrF0, _MatrF0,mata);
            half3 fresnel = Fresnel_schlick(VoN, rF0);
            half3 fresnelOutline = fresnel;

            UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

            atten = warp(atten, 0.3);

            half3 lambertDiff = saturate(NoL) * col.rgb;
            half3 diffuse = (1 - fresnel) * lambertDiff * atten;

            float Ndf =  D_GGX(roughness * roughness, NoH);
            float3 unMatSpecular = fresnel * Ndf * _LightColor0.rgb * atten;
            float3 MatSpecular = unMatSpecular * col.rgb;


            float3 reflectDir = reflect(-viewDir, worldNormal);
            float4 envSample = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, reflectDir, roughness * UNITY_SPECCUBE_LOD_STEPS);
            float3 Ambient = fresnel * envSample * col.rgb;

            half3 outColor = diffuse + lerp(unMatSpecular, MatSpecular, mata) + Ambient;

            return half4(outColor,1.0);

        }
        ENDCG
    }
    }
        FallBack "Specular" 
}
