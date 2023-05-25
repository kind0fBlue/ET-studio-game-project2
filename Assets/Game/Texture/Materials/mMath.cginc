#ifndef MMATH_CGINC
#define MMATH_CGINC

float Pow5(float3 x) {
    return x * x * x * x * x;
}

float3 Fresnel_schlick(float VoN, float3 rF0) {
    return rF0 + (1 - rF0) * Pow5(1 - VoN);
}

float D_GGX(float a2, float NoH) {
    float d = (NoH * a2 - NoH) * NoH + 1;
    return a2 / (3.14159 * d * d + 0.000001);
}

float D_GGXaniso(float ax, float ay, float NoH, float3 H, float3 X, float3 Y)
{
    float XoH = dot(X, H);
    float YoH = dot(Y, H);
    float d = XoH * XoH / (ax * ax) + YoH * YoH / (ay * ay) + NoH * NoH;
    return 1 / (3.14159 * ax * ay * d * d);
}

float warp(float x, float w) {
    return (x + w) / (1 + w);
}

#endif