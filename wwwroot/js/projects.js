let index = 0;

function AddProjectUser() {
    var userOptionSelect = document.getElementById("UserOptionSelect");
    var userOption = document.getElementById(userOptionSelect.options[userOptionSelect.selectedIndex].value);


    let searchResult = Search(userOption.value);
    if (searchResult != null) {
        return
    }

    let newUserOption = new Option(userOption.value, userOption.value);
    document.getElementById("UserList").options[index++] = newUserOption;
}

function DeleteProjectUser() {
    let userList = document.getElementById("UserList");

    if (!userList) {
        return;
    }

    if (userList.selectedIndex == -1) {
        return;
    }
    else {
        userList.options[userList.selectedIndex] = null;
        index--;
    }

}

function Search(str) {
    if (str == "") {
        return "Empty tags are not permitted";
    }

    var userList = document.getElementById("UserList");
    let options = userList.options;

    for (let i = 0; i < options.length; i++)
    if (options[i].value == str) {
        return "Duplicate tags are not permitted";
    }

}


$("form").on("submit", function () {
    $("#UserList option").prop("selected", "selected");
});
