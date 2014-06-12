#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
void main(void) {
  vec4 color = texture2D(texture, vTexCoord);
  float y = dot(color.rgb, vec3(0.2126, 0.7152, 0.0722));
  gl_FragColor = vec4(mix(color.rgb, vec3(y), arg), color.a);
}