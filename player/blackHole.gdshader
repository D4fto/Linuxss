shader_type canvas_item;
uniform sampler2D ScreenTexture : hint_screen_texture;


void fragment() {
	vec2 distordedUV = SCREEN_UV + texture(TEXTURE, UV).a * 0.1;
	vec4 screenColor = texture(ScreenTexture, distordedUV);
	COLOR = screenColor;
}
