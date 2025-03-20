shader_type canvas_item;

uniform sampler2D noise : repeat_enable;
uniform vec2 scroll = vec2(0.05, 0.05);
uniform sampler2D noise2 : repeat_enable;
uniform vec2 scroll2 = vec2(0.05, 0.05);
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;


void fragment() {
    vec4 pixelcolor = texture(SCREEN_TEXTURE, SCREEN_UV);
    float noise_col = texture(noise, UV + scroll * TIME).r * texture(noise2, UV + scroll2 * TIME).r;
	if(pixelcolor.rgb==vec3(1.0)){
		COLOR = texture(noise, UV * vec2(noise_col));

	}


}