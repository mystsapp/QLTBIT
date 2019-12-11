var editController = {
    init: function () {
        
        editController.registerEvent();
        var optionValue = $('.ddlChiNhanh').val();
        editController.loadDdlVanPhongByChiNhanh(optionValue);
    },
    registerEvent: function () {
        $('.ddlChiNhanh').off('change').on('change', function () {
            var optionValue = $(this).val();
            editController.loadDdlVanPhongByChiNhanh(optionValue);
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
                    option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1

                });
                $('.ddlVanPhong').html(option);
            }
        });
    }
};
editController.init();