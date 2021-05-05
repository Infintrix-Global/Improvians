jQuery(document).ready(function ($) {
    if ('serviceWorker' in navigator) {
        console.log('CLIENT: service worker registration in progress.');
        navigator.serviceWorker.register('./sw.js').then(function () {
            console.log('CLIENT: service worker registration complete.');
        }).catch(function (err) {
            console.log("Service Worker Failed to Register", err);
        })
    } else {
        console.log('CLIENT: service worker is not supported.');
    }

    var todaysDate = new Date().toLocaleDateString('en-CA');
    $(".todaysDate").val(todaysDate);

    if ($("#task-distribution").length > 0) {
        google.charts.load('current', {
            callback: function () {
                drawTaskDist();
                $(window).resize(drawTaskDist);
            },
            packages: ['corechart', 'bar']
        });

        google.charts.setOnLoadCallback(drawTaskDist);
        function drawTaskDist() {
            var taskDistData = google.visualization.arrayToDataTable([
                ['Profiles', 'Tasks'],
                ['Assitant Grower', 50],
                ['Grower', 16],
                ['Sprayer', 5],
                ['Irrigator', 0],
                ['Crew Lead', 15],
            ]);

            var taskDistOptions = {
                bar: { groupWidth: 30 },
                legend: { position: 'none' },
                title: 'Task Distribution of Profiles',
            };

            var taskDistChart = new google.visualization.ColumnChart(document.getElementById('task-distribution'));
            taskDistChart.draw(taskDistData, taskDistOptions);

            google.visualization.events.addListener(taskDistChart, 'ready', findSVG);

            function findSVG() {
                $("#task-distribution svg").attr("viewBox", "0 0 " + taskDistOptions.width + " " + taskDistOptions.height);
            }
        }
    } //If ends
});