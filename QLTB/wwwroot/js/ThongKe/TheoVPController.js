var TheoVPController = {
    init: function () {
        //$('.ddlVanPhong').html('');
        //var option = '';

        //$.ajax({
        //    url: '/NhanViens/GetDmVanPhongByChiNhanh',
        //    type: 'GET',
        //    data: {
        //        chinhanh: 0
        //    },
        //    dataType: 'json',
        //    success: function (response) {
        //        var data = JSON.parse(response.data);

        //        $.each(data, function (i, item) {
        //            option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1

        //        });
        //        $('.ddlVanPhong').html(option);
        //    }
        //});
        TheoVPController.registerEvent();
    },
    registerEvent: function () {

        //$('#frmSearch').validate({
        //    rules: {
        //        searchFromDate: {
        //            required: true,
        //            //date: true
        //            //dateFormat: true
        //        },
        //        searchToDate: {
        //            required: true,
        //            //date: true
        //            //dateFormat: true
        //        }
        //    },
        //    messages: {
        //        searchFromDate: {
        //            required: "Trường này không được để trống.",
        //            //date: "Chưa đúng định dạng."
        //        },
        //        searchToDate: {
        //            required: "Trường này không được để trống.",
        //            //number: "password phải là số",
        //            //date: "Chưa đúng định dạng."
        //        }
        //    }
        //});

        $('#btnExport').off('click').on('click', function () {
            //if ($('#frmSearch').valid()) {
                $('#frmSearch').submit();
                
           // }
        });
        $('#btnSearch').off('click').on('click', function () {
            //if ($('#frmSearch').valid()) {
                var fromDate = $('#txtFromDate').val();
                var toDate = $('#txtToDate').val();
                var vp = $('#txtVp').val();

                var url = '/ThongKes/TheoVPPartial';
                $.get(url, {
                    searchFromDate: fromDate,
                    searchToDate: toDate,
                    vP: vp
                }, function (data) {
                    $('#ctbgList').html(data);
                });

                //$.ajax({
                //    url: '/ThongKes/TheoVP',
                //    type: 'GET',
                //    data: {
                //        searchFromDate: fromDate,
                //        searchToDate: toDate,
                //        vP: vp
                //    },
                //    dataType: 'json',
                //    success: function (response) {
                //        //var data = JSON.parse(response.data);

                //        //$.each(data, function (i, item) {
                //        //    option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1

                //        //});
                //        //$('.ddlVanPhong').html(option);
                //        alert('Yes!');
                //    }
                //});
            //}
        });
    }

};
TheoVPController.init();