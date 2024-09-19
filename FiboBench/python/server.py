from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse, parse_qs
import json

class FibonacciHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        parsed_path = urlparse(self.path)
        if parsed_path.path == '/fibonacci':
            params = parse_qs(parsed_path.query)
            if 'n' in params:
                try:
                    n = int(params['n'][0])
                    if n <= 0:
                        self.send_error(400, "Invalid input")
                    else:
                        result = self.calculate_fibonacci(n)
                        response = {'fibonacci': result}
                        self.send_response(200)
                        self.send_header('Content-type', 'application/json')
                        self.end_headers()
                        self.wfile.write(json.dumps(response).encode('utf-8'))
                except ValueError:
                    self.send_error(400, "Invalid input")
            else:
                self.send_error(400, "Missing parameter")

    def calculate_fibonacci(self, n):
        fib_sequence = [0, 1]
        while len(fib_sequence) < n+1:
            fib_sequence.append(fib_sequence[-1] + fib_sequence[-2])
        return fib_sequence[n]

if __name__ == '__main__':
    server_address = ('', 8000)
    httpd = HTTPServer(server_address, FibonacciHandler)
    print('Starting server...')
    httpd.serve_forever()
