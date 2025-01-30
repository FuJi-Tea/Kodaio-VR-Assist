Shader "Unlit/CropShader"
{
  Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CropRect ("Crop Rect", Vector) = (0, 0, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _CropRect;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // UV座標の左右を反転
                float2 flippedUV = float2(1.0 - i.uv.x, i.uv.y);

                // UV座標を切り抜き範囲で補間
                float2 croppedUV = lerp(_CropRect.xy, _CropRect.zw, flippedUV);
                return tex2D(_MainTex, croppedUV);
            }
            ENDCG
        }
    }
}
