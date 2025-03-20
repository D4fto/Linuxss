shader_type canvas_item;

void fragment() {
    vec4 texture_color = texture(TEXTURE, UV);
	if(texture_color.a > 0.0){
    	texture_color.rgba = vec4(1.0, 1.0, 1.0, 1.0);
	}
    COLOR = texture_color;
}