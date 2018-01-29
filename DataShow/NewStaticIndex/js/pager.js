
/*****分页****/
; (function ($) {
    $.fn.extend({
        pager: function (options, callback) {
            var defaults = { page: 1, size: 0, total: 0, onpagechange: null };
            var me = $(this);
            me.html('');
            me.data('page', 1);
            options = $.extend(defaults, options);
            var total = options.total;
            var size = options.size;

            var page = options.page
            if (size <= 0 || size >= total)
                return me;
            var html = []
            var showCount = 8;
            var pageCount = Math.ceil(total / size);
            var startIndex = page - parseInt((showCount / 2));
            if (startIndex + showCount > pageCount)
                startIndex = pageCount + 1 - showCount;
            startIndex = Math.max(startIndex, 1);
            html.push('<nav><ul class="pagination"><li><a href="javascript:;" data-page="1" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>');;

            var endIndex = Math.min(startIndex + showCount - 1, pageCount);
            for (var i = startIndex; i <= endIndex; i++) {

                if (i == page)
                    html.push(" <li class='active' ><a href='javascript:;'>" + i + "</a></li>");
                else
                    html.push(" <li ><a href='javascript:;' data-page=" + i + ">" + i + "</a></li>");
            }
            html.push('<li><a href="javascript:;" aria-label="Next" data-page="' + pageCount + '"><span aria-hidden="true">&raquo;</span></a></li></ul></nav>');
            html.push('<input id="page" type="hidden" value />')
            me.empty().html(html.join(''))
            $('a', me).click(function () {
                var page = $(this).data('page');
                me.find('#page').val(page);
                me.data('page', page);
                page && callback && callback(page);

            });
            return me;

        }
    });
})(jQuery);