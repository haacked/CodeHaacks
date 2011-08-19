$(function() {
    $('#loadButton').click(
        function() {
            $mvc.ComicsDemo.List().success(function(data) {
                $.each(data, function() {
                    $('#comics').append('<li>' + this.Title + '</li>');
                });
            });    
        }
    );
});