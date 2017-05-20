
// variables
var $menu = $('#menu');
var $btnMenu = $('.btn-menu');
var $img = $('.imgMenu');

// mmenu customization
$menu.mmenu({
  counters: true,
  navbar: {
    title: "Menu Content"
  },
  extensions: ["pageshadow", "effect-zoom-menu", "effect-zoom-panels"],
  offCanvas: {
    position  : "right",
    zposition : "back"
  }
});

// toggle menu
var api = $menu.data("mmenu");

$btnMenu.click(function() {
  api.open();
});

// callbacks
api.bind('opening', function() {
  $img.attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/arrows_remove.svg');
});

api.bind('closing', function() {
  $img.attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/arrows_hamburger.svg');
});

// change toggle behavior for subpanels
$menu.find( ".mm-next" ).addClass("mm-fullsubopen");