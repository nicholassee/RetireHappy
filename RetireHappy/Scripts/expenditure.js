    $(document).ready(function(){
        var addCount = 0;
        var numItems = 0;
        var totalAmount = parseFloat("0.00");

        // Hide the done button if cart is empty
        if (addCount == 0) {
            $("#Done").hide();
            $("#Done").prop("disabled", true);
        }

        $("#Done").on('click', function () {
            // $('#myModal').modal('show');
            var liIds = $('#expList li').map(function (i, n) {
                return $(n).attr('id');
            }).get().join(',');
            //alert(liIds);

            //pass back to controller
            //$.get('@Url.Action("Tabulate", "Expenditure")', { idArr: liIds });
            var url = $(this).data('request-url');
            $.ajax({
                type: "GET",
                url: url,
                data: { idArr: liIds },
                contentType: "application/json; charset=utf-8",
                dataType: "html",
            })

        });
        ////http://www.codeproject.com/Tips/891309/Custom-Confirmation-Box-using-Bootstrap-Modal-Dial


        $("Button[id^='AddBtn_']").on('click', function () {

            
            //updating text fields in View
            var text = $(this).val();
            var arr = text.split('?');
            var tempAmt = parseFloat(arr[2]);//get from button value
            var initAmtstr = document.getElementById('totalAmt').innerText;
            var initAmt = parseFloat(initAmtstr);

            //modifying Buttons in View
            if (addCount > -1 && addCount < 10) {
                addCount += 1;
                var end = initAmt + tempAmt;
                $("#totalAmt").text(end.toFixed(2).toString());
                $("#NumItems").text("View Selected(" + addCount.toString() + ")");

                //enable Done button
                $("#Done").show();
                $("#Done").prop("disabled", false);


                $(this).hide();
                $("Button[id^='RemBtn_" + arr[0] + "']").show();
                $('#expList').append('<li id="'+arr[0]+'">'+ arr[1]+ '</li>');
                
            }
            else {
                
                alert("Added "+(addCount)+" Items already!");
            }
            //addCount++;
        });


        //});
        $("Button[id^='RemBtn_']").on('click', function () {
            
            //updating text fields in View
            
            var text = $(this).val();
            var arr = text.split('?');
            var tempAmt = parseFloat(arr[2]);
            var initAmtstr = document.getElementById('totalAmt');
            var initAmt = parseFloat(initAmtstr.innerText);
            
            if ((addCount-1) >= -1) {
                var end = initAmt - tempAmt.toFixed(2);
                $("#totalAmt").text(end.toFixed(2).toString());

                // Hide the done button if cart is empty
                if ((addCount - 1) == 0) {
                    $("#Done").hide();
                    $("#Done").prop('disabled', true);
                }
                
            }
            addCount--;
            $("#NumItems").text("View Selected(" + addCount.toString() + ")");
           

            $(this).hide();
            $("Button[id^='AddBtn_" + arr[0] + "']").show();

            $('li').filter(function () { return $.text([this]) === arr[1]; }).remove();
                
            
        });

        // Prevent default behaviour
        $('a#detail').click(function (e) {
            e.preventDefault();
        });

    });


