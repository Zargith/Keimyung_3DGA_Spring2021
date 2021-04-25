Shader "VertexColorFarmAnimals/VertexColorUnlit" {
Properties {
	_MainTex ("Texture", 2D) = "white" {}
	_Color ("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
}
 
Category {
	Tags { "Queue"="Geometry" }
	Lighting Off
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex

	}
 
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine primary 
			}
		}
	}
}
}