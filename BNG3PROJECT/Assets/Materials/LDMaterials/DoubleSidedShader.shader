Shader "Custom/DoubleSidedTransparent" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass {
            Cull Off  
            Blend SrcAlpha OneMinusSrcAlpha 
            ZWrite Off  
            SetTexture [_MainTex] { combine texture }
        }
    }
}
