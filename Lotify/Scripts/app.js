//Define la App Angular.
var app = angular.module("App", ['datatables'])

//Constante de la url del host
app.constant("HOST", "http://localhost:8000" );

//Constante Token CSRF
app.constant("TOKEN", " {{ csrf_token() }} ");