// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    $('#searchForm').bind('submit', function (e) {
        e.preventDefault();
        makeSearchRequest();
        return false;
    });
});

function makeSearchRequest() {
    $.ajax({
            url: '/SearchResults',
            data: {
                searchRequest: $("#SearchRequest").val()
            }
        })
        .done(function (result) {
            console.log(result);
            var container = $("#searchResult");
            var songs = result["Songs"];
            var artists = result["Artists"];
            container.html("");

            container.append("<h5>Songs: " + Object.keys(songs).length + "</h5>");

            for (var song in songs) {
                container.append('<a href="'+song+'">'+songs[song]+"</a><br/>");
            }
            container.append("<h5>Artists: "+ Object.keys(artists).length +"</h5>");

             

            for (var artist in artists) {
                container.append('<a href="' + artist + '">' + artists[artist] + "</a><br/>");
            }

        });
}

