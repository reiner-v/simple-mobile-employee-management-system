<?php 

include_once('connect.php');
$empid = $_GET['EmpID'];
$empname =  $_GET['Emp_Name'];
$empbranch = $_GET['Emp_Branch'];
$emppos =  $_GET['Emp_Pos'];
$empjoin =$_GET['Emp_Joined'];

$result = mysqli_query($con,"INSERT INTO employee (employeeid,name,branch,position,joined) VALUES('$empid','$empname','$empbranch','$emppos','$empjoin')");
echo 'Created Employee Successful'; 

?>