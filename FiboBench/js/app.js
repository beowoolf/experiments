var qs = require('querystring');

function fibonacci(num) {
  if (num <= 1) return num;

  return fibonacci(num - 1) + fibonacci(num - 2);
}

const hostname = '127.0.0.1';
const port = 3001;

require('http').createServer(function (req, res) {
    if (req.url.startsWith('/?i=')) {
        res.writeHead(200);
        var i = qs.parse(req.url.split('?')[1]).i;
        var c = fibonacci(i);
        console.log(i);
        console.log(c);
        res.end(c.toString());
    }
}).listen(port, hostname, () => {
  console.log(`Server running at http://${hostname}:${port}/`);
});
