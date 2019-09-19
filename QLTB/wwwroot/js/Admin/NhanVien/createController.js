var createController = {
    init: function () {
        createController.registerEvent();
        var optionValue = $('.ddlChiNhanh').val();
        createController.loadDdlVanPhongByChiNhanh(optionValue);
    },
    registerEvent: function () {
        $('.ddlChiNhanh').off('change').on('change', function () {
            var optionValue = $(this).val();
            createController.loadDdlVanPhongByChiNhanh(optionValue);
        });
    },
    loadDdlVanPhongByChiNhanh: function (optionValue) {
        $('.ddlVanPhong').html('');
        var option = '';

        $.ajax({
            url: '/NhanViens/GetDmVanPhongByChiNhanh',
            type: 'GET',
            data: {
                chinhanh: optionValue
            },
            dataType: 'json',
            success: function (response) {
                var data = JSON.parse(response.data);

                $.each(data, function (i, item) {
                    option = option + '<option value="' + item.TenVP + '">' + item.TenVP + '</option>'; //chinhanh1

                });
                $('.ddlVanPhong').html(option);
            }
        });
    }
};
createController.init();