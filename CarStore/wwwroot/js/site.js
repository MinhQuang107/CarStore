var siteRequest = {
    loadAjaxUrl: function (url, id, contentid, data, isPost, callback) {
        var target = document.querySelector(contentid);
        var blockUI = new KTBlockUI(target, {
            message: '<div class="blockui-message"><span class="spinner-border text-primary"></span> Loading...</div>',
            overlayColor: '#000000',
        });
        blockUI.block();

        if (isPost) {
            // Gửi dữ liệu qua body nếu là POST
            $.post(url, data, function (response) {
                blockUI.release();
                blockUI.destroy();
                $(contentid).html(response);
                if (callback && typeof callback === "function")
                    setTimeout(callback, 1);
            });
        } else {
            // Gửi dữ liệu qua query string nếu là GET
            var queryString = $.param(data); // Chuyển object data thành query string
            var fullUrl = url + "?" + queryString;

            $.get(fullUrl, function (response) {
                blockUI.release();
                blockUI.destroy();
                console.log(response);
                $(contentid).html(response);
                if (callback && typeof callback === "function")
                    setTimeout(callback, 1);
            });
        }
    }
};
