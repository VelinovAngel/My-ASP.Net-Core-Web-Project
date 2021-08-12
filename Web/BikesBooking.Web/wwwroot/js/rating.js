
const ratingStars = [...document.getElementsByClassName("rating__star")];

function executeRating(stars) {
    const starClassActive = "rating__star fas fa-star";
    const starClassInactive = "rating__star far fa-star";
    const starsLength = stars.length;
    let i;
    stars.map((star) => {
        star.onclick = () => {

            i = stars.indexOf(star);

            if (star.className === starClassInactive) {
                for (i; i >= 0; --i) stars[i].className = starClassActive;
            } else {
                for (i; i < starsLength; ++i) stars[i].className = starClassInactive;
            }
        };
    });
}
executeRating(ratingStars);
$("span[data-vote]").each(function (el) {
    $(this).click(function () {
        var value = $(this).attr("data-vote");
        var motorcycleId = $('#h_v').val();;
        var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
        var data = { motorcycleId: motorcycleId, value: value };
        $.ajax({
            type: "POST",
            url: "/api/Vote",
            data: JSON.stringify(data),
            headers: {
                'X-CSRF-TOKEN': antiForgeryToken
            },
            success: function (data) {
                $('#avarageVoteValue').html(data.avarageVote);
                console.log(data);
                for (var i = 1; i <= 5; i++) {
                    var item = $("#starsForRate" + i);
                    if (i - 0.5 <= data.averageVote) {
                        item.addClass("checked");
                    }
                    else {
                        item.removeClass("checked");
                    }
                }
            },

            contentType: 'application/json',

        });
    })
});