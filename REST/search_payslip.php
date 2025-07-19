<?php

include_once('connect.php');

    $empid = $_GET['empID'];
    $empperiod = $_GET['empPeriod'];
	$result = mysqli_query($con,"SELECT * FROM payslip WHERE employeeid='$empid' AND payperiod='$empperiod'");
    $payslipinfo = array();


  if (mysqli_num_rows($result)==0)
    {
        echo "no rows";
    }
    else if (mysqli_num_rows($result)!=0)
    {
        while($value = mysqli_fetch_assoc($result))
        {  
            $payslipinfo[] = $value;
        }
        
        echo json_encode($payslipinfo);
    }
	mysqli_close($con);
?>
