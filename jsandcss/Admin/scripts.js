window.editors = {};

document.querySelectorAll("#editor").forEach((node, index) => {
  ClassicEditor.create(node, {}).then((newEditor) => {
    window.editors[index] = newEditor;
  });
});

$(document).ready(function () {
  $("#upload-file").change(function () {
    var filename = $(this).val();
    $("#file-upload-name").html(filename);
    if (filename != "") {
      setTimeout(function () {
        $(".upload-wrapper").addClass("uploaded");
      }, 600);
      setTimeout(function () {
        $(".upload-wrapper").removeClass("uploaded");
        $(".upload-wrapper").addClass("success");
      }, 1600);
    }
  });
});
$(".writeImageArea").hide();

function readURL(input) {
  if (input.files && input.files[0]) {
    var reader = new FileReader();
    reader.onload = function (e) {
      $(".writeImageArea img").attr("src", e.target.result);
      $(".writeImageArea").show();
    };
    reader.readAsDataURL(input.files[0]);
  }
}

$("input:file").change(function () {
  setTimeout(() => {
    readURL(this);
  }, 1600);
});

$(".card-columns .card .card-content .card-img-top").on("click", function () {
  var imgPath = $(this).attr("data-image");

  $(".selectImage .img-wrapper img").attr("src", imgPath);
});

$(document).ready(function () {
  $('#example').DataTable();
});