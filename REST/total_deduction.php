<?php

include_once('connect.php');

	$deduc = mysqli_query($con,"SELECT SUM(amount) AS TotalDeduction FROM deduction");


  if (mysqli_num_rows($deduc)==0)
    {
        echo "no rows";
    }
    else if (mysqli_num_rows($deduc)!=0)
    {   
        $row = mysqli_fetch_assoc($deduc); 
        echo $row['TotalDeduction'];
    }
	mysqli_close($con);
?>