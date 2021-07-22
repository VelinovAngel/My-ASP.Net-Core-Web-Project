$('#statistics-button').on('click', ev => {
    $.get('/api/statistics', (data) => {
        $('#totals-motorcycles').text(data.totalsMotorcycles);
        $('#totals-users').text(data.totalsUsers);
        $('#total-rents').text(data.totalsRent);

        $('#statistics-button').hide();
    });
});