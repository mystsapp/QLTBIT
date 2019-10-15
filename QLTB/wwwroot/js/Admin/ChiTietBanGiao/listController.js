var listController = {
    init: function () {
        listController.registerEvent();
    },
    registerEvent: function () {
        //$('.btnExportToWord').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var link = $(this).attr("href");

        //    var id = $(this).data("id");
        //    $.ajax({
        //        url: '/BanGiaos/ChiTietBanGiaoByBanGiaoId',
        //        type: 'POST',
        //        data: {
        //            id: id
        //        },
        //        dataType: 'json',
        //        success: function (response) {
        //            if (!response.status) {
        //                bootbox.alert({
        //                    size: "small",
        //                    title: "Warning",
        //                    message: "Bàn giao này chưa có chi tiết",
        //                    callback: function () {
        //                        //e.preventDefault();

        //                    }
        //                });

        //            }
        //            else {
        //                document.location.href = link;
        //            }
        //        }
        //    });



        //});
        $('#btnExportAll').off('click').on('click', function () {
            listController.exportList();

        });

    },
    exportList: function () {
        var idList = [];
        $.each($('.ckId'), function (i, item) {
            if ($(this).prop('checked')) {
                idList.push({
                    Id: $(item).data('id')
                });
            }

        });

        $('#stringId').val(JSON.stringify(idList));

        if (idList.length !== 0) {
            $('#stringId').val(JSON.stringify(idList));
            $('#frmExportAll').submit();
        }
        else {
            bootbox.alert({
                size: "small",
                title: "Information",
                message: "Bạn chưa chọn bàn giao!",
                callback: function () {
                    //e.preventDefault();

                }
            });
        }

        //if (idList.length === 1) {
        //    $.ajax({
        //        url: '/BanGiaos/ExportToWord',
        //        type: 'GET',
        //        data: {
        //            idDataList: JSON.stringify(idList)
        //        },
        //        dataType: 'json',
        //        success: function (response) {
        //            if (response.status) {
        //                bootbox.alert({
        //                    size: "small",
        //                    title: "Information",
        //                    message: "OK Men!",
        //                    callback: function () {
        //                        //e.preventDefault();

        //                    }
        //                });


        //            }
        //        }
        //    });
        //}
        //else {
        //    $.ajax({
        //        url: '/BanGiaos/ExportList',
        //        type: 'GET',
        //        data: {
        //            idDataList: JSON.stringify(idList)
        //        },
        //        dataType: 'json',
        //        success: function (response) {
        //            if (response.status) {
        //                bootbox.alert({
        //                    size: "small",
        //                    title: "Information",
        //                    message: "OK Men!",
        //                    callback: function () {
        //                        //e.preventDefault();

        //                    }
        //                });


        //            }
        //        }
        //    });
        //}

    }

    //checkUse: function (id, e) {
    //         $.ajax({
    //            url: '/ChiTietBanGiaos/CheckUse',
    //            type: 'GET',
    //            data: {
    //                id: id
    //            },
    //            dataType: 'json',
    //            success: function (response) {
    //                if (response.status) {
    //                    bootbox.alert({
    //                        size: "small",
    //                        title: "Information",
    //                        message: "Thiết bị này đã được chuyển đi!",
    //                        callback: function () {
    //                            e.preventDefault();

    //                        }
    //                    });


    //                }

    //            }
    //        });

    //}

};
listController.init();