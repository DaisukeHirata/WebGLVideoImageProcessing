#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
uniform vec2 size;
const float PI = 3.14159265358979323846264;

vec4 mangaCool(vec2 coord) {

  float dx = 1.0 / size.x;
  float dy = 1.0 / size.y;
  float c  = -1.0 / 8.0; 

  float r = ((texture2D(texture, coord + vec2(-dx, -dy)).r
          +   texture2D(texture, coord + vec2(0.0, -dy)).r
          +   texture2D(texture, coord + vec2( dx, -dy)).r
          +   texture2D(texture, coord + vec2(-dx, 0.0)).r
          +   texture2D(texture, coord + vec2( dx, 0.0)).r
          +   texture2D(texture, coord + vec2(-dx,  dy)).r
          +   texture2D(texture, coord + vec2(0.0,  dy)).r
          +   texture2D(texture, coord + vec2( dx,  dy)).r) * c
          +   texture2D(texture, coord).r) * -19.2;

  float g = ((texture2D(texture, coord + vec2(-dx, -dy)).g
          +   texture2D(texture, coord + vec2(0.0, -dy)).g
          +   texture2D(texture, coord + vec2( dx, -dy)).g
          +   texture2D(texture, coord + vec2(-dx, 0.0)).g
          +   texture2D(texture, coord + vec2( dx, 0.0)).g
          +   texture2D(texture, coord + vec2(-dx,  dy)).g
          +   texture2D(texture, coord + vec2(0.0,  dy)).g
          +   texture2D(texture, coord + vec2( dx,  dy)).g) * c
          +   texture2D(texture, coord).g) * -9.6;

  float b = ((texture2D(texture, coord + vec2(-dx, -dy)).b
          +   texture2D(texture, coord + vec2(0.0, -dy)).b
          +   texture2D(texture, coord + vec2( dx, -dy)).b
          +   texture2D(texture, coord + vec2(-dx, 0.0)).b
          +   texture2D(texture, coord + vec2( dx, 0.0)).b
          +   texture2D(texture, coord + vec2(-dx,  dy)).b
          +   texture2D(texture, coord + vec2(0.0,  dy)).b
          +   texture2D(texture, coord + vec2( dx,  dy)).b) * c
          +   texture2D(texture, coord).b) * -4.0;

  if (r < 0.0) r = 0.0;
  if (g < 0.0) g = 0.0;
  if (b < 0.0) b = 0.0;
  if (r > 1.0) r = 1.0;
  if (g > 1.0) g = 1.0;
  if (b > 1.0) b = 1.0;

  vec3 rgb = 1.0 - vec3(r, g, b);
  float a = texture2D(texture, coord).a;
  
  return vec4(rgb-(arg), a);
}

void main(void) {
  if (arg == 0.5) {
    gl_FragColor = texture2D(texture, vTexCoord);
  } else if (arg == 1.0) {
    gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
  } else if (arg > 0.5) {
    vec2 coordOffset = size*0.5;
    float fd = 500.0 / tan((arg-0.5) * PI);

    vec2 v = gl_FragCoord.xy - coordOffset;
    float d = length(v);
    vec2 xy = v/d * fd*tan(clamp(d/fd, -0.5*PI, 0.5*PI)) + coordOffset;
    vec2 tc = xy/size;
    if (all(greaterThanEqual(tc, vec2(0.0))) && all(lessThanEqual(tc, vec2(1.0)))) {
      gl_FragColor = mangaCool(xy/size);
    } else {
      gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
  } else {
    vec2 coordOffset = size*0.5;
    float fd = 500.0 / tan((0.5-arg) * PI);

    vec2 v = gl_FragCoord.xy - coordOffset;
    float d = length(v);
    vec2 xy = v/d * fd*atan(d/fd) + coordOffset;
    gl_FragColor = mangaCool(xy/size);
  }
}