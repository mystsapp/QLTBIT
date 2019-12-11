var indexController = {
    init: function () {
        indexController.registerEvent();
    },
    registerEvent: function () {

        $('#btnLiquidateAll').off('click').on('click', function () {
            indexController.liquidateList();

        });

        $('#btnDeleteAll').off('click').on('click', function () {
            bootbox.confirm({
                title: "Delete Confirm?",
                message: "Bạn có muốn xóa những item này không?",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Confirm'
                    }

                },
                callback: function (result) {
                    if (result) {
                        indexController.deleteList();
                       // $('#frmSearch').submit();
                    }
                }

            });
            

        });

        $('.btnDetail').off('click').on('click', function () {
            var id = $(this).data('id');
            //$.ajax({
            //    url: '/NhapKhos/Details/',
            //    type: 'GET',
            //    data: { id: id },
            //    dataType: 'json',
            //    success: function (response) {
            //        console.log(response);
            //    }
            //});

            var url = '/NhapKhos/Details/';
            $.get(url, { id: id }, function (result) {
                if (result !== null) {
                    $('#partialDetail').html(result);

                }
                //else {
                //    $('#u_txtLandtour').val('0');
                //}
            });

            $('#detailModal').modal('show');
        });

    },
    liquidateList: function () {
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
            //$('#stringId').val(JSON.stringify(idList));
            $('#frmLiquidateAll').submit();
        }
        else {
            bootbox.alert({
                size: "small",
                title: "Information",
                message: "Bạn chưa chọn thiết bị!",
                callback: function () {
                    //e.preventDefault();

                }
            });
        }



    },

    deleteList: function () {
        var idList = [];
        $.each($('.ckId'), function (i, item) {
            if ($(this).prop('checked')) {
                idList.push({
                    Id: $(item).data('id')
                });
            }

        });

        //$('#stringId').val(JSON.stringify(idList));

        if (idList.length !== 0) {
            // $('#stringId').val(JSON.stringify(idList));
           
            $.ajax({
                url: '/NhapKhos/DeleteList/',
                type: 'POST',
                data: { ids: JSON.stringify(idList) },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        bootbox.alert({
                            size: "small",
                            title: "Information",
                            message: "Đã xóa thành công!",
                            callback: function () {
                                //e.preventDefault();
                                window.location.reload(); 
                            }
                        });
                        
                    }

                }
            });
        }
        else {
            bootbox.alert({
                size: "small",
                title: "Information",
                message: "Bạn chưa chọn thiết bị!",
                callback: function () {
                    //e.preventDefault();

                }
            });
        }



    }



};
indexController.init();