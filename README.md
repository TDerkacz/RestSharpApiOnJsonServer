Fake Json Server instructions

1. install Node.js
2. npm install -g json-server
3. C:\Users\user\AppData\Roaming\npm json-server –-watch jsonServer.json // does not work
4. C:\Users\user\AppData\Roaming\npm json-server -w jsonServer.json // this works

call examples :

http://localhost:3000/posts/1
http://localhost:3000/post?id=1
http://localhost:3000/post?limit=2

response e.g.:

[
  {
    "id": 1,
    "key": "NEW",
    "language": "en_GB",
    "value": "NEW",
    "context": "Android"
  }
]

File with json 

{
 "post" : [
    {
      "id": 1,
      "key": "NEW",
      "language": "en_GB",
      "value": "NEW",
      "context": "Android"
    },
    {
      "id": 2,
      "key": "STORE",
      "language": "en_GB",
      "value": "STORE",
      "context": "iOS"
    },
    {
      "id": 3,
      "key": "STORES",
      "language": "pl_PL",
      "value": "STORES",
      "context": "Windows"
    }
  ]
}
