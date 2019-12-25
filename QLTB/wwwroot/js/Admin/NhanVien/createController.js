var createController = {
    init: function () {
        createController.registerEvent();
        var optionValue = $('.ddlChiNhanh').val();
        createController.loadDdlVanPhongByChiNhanh(optionValue);

        var roleNames = $('#roles').data('roles');
        var khuvuc = $('#txtRole').val();

        //$.each(roleNames, function (i, item) {
        //    if (item !== 'DNB' && item !== 'TNB' && item !== 'MB' && item !== 'MT')
        //    {

        //    }
        //});
        if (typeof khuvuc !== 'undefined') {
            if (khuvuc !== 'DNB' && khuvuc !== 'TNB' && khuvuc !== 'MB' && khuvuc !== 'MT') {

                $("#divCN").prop('hidden', true);
                $("#divVP").prop('hidden', true);
                createController.loadDdlVanPphongByRole(khuvuc);
            } else {

                $("#vpLabel").prop('hidden', true);
                $("#vpSelect").prop('hidden', true);
            }
        }


        $('#txtRole').off('change').on('change', function () {
            khuvuc = $(this).val();

            if (khuvuc !== 'DNB' && khuvuc !== 'TNB' && khuvuc !== 'MB' && khuvuc !== 'MT') {
                $("#vpLabel").prop('hidden', false);
                $("#vpSelect").prop('hidden', false);

                $(".ddlChiNhanh").prop('disabled', true);
                $("#divVP").prop('hidden', true);

                createController.loadDdlVanPphongByRole(khuvuc);

            } else {
                $("#vpLabel").prop('hidden', true);
                $("#vpSelect").prop('hidden', true);

                $(".ddlChiNhanh").prop('disabled', false);
                $("#divVP").prop('hidden', false);

            }
        });

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
                    option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1

                });
                $('.ddlVanPhong').html(option);
            }
        });
    },
    loadDdlVanPphongByRole: function (role) {
        $('.ddlVanPhong').html('');
        var option = '';

        $.ajax({
            url: '/NhanViens/GetDmVanPhongByRole',
            type: 'GET',
            data: {
                role: role
            },
            dataType: 'json',
            success: function (response) {
                var data = JSON.parse(response.data);

                $.each(data, function (i, item) {
                    option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1
                    
                    $('.ddlChiNhanh').html('<option value="' + item.NameCN + '">' + item.NameCN + '</option>');
                    $('#hidCN').val(item.NameCN);
                });
                $('.ddlVanPhong').html(option);
                
            }
        });
    }
};
createController.init();