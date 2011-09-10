$(function() {
    $('#loadButton').click(
        function() {
            $mvc.ComicsDemo.List().done(function(data) {
                $.each(data, function() {
                    $('#comics').append('<li>' + this.Title + '</li>');
                });
            });    
        }
    );
});