<?php 

include_once('connect.php');
$empid = $_GET['EmpID'];
$Password =  "slay@lehitimo2023";
$role =  $_GET['rs'];

$result = mysqli_query($con,"INSERT INTO account (employeeid,pword,role) VALUES('$empid','$Password','$role')");
echo 'Created Successfully';
?>