
var URl = "";
var ID = "";
function AskQuestion(url,id)
{
    $('#modalmessages').modal();
    URl = url;
    ID = id;
}
function Delete()
{
    $.ajax(
        {
            url: URl + ID,
            type: "Post",
            success: function (result)
            {
                $('#a_' + ID).fadeOut();
                $('#modalmessages').modal('hide');
            }
        })
}