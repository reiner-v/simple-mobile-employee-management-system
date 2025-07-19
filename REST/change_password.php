<?php
include_once('connect.php');

    $empid = $_GET['empID'];
    $newp = $_GET['newpword'];
    $currentp = $_GET['currpword'];

    $resultcheck = mysqli_query($con, "SELECT * FROM account WHERE employeeid = '$empid' AND pword='$currentp'");
  
    
    if (mysqli_num_rows($resultcheck)==0)
    {
        echo "Current Password doesn't match";
    }
    else if (mysqli_num_rows($resultcheck)!=0)
    {
        $result = mysqli_query($con, "UPDATE account SET  pword = '$newp' WHERE employeeid = '$empid' AND pword='$currentp'");
        echo "Update successful";
    }
    mysqli_close($con);

?>