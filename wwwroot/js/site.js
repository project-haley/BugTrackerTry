const searchButton = document.getElementById('searchButton');
const searchInput = document.getElementById('searchInput');



function Search() {
    $.ajax({
        url: '/Home/Index',
        type: "GET",
        dataType: "json",
/*        data: { searchString: input },*/
        contentType: "application/json; charset=utf-8"
    });
};

searchButton.addEventListener('click', Search());

$(document).ready(function () {
    $('searchButton').click(Search());
});