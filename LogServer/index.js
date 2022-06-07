const http = require('http');
const fs = require('fs');
const { networkInterfaces } = require('os');
const portNo = 8080;

const requestListener = function (req, res) {
  res.writeHead(200);
  if (req.url.includes("favicon.ico")) return;
  console.log("Request URL:" + req.url);
  if (req.url === "/isViz") {
    fs.readFile('state.txt', 'utf8', (err, data) => {
      if (err) {
        res.end(err);
        return
      }
      res.end(data);
      console.log(data)
    })
    return;
  }
  var textToWrite = req.url.substring(1);
  textToWrite = decodeURI(textToWrite);
  textToWrite += "\n";
  console.log(">" + textToWrite);
  fs.appendFile('file.log', textToWrite, err => {
    if (err) {
      console.error(err);
      res.end(err);
    }
  });
  res.end('Success');

}

const server = http.createServer(requestListener);
server.listen(portNo);
console.log("Server is listening on: " + getIp() + ":" + portNo);

function getIp() {
  const nets = networkInterfaces();
  var listOfIps = [];

  for (const name of Object.keys(nets)) {
    for (const net of nets[name]) {
      if (net.family === 'IPv4' && !net.internal) {
        if (!name.includes("Virtual") || !name.includes("virtual")) listOfIps.push(net.address);
      }
    }
  }
  //console.log(listOfIps);
  return listOfIps[1];
}