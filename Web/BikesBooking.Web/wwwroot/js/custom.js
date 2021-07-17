$(function () {
  $('[data-toggle="tooltip"]').tooltip({
    html: true
  })
})

$("#name-input").focusout(function () {
  let name = $("#name-input").val();

  if (name.length != 0) {
      $('#motor-name').text(name);
  }
})

$("#image-input").focusout(function () {
  let imageUrl = $("#image-input").val();

  if (imageUrl.length != 0) {
      $('#motor-image').attr('src', imageUrl);
  }
})

$("#keyword-input").focusout(function () {
    $('#motor-keyword').text($("#keyword-input").val());
})

$("#attack-input").focusout(function () {
  let attack = $("#attack-input").val();

  if (attack.length != 0) {
      $('#motor-attack').text(attack);
  }
})

$("#health-input").focusout(function () {
    let health = $("#motor-input").val();

  if (health.length != 0) {
      $('#motor-health').text(health);
  }
})

$("#description-input").focusout(function () {
    $('#motor-name').attr('data-original-title', `Description:<br>${$("#description-input").val()}`);
})