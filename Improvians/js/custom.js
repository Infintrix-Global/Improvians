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

   

    var todaysDate = new Date().toLocaleDateString('en-CA'),
        nextWeek = new Date(new Date(todaysDate).getTime() + 7 * 24 * 60 * 60 * 1000).toLocaleDateString('en-CA');

    function mmddyy(date) {
        return ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '-' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '-' + date.getFullYear();
    }

    $("#seeded_date_field, .todaysDate").val(todaysDate);
    $(".todaysDateLabel").text(mmddyy(new Date(todaysDate)));

    $(".nextWeekDate").val(nextWeek);
    $(".nextWeekDateLabel").text(mmddyy(new Date(nextWeek)));


    var taskDistOptions;

    if ($(".googleChart").length > 0) {

        taskDistOptions = {
            chartArea: {
                left: 150,
                bottom: 70,
            },
            height: 300,
            bar: { groupWidth: 30 },
            legend: { position: 'none' },
            colors: ['#438644'],
            hAxis: { title: 'Task Distribution of Profiles', titleTextStyle: { fontStyle: 'bold' } },
            vAxis: { title: 'Hours/Day', },
            seriesType: 'bars',
            series: { 1: { type: 'line', color: '#FF0000', lineWidth: 1, lineDashStyle: [10, 2] } },
            fontSize: 14,
        };

        google.charts.load('current', {
            callback: function () {
                renderCharts();
                $(window).resize(renderCharts);
            },
            packages: ['corechart']
        });

        google.charts.setOnLoadCallback(renderCharts);

        function makeTitleBold() {
            $(".googleChart").each(function () {
                $(this).find("svg text").each(function () {
                    if ($(this).attr("font-style") == 'italic') {
                        $(this).attr("font-style", "").addClass("robotobold");
                    }
                })
            });
        }

        function renderCharts() {

            //Dashboard Task Chart Call
            if ($("#task-distribution").length > 0) { drawDashTaskDist(); }
            if ($("#task-distribution-1").length > 0) { drawTaskDist1(); }
            if ($("#task-distribution-2").length > 0) { drawTaskDist2(); }
            if ($("#task-distribution-3").length > 0) { drawTaskDist3(); }

            makeTitleBold();
        }

    }

    //Dashboard Task Chart Function
    function drawDashTaskDist() {

        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'Dashboard.aspx/GetTaskDisctributionChartData',
            data: '{}',
            success: function (response) {
                bindData(response.d);
            },

            error: function () {
                alert("Error loading data! Please try again.");
            }
        });

        function bindData(dataValues) {
            var taskDistData = new google.visualization.DataTable();

            taskDistData.addColumn('string', 'EmployeeName');
            taskDistData.addColumn('number', 'TaskHours');
            taskDistData.addColumn('number', 'TaskLimit');

            for (var i = 0; i < dataValues.length; i++) {
                taskDistData.addRow([dataValues[i].EmployeeName, dataValues[i].TaskHours, 8]);
            }

            taskDistOptions.height = 250;
            taskDistOptions.chartArea.left = 100;

            var taskDistChart = new google.visualization.ColumnChart(document.getElementById('task-distribution'));
                taskDistChart.draw(taskDistData, taskDistOptions);

            makeTitleBold();
        }
    }

    //Dashboard Task Chart Function
    function drawTaskDist1() {
        var taskDistData = google.visualization.arrayToDataTable([
            ['Profiles', 'Tasks', 'Task Limit'],
            ['Assitant Grower', 3, 8],
            ['Supervisor', 8, 8],
            ['Sprayer', 5, 8],
            ['Irrigator', 13, 8],
            ['Crew Lead', 6, 8],
        ]);

        var taskDistChart = new google.visualization.ColumnChart(document.getElementById('task-distribution-1'));
        taskDistChart.draw(taskDistData, taskDistOptions);
    }

    //Dashboard Task Chart Function
    function drawTaskDist2() {
        var taskDistData = google.visualization.arrayToDataTable([
            ['Profiles', 'Tasks', 'Task Limit'],
            ['Assitant Grower', 2, 8],
            ['Supervisor', 4, 8],
            ['Sprayer', 6, 8],
            ['Irrigator', 8, 8],
            ['Crew Lead', 10, 8],
        ]);

        var taskDistChart = new google.visualization.ColumnChart(document.getElementById('task-distribution-2'));
        taskDistChart.draw(taskDistData, taskDistOptions);
    }

    //Dashboard Task Chart Function
    function drawTaskDist3() {
        var taskDistData = google.visualization.arrayToDataTable([
            ['Profiles', 'Tasks', 'Task Limit'],
            ['Assitant Grower', 10, 8],
            ['Supervisor', 12, 8],
            ['Sprayer', 5, 8],
            ['Irrigator', 3, 8],
            ['Crew Lead', 15, 8],
        ]);

        var taskDistChart = new google.visualization.ColumnChart(document.getElementById('task-distribution-3'));
        taskDistChart.draw(taskDistData, taskDistOptions);
    }
});