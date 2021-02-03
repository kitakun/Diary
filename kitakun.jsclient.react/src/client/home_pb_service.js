/* eslint-disable */
// package: home
// file: home.proto

var home_pb = require("./home_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Home = (function() {
    function Home() {}
    Home.serviceName = "home.Home";
    return Home;
}());

Home.FetchHomePreviewRecords = {
    methodName: "FetchHomePreviewRecords",
    service: Home,
    requestStream: false,
    responseStream: false,
    requestType: home_pb.FetchHomePreviewRecordsRequest,
    responseType: home_pb.FetchHomePreviewRecordsResponse
};

exports.Home = Home;

function HomeClient(serviceHost, options) {
    this.serviceHost = serviceHost;
    this.options = options || {};
}

HomeClient.prototype.fetchHomePreviewRecords = function fetchHomePreviewRecords(requestMessage, metadata, callback) {
    if (arguments.length === 2) {
        callback = arguments[1];
    }
    var client = grpc.unary(Home.FetchHomePreviewRecords, {
        request: requestMessage,
        host: this.serviceHost,
        metadata: metadata,
        transport: this.options.transport,
        debug: this.options.debug,
        onEnd: function(response) {
            if (callback) {
                if (response.status !== grpc.Code.OK) {
                    var err = new Error(response.statusMessage);
                    err.code = response.status;
                    err.metadata = response.trailers;
                    callback(err, null);
                } else {
                    callback(null, response.message);
                }
            }
        }
    });
    return {
        cancel: function() {
            callback = null;
            client.close();
        }
    };
};

exports.HomeClient = HomeClient;