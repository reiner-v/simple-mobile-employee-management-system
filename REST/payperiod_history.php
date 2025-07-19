<?php

include_once('connect.php');

    $empid = $_GET["empID"];
	$result = mysqli_query($con,"SELECT payperiod FROM payslip WHERE employeeid='$empid'");
    $allpayperiod = array();

  if (mysqli_num_rows($result)==0)
    {
        echo "no rows";
    }
   else if (mysqli_num_rows($result)!=0)
    {
        while($value = mysqli_fetch_assoc($result))
        {  
            $allpayperiod[] = $value;
        }
        
        echo json_encode($allpayperiod);
    }
	mysqli_close($con);
?>