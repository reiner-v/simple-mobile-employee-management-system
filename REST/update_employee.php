<?php
include_once('connect.php');

    $empid = $_GET['empID'];
    $emppos = $_GET['Emp_Pos'];
    $empbranch = $_GET['Emp_Branch'];

    $sql = "UPDATE employee SET  branch = '$empbranch', position = '$emppos' WHERE employeeid = '$empid'";
    $result = mysqli_query($con, $sql);
    
    echo "Update successful";


?>