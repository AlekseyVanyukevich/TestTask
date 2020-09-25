// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const deleteFn = e => {
    e.preventDefault();
    const { target } = e;
    target.parentNode.remove();
}

$(document).ready(function (){
    $('[data-toggle="popover"]').popover();
    const $fields = $("#fields");
    $('#add-field').click(function (e){
        e.preventDefault();
        $(`<div class="form-group form-group-delete">
            <input type="text" class="form-control" name="author" placeholder="Enter author surname">
            <button  type="button" class="btn btn-outline-danger delete-btn"></button>
        </div>`).appendTo($fields);
        $fields.find('.delete-btn').click(deleteFn);
    });
    $('.delete-btn').click(deleteFn);
    $('.alert').click(function ({ target }) {
        console.log(this);
    });
});

