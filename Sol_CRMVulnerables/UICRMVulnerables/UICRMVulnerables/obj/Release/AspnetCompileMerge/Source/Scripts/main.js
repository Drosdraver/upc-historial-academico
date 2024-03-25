// function makeGraphe()
// {

//   var labels = document.getElementsByClassName('label')
//   var container = document.getElementById('graphe');
//    var max = container.offsetWidth;
//   var barre = container.getElementsByTagName('li');
//   for (var i =0 ;i<barre.length;i++)
//     {
//       var label = labels.item(i);
//       var item = barre.item(i);
//         var content = item.innerHTML.split(":");
//        value = content[0];


//         item.style.marginTop=10 +'px';
//        item.style.marginBottom=10 +'px';

//       item.style.height= 25 +'px';
//       item.style.width= ((value*500)/100) +'px';
//       label.innerHTML =value +"%";
//       label.style.marginLeft = ((value*500)/100)+'px';


//     }

// }
// window.onload=makeGraphe;

$(".progress").each(function(i, e) {
  updateWidth(e);
});

function updateWidth(e) {
  var width = $(e).data("width");
  $(e).css({
    width: width + "%"
  });
}

//var elem = document.querySelector('input[type="range"]');
//elem.addEventListener("input", rangeValue);

$(document).ready(function($){
  var tabItems = $('.cd-tabs-navigation a'),
    tabContentWrapper = $('.cd-tabs-content');

  tabItems.on('click', function(event){
    event.preventDefault();
    var selectedItem = $(this);
    if( !selectedItem.hasClass('selected') ) {
      var selectedTab = selectedItem.data('content'),
        selectedContent = tabContentWrapper.find('li[data-content="'+selectedTab+'"]'),
        slectedContentHeight = selectedContent.innerHeight();

      tabItems.removeClass('selected');
      selectedItem.addClass('selected');
      selectedContent.addClass('selected').siblings('li').removeClass('selected');
      //animate tabContentWrapper height when content changes
      tabContentWrapper.animate({
        'height': slectedContentHeight
      }, 200);
    }
  });

  $('#myTabs a').click(function (e) {
  e.preventDefault()
  $(this).tab('show')
})

  //hide the .cd-tabs::after element when tabbed navigation has scrolled to the end (mobile version)
  checkScrolling($('.cd-tabs nav'));
  $(window).on('resize', function(){
    checkScrolling($('.cd-tabs nav'));
    tabContentWrapper.css('height', 'auto');
  });
  $('.cd-tabs nav').on('scroll', function(){
    checkScrolling($(this));
  });

  function checkScrolling(tabs){
    var totalTabWidth = parseInt(tabs.children('.cd-tabs-navigation').width()),
      tabsViewport = parseInt(tabs.width());
    if( tabs.scrollLeft() >= totalTabWidth - tabsViewport) {
      tabs.parent('.cd-tabs').addClass('is-ended');
    } else {
      tabs.parent('.cd-tabs').removeClass('is-ended');
    }
  }

});

// $(function() {
//   $("#bars li .bar").each( function( key, bar ) {
//     var percentage = $(this).data('percentage');

//     $(this).animate({
//       'height' : percentage + '%'
//     }, 1000);
//   });
// });



// $("#chart-container")
// (function () {

//     $("#chart-container").insertFusionCharts({
//         type: "column2d",
//         width: "500",
//         height: "300",
//         dataFormat: "json",
//         dataSource: {
//             "chart": {
//                 "caption": "Monthly revenue",
//                 "subCaption": "Last Year",
//                 "xAxisName": "Month",
//                 "yAxisName": "Revenues",
//                 //Making the chart export enabled in various formats
//                 "exportEnabled" :"1",
//                 "numberPrefix": "$",
//                 "theme": "fint"
//             },

//             "data": [{
//                 "label": "Jan",
//                     "value": "420000"
//             }, {
//                 "label": "Feb",
//                     "value": "810000"
//             }, {
//                 "label": "Mar",
//                     "value": "720000"
//             }, {
//                 "label": "Apr",
//                     "value": "550000"
//             }, {
//                 "label": "May",
//                     "value": "910000"
//             }, {
//                 "label": "Jun",
//                     "value": "510000"
//             }, {
//                 "label": "Jul",
//                     "value": "680000"
//             }, {
//                 "label": "Aug",
//                     "value": "620000"
//             }, {

//$(document).ready(function () {
//  //can also be wrapped with:
//  //1. $(function () {...});
//  //2. $(window).load(function () {...});
//  //3. Or your own custom named function block.
//  //It's better to wrap it.

//  //Tooltip, activated by hover event
//  //  $("body")
//  //      .tooltip({
//  //  selector: "[data-toggle='tooltip']",
//  //  container: "body"
//  //      })
//  //      .popover({
//  //  selector: "[data-toggle='popover']",
//  //  container: "body",
//  //  html: true
//  //});
//  //They can be chained like the example above (when using the same selector).

//});


