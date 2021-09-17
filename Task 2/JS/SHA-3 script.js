const fs = require('fs')
const path = require('path')
const {SHA3} = require("sha3")
const ignore = ["node_modules"]

const readStreamFile = f => new Promise((resolve, reject) => {
    const readStream = fs.createReadStream(f, {highWaterMark: 1024 * 1024});
    const data = []

    readStream.on('data', (chunk) => {
        data.push(chunk)
    })

    readStream.on('end', () => {
        resolve(data)
    })

    readStream.on('error', (err) => {
        reject(err)
    })
})

const getHash = async (path, data) => {
    const hash = new SHA3(256);
    hash.update(Buffer.concat(data))
    console.log(path, hash.digest('hex'))
}

const processDirectory = async (pathDirectory,isWorkingDirectory=true) => {
    let statPath = fs.statSync(pathDirectory)
    if (statPath.isFile()) {
        getHash(pathDirectory, await readStreamFile(pathDirectory));
    } else if(isWorkingDirectory)
        if (statPath.isDirectory() )
        for (let e of fs.readdirSync(pathDirectory)) {
            if (!ignore.includes(e))
                processDirectory(path.join(pathDirectory, "\\", e),false);
        }
}

processDirectory(process.cwd());
