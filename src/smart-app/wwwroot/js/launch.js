
/*
FHIR.oauth2.ready(function (smart) {
    // now do something cool

    var smart = FHIR.client({
        serviceUrl: 'https://r2.smarthealthit.org',
        patientId: 'smart-1137192'
    });

    smart.user.read();
});
*/

// get the URL parameters received from the authorization server
//var state = getUrlParameter("state");  // session key
//var code = getUrlParameter("code");    // authorization code

console.log('state=' + state);
console.log('code=' + code);

// load the app parameters stored in the session
var params = state; //"CfDJ8MBaBzOx4pBEsdguO - E64CDqIjRVHMNCT9SAKDkOsrRvkBcyt1Y4lFzwrviIVP3xjP1v - lhtiHZgHKSTjzQKqZaObFZR01Q1TrU8ur - DcDxfjNhoAZL--IRcHdJPQT4FUAdczkw26Zuz5mHlURavfiL7Ig6fz9UJbA_HKHP9KjTeTU0tCUv_PkN0vG0KT_Lw97vGKRFhfNcvXxBXO6toGLvd8_LgpqAGRQvtFHNdmX9x2Ws5B5ZmEEnszz5mFzfzLSTCIbm-AEX -azFCsNtbexwwOg0q5p66yRQCKudM1RYwDTM_LB8UHHj3MyDkbyImlG6HFDOjVLz5hRKyYY95OVI"; //JSON.parse(sessionStorage[state]);  // load app session
var tokenUri = "http://localhost:5000/connect/token";
var clientId = "7d26357a-6904-4b93-8759-b53f635a6241";
// Secret not beging used right now
var secret = "secret";
var serviceUri = "http://localhost:5000/connect/token";
var redirectUri = "http://localhost:5002/Launch";
var launch = "21984d1d-8c96-4f76-afe4-9e9a262ccd8c";

// Prep the token exchange call parameters
var data = {
    client_id: clientId,
    //client_secret: secret,
    code: code,
    grant_type: 'authorization_code',
    redirect_uri: redirectUri,
    launch: launch
};
var options;
if (!secret) {
    data['client_id'] = clientId;
}
options = {
    url: tokenUri,
    type: 'POST',
    data: data
};
if (secret) {
    options['headers'] = { 'Authorization': 'Basic ' + btoa(clientId + ':' + secret) };
}

// obtain authorization token from the authorization service using the authorization code
$.ajax(options).done(function (res) {
    // should get back the access token and the patient ID
    var accessToken = res.access_token;
    var patientId = res.patient;

    document.body.innerHTML += "<h3>accessToken: " + accessToken + "</h3>";
    document.body.innerHTML += "<h3>patientId: " + patientId + "</h3>";

    // and now we can use these to construct standard FHIR
    // REST calls to obtain patient resources with the
    // SMART on FHIR-specific authorization header...
    // Let's, for example, grab the patient resource and
    // print the patient name on the screen
    /*var url = serviceUri + "/Patient/" + patientId;
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        headers: {
            "Authorization": "Bearer " + accessToken
        },
    }).done(function (pt) {
        var name = pt.name[0].given.join(" ") + " " + pt.name[0].family.join(" ");
        document.body.innerHTML += "<h3>Patient: " + name + "</h3>";
    });*/
});

// Convenience function for parsing of URL parameters
// based on http://www.jquerybyexample.net/2012/06/get-url-parameters-using-jquery.html
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            var res = sParameterName[1].replace(/\+/g, '%20');
            return decodeURIComponent(res);
        }
    }
}

