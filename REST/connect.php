<?php

$DB_HOST = "localhost";
$DB_USER = "root";
$DB_PASS = "";
$DB_NAME = "lehitimo";

//create database connection
$con=mysqli_connect($DB_HOST,$DB_USER,$DB_PASS,$DB_NAME);

//check connection
if (!$con)
{
   die( "Connection Failed");
}
?>