jQuery(document).ready(function($){
    if ('serviceWorker' in navigator) {
        console.log('CLIENT: service worker registration in progress.');
        navigator.serviceWorker.register('/sw.js').then(function() {
            console.log('CLIENT: service worker registration complete.');
        }, function() {
            console.log('CLIENT: service worker registration failure.');
        });
    } else {
        console.log('CLIENT: service worker is not supported.');
    }

    $('.dropdown-menu').click(function(e) {
        e.stopPropagation();
    });

    $(".menu-toggle").on("click", function(){
        $(".admin__page").toggleClass("toggled");
    });

	var inputs = document.querySelectorAll( '.custom-file-input' );
	Array.prototype.forEach.call( inputs, function( input )
	{
		var label	 = input.nextElementSibling,
			labelVal = label.innerHTML;

		input.addEventListener( 'change', function( e )
		{
            var fileName = '';
            fileName = e.target.value.split( '\\' ).pop();

			if( fileName ) {
                label.querySelector( 'span' ).innerHTML = fileName;
            }
			else {
                label.innerHTML = labelVal;
            }
		});
    });
    
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            
            reader.onload = function(e) {
                $('.profile-picture img').attr('src', e.target.result);
            }
            
            reader.readAsDataURL(input.files[0]); // convert to base64 string
        }
    }
    
    $(".custom-file-input").change(function() {
        readURL(this);
    });

    /*Aside Dropdown*/
    $(".has__dropdown > a").on("click", function(e){
        e.preventDefault();
        $(this).next(".dropdown__list").slideToggle();
        $(this).parents(".has__dropdown").toggleClass('active');
    });
});