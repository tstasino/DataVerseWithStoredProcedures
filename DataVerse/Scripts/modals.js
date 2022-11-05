
//Modal to Inform user that he is going to delete a customer
$('.btn--show-modal').on('click', (e) => {
    e.preventDefault();
    $('.modal1').removeClass('hidden');
    var button = $(e.target).attr("data-customerId");
    console.log(button);
    $('.delete-btn').on('click', () => {
        $.ajax({
            url: "/Customers/DeleteConfirmed/" + button,
            method:"post"
        }).done(() => {
            $('.modal1').addClass('hidden');
            location.reload(true);
        }).fail(() => {
            
        })
    })

})

$('.btn--close-modal').on('click', () => {
    $('.modal1').addClass('hidden');
})

$(document).bind('keydown', function (e) {
    if (event.keyCode == 27) {
        $('.modal').addClass('hidden');
    }
});

