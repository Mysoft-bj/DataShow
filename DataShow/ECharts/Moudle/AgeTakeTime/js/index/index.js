(function(){

var now = { row:1, col:1 }, last = { row:0, col:0};
const towards = { up:1, right:2, down:3, left:4};
var isAnimating = true;
var ImagePreScale = 100;
s=window.innerHeight/500;
ss=250*(1-s);

$('.wrap').css('-webkit-transform','scale('+s+','+s+') translate(0px,-'+ss+'px)');

document.addEventListener('touchmove',function(event){
	event.preventDefault();
},{ passive: false });
//上滑
$(document).swipeUp(function () {
    ImagePreScale = ImagePreScale + 10;
    $("body")[0].style.backgroundSize = ImagePreScale + "%"
	if (isAnimating) return;
	last.row = now.row;
	last.col = now.col;
	if (last.row != 13) { now.row = last.row+1; now.col = 1; pageMove(towards.up);}	
})
//下滑
$(document).swipeDown(function(){
    if (isAnimating) return;
	last.row = now.row;
	last.col = now.col;
    //调节背景景深
	(ImagePreScale < 110) ? ImagePreScale = ImagePreScale : ImagePreScale = ImagePreScale -10;
	$("body")[0].style.backgroundSize = ImagePreScale + "%"
    //Debugger
	//var bgimage = document.getElementsByTagName("body")
	//$("body")[0].style.backgroundSize = "130%"
	if (last.row!=1) { now.row = last.row-1; now.col = 1; pageMove(towards.down);}	
})

$(document).swipeLeft(function () {
    return;
    if (isAnimating) return;
	last.row = now.row;
	last.col = now.col;
	if ((last.row == 5 && last.col < 3) || (last.row == 6 && last.col < 2)) { now.row = last.row; now.col = last.col + 1; pageMove(towards.left); }
})

$(document).swipeRight(function () {
    return;
    if (isAnimating) return;
	last.row = now.row;
	last.col = now.col;
	if ((last.row == 5 && last.col > 1) || (last.row == 6 && last.col > 1)) { now.row = last.row; now.col = last.col - 1; pageMove(towards.right); }
})



function pageMove(tw){
	var lastPage = ".page-"+last.row+"-"+last.col,
		nowPage = ".page-"+now.row+"-"+now.col;
	
	switch(tw) {
		case towards.up:
			outClass = 'pt-page-moveToTop';
			inClass = 'pt-page-moveFromBottom';
			break;
		case towards.right:
			outClass = 'pt-page-moveToRight';
			inClass = 'pt-page-moveFromLeft';
			break;
		case towards.down:
			outClass = 'pt-page-moveToBottom';
			inClass = 'pt-page-moveFromTop';
			break;
		case towards.left:
			outClass = 'pt-page-moveToLeft';
			inClass = 'pt-page-moveFromRight';
			break;
	}
	isAnimating = true;
	$(nowPage).removeClass("hide");
	
	$(lastPage).addClass(outClass);
	$(nowPage).addClass(inClass);
	
	setTimeout(function(){
		$(lastPage).removeClass('page-current');
		$(lastPage).removeClass(outClass);
		$(lastPage).addClass("hide");
		$(lastPage).find("img").addClass("hide");
		
		$(nowPage).addClass('page-current');
		$(nowPage).removeClass(inClass);
		$(nowPage).find("img").removeClass("hide");
		
		isAnimating = false;
		var fun = "div" + now.row + "_" + now.col + "Load";
		var amimat = "animatExtend" + now.row + "_" + now.col;
	    try{
	        window[fun]();
	        window[amimat]();
	    }catch(err){
	        console.log(err)
	    }
		
	},600);
}



$(window).on('load', function () {
    setTimeout(function () {
        loadingOut()
    }, 1000)
   
    setTimeout(function(){
        isAnimating = false;
    },4600)

    });
function loadingOut() {
    $(".page-1-1").removeClass("hide");
    $(".page-loading").addClass("pt-page-moveToTop");

    setTimeout(function () {
        $(".page-loading").addClass("hide");
        $(".page-1-1").addClass('page-current');

        $(".page-index").addClass('pt-page-moveUpDown');
        $(".circle").addClass('pt-page-index-moveCircle');
        $(".divTitle-right").addClass('pt-page-index-moveFromTop');
        $(".divTitle-left").addClass('pt-page-index-moveFromBottom');
        $(".divTitle-mid").addClass('pt-page-index-bounceIn');
        
        //$("body").css("backgroundImage", "url(/images/background/background3.gif)")

    }, 600);
}
})();