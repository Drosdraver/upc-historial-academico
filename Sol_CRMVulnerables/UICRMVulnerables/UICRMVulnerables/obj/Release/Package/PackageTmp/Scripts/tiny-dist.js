!function(t){"function"==typeof define&&define.amd?define(["jquery"],t):"object"==typeof exports?module.exports=t(require("jquery")):t(jQuery)}(function(t){function e(e,s){function o(){return u.update(),u.move(u.slideCurrent),r(),u}function r(){u.options.buttons&&(m.click(function(){return u.move(--b),!1}),f.click(function(){return u.move(++b),!1})),t(window).resize(u.update),u.options.bullets&&e.on("click",".bullet",function(){return u.move(b=+t(this).attr("data-slide")),!1})}function l(){u.options.buttons&&!u.options.infinite&&(m.toggleClass("disable",u.slideCurrent<=0),f.toggleClass("disable",u.slideCurrent>=u.slidesTotal-C)),u.options.bullets&&(p.removeClass("active"),t(p[u.slideCurrent]).addClass("active"))}this.options=t.extend({},n,s),this._defaults=n,this._name=i;var u=this,a=e.find(".viewport:first"),d=e.find(".overview:first"),c=null,f=e.find(".next:first"),m=e.find(".prev:first"),p=e.find(".bullet"),v=0,h={},C=0,T=0,b=0,g="x"===this.options.axis,y=g?"Width":"Height",x=g?"left":"top",w=null;return this.slideCurrent=0,this.slidesTotal=0,this.intervalActive=!1,this.update=function(){return d.find(".mirrored").remove(),c=d.children(),v=a[0]["offset"+y],T=c.first()["outer"+y](!0),u.slidesTotal=c.length,u.slideCurrent=u.options.start||0,C=Math.ceil(v/T),d.append(c.slice(0,C).clone().addClass("mirrored")),d.css(y.toLowerCase(),T*(u.slidesTotal+C)),l(),u},this.start=function(){return u.options.interval&&(clearTimeout(w),u.intervalActive=!0,w=setTimeout(function(){u.move(++b)},u.options.intervalTime)),u},this.stop=function(){return clearTimeout(w),u.intervalActive=!1,u},this.move=function(t){return b=isNaN(t)?u.slideCurrent:t,u.slideCurrent=b%u.slidesTotal,0>b&&(u.slideCurrent=b=u.slidesTotal-1,d.css(x,-u.slidesTotal*T)),b>u.slidesTotal&&(u.slideCurrent=b=1,d.css(x,0)),h[x]=-b*T,d.animate(h,{queue:!1,duration:u.options.animation?u.options.animationTime:0,always:function(){e.trigger("move",[c[u.slideCurrent],u.slideCurrent])}}),l(),u.start(),u},o()}var i="tinycarousel",n={start:0,axis:"x",buttons:!0,bullets:!1,interval:!1,intervalTime:3e3,animation:!0,animationTime:1e3,infinite:!0};t.fn[i]=function(n){return this.each(function(){t.data(this,"plugin_"+i)||t.data(this,"plugin_"+i,new e(t(this),n))})}}),$(document).ready(function(){$("#slider1").tinycarousel(),$("#slider2").tinycarousel()});