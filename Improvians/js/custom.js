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
                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json',
                        url: 'Dashboard.aspx/GetTaskDisctributionChartData',
                        data: '{}',
                        success: function (response) {
                            drawTaskDist(response.d); // calling method  
                        },

                        error: function () {
                            alert("Error loading data! Please try again.");
                        }
                    });
                $(window).resize(drawTaskDist);
            },
            packages: ['corechart', 'bar']
        });

        google.charts.setOnLoadCallback(drawTaskDist);
        function drawTaskDist(dataValues) {
            var taskDistData = new google.visualization.DataTable();

            taskDistData.addColumn('string', 'EmployeeName');
            taskDistData.addColumn('number', 'TaskHours');

            for (var i = 0; i < dataValues.length; i++) {
                taskDistData.addRow([dataValues[i].EmployeeName, dataValues[i].TaskHours]);
            }
            // Instantiate and draw our chart, passing in some options  
                     
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