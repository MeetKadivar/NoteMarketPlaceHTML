

// /* *************************************************
// *******************form **************************
// ************************************************* */


togglePassword.addEventListener('click', function (e) {
  
const password = document.querySelector('#inputpassword');
 var type = password.getAttribute('type');
 if( type === 'password'){
   password.setAttribute('type','text');
     $("#togglePassword").attr("src","/Content/fonts/eye.png"); } 
 else{
  password.setAttribute('type','password');
     $("#togglePassword").attr("src","/Content/fonts/eye.png"); 
 }
 
});

togglePassword_1.addEventListener('click', function (e) {
  
const password = document.querySelector('#inputpassword-1');
 var type = password.getAttribute('type');
 if( type === 'password'){
   password.setAttribute('type','text');
     $(".eye-img").attr("src","/Content/fonts/eye.png"); } 
 else{
  password.setAttribute('type','password');
    $(".eye-img").attr("src","/Content/fonts/eye.png"); 
 }
 
});


togglePassword_2.addEventListener('click', function (e) {
 
const password = document.querySelector('#inputpassword_2');
 var type = password.getAttribute('type');
 if( type === 'password'){
   password.setAttribute('type','text');
     $(".eye-img").attr("src","/Content/fonts/eye.png"); } 
 else{
  password.setAttribute('type','password');
     $(".eye-img").attr("src","/Content/fonts/eye.png"); 
 }
 
});


  

$(".toggle-sign").click(function () {
    console.log("click");
  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});


$(".toggle-sign-1").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});

$(".toggle-sign-2").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});
$(".toggle-sign-3").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});
$(".toggle-sign-4").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});
$(".toggle-sign-5").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
});
$(".toggle-sign-6").click(function() {

  $(this).find('i').toggleClass('fa-minus fa-plus');
  
}); 

//***************************** */ uplod img**********************************

document.querySelectorAll(".drop-zone__input").forEach((inputElement) => {
  const dropZoneElement = inputElement.closest(".drop-zone");

  dropZoneElement.addEventListener("click", (e) => {
    inputElement.click();
  });

  inputElement.addEventListener("change", (e) => {
    if (inputElement.files.length) {
      updateThumbnail(dropZoneElement, inputElement.files[0]);
    }
  });

  dropZoneElement.addEventListener("dragover", (e) => {
    e.preventDefault();
    dropZoneElement.classList.add("drop-zone--over");
  });

  ["dragleave", "dragend"].forEach((type) => {
    dropZoneElement.addEventListener(type, (e) => {
      dropZoneElement.classList.remove("drop-zone--over");
    });
  });

  dropZoneElement.addEventListener("drop", (e) => {
    e.preventDefault();

    if (e.dataTransfer.files.length) {
      inputElement.files = e.dataTransfer.files;
      updateThumbnail(dropZoneElement, e.dataTransfer.files[0]);
    }

    dropZoneElement.classList.remove("drop-zone--over");
  });
});

/**
 * Updates the thumbnail on a drop zone element.
 *
 * @param {HTMLElement} dropZoneElement
 * @param {File} file
 */
function updateThumbnail(dropZoneElement, file) {
  let thumbnailElement = dropZoneElement.querySelector(".drop-zone__thumb");

  // First time - remove the prompt
  if (dropZoneElement.querySelector(".drop-zone__prompt")) {
    dropZoneElement.querySelector(".drop-zone__prompt").remove();
  }

  // First time - there is no thumbnail element, so lets create it
  if (!thumbnailElement) {
    thumbnailElement = document.createElement("div");
    thumbnailElement.classList.add("drop-zone__thumb");
    dropZoneElement.appendChild(thumbnailElement);
  }

  thumbnailElement.dataset.label = file.name;

  // Show thumbnail for image files
  if (file.type.startsWith("image/")) {
    const reader = new FileReader();

    reader.readAsDataURL(file);
    reader.onload = () => {
      thumbnailElement.style.backgroundImage = `url('${reader.result}')`;
    };
  } else {
    thumbnailElement.style.backgroundImage = null;
  }
}
// ******************************************************************************

$(document).ready( function() {
  
  if ( $(window).width() < 600) {
   $('#download .table-row-scroll').addClass('table-row-scroll-1');
  }
  else {
    $('#download .table-row-scroll').removeClass('table-row-scroll-1');
  }
});

$(document).ready( function() {
  
  if ( $(window).width() < 600) {
   $('#progress .table-row-scroll').addClass('table-row-scroll-1');
  }
  else {
    $('#progress .table-row-scroll').removeClass('table-row-scroll-1');
  }
});
$(document).ready( function() {
  
  if ( $(window).width() < 600) {
   $('.admin-table .table-row-scroll').addClass('table-row-scroll-1');
  }
  else {
    $('.admin-table .table-row-scroll').removeClass('table-row-scroll-1');
  }
});