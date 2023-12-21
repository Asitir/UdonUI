// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-2460-OUT,custl-5085-OUT,voffset-5615-OUT,tess-6643-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32734,y:33089,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32734,y:32952,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31858,y:32654,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31858,y:32782,prsc:2,pt:True;n:type:ShaderForge.SFN_HalfVector,id:9471,x:31858,y:32933,varname:node_9471,prsc:2;n:type:ShaderForge.SFN_Dot,id:7782,x:32070,y:32697,cmnt:Lambert,varname:node_7782,prsc:2,dt:1|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Dot,id:3269,x:32070,y:32871,cmnt:Blinn-Phong,varname:node_3269,prsc:2,dt:1|A-9684-OUT,B-9471-OUT;n:type:ShaderForge.SFN_Multiply,id:2746,x:32465,y:32866,cmnt:Specular Contribution,varname:node_2746,prsc:2|A-7782-OUT,B-4865-RGB;n:type:ShaderForge.SFN_Tex2d,id:851,x:31619,y:32145,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1941,x:32465,y:32693,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-544-OUT,B-7782-OUT;n:type:ShaderForge.SFN_Color,id:5927,x:32070,y:32534,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5927,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Exp,id:1700,x:32070,y:33054,varname:node_1700,prsc:2,et:1|IN-9978-OUT;n:type:ShaderForge.SFN_Slider,id:5328,x:31529,y:33056,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Power,id:5267,x:32268,y:32940,varname:node_5267,prsc:2|VAL-3269-OUT,EXP-1700-OUT;n:type:ShaderForge.SFN_Add,id:2159,x:32734,y:32812,cmnt:Combine,varname:node_2159,prsc:2|A-1941-OUT,B-2746-OUT;n:type:ShaderForge.SFN_Multiply,id:5085,x:32979,y:32952,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-2159-OUT,B-3406-RGB,C-8068-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:9978,x:31858,y:33056,varname:node_9978,prsc:2,a:1,b:11|IN-5328-OUT;n:type:ShaderForge.SFN_Color,id:4865,x:32268,y:33095,ptovrint:False,ptlb:Spec Color,ptin:_SpecColor,varname:node_4865,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32734,y:32646,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:32927,y:32598,cmnt:Ambient Light,varname:node_2460,prsc:2|A-544-OUT,B-7528-RGB;n:type:ShaderForge.SFN_Multiply,id:544,x:32268,y:32448,cmnt:Diffuse Color,varname:node_544,prsc:2|A-1627-OUT,B-5927-RGB;n:type:ShaderForge.SFN_Tex2d,id:9732,x:32400,y:33635,ptovrint:False,ptlb:Render,ptin:_Render,varname:node_9732,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7969,x:32400,y:33398,ptovrint:False,ptlb:Map,ptin:_Map,varname:node_7969,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Subtract,id:8446,x:32645,y:33492,varname:node_8446,prsc:2|A-7969-R,B-9732-R;n:type:ShaderForge.SFN_Append,id:4360,x:33093,y:33482,varname:node_4360,prsc:2|A-6312-OUT,B-4031-OUT,C-3431-OUT;n:type:ShaderForge.SFN_Vector1,id:6312,x:33093,y:33436,varname:node_6312,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3431,x:33117,y:33607,varname:node_3431,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:5890,x:32764,y:33359,ptovrint:False,ptlb:Tesse,ptin:_Tesse,varname:node_5890,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:5,max:10;n:type:ShaderForge.SFN_Max,id:4031,x:32892,y:33503,varname:node_4031,prsc:2|A-8446-OUT,B-8359-OUT;n:type:ShaderForge.SFN_Vector1,id:8359,x:32892,y:33618,varname:node_8359,prsc:2,v1:0;n:type:ShaderForge.SFN_Distance,id:2841,x:31509,y:33510,varname:node_2841,prsc:2|A-8978-UVOUT,B-1478-OUT;n:type:ShaderForge.SFN_TexCoord,id:8978,x:30971,y:33349,varname:node_8978,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:8779,x:31294,y:33692,varname:node_8779,prsc:2|A-7552-OUT,B-9398-OUT;n:type:ShaderForge.SFN_Slider,id:7552,x:30964,y:33669,ptovrint:False,ptlb:X,ptin:_X,varname:node_7552,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0,max:1.5;n:type:ShaderForge.SFN_Slider,id:9398,x:30961,y:33759,ptovrint:False,ptlb:Y,ptin:_Y,varname:node_9398,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0,max:1.5;n:type:ShaderForge.SFN_OneMinus,id:2837,x:31717,y:33510,varname:node_2837,prsc:2|IN-2841-OUT;n:type:ShaderForge.SFN_OneMinus,id:1478,x:31468,y:33708,varname:node_1478,prsc:2|IN-8779-OUT;n:type:ShaderForge.SFN_Add,id:6643,x:32892,y:33230,varname:node_6643,prsc:2|A-9245-OUT,B-5890-OUT;n:type:ShaderForge.SFN_Multiply,id:6881,x:32186,y:33335,varname:node_6881,prsc:2|A-4921-OUT,B-7607-OUT;n:type:ShaderForge.SFN_Vector1,id:7607,x:32174,y:33461,varname:node_7607,prsc:2,v1:10;n:type:ShaderForge.SFN_Max,id:9245,x:32568,y:33237,varname:node_9245,prsc:2|A-6881-OUT,B-5445-OUT;n:type:ShaderForge.SFN_Vector1,id:5445,x:32568,y:33356,varname:node_5445,prsc:2,v1:0;n:type:ShaderForge.SFN_RemapRange,id:4921,x:31908,y:33493,varname:node_4921,prsc:2,frmn:0,frmx:1,tomn:-2,tomx:1|IN-2837-OUT;n:type:ShaderForge.SFN_Tex2d,id:8223,x:31619,y:32327,ptovrint:False,ptlb:Diffuse2,ptin:_Diffuse2,varname:node_8223,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:1627,x:32044,y:32236,varname:node_1627,prsc:2|A-851-RGB,B-8223-RGB,T-6381-OUT;n:type:ShaderForge.SFN_Multiply,id:4198,x:31858,y:32473,varname:node_4198,prsc:2|A-9732-R,B-5437-OUT;n:type:ShaderForge.SFN_Vector1,id:5437,x:31858,y:32593,varname:node_5437,prsc:2,v1:20;n:type:ShaderForge.SFN_Clamp01,id:6381,x:31858,y:32363,varname:node_6381,prsc:2|IN-4198-OUT;n:type:ShaderForge.SFN_Slider,id:1746,x:33240,y:33539,ptovrint:False,ptlb:height,ptin:_height,varname:node_1746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:5615,x:33319,y:33361,varname:node_5615,prsc:2|A-4360-OUT,B-1746-OUT;proporder:851-8223-5927-5328-4865-7969-1746-9732-5890-7552-9398;pass:END;sub:END;*/

