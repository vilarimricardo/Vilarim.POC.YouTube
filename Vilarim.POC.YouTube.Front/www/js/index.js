$(document).ready(function () {
    console.log("ready!");
    registerEvents();
});

function registerEvents() {
    $(".btn-search").click(searchContent);
}

function searchContent() {

    var text = $("#search").val();

    $.ajax({
        method: "GET",
        url: `http://127.0.0.1:5000/api/YouTube/"${text}`
    }).done(function (data) {
        processResponse(data);
    });
}

function processResponse(data) {

    var template = $(".template");

    var content = "";

    for (var i = 0; i < data.length; i++) {
        var video = data[i];
        var item = template.clone();

        $(item).find(".tittle").html(video.name);
        $(item).find(".type").html(video.type);
        $(item).find(".tumb-link").attr('href', `https://www.youtube.com/watch?v=${video.videoId}`);
        $(item).find(".tumb-img").attr('src', video.url);

        content += "<div>";
        content += item.html();
        content += "</div>";
    }

    $(".content").html(content);
}