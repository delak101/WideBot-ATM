# WideBot-ATM
Simple ATM API For Deposit and Withdrawal

<h1>Getting Started</h1>
<li>Clone WideBot-ATM Repository: https://github.com/delak101/WideBot-ATM
    
    $ git clone https://github.com/delak101/WideBot-ATM

<!--<li>(windows) install VScode & <a href="https://dotnet.microsoft.com/en-us/apps/aspnet">ASP.Net</a>-->
<li> move to api Folder

```
cd WideBot/api
dotnet watch --no-hot-reload
```
Postman cURLs

register:
```
curl --location 'https://localhost:5001/api/account/register' \
--header 'Content-Type: application/json' \
--data '{
	"CardNum": 987654321,
	"PIN": 4321
}'
```
login:
```
curl --location 'https://localhost:5001/api/account/login' \
--header 'Authorization: eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI5ODc2NTQzMjEiLCJuYmYiOjE3MDcyOTg3MjQsImV4cCI6MTcwNzkwMzUyNCwiaWF0IjoxNzA3Mjk4NzI0fQ.Tm049mGxXZgz0bUSP0vpTIr1lGgMSds_Fy0AOz1zct1nXyI1yJ_xxV0w54dJOcExGl5oh1NOE-WHNgXj_UC3DA' \
--header 'Content-Type: application/json' \
--data '{
	"CardNum": 987654321,
	"PIN": 4321
}'
```
deposit:
```
curl --location 'https://localhost:5001/api/dw/deposit' \
--header 'Content-Type: application/json' \
--data '{
  "CardNum": 987654321,
  "Amount": 100.50
}
'
```
withdraw:
```
curl --location 'https://localhost:5001/api/dw/withdraw' \
--header 'Content-Type: application/json' \
--data '{
  "CardNum": 987654321,
  "Amount": 50.25
}
'
```
balance:
```
curl --location 'https://localhost:5001/api/dw/Balance/987654321'
```
