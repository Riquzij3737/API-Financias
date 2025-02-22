function VerifyIsURLValid(url) {
    var url = url;
    var isURLValid = false;
    var request = new XMLHttpRequest();
    request.open('GET', url, false);
    request.send();
    if (request.status === 200) {
        isURLValid = true;
    }
    return isURLValid;
})

function Send() {

    let KeyOPbject = Document.getElementById("DevKey").value;

    var url = "http://localhost:5008/Api/Definitions/GetDefinitions?Key=" + KeyOPbject;
    var request = new XMLHttpRequest();

    request.open('POST', url, false);

    if (request.status === 200) {
        alert("Key aceppted");

        window.location.href = "http://localhost:5008/Swagger/index.html";
    } else {
        alert("Key not aceppted");
    }
    

}