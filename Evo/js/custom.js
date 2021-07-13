jQuery(document).ready(function($){
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

    $('.dropdown-menu').click(function(e) {
        e.stopPropagation();
    });
    
    //Enable Tooltips
    if($('[data-toggle="tooltip"]').length > 0) {
        $('[data-toggle="tooltip"]').tooltip();
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

    if($(".print__page").length > 0) {
        $(".print__page").on("click", function(){
            window.print();
        });
    }

    var installPromptEvent;
    window.addEventListener('beforeinstallprompt', function(event) {
        event.preventDefault();
        installPromptEvent = event;
        $('.pwa').css('display', 'block');
    });

    function callInstallPrompt() {
        if (window.matchMedia('(display-mode: standalone)').matches) {
            $(".pwa").remove();
        } 

        // We can't fire the dialog before preventing default browser dialog
        if (installPromptEvent !== undefined) {
            installPromptEvent.prompt();
        }
    }

    $(".pwa button").on("click", function(){
        callInstallPrompt();
    });

    var taskDistOptions;

    if($(".googleChart").length > 0) {

        taskDistOptions = {
            chartArea: {
                left: 150,
                bottom: 70,
            },
            height: 300,
            bar: { groupWidth: 30 },
            legend: { position: 'none' },
            colors: ['#438644'],
            hAxis: {title: 'Task Distribution of Profiles', titleTextStyle: { fontStyle: 'bold' }},
            vAxis: {title: 'Hours/Day', },
            seriesType: 'bars',
            series: {1: {type: 'line', color: '#FF0000', lineWidth : 1, lineDashStyle: [10, 2] }},
            fontSize: 14,
        };
        
        google.charts.load('current', {
            callback: function () {
                renderCharts();
                $(window).resize(renderCharts);
            },
            packages:['corechart']
        });

        google.charts.setOnLoadCallback(renderCharts);

        function makeTitleBold() {
            $(".googleChart").each(function(){
                $(this).find("svg text").each(function(){
                    if($(this).attr("font-style") == 'italic') {
                        $(this).attr("font-style","").addClass("robotobold");
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

    if($('.SelectBox').length > 0) {
        $('.SelectBox').SumoSelect({
            placeholder: '--- Select ---',
            forceCustomRendering: true
        });
    }

    //If date field
    function initDatePicker() {
        if ($('.jsDatePicker').length > 0) {
            $('.jsDatePicker').datepicker();
            $(".jsDatePicker").keypress(function (event) { event.preventDefault(); });
           
        }
    }
   
    initDatePicker();


    function translateManipulate() {
        $(".goog-te-combo").addClass("custom__dropdown input__control-auto");
        $(".goog-te-combo option:first-child").html('Translate');
    }

    $(window).on("load", function () {
        translateManipulate();
    });

    translateManipulate();

    setTimeout(function () { translateManipulate(); }, 200);

    jQuery('#google_translate_element').one("DOMSubtreeModified", function () {
        translateManipulate();
    });


    //Automation Controls
    function zerox(n) {
        return (n < 10) ? ("0" + n) : n;
    }

    if($(".automation_dashboard").length > 0) {
        var automationStatus = {
            0:  'Mover to Position',
            1:  'Mover Lock to Bench',
            2:  'Move to home position',
            3:  'Move to position',
            4:  'Mover Lock Boom',
            5:  'Mover Unlock from Bench',
            6:  'Mover to Position',
            7:  'Mover Lock to Bench',
            8:  'Move to Position',
            9:  'Move to home position',
            10: 'Mover Unlock from Bench',
            11: 'Mover to Position',
        };

        var rackStatus = {
            0: 'Loading',
            1: 'Full',
            2: 'Put Away'
        }, rackStatusClass = {
            0: { 0: 'full', 1: 'partially-full'},
            1: 'awaiting',
            2: 'intransit',
            3: 'putaway',
        }, rackCurrentStatus = {
            0: { 0: 'Full', 1: 'Partially Full'},
            1: 'Awaiting Pick-up',
            2: 'In Transit',
            3: 'Put Away',
        }, rackLocation = {
            0: 'Loading Area',
            1: 'House 7A',
            2: 'House 32B-02',
        };

        var currentStatus = 0,
            trayCycleInterval,
            totlCycles = $("#total-trays").text(),
            currentRack = 0;

        if($("#total-trays").text() !== $("#filled-trays").text()) {
            $("#remaining-trays").text(totlCycles);
        }

        function trayCycle() {
            
            if(currentStatus==10) {
                $("#cycle-status").attr("data-status", "0");

                clearInterval(trayCycleInterval);
                
                if(parseInt($("#cycle-count").text()) == (totlCycles-1)) {
                    $("#complete-job").fadeIn();
                }

                if(parseInt($("#cycle-count").text()) !== totlCycles) {
                    $("#cycle-count").text( zerox(parseInt($("#cycle-count").text()) + 1) );
                }

                if($("#cycle-status").attr("data-status") == '0') {
                    if($("#complete__loading").hasClass("bttn-disabled")) {
                        $("#start__putaway").removeClass("bttn-disabled").attr("disabled", false).addClass("bttn-active");
                    }
                }
            }

            if(currentStatus>=10) { currentStatus = 0; }

            $("#cycle-status").text(automationStatus[currentStatus]);
            ++currentStatus;
        }

        $("#tray_completed").on("click", function(e){
            e.preventDefault();
            if(!$(this).hasClass("bttn-disengage")) {
                clearInterval(trayCycleInterval);
                $("#cycle-status").attr("data-status", "1");
                trayCycleInterval = setInterval(function(){ trayCycle(); }, 1000);
                $(this).addClass("bttn-active bttn-disengage");
                $("#add__rack, #addrack__field").removeClass("bttn-disabled").attr("disabled", false);
                
                $("#addrack__field").val("").change().focus();
                $("#addrack__field").parents(".automation__bttngroup").removeClass("bttninput__activate");
            }
        });

        //Tray Loading

        var loadingTraySize = '64';

        $("#add__rack").on("click", function(e){
            e.preventDefault();
            if($("#rackscan__id").val() !== '' && $("#rackscan__id").val() !== null) {
                //$("#cycle-status").text(automationStatus[0]);
                $(this).addClass("bttn-disabled").attr("disabled", "disabled");
                $("#rackscan__id").addClass("bttn-disabled").attr("disabled", "disabled");

                //$("#addrack__status").text(rackStatus[0]);
                
                $(".loadingtray__col button, .loadingtray__col input").removeClass("bttn-disabled").attr("disabled", false);
                $("#trayscan__id").focus().val("").change();
                $("#trays__inrack").val(loadingTraySize);

                ++currentRack;
                $(".automation__loadingarea").attr("data-racks", currentRack);
            } else {
                $("#rackscan__id").focus();
            }
        });

        $("#rackscan__id").focus();

        function loadTray(trayNo, traySerial, trayBarcode) {
            var trayRow = '<tr>';
                trayRow += '<td class="text-center">' + trayNo + '</td>';
                trayRow += '<td>' + traySerial + '</td>';
                trayRow += '<td>' + trayBarcode + '</td>';
                trayRow += '</tr>';

            $(".loading__grids--tray table").append(trayRow);
        }

        $("#add__tray").on("click", function(e){
            e.preventDefault();
            if($("#trayscan__id").val() !== '' && $("#trayscan__id").val() !== null) {
                
                if($("#trays__inrack").val() == loadingTraySize) { $("#trays__inrack").val("0") }

                var traysInRack = ($("#trays__inrack").val() == '' || $("#trays__inrack").val() == null)? 0 : $("#trays__inrack").val();
                
                var trayNo = parseInt($(".loading__grids--tray tr").length - 1) + 1;
                

                if(trayNo <= loadingTraySize) {
                    
                    var currentRack = zerox($(".automation__loadingarea").attr("data-racks")),
                        jobID = $("#job-id").text(),
                        traySerial =  jobID + "-R" + currentRack + "-" + zerox(trayNo),
                        trayBarcode = $("#trayscan__id").val();

                    loadTray(trayNo, traySerial, trayBarcode);
                    $("#trays__inrack").val(parseInt(traysInRack)+1);
                    $("#trayscan__id").val("").focus();
                }
            } else {
                $("#trayscan__id").focus();
            }
        });

        $("#rackscan__id").focus();

        function loadRack(rackCount, rackNo, rackRowStatus, rackClass, rackLoadingLocation, barcodeData){

            var rackRow = '<tr id="rack-row-' + rackCount + '" class="' + rackClass + '" data-barcodes="' + barcodeData + '">';
                rackRow += '<td>' + rackNo + '</td>';
                rackRow += '<td id="rackstatus-' + rackCount + '">' + rackRowStatus + '</td>';
                rackRow += '<td id="racklocate-' + rackCount + '">' + rackLoadingLocation + '</td>';
                //rackRow += '<td class="text-center"> <a class="rack__details" href="javascript:void(0)" data-toggle="modal" data-target="#loadedTrays"> <i class="fas fa-file-alt"></i> </a> </td>';
                rackRow += '<td class="text-center" id="rackdata-' + rackCount + '"></td>';
                rackRow += '</tr>';

            $(".loading__grids--rack table").append(rackRow);
        }

        var totalTraysToLoad = parseInt($("#total-trays").text());

        for(var rackLoop=1; rackLoop<=totalTraysToLoad; rackLoop++) {
            var rackNo = 'Rack ' + rackLoop,
                rackCount = rackLoop;

            loadRack(rackCount, rackNo, '', '', '', '');
        }

        $("#complete__loading").on("click", function(e){
            e.preventDefault();
            var traysInRack = "" + $("#trays__inrack").val();
            var totalTrayLoaded = "" + $(".loading__grids--tray tr").length - 1,
                currentRack = zerox($(".automation__loadingarea").attr("data-racks")),
                jobID = $("#job-id").text(),
                trayBarcode = '';
                
            if(traysInRack <= loadingTraySize && traysInRack >= totalTrayLoaded) { 

                //Disable controls
                $(".loadingtray__col button, .loadingtray__col input").addClass("bttn-disabled").attr("disabled", true);

                //First Load Table
                var createTotalTray = parseInt(traysInRack),
                    totalCurrentTrays = parseInt(totalTrayLoaded),
                    startTrayFrom = totalCurrentTrays + 1;

                for(var i=startTrayFrom; i<=(createTotalTray); i++){
                    var traySerial =  jobID + "-R" + currentRack + "-" + zerox(i);

                    loadTray(i, traySerial, trayBarcode);
                }

                //Store Data
                var barcodeData = [];

                $(".loading__grids--tray tr:nth-child(n+2)").each(function(){
                    barcodeData.push($(this).find("td:last-child").text()); 
                });

                var dataRacks = $(".automation__loadingarea").attr("data-racks");
                $(".loading__grids--rack #rack-row-" + dataRacks).attr("data-barcodes", barcodeData.toString());
                $(".loading__grids--rack #rackdata-" + dataRacks).html('<a class="rack__details" href="javascript:void(0)" data-toggle="modal" data-target="#loadedTrays"> <i class="fas fa-file-alt"></i> </a>');

                
                if($("#cycle-status").attr("data-status") == '0') {
                    $("#start__putaway").removeClass("bttn-disabled").attr("disabled", false).addClass("bttn-active");
                }

                //Update Records of Filled & Remaining Racks
                var totalRacks = $("#total-trays").text(),
                    filledRacks = $(".automation__loadingarea").attr("data-racks"),
                    remainingRacks = totalRacks - filledRacks;
                
                $("#filled-trays").text(filledRacks);
                $("#remaining-trays").text(remainingRacks);

                
            } else {
                $("#trays__inrack").focus();
            }
        });

        $("#start__putaway").on("click", function(e){
            e.preventDefault();
            //Disabled this button
            $(this).addClass("bttn-disabled").attr("disabled", true).removeClass("bttn-active");

            var totlCycles = parseInt($("#total-trays").text());

            //TrayCycle
            if(parseInt($("#cycle-count").text()) !== totlCycles) {
                $("#cycle-count").text( zerox(parseInt($("#cycle-count").text()) + 1) );
            }
            
            //Empty Table
            $(".loading__grids--tray tr + tr").remove();
            //TrayCycle
            if(parseInt($("#cycle-count").text()) == totlCycles) {
                $("#complete-job").fadeIn();
                $("#add__rack, #rackscan__id").addClass("bttn-disabled").attr("disabled", true);
            } else {
                $("#add__rack, #rackscan__id").removeClass("bttn-disabled").attr("disabled", false);
                $("#rackscan__id").val("").focus();
            }
        });

        function createModalTray(trayNo, traySerial, trayBarcode) {
            var trayRow = '<tr>';
                trayRow += '<td class="text-center">' + trayNo + '</td>';
                trayRow += '<td>' + traySerial + '</td>';
                trayRow += '<td>' + trayBarcode + '</td>';
                trayRow += '</tr>';

            $("#loadedTrayTable").append(trayRow);
        }

        $(document).on("click", ".rack__details", function(){
            $(".modal #loadedTrayTable").find("tr + tr").remove();

            var barcodeValues = $(this).parents("tr").attr("data-barcodes").split(','),
                totalRows = barcodeValues.length,
                currentRack = $(this).parents("tr").index();

            for (var createTray = 1; createTray <= totalRows; createTray++) {
                console.log(createTray);
                var jobID = $("#job-id").text(),
                    traySerial =  jobID + "-R" + currentRack + "-" + zerox(createTray),
                    barcodeIndex = parseInt(createTray) - 1,
                    trayBarcode = barcodeValues[barcodeIndex];

                createModalTray(createTray, traySerial, trayBarcode);
            }
        });
        //loadedTrayTable
    }

    if($(".range-slider").length>0){

        var rangeSlider = function(){
            var slider = $('.range-slider'),
            range = $('.range-slider__range'),
            value = $('.range-slider__value');

            slider.each(function(){
                value.each(function(){
                    var value = $(this).next().find('input').attr('value');
                    $(this).html(value);
                });

                range.on('input', function(){
                    $(this).closest('.range-slider').prev('.range-slider__value').html(this.value);
                });
            });
        };

        rangeSlider();
    }

    if($(".automation__controls").length>0){

        /*
        var controlPower = $("#control-stop");

        function initAutoControls(){
            if(!controlPower.is(":checked")) {
                $(".automate-control").attr("disabled", false);
            } else {
                $(".automate-control").attr("disabled", true);
            }
        }

        initAutoControls();

        controlPower.on("change", function(e){
            initAutoControls();
        });
        */

        $("#control-forward, #control-backward, #control-pause").on("click", function(e){
            if(!$(this).is(":checked")){
                e.preventDefault();
            }

            $("#control-forward, #control-backward, #control-pause").not($(this)).prop("checked", false);
        });

        var enableDisableAll = $("#enable-disable-all");

        function initAutoControls(){
            if(!enableDisableAll.is(":checked")) {
                $(".automate-control").attr("disabled", true);
                $(enableDisableAll).parents(".control__group").find(".control__label > span").hide();
                $(enableDisableAll).parents(".control__group").find(".control__label > span.enableall").show();
                disableRobot();
            } else {
                $(".automate-control").attr("disabled", false);
                $(enableDisableAll).parents(".control__group").find(".control__label > span").hide();
                $(enableDisableAll).parents(".control__group").find(".control__label > span.disableall").show();
                enableRobot();
            }
        }

        enableDisableAll.on("change", function(e){
            initAutoControls();
        });

        initAutoControls();
    }

    if($("#act-tray-type").length > 0) {
        function racksNeed() {
            var actTrayNo = $("#act-tray-no").val();
            var actTrayType = $("#act-tray-type").val();

            if(actTrayNo !== '' && actTrayType !== '') {
                $("#act-racks-need").val(parseInt(actTrayNo/actTrayType)).change();
            } else {
                $("#act-racks-need").val('').change();
            }
        }

        $("#act-tray-no, #act-tray-type").bind("change keyup", function(){
            racksNeed();
        });
    }

    if($("#slot-endpos").length > 0) {
        function slotEndPos() {
            var actRacksNeed = parseInt($("#act-racks-need").val());
            var actSlotStartPos = parseInt($("#slot-startpos").val());

            if(actRacksNeed && actSlotStartPos) {
                var populateSelection = actSlotStartPos + actRacksNeed,
                    slotPosCheck = 0 != $('#slot-endpos option[value='+populateSelection+']').length;
                
                if(slotPosCheck) {
                    $("#slot-endpos").val(actSlotStartPos + actRacksNeed);
                } else {
                    $("#slot-endpos").val('');
                }

            } else {
                $("#slot-endpos").val('');
            }
        }

        $("#act-racks-need, #slot-startpos").on("change keyup", function(){
            slotEndPos();
        });
    }

    if($("#act-equip-type").length > 0) {
        $("#act-equip-type").on("change", function(){
            var thisEquip = $(this).val();
            if(thisEquip == 'carrier') {
                $('#movereq-rack').hide();
                $('#movereq-boomover').fadeIn();
                $("#movereq-slotpos").attr("disabled", true);
            }
            if(thisEquip == 'boom') {
                $('#movereq-rack').hide();
                $('#movereq-boomover').fadeIn();
                $("#movereq-slotpos").attr("disabled", false);
            }
            if(thisEquip == 'rack') {
                $('#movereq-boomover').hide();
                $('#movereq-rack').fadeIn();
            }
        });
    }

    if($(".live__map").length>0) {

        var benches = [];

        for (var i=5; i<53; i++) {
            i = zerox(i);
            benches[(i-5)] = ['ENC1-OUTSIDE-' + i + '-A', 'ENC1-OUTSIDE-' + i + '-B'];
        }

        
        for (var i=0; i<benches.length; i++){
            var benchClass = (i % 2 == 0 && i !== 0) ? 'benchtrack__space' : '';

            var benchRowHTML = '';
                benchRowHTML += '<div class="sys__benchtrack ' + benchClass + '">';
                benchRowHTML += '<div class="sys__bench" data-bench="' + benches[i][0] + '">';
                benchRowHTML += '<a href="#bench-view" data-toggle="modal" class="bench__label">' + benches[i][0] + '</a>';
                benchRowHTML += '</div>';
                benchRowHTML += '<div class="sys__bench" data-bench="' + benches[i][1] + '">';
                benchRowHTML += '<a href="#bench-view" data-toggle="modal" class="bench__label">' + benches[i][1] + '</a>';
                benchRowHTML += '</div>';
                benchRowHTML += '</div>';
            
            $(".sys__bencharea").append(benchRowHTML);
        }

        $(".sys__bench").each(function(){
            for(var i=54; i>=1; i--) {
                if(i!==53 && i!==54) {
                    $(this).append('<div class="sys__tray" data-slot="' + i + '" title="' + i + '"></div>');
                }
                if(i==53) {
                    $(this).append('<div class="sys__tray sys__tray-53" data-slot="' + i + '"  title="' + i + '"></div>');
                }
                if(i==54) {
                    $(this).append('<div class="sys__tray sys__tray-54" data-slot="' + i + '"  title="' + i + '"></div>');
                }
            }
        });

        //Dyanmic Bench Jobs
        $('.automation__dashboardpage [data-toggle="tab"]').on('shown.bs.tab', function (event) {
            if($(event.target).attr("href").split("#")[1] == "live-map") {

                var jobColors = [
                    '#fff600',
                    '#fabed4',
                    '#ffd8b1',
                    '#fffac8',
                    '#aaffc3',
                    '#adf0ff',
                    '#dcbeff',
                    '#c6c6ff',
                    '#ffabab',
                ];

                if($(".bench__job").length > 0) { $(".bench__job").remove(); }

                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: 'DashBoard.aspx/GetLiveMapData',
                    data: '{}',
                    success: function (response) {
                        var benchJobCount = response.d;
                            benchJobCount = Object.keys(benchJobCount).length,
                            jsonData = response;
                        
                        if(benchJobCount != 0) {
                            for (var i = 0; i < benchJobCount; i++) {
                                var jobData = jsonData['d'][i];

                                if ($(".sys__bench[data-bench='" + jobData.Bench + "']").length > 0) {

                                    var slotEND = parseFloat(jobData.SlotPositionEnd);
                                    var slotSTART = parseFloat(jobData.SlotPositionStart);

                                    function isFloat(n) {
                                        return Number(n) === n && n % 1 !== 0;
                                    }

                                    var slotHalf = $(".sys__bench [data-slot]").outerWidth() / 2,
                                        slotStartOffset = (isFloat(slotSTART)) ? parseFloat(slotHalf) : 0,
                                        slotEndOffset = (isFloat(slotEND)) ? parseFloat(slotHalf) : 0,
                                        slotSTART = Math.floor(slotSTART),
                                        slotEND = Math.floor(slotEND),
                                        slotSTART = (slotSTART == 0) ? 1 : slotSTART,
                                        slotEND = (slotEND == 0) ? 1 : slotEND;

                                    var leftPos = $(".sys__bench[data-bench='" + jobData.Bench + "']").find("[data-slot=" + slotEND + "]").position().left + slotEndOffset;

                                    var jobWidth = $(".sys__bench[data-bench='" + jobData.Bench + "']").find("[data-slot=" + slotSTART + "]").position().left + slotStartOffset;
                                    jobWidth = parseInt(jobWidth) + parseInt($(".sys__bench[data-bench='" + jobData.Bench + "']").find("[data-slot=" + slotSTART + "]").outerWidth());
                                    jobWidth = jobWidth - leftPos;

                                    $(".sys__bench[data-bench='" + jobData.Bench + "']").prepend('<a target="_blank" href="https://webportal.growerstrans.com/GEM/JobReports.aspx?JobCode=' + jobData.JobID + '" class="bench__job" style="left:' + leftPos + 'px; width:' + jobWidth + 'px; background:' + jobColors[i] + ';" data-toggle="tooltip" data-placement="top" data-jobid="' + jobData.JobID + '" title="' + jobData.JobID + '">' + jobData.JobID + '</a>');
                                }

                                $("#livejobno").append("<option value='" + jobData.JobID + "'>" + jobData.JobID + "</option>");
                            }
    
                            $("#livebench").on("change", function () {
                                if ($(this).val() !== '' && $(this).val() !== null) {
                                    var selectedBench = $(this).val();

                                    if ($(".sys__bench[data-bench=" + selectedBench + "]").length > 0) {
                                        benchTopPos = $(".sys__bench[data-bench=" + selectedBench + "]").offset().top;

                                        $('html, body').stop().animate({
                                            scrollTop: benchTopPos
                                        }, 2000);

                                        $(".sys__bench[data-bench=" + selectedBench + "]").addClass("blinker").delay(3500).queue(function () {
                                            $(this).removeClass("blinker").dequeue();
                                        });

                                    } else {
                                        //alert("Bench Not Found");
                                    }
                                }
                            });

                            $("#livejobno").on("change", function () {
                                if ($(this).val() !== '' && $(this).val() !== null) {
                                    var selectedJob = $(this).val();

                                    if ((selectedJob !== 0) && ($("[data-jobid=" + selectedJob + "]").length > 0)) {
                                        var jobBench = $("[data-jobid=" + selectedJob + "]").parents(".sys__bench");
                                            jobTopPos = $(jobBench).offset().top;

                                        $('html, body').stop().animate({
                                            scrollTop: jobTopPos
                                        }, 2000);

                                        $("[data-jobid=" + selectedJob + "]").addClass("blinker").delay(3500).queue(function () {
                                            $(this).removeClass("blinker").dequeue();
                                        });

                                    } else {
                                        //alert("Job Not Found");
                                    }
                                }
                            });
                            
                            if($('[data-toggle="tooltip"]').length > 0) {
                                $('[data-toggle="tooltip"]').tooltip();
                            }
                        }
                    },
                    error: function () {
                        //alert("Error loading data! Please try again.");
                    }
                });

                //Boom Placing
                var booms = [
                    ['ENC1-OUTSIDE-07-B', '3'],
                    ['ENC1-OUTSIDE-07-B', '15']
                ];

                setTimeout(function () {
                    $.each(booms, function (key, value) {
                        if ($(".sys__bencharea [data-bench='" + value[0] + "']").length > 0) {
                            var boomTop = $(".sys__bencharea .sys__benchtrack [data-bench='" + value[0] + "']").position().top,
                                boomLeft = 0 + parseFloat($(".sys__bencharea .sys__benchtrack  [data-bench='" + value[0] + "']").position().left);
                            boomLeft = boomLeft + parseFloat($(".sys__bencharea .sys__benchtrack [data-bench='" + value[0] + "']").find("[data-slot='" + value[1] + "']").position().left);

                            $(".boom__group").append('<div style="top: ' + boomTop + 'px; left: ' + boomLeft + 'px;" class="boom" data-slot="' + value[1] + '" data-bench="' + value[0] + '"></div>');

                        } else {
                            alert("Bench Not Found for boom!");
                        }
                    });
                }, 400);
            }
        });

        function createModalBench(currentBench, currentBenchName) {
            var benchModal = $("#bench-view");
            benchModal.find(".modal-title").text(currentBenchName);
            benchModal.find(".modal__bencharea").html("").append('<div class="sys__bench" data-bench="' + currentBenchName + '"></div>');
            benchModal.find(".sys__bench").html(currentBench.html());


            if ($(".sys__bencharea .boom__group .boom[data-bench='" + currentBenchName + "']").length > 0) {
                setTimeout(function () {
                    $(".sys__bencharea .boom__group .boom[data-bench='" + currentBenchName + "']").each(function () {
                        var modalBoomSlot = $(this).attr("data-slot"),
                            modalBoomWidth = $(".modal__bencharea .sys__tray").outerWidth(),
                            modalBoomTop = $(".modal__bencharea .sys__bench").position().top,
                            modalBoomLeft = $(".modal__bencharea .sys__tray[data-slot='" + modalBoomSlot + "']").position().left;

                        $(".modal__boomgroup").append('<div style="top: ' + modalBoomTop + 'px; left: ' + modalBoomLeft + 'px; width: ' + modalBoomWidth + 'px;" class="boom" data-slot="' + modalBoomSlot + '"></div>');
                    });
                }, 1000);
            } else {
                $(".modal__boomgroup").html("");
            }
        }

        $(".bench__label").on("click", function (e) {
            e.preventDefault();
            var currentBench = $(this).parents(".sys__bench"),
                currentBenchName = $(currentBench).attr("data-bench");

            createModalBench(currentBench, currentBenchName);
        });
    }

    if($(".table__demoscript").length>0) {
        $(".bttn-resetstatus").on("click", function(e) {
            e.preventDefault();
            $(this).parents("tr").find(".badge-status").attr("data-status", "inactive")
        });
    }

    if($(".irrigate__taskcontrols").length > 0) {
        var passesCount = $("#no-passes").text(),
            passCounter = 0,
            passesCycle = setInterval(function(){
                ++passCounter;
                $("#passes-count").text(passCounter);

                if(passCounter >= passesCount ){        
                    clearInterval(passesCycle);
                    $("#complete-job").fadeIn();
                }

            }, 2000);
    }
});