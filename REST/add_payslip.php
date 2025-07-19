<?php 

include_once('connect.php');
$payid = $_GET['payslipID'];
$empid = $_GET['EmpID'];
$cmon =  $_GET['mon'];
$wdays = $_GET['workdays'];
$empsalary =  $_GET['Emp_Salary'];
$tearn = $_GET['totalE'];
$tdeduc =  $_GET['totalD'];
$tnpay =  $_GET['emp_npay'];

$result = mysqli_query($con,"INSERT INTO payslip VALUES('$payid','$empid','$cmon','$wdays','$empsalary','$tearn','$tdeduc','$tnpay')");
echo 'Payslip has been successfully added'; 

?>