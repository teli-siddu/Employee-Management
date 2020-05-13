

	
$(function ()
{
	var fullHeight = function () {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function () {
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$("#sidebarCollapse").click(function () {
		$("#sidebar").toggleClass("active")
	})
})

