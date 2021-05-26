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

    if ($(".dropdown-showonload").length > 0) {
        $(".dropdown-showonload .dropdown-toggle").dropdown('toggle');
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

    if($(".input__control-icon").length > 0) {
        $(".input__control-icon").each(function(){
            var thisName = $(this).attr("type");
            var thisClass = (thisName == 'text') ? 'input__control--user' : 'input__control--password';
            $(this).wrapAll('<div class="input__control-iconwrapper ' + thisClass + '"></div>');
        });
    }

    if ($('.SelectBox').length > 0) {
        $('.SelectBox').SumoSelect({ placeholder: '--- Select ---' });
    }

});