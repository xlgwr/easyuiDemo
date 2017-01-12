
 
function submitForm(formId)
{
    var submitform = $('#' + formId);
    $.ajax({
        type: 'POST',
        url: submitform.attr('action'),
        data: submitform.serialize(),
        success: function (data) {
            if (data.result) {
                submitform[0].reset();
                courtjs.closeDialog();
                loadData();
            } else {

            }
        }
    });
}