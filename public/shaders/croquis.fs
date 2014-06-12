#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
uniform vec2 size;
void main(void) {
  float dx = 1.0 / size.x;
  float dy = 1.0 / size.y;
  float c  = -1.0 / 8.0; 

  float r = ((texture2D(texture, vTexCoord + vec2(-dx, -dy)).r
          +   texture2D(texture, vTexCoord + vec2(0.0, -dy)).r
          +   texture2D(texture, vTexCoord + vec2( dx, -dy)).r
          +   texture2D(texture, vTexCoord + vec2(-dx, 0.0)).r
          +   texture2D(texture, vTexCoord + vec2( dx, 0.0)).r
          +   texture2D(texture, vTexCoord + vec2(-dx,  dy)).r
          +   texture2D(texture, vTexCoord + vec2(0.0,  dy)).r
          +   texture2D(texture, vTexCoord + vec2( dx,  dy)).r) * c
          +   texture2D(texture, vTexCoord).r) * -2.0;

  float g = ((texture2D(texture, vTexCoord + vec2(-dx, -dy)).g
          +   texture2D(texture, vTexCoord + vec2(0.0, -dy)).g
          +   texture2D(texture, vTexCoord + vec2( dx, -dy)).g
          +   texture2D(texture, vTexCoord + vec2(-dx, 0.0)).g
          +   texture2D(texture, vTexCoord + vec2( dx, 0.0)).g
          +   texture2D(texture, vTexCoord + vec2(-dx,  dy)).g
          +   texture2D(texture, vTexCoord + vec2(0.0,  dy)).g
          +   texture2D(texture, vTexCoord + vec2( dx,  dy)).g) * c
          +   texture2D(texture, vTexCoord).g) * -24.0;

  float b = ((texture2D(texture, vTexCoord + vec2(-dx, -dy)).b
          +   texture2D(texture, vTexCoord + vec2(0.0, -dy)).b
          +   texture2D(texture, vTexCoord + vec2( dx, -dy)).b
          +   texture2D(texture, vTexCoord + vec2(-dx, 0.0)).b
          +   texture2D(texture, vTexCoord + vec2( dx, 0.0)).b
          +   texture2D(texture, vTexCoord + vec2(-dx,  dy)).b
          +   texture2D(texture, vTexCoord + vec2(0.0,  dy)).b
          +   texture2D(texture, vTexCoord + vec2( dx,  dy)).b) * c
          +   texture2D(texture, vTexCoord).b) * -24.0;

  float brightness = (r*0.3 + g*0.59 + b*0.11);
  brightness = 1.0 - brightness;
  if (brightness < 0.0) brightness = 0.0;
  if (brightness > 1.0) brightness = 1.0;
  r = g = b = brightness;  

  vec3 rgb = vec3(r, g, b);
  float a = texture2D(texture, vTexCoord).a;
  
  gl_FragColor = vec4(rgb-(1.0-arg), a);
}