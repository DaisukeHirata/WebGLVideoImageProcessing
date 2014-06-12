#ifdef GL_ES
precision highp float;
precision highp int;
#endif

varying vec2 vTexCoord;
uniform sampler2D texture;
uniform float arg;
uniform vec2 size;

vec3 gray(vec4 color) {
  float y = dot(color.rgb, vec3(0.2126, 0.7152, 0.0722));
  return vec3(y);
}

void main(void) {
  float dx = 1.0 / size.x;
  float dy = 1.0 / size.y;

  // gray scale
  vec4 color = texture2D(texture, vTexCoord);
  
  vec3 upperLeft   = gray(texture2D(texture, vTexCoord + vec2(0.0, -dy)));
  vec3 upperCenter = gray(texture2D(texture, vTexCoord + vec2(0.0, -dy)));
  vec3 upperRight  = gray(texture2D(texture, vTexCoord + vec2( dx, -dy)));
  vec3 left        = gray(texture2D(texture, vTexCoord + vec2(-dx, 0.0)));
  vec3 center      = gray(texture2D(texture, vTexCoord + vec2(0.0, 0.0)));
  vec3 right       = gray(texture2D(texture, vTexCoord + vec2( dx, 0.0)));
  vec3 lowerLeft   = gray(texture2D(texture, vTexCoord + vec2(-dx,  dy)));
  vec3 lowerCenter = gray(texture2D(texture, vTexCoord + vec2(0.0,  dy)));
  vec3 lowerRight  = gray(texture2D(texture, vTexCoord + vec2( dx,  dy)));

  // vertical convolution
  //[ -1, 0, 1,
  //  -2, 0, 2,
  //  -1, 0, 1 ]
  vec3 vertical  = upperLeft   * -1.0
                 + upperCenter *  0.0
                 + upperRight  *  1.0
                 + left        * -2.0
                 + center      *  0.0
                 + right       *  2.0
                 + lowerLeft   * -1.0
                 + lowerCenter *  0.0
                 + lowerRight  *  1.0;

  // horizontal convolution
  //[ -1, -2, -1,
  //   0,  0,  0,
  //   1,  2,  1 ]
  vec3 horizontal = upperLeft   * -1.0
                  + upperCenter * -2.0
                  + upperRight  * -1.0
                  + left        *  0.0
                  + center      *  0.0
                  + right       *  0.0
                  + lowerLeft   *  1.0
                  + lowerCenter *  2.0
                  + lowerRight  *  1.0;


  float v = abs(vertical.r);
  float h = abs(horizontal.r) + v;
  float m = (v+h)/4.0;
  gl_FragColor = vec4(mix(color.rgb, vec3(v, m, h), arg), color.a);

}