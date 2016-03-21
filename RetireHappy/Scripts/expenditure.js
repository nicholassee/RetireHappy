    $(document).ready(function(){
        var addCount = 0;

        $("#Done").on('click', function () {
            // $('#myModal').modal('show');
            var liIds = $('#expList li').map(function (i, n) {
                return $(n).attr('id');
            }).get().join(',');
            //alert(liIds);

            //pass back to controller
            $.get('@Url.Action("Tabulate","Expenditure")', { idArr: liIds });
        });
    
        

        $("Button[id^='AddBtn_']").on('click', function () {
            
            
            if (addCount < 10) {
                var text = $(this).val();
                var arr = text.split(',');
               
                $(this).prop("disabled", true);
                $("Button[id^='RemBtn_" + arr[0] + "']").prop("disabled", false);
                $('#expList').append('<li id="'+arr[0]+'">' + arr[1]+ '</li>');
                addCount++;
            }
            else {
                alert("Added "+addCount+" Items already!");
            }
        });
    

        //});
        $("Button[id^='RemBtn_']").on('click', function(){
            var text =$(this).val();
            var arr = text.split(',');

            $(this).prop('disabled', true);
            $("Button[id^='AddBtn_" + arr[0] + "']").prop("disabled", false);

            $('li').filter(function () { return $.text([this]) === arr[1]; }).remove();
            addCount--;
        });
            
    });

     
