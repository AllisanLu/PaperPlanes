Shader "Unlit/ProceduralWind"
{
    Properties
    {
        _PixelsPerUnit ("Pixels Per Unit", float) = 8
        _LineWidth ("Line Width", float) = 1
        _WindSpeed ("Wind Speed", float) = 1
        _FrontColor ("Front Color", Color) = (1,1,1,1)
        _BackColor ("Back Color", Color) = (.8,.8,.8,1)
        _SpeedFactor ("Wind Speed Factor", float) = 1.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        // Cull front 
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _PixelsPerUnit;
            float4 _FrontColor;
            float4 _BackColor;
            float _LineWidth;
            float _WindSpeed;
            float _SpeedFactor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float inverseLerp(float a, float b, float v) {
                return (v-a)/(b-a);
            }

            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                // need full precision, otherwise half overflows when p > 1
                float x = float(34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }

            float windNoise(float2 uv) {
                
                // Pixelate UV's
                uv = (floor(uv * _PixelsPerUnit) + .5) / _PixelsPerUnit;

                float lowerBound = clamp(inverseLerp(.6,0,uv.y), 0, 1);
                float upperBound = clamp(inverseLerp(_LineWidth-.6,_LineWidth,uv.y), 0, 1);
                float bounds = max(lowerBound,upperBound);
                float boundNoise;
                Unity_GradientNoise_float(uv, 4, boundNoise);
                boundNoise/=3;
                bounds -= boundNoise;

                float2 windUv = uv;
                windUv.x /= 3;
                float gradNoise;
                Unity_GradientNoise_float(windUv, 6, gradNoise);

                float boundedWindNoise = max(bounds, gradNoise);

                return step(boundedWindNoise, .3);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv *= float2(1,_LineWidth);
                float2 backUV = uv + float2(_Time.y * -_WindSpeed, 0);
                float2 frontUV = uv + float2(_Time.y * -_WindSpeed * _SpeedFactor, 0);

                float frontSample = windNoise(frontUV);
                float backSample = windNoise(backUV);

                float alpha = max(frontSample, backSample);

                float3 color = backSample > frontSample ? _BackColor.xyz : _FrontColor.xyz;

                return fixed4(color,alpha);
            }
            ENDCG
        }
    }
}