Shader "Shader Forge/mapDam" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Diffuse2 ("Diffuse2", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Range(0, 1)) = 0.5
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
        _Map ("Map", 2D) = "white" {}
        _height ("height", Range(1, 10)) = 1
        _Render ("Render", 2D) = "white" {}
        _Tesse ("Tesse", Range(1, 10)) = 5
        _X ("X", Range(-0.5, 1.5)) = 0
        _Y ("Y", Range(-0.5, 1.5)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Render; uniform float4 _Render_ST;
            uniform sampler2D _Map; uniform float4 _Map_ST;
            uniform sampler2D _Diffuse2; uniform float4 _Diffuse2_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesse)
                UNITY_DEFINE_INSTANCED_PROP( float, _X)
                UNITY_DEFINE_INSTANCED_PROP( float, _Y)
                UNITY_DEFINE_INSTANCED_PROP( float, _height)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 _Map_var = tex2Dlod(_Map,float4(TRANSFORM_TEX(o.uv0, _Map),0.0,0));
                float4 _Render_var = tex2Dlod(_Render,float4(TRANSFORM_TEX(o.uv0, _Render),0.0,0));
                float _height_var = UNITY_ACCESS_INSTANCED_PROP( Props, _height );
                v.vertex.xyz += (float3(0.0,max((_Map_var.r-_Render_var.r),0.0),0.0)*_height_var);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float _X_var = UNITY_ACCESS_INSTANCED_PROP( Props, _X );
                    float _Y_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Y );
                    float _Tesse_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesse );
                    return (max((((1.0 - distance(v.texcoord0,(1.0 - float2(_X_var,_Y_var))))*3.0+-2.0)*10.0),0.0)+_Tesse_var);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 _Diffuse2_var = tex2D(_Diffuse2,TRANSFORM_TEX(i.uv0, _Diffuse2));
                float4 _Render_var = tex2D(_Render,TRANSFORM_TEX(i.uv0, _Render));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 node_544 = (lerp(_Diffuse_var.rgb,_Diffuse2_var.rgb,saturate((_Render_var.r*20.0)))*_Color_var.rgb); // Diffuse Color
                float3 emissive = (node_544*UNITY_LIGHTMODEL_AMBIENT.rgb);
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float4 _SpecColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpecColor );
                float3 finalColor = emissive + (((node_544*node_7782)+(node_7782*_SpecColor_var.rgb))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Render; uniform float4 _Render_ST;
            uniform sampler2D _Map; uniform float4 _Map_ST;
            uniform sampler2D _Diffuse2; uniform float4 _Diffuse2_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesse)
                UNITY_DEFINE_INSTANCED_PROP( float, _X)
                UNITY_DEFINE_INSTANCED_PROP( float, _Y)
                UNITY_DEFINE_INSTANCED_PROP( float, _height)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 _Map_var = tex2Dlod(_Map,float4(TRANSFORM_TEX(o.uv0, _Map),0.0,0));
                float4 _Render_var = tex2Dlod(_Render,float4(TRANSFORM_TEX(o.uv0, _Render),0.0,0));
                float _height_var = UNITY_ACCESS_INSTANCED_PROP( Props, _height );
                v.vertex.xyz += (float3(0.0,max((_Map_var.r-_Render_var.r),0.0),0.0)*_height_var);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float _X_var = UNITY_ACCESS_INSTANCED_PROP( Props, _X );
                    float _Y_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Y );
                    float _Tesse_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesse );
                    return (max((((1.0 - distance(v.texcoord0,(1.0 - float2(_X_var,_Y_var))))*3.0+-2.0)*10.0),0.0)+_Tesse_var);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 _Diffuse2_var = tex2D(_Diffuse2,TRANSFORM_TEX(i.uv0, _Diffuse2));
                float4 _Render_var = tex2D(_Render,TRANSFORM_TEX(i.uv0, _Render));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 node_544 = (lerp(_Diffuse_var.rgb,_Diffuse2_var.rgb,saturate((_Render_var.r*20.0)))*_Color_var.rgb); // Diffuse Color
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float4 _SpecColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpecColor );
                float3 finalColor = (((node_544*node_7782)+(node_7782*_SpecColor_var.rgb))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _Render; uniform float4 _Render_ST;
            uniform sampler2D _Map; uniform float4 _Map_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesse)
                UNITY_DEFINE_INSTANCED_PROP( float, _X)
                UNITY_DEFINE_INSTANCED_PROP( float, _Y)
                UNITY_DEFINE_INSTANCED_PROP( float, _height)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float4 _Map_var = tex2Dlod(_Map,float4(TRANSFORM_TEX(o.uv0, _Map),0.0,0));
                float4 _Render_var = tex2Dlod(_Render,float4(TRANSFORM_TEX(o.uv0, _Render),0.0,0));
                float _height_var = UNITY_ACCESS_INSTANCED_PROP( Props, _height );
                v.vertex.xyz += (float3(0.0,max((_Map_var.r-_Render_var.r),0.0),0.0)*_height_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float _X_var = UNITY_ACCESS_INSTANCED_PROP( Props, _X );
                    float _Y_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Y );
                    float _Tesse_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesse );
                    return (max((((1.0 - distance(v.texcoord0,(1.0 - float2(_X_var,_Y_var))))*3.0+-2.0)*10.0),0.0)+_Tesse_var);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
