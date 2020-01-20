
function updateUrl(url, key, value) {
    if (value !== undefined) {
        value = encodeURI(value);
    }
    var hashIndex = url.indexOf("#") | 0;
    if (hashIndex === -1) hashIndex = url.length | 0;
    var urls = url.substring(0, hashIndex).split('?');
    var baseUrl = urls[0];
    var parameters = '';
    var outPara = {};
    if (urls.length > 1) {
        parameters = urls[1];
    }
    if (parameters !== '') {
        parameters = parameters.split('&');
        for (k in parameters) {
            var keyVal = parameters[k];
            keyVal = keyVal.split('=');
            var ekey = keyVal[0];
            var evalue = '';
            if (keyVal.length > 1) {
                evalue = keyVal[1];
            }
            outPara[ekey] = evalue;
        }
    }

    if (value !== undefined) {
        outPara[key] = value;
    } else {
        delete outPara[key];
    }
    parameters = [];
    for (var k in outPara) {
        parameters.push(k + '=' + outPara[k]);
    }

    var finalUrl = baseUrl;

    if (parameters.length > 0) {
        finalUrl += '?' + parameters.join('&');
    }

    return finalUrl + url.substring(hashIndex);
}

function insertParam(key, value) {
    history.pushState({}, null, updateUrl(window.location.href, key, value));
}