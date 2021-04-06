const fs = require('fs');
const path = require('path');
const exec = require('child_process').exec;

const currentAbsolutePath = __dirname;

/* Script for multiple processing of proto files */
// const protoFilesDirectory = 'proto/';
const PROTOC_GEN_TS_PATH = path.join(currentAbsolutePath, 'node_modules', '.bin', 'protoc-gen-ts_proto.cmd');
const getProtosFrom = path.join(currentAbsolutePath, '../', 'Kitakun.TagDiary.Web', 'Protos');

const wherePlaceGeneratedFiles = 'src/client';


const allAvailableProtoFiles = fs.readdirSync(getProtosFrom);
for (const file of allAvailableProtoFiles) {
    if (file.endsWith('.proto')) {
        const cmdCreateClient = `protoc -I=${getProtosFrom} --js_out=import_style=commonjs,binary:${path.join(currentAbsolutePath, wherePlaceGeneratedFiles)} --grpc-web_out=import_style=typescript,mode=grpcwebtext:"${path.join(currentAbsolutePath, wherePlaceGeneratedFiles)}"`;
        exec(cmdCreateClient, (err, stdout, stderr) => {
            if (err) {
                console.error(err);
                console.log('Client API for \x1b[31m%s\x1b[0m failed.', file);
            } else {
                console.log('Client API for \x1b[32m%s\x1b[0m was created successfully!', file);
            }
        });

        const cmdCommandWithAbsolutePath = `protoc -I=${getProtosFrom} ${path.join(getProtosFrom, file)} --js_out=import_style=commonjs,binary:${path.join(currentAbsolutePath, wherePlaceGeneratedFiles)} --grpc-web_out=import_style=typescript,mode=grpcwebtext:${path.join(currentAbsolutePath, wherePlaceGeneratedFiles)}`;
        exec(cmdCommandWithAbsolutePath, (err, stdout, stderr) => {
            if (err) {
                console.error(err);
                console.log('Definitions for \x1b[31m%s\x1b[0m failed.', file);
            } else {
                console.log('Definitions for \x1b[32m%s\x1b[0m was created successfully!', file);
            }
        });
    }
}