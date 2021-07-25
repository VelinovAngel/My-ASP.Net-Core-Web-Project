﻿$('#statistics-button').on('click', ev => {
    $.get('/api/statistics', (data) => {
        $('#totals-motorcycles').text(data.totalsMotorcycles);
        $('#totals-dealers').text(data.totalsDealers);
        $('#totals-users').text(data.totalsUsers);
        $('#totals-rent').text(data.totalsRent);

        $('#statistics-button').hide();
    });
});